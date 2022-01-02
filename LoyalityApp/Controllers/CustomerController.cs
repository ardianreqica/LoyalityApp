using LoyalityApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoyalityApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : Controller
    {
        private LoyalityContext _context { get; set; }

        public CustomerController(
            LoyalityContext loyalityContext
            )
        {
            _context = loyalityContext;
        }

        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            return _context.Customers
                .Include(t => t.LoyalityPointsTransactions)
                .Include(t => t.Transactions)
                .ToList();
        }


        [HttpGet]
        [Route("{id}")]
        public Customer Get(Guid id)
        {
            return _context.Customers.Where(t => t.Id == id)
               .Include(t => t.LoyalityPointsTransactions)
               .Include(t => t.Transactions)
               .FirstOrDefault();
        }


        // GET: CustomerController
        //public int Index()
        //{
        //    return 1;
        //}

        //// GET: CustomerController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: CustomerController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: CustomerController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {

        //        //db.Students.Add(student);
        //        //db.SaveChanges();
        //        return RedirectToAction("Index");
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: CustomerController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: CustomerController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: CustomerController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: CustomerController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();        //    }
        //}
    }
}
