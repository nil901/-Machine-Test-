using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Asp.NETMVCCRUD.Models;
using System.Data.Entity;
using Asp.NETMVCCRUD.Models;

namespace Asp.NETMVCCRUD.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Employee/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            using (MDModel3 db = new MDModel3())
            {
                List<Product> prtList = db.Products.ToList<Product>();
                return Json(new { data = prtList }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Product());
            else
            {
                using (MDModel3 db = new MDModel3())
                {
                    return View(db.Products.Where(x => x.ProductId == id).FirstOrDefault<Product>());
                }
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(Product prt)
        {
            using (MDModel3 db = new MDModel3())
            {
                if (prt.ProductId == 0)
                {
                    db.Products.Add(prt);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    db.Entry(prt).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
                }
            }


        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (MDModel3 db = new MDModel3())
            {
                Product emp = db.Products.Where(x => x.ProductId == id).FirstOrDefault<Product>();
                db.Products.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }



    }
}