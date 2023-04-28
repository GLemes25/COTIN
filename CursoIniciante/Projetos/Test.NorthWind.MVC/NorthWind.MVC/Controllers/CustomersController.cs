using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Northwind.Business.Logic.Validation;
using Northwind.Data.Logic.Data;
using Northwind.Data.Logic.Interface;
using Northwind.Data.Logic.Repository;
using NorthWind.MVC.Models;

namespace NorthWind.MVC.Controllers
{
    public class CustomersController : Controller
    {
        IRepository<Customer> _Repository = new Repository<Customer>();

        private NorthwindEntities db = new NorthwindEntities();

        // GET: Customers
        public ActionResult Index()
        {
            var customer = _Repository.GetAll();

            return View(customer.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = _Repository.GetByID(id);

            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,CompanyName,ContactName,ContactTitle,Address,City,Region,PostalCode,Country,Phone,Fax")] Customer customer)
        
        {
            CustomersValidation validation = new CustomersValidation();
            var results = validation.Validate(customer);

            foreach (var result in results.Errors)
            {
                ModelState.AddModelError(result.ErrorCode, result.ErrorMessage);
            }


            if (ModelState.IsValid)
            {
                _Repository.Insert(customer);
                _Repository.Save();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = _Repository.GetByID(id);

            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,CompanyName,ContactName,ContactTitle,Address,City,Region,PostalCode,Country,Phone,Fax")] Customer customer)
        {

            CustomersValidation validation = new CustomersValidation();
            var results = validation.Validate(customer);

            foreach (var result in results.Errors)
            {
                ModelState.AddModelError(result.ErrorCode, result.ErrorMessage);
            }


            if (ModelState.IsValid)
            {
                _Repository.Update(customer);
                _Repository.Save();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = _Repository.GetByID(id);

            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Customer customer= _Repository.GetByID(id);
            _Repository.Delete(customer.CustomerID);
            _Repository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult LoadData()  ///LoadData, pode ser o nome que quiser
        {
            try
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault(); ///PARAMETROS QUE VEM DA REQUISIÇÃO
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();


                //Tamanho da paginação (10,20,50,100)    
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                // Obtendo todos os dados do cliente    
                var customersData = _Repository.GetAll().AsQueryable();


                //Ordenação    
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    customersData = customersData.OrderBy(sortColumn + " " + sortColumnDir);
                }

                //Procurar    
                if (!string.IsNullOrEmpty(searchValue))
                {
                    customersData = customersData.Where(m => m.CompanyName.ToLower().Contains(searchValue)); ///CONTAINS usado para pesquisar por apenas um letra  ///ToLower para aceitar letras minusculas.
                }

                //Número total de contagem de linhas    
                recordsTotal = customersData.Count();

                //Páginação     
                var data = customersData.Skip(skip).Take(pageSize).ToList();

                var customers = new List<CustomersDto>();

                foreach (var item in data)
                {
                    var customer = new CustomersDto()
                    {
                        CustomerID = item.CustomerID,
                        CompanyName = item.CompanyName,
                        ContactName = item.ContactName,
                        ContactTitle = item.ContactTitle,
                        Address = item.Address,
                        City = item.City,
                        Region = item.Region,
                        PostalCode = item.PostalCode,
                        Country = item.Country,
                        Phone = item.Phone,
                        Fax = item.Fax
                    };
                    customers.Add(customer);
                }
                //Retornando dados Json    
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = customers });
            }
            catch (Exception) { throw; }
        }
    }
}
