//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace LoyalityApp.Controllers
//{
//    public class TransactionPointRangeController : Controller
//    {
//        public IActionResult Index()
//        {
//            return View();
//        }
//    }
//}

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
    public class TransactionPointRangeController : Controller
    {
        private LoyalityContext _context { get; set; }

        public TransactionPointRangeController(
            LoyalityContext loyalityContext
            )
        {
            _context = loyalityContext;
        }

        [HttpGet]
        public IEnumerable<TransactionPointRange> Get()
        {
            return _context.TransactionPointRanges.ToList();
        }


        [HttpGet]
        [Route("{id}")]
        public TransactionPointRange Get(Guid id)
        {
            return _context.TransactionPointRanges
                .Where(t => t.Id == id)
                .FirstOrDefault();
        }

        
        [HttpGet]
        [Route("{acquire-point-logic}")]
        public int Get(int points)
        {
            decimal result = 0;
            var pointRange = _context.TransactionPointRanges;

            foreach (var range in pointRange)
            {
                if (points > range.Max)
                    result += (range.Max - range.Min) * range.PointValue;
                else
                    if (range.Min < points && points < range.Max)
                    result += (result - range.Min) * range.PointValue;
            }
            return (int)result;
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


