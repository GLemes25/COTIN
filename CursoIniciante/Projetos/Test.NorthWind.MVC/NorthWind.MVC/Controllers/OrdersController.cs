using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Northwind.Business.Logic.Validation;
using Northwind.Data.Logic.Data;
using Northwind.Data.Logic.Interface;
using Northwind.Data.Logic.Repository;
using NorthWind.MVC.Models;
using System.Linq.Dynamic;


namespace NorthWind.MVC.Controllers
{
    public class OrdersController : Controller
    {
        IRepository<Order> _RepositoryOrders = new Repository<Order>();
        IRepository<Customer> _RepositoryCustomer = new Repository<Customer>();
        IRepository<Employee> _RepositoryEmployee = new Repository<Employee>();
        IRepository<Shipper> _RepositoryShipper = new Repository<Shipper>();

        private NorthwindEntities db = new NorthwindEntities();

        // GET: Orders
        public ActionResult Index()
        {
            var orders = _RepositoryOrders.GetAll();
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = _RepositoryOrders.GetByID(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            var Customer = _RepositoryCustomer.GetAll();
            var Employees = _RepositoryEmployee.GetAll();
            var Shippers = _RepositoryShipper.GetAll();

            ViewBag.CustomerID = new SelectList(Customer, "CustomerID", "CompanyName");
            ViewBag.EmployeeID = new SelectList(Employees, "EmployeeID", "LastName");
            ViewBag.ShipVia = new SelectList(Shippers, "ShipperID", "CompanyName");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,CustomerID,EmployeeID,OrderDate,RequiredDate,ShippedDate,ShipVia,Freight,ShipName,ShipAddress,ShipCity,ShipRegion,ShipPostalCode,ShipCountry")] Order order)
        {

            OrdersValidation validation = new OrdersValidation();
            var results = validation.Validate(order);

            foreach (var result in results.Errors)
            {
                ModelState.AddModelError(result.ErrorCode, result.ErrorMessage);
            }

            if (ModelState.IsValid)
            {
                _RepositoryOrders.Insert(order);
                _RepositoryOrders.Save();

                return RedirectToAction("Index");
            }

            var Customer = _RepositoryCustomer.GetAll();
            var Employees = _RepositoryEmployee.GetAll();
            var Shippers = _RepositoryShipper.GetAll();

            ViewBag.CustomerID = new SelectList(Customer, "CustomerID", "CompanyName", order.CustomerID);
            ViewBag.EmployeeID = new SelectList(Employees, "EmployeeID", "LastName", order.EmployeeID);
            ViewBag.ShipVia = new SelectList(Shippers, "ShipperID", "CompanyName", order.ShipVia);
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = _RepositoryOrders.GetByID(id);
            if (order == null)
            {
                return HttpNotFound();
            }

            var Customer = _RepositoryCustomer.GetAll();
            var Employees = _RepositoryEmployee.GetAll();
            var Shippers = _RepositoryShipper.GetAll();

            ViewBag.CustomerID = new SelectList(Customer, "CustomerID", "CompanyName", order.CustomerID);
            ViewBag.EmployeeID = new SelectList(Employees, "EmployeeID", "LastName", order.EmployeeID);
            ViewBag.ShipVia = new SelectList(Shippers, "ShipperID", "CompanyName", order.ShipVia);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,CustomerID,EmployeeID,OrderDate,RequiredDate,ShippedDate,ShipVia,Freight,ShipName,ShipAddress,ShipCity,ShipRegion,ShipPostalCode,ShipCountry")] Order order)
        {

            OrdersValidation validation = new OrdersValidation();
            var results = validation.Validate(order);

            foreach (var result in results.Errors)
            {
                ModelState.AddModelError(result.ErrorCode, result.ErrorMessage);
            }

            if (ModelState.IsValid)
            {
                _RepositoryOrders.Update(order);
                _RepositoryOrders.Save();

                return RedirectToAction("Index");
            }

            var Customer = _RepositoryCustomer.GetAll();
            var Employees = _RepositoryEmployee.GetAll();
            var Shippers = _RepositoryShipper.GetAll();

            ViewBag.CustomerID = new SelectList(Customer, "CustomerID", "CompanyName", order.CustomerID);
            ViewBag.EmployeeID = new SelectList(Employees, "EmployeeID", "LastName", order.EmployeeID);
            ViewBag.ShipVia = new SelectList(Shippers, "ShipperID", "CompanyName", order.ShipVia);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = _RepositoryOrders.GetByID(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = _RepositoryOrders.GetByID(id);
            _RepositoryOrders.Delete(order);
            _RepositoryOrders.Save();

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
                var OrderData = _RepositoryOrders.GetAll().AsQueryable();

                //var productData = (from tempcustomer in db.Products
                //                    select tempcustomer);

                //Sorting    
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    OrderData = OrderData.OrderBy(sortColumn + " " + sortColumnDir);
                }
                //Search    
                if (!string.IsNullOrEmpty(searchValue))
                {
                    OrderData = OrderData.Where(m => m.ShipName == searchValue);
                }

                //total number of rows count     
                recordsTotal = OrderData.Count();
                //Paging     
                var data = OrderData.Skip(skip).Take(pageSize).ToList();

                var orders = new List<OrdersDto>();

                foreach (var item in data)
                {
                    var order = new OrdersDto()
                    {
                        OrderID = item.OrderID,
                        CustomerID = item.CustomerID,
                        EmployeeID = item.EmployeeID,
                        OrderDate = item.OrderDate,
                        RequiredDate = item.RequiredDate,
                        ShippedDate = item.ShippedDate,
                        ShipVia = item.ShipVia,
                        Freight = item.Freight,
                        ShipName = item.ShipName,
                        ShipAddress = item.ShipAddress,
                        ShipCity = item.ShipCity,
                        ShipRegion = item.ShipRegion,
                        ShipPostalCode = item.ShipPostalCode,
                        ShipCountry = item.ShipCountry,
                    };
                    orders.Add(order);
                }
                //Returning Json Data    
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = orders });

            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}
