using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCEntityFramework2Complete.Models;

namespace MVCEntityFramework2Complete.Controllers
{
    public class ProductController : Controller
    {
        MVCDatabaseDemoContext dbContext = new MVCDatabaseDemoContext();
        public IActionResult Index()
        {
            //dbContext.Categories.ToList();
            //return View(dbContext.Products.ToList());
            return View(dbContext.Products.Include(p => p.CategoryNavigation).ToList());
        }
        public IActionResult Create()
        {
            var categories = dbContext.Categories.ToList();
            ViewBag.category = categories;
            return View();
        }
        public IActionResult Delete(int id)
        {
            var productToDelete = dbContext.Products.SingleOrDefault(x => x.ProductId == id);
            dbContext.Products.Remove(productToDelete);
            dbContext.SaveChanges();
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> CreateNew(IFormFile postedFile, Product product)
        {
            if (product.ProductName !=null && product.ProductName.Length <=5)
            {
                ModelState.AddModelError("ProductName", "Name k duoc <= 5 ky tu!");
                var categories = dbContext.Categories.ToList();
                ViewBag.category = categories;
                return View("Create");

            }
            using (var dataStream = new MemoryStream())
            {
                await postedFile.CopyToAsync(dataStream);
                product.Picture = dataStream.ToArray();
            }
            dbContext.Products.Add(product);
            dbContext.SaveChanges();
            return View("Index", dbContext.Products.Include(p => p.CategoryNavigation).ToList());
        }
    }
}
