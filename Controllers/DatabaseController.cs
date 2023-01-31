using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using FinalProject.Models;

namespace FinalProject.Controllers
{
    public class DatabaseController : Controller
    {
        WebApplicationMidtermEntities1 db = new WebApplicationMidtermEntities1();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string name, string password)
        {
            var context = new WebApplicationMidtermEntities1();
            
            var customer = (from Customer in context.Customer
                            where Customer.CompanyName == name && Customer.Password == password select Customer).FirstOrDefault();
            if(customer == null)
            {
                return View();

            } 
            else
            {
                return RedirectToAction("Home","Home");
            }

        }

        [HttpGet]
        public ActionResult SignUp() {
             return View();
        }

        [HttpPost]
        public ActionResult SignUp(Customer customer)
        {
            db.Customer.Add(customer);
            db.SaveChanges();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Profile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Profile(string TxtUpdateCompanyName,string TxtUpdatePhoneNumber,string TxtUpdateContactName,string TxtUpdateEmail)
        {
            var context = new WebApplicationMidtermEntities1();
            var customer = context.Customer.Where(c => c.CompanyName == TxtUpdateCompanyName).FirstOrDefault();
            if(customer != null) {
                customer.ContactName = TxtUpdateContactName;
                customer.PhoneNumber = TxtUpdatePhoneNumber;
                customer.Email = TxtUpdateEmail;
                context.SaveChanges();
            }

            return View();
        }
    }
}