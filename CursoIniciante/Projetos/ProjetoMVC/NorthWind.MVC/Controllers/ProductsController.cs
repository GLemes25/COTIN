using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Northwind.Business.Logic.Validation;
using Northwind.Data.Logic.Data;
using Northwind.Data.Logic.Interface;
using NorthWind.Data.Logic.Repository;
using System.Linq;
using System.Linq.Dynamic;
using NorthWind.MVC.Models;

namespace NorthWind.MVC.Controllers
{
    public class ProductsController : Controller
    {

        IRepository<Products> _RepositoryProducts = new Repository<Products>();
        IRepository<Categories> _RepositoryCategories = new Repository<Categories>();
        IRepository<Suppliers> _RepositorySuppliers = new Repository<Suppliers>();

        private NorthwindEntities db = new NorthwindEntities();

        // GET: Products
        public ActionResult Index()
        {
            var products = _RepositoryProducts.ObterTodos();
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = _RepositoryProducts.ObterPorID(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            var categories = _RepositoryCategories.ObterTodos();
            var suppliers = _RepositorySuppliers.ObterTodos();


            ViewBag.CategoryID = new SelectList(categories, "CategoryID", "CategoryName");
            ViewBag.SupplierID = new SelectList(suppliers, "SupplierID", "CompanyName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProductName,SupplierID,CategoryID,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued")] Products products)
        {
            ProductsValidation validation = new ProductsValidation();
            var results = validation.Validate(products);

            foreach (var result in results.Errors)
            {
                ModelState.AddModelError(result.ErrorCode, result.ErrorMessage);
            }

            if (ModelState.IsValid)
            {
                _RepositoryProducts.Inserir(products);
                _RepositoryProducts.Salvar();

                return RedirectToAction("Index");
            }


            var categories = _RepositoryCategories.ObterTodos();
            var suppliers = _RepositorySuppliers.ObterTodos();

            ViewBag.CategoryID = new SelectList(categories, "CategoryID", "CategoryName", products.CategoryID);
            ViewBag.SupplierID = new SelectList(suppliers, "SupplierID", "CompanyName", products.SupplierID);
            return View(products);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = _RepositoryProducts.ObterPorID(id);
            if (products == null)
            {
                return HttpNotFound();
            }


            var categories = _RepositoryCategories.ObterTodos();
            var suppliers = _RepositorySuppliers.ObterTodos();

            ViewBag.CategoryID = new SelectList(categories, "CategoryID", "CategoryName", products.CategoryID);
            ViewBag.SupplierID = new SelectList(suppliers, "SupplierID", "CompanyName", products.SupplierID);
            return View(products);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,ProductName,SupplierID,CategoryID,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued")] Products products)
        {
            ProductsValidation validation = new ProductsValidation();
            var results = validation.Validate(products);

            foreach(var result in results.Errors)
            {
                ModelState.AddModelError(result.ErrorCode,result.ErrorMessage);
            }

            if (ModelState.IsValid)
            {
                _RepositoryProducts.Alterar(products);
                _RepositoryProducts.Salvar();

                return RedirectToAction("Index");
            }

            var categories = _RepositoryCategories.ObterTodos();
            var suppliers = _RepositorySuppliers.ObterTodos();

            ViewBag.CategoryID = new SelectList(categories, "CategoryID", "CategoryName", products.CategoryID);
            ViewBag.SupplierID = new SelectList(suppliers, "SupplierID", "CompanyName", products.SupplierID);
            return View(products);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = _RepositoryProducts.ObterPorID(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Products products = _RepositoryProducts.ObterPorID(id);
            _RepositoryProducts.Apagar(products.ProductID);
            _RepositoryProducts.Salvar();

            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        public ActionResult LoadData()
        {
            try
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();


                //Paging Size (10,20,50,100)    
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                // Getting all Product data    
                var productData = _RepositoryProducts.ObterTodos().AsQueryable();

                //var productData = (from tempcustomer in db.Products
                //                    select tempcustomer);

                //Sorting    
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    productData = productData.OrderBy(sortColumn + " " + sortColumnDir);
                }
                //Search    
                if (!string.IsNullOrEmpty(searchValue))
                {
                    productData = productData.Where(m => m.ProductName == searchValue);
                }

                //total number of rows count     
                recordsTotal = productData.Count();
                //Paging     
                var data = productData.Skip(skip).Take(pageSize).ToList();

                var products = new List<ProductsDto>();

                foreach (var item in data)
                {
                    var product = new ProductsDto()
                    {
                        ProductID = item.ProductID,
                        ProductName = item.ProductName,
                        CategoryID = item.CategoryID,
                        Discontinued = item.Discontinued,
                        QuantityPerUnit = item.QuantityPerUnit,
                        SupplierID = item.SupplierID,
                        UnitsInStock = item.UnitsInStock,
                        UnitsOnOrder = item.UnitsOnOrder,
                        UnitPrice = item.UnitPrice,
                        ReorderLevel = item.ReorderLevel
                    };
                    products.Add(product);
                }
                //Returning Json Data    
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = products });

            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}
