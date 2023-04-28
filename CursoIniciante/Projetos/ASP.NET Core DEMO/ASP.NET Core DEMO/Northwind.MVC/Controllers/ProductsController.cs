using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Northwind.Data.Logic.Data.Northwind.Context;
using Northwind.Data.Logic.Data.Northwind.Entity;
using Northwind.MVC.Logic.Interfaces;
using Northwind.MVC.Models;

namespace Northwind.MVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly NorthWindContext _context;
        private readonly IProductsServices _productsServices;
        private readonly ICategoriesServices _categoriesServices;
        private readonly ISuppliersServices _suppliersServices;
        private readonly IMapper _mapper;

        public ProductsController(NorthWindContext context, IProductsServices productsServices, ICategoriesServices categoriesServices, ISuppliersServices suppliersServices, IMapper mapper)
        {
            _context = context;
            _productsServices = productsServices;
            _categoriesServices = categoriesServices;
            _suppliersServices = suppliersServices;
            _mapper = mapper;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var northWindContext = await _productsServices.GET_ListaProducts();
            return View(northWindContext.Data);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _productsServices.GET_Products(id.Value);

            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            var categoria = await _categoriesServices.GET_ListaCategories();
            var supplier = await _suppliersServices.GET_ListaSuppliers();


            ViewData["CategoryID"] = new SelectList(categoria.Data, "CategoryID", "CategoryName");
            ViewData["SupplierID"] = new SelectList(supplier.Data, "SupplierID", "CompanyName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductID,ProductName,SupplierID,CategoryID,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued")] Products products)
        {

            if (ModelState.IsValid)
            {
                var instancia = _mapper.Map<ProductsViewModel>(products);
                await _productsServices.POST_Products(instancia);
                return RedirectToAction(nameof(Index));
            }
            var categoria = await _categoriesServices.GET_ListaCategories();
            var supplier = await _suppliersServices.GET_ListaSuppliers();


            ViewData["CategoryID"] = new SelectList(categoria.Data, "CategoryID", "CategoryName", products.CategoryID);
            ViewData["SupplierID"] = new SelectList(supplier.Data, "SupplierID", "CompanyName", products.SupplierID);
            return View(products);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _productsServices.GET_Products(id.Value);

            if (products == null)
            {
                return NotFound();
            }

            var categoria = await _categoriesServices.GET_ListaCategories();
            var supplier = await _suppliersServices.GET_ListaSuppliers();


            ViewData["CategoryID"] = new SelectList(categoria.Data, "CategoryID", "CategoryName", products.CategoryID);
            ViewData["SupplierID"] = new SelectList(supplier.Data, "SupplierID", "CompanyName", products.SupplierID);
            return View(products);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductID,ProductName,SupplierID,CategoryID,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued")] Products products)
        {
            if (id != products.ProductID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                var instancia = _mapper.Map<ProductsViewModel>(products);
                await _productsServices.PUT_Products(instancia);

                return RedirectToAction(nameof(Index));
            }
            var categoria = await _categoriesServices.GET_ListaCategories();
            var supplier = await _suppliersServices.GET_ListaSuppliers();


            ViewData["CategoryID"] = new SelectList(categoria.Data, "CategoryID", "CategoryName", products.CategoryID);
            ViewData["SupplierID"] = new SelectList(supplier.Data, "SupplierID", "CompanyName", products.SupplierID);
            return View(products);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'NorthWindContext.Products'  is null.");
            }
            var products = await _context.Products.FindAsync(id);
            if (products != null)
            {
                _context.Products.Remove(products);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductsExists(int id)
        {
            return (_context.Products?.Any(e => e.ProductID == id)).GetValueOrDefault();
        }
    }
}
