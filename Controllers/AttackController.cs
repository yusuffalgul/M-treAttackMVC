using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;

namespace FinalProject.Controllers
{
    public class AttackController : Controller
    {
        [HttpGet]
        public ActionResult Attack()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Attack(string testCompanyName1016, string labelResult)
        {
            int dateTime;
            int hour;
            int miliSecond;
            string filePath = "C:\\Users\\batuh\\OneDrive\\Masaüstü\\FinalProject\\FinalProject\\Results\\{0}_{1}_{2}_{3}.txt";
            dateTime = DateTime.Now.Day;
            hour = DateTime.Now.Hour;
            miliSecond = DateTime.Now.Millisecond;
            filePath = String.Format(filePath, testCompanyName1016, dateTime, hour, miliSecond);

            var context = new WebApplicationMidtermEntities1();
            var customer = new TestResult { CompanyName = testCompanyName1016, FilePath = filePath };
            context.TestResult.Add(customer);
            context.SaveChanges();
            Process.Start("cmd.exe", "/k" + "tasklist.exe" + "&" + "sc query" + "&" + "sc query state= all" +">" + filePath);
            return View();
        }

        [HttpGet]
        public ActionResult T1007()
        {
            return View();
        }

        [HttpPost]
        public ActionResult T1007(string testCompanyName1007)
        {
            int dateTime;
            int hour;
            int miliSecond;
            string filePath = "C:\\Users\\batuh\\OneDrive\\Masaüstü\\FinalProject\\FinalProject\\Results\\{0}_{1}_{2}_{3}.txt";
            dateTime= DateTime.Now.Day;
            hour = DateTime.Now.Hour;
            miliSecond = DateTime.Now.Millisecond;
            filePath = String.Format(filePath, testCompanyName1007,dateTime,hour,miliSecond);

            var context = new WebApplicationMidtermEntities1();
            var customer = new TestResult { CompanyName = testCompanyName1007, FilePath = filePath };
            context.TestResult.Add(customer);
            context.SaveChanges();

            Process.Start("cmd.exe", "/k" + "ipconfig /all" + "&" + "netsh interface show interface" + "&" + "arp -a" + "&" + "nbtstat -n" + "&" + "net config" + ">" + filePath);
            
            return View();
        }

        [HttpGet]
        public ActionResult TestResults()
        {

            return View();
        }

        [HttpPost]
        public ActionResult TestResults(string testResultName,string labelResult)
        {
            var context = new WebApplicationMidtermEntities1();
            var customer = (from TestResult in context.TestResult where TestResult.CompanyName == testResultName select TestResult.FilePath).ToList();
            if(customer == null)
            {
                    Debug.WriteLine("Not Found");
            }
            else
            {
                for (int i = 0; i < customer.Count; i++)
                {
                    Debug.WriteLine(customer[i]);
                    Debug.WriteLine(customer.Count);
                    string path = customer[i].Split('\\').Last().ToString();
                    Debug.WriteLine(path);
                    FileStream fs = new FileStream(Server.MapPath("~/Results/" + path), FileMode.Open);
                    StreamReader sr = new StreamReader(fs);
                    labelResult = sr.ReadToEnd();
                    if (i == 0)
                    {
                        TempData["Result"] = labelResult;
                    }
                    else if(i == 1) {

                        TempData["Result2"] = labelResult;
                    }
                    else if (i == 2)
                    {
                        TempData["Result3"] = labelResult;
                    }

                }
                
                //foreach (var item in customer)
                //{
                //    Debug.WriteLine(item);
                //    string path = item.Split('\\').Last().ToString();
                //    Debug.WriteLine(path);
                //    FileStream fs = new FileStream(Server.MapPath("~/Results/" + path), FileMode.Open);
                //    StreamReader sr = new StreamReader(fs);
                //    labelResult = sr.ReadToEnd();
                //    TempData["Result"] = labelResult;
     
                //}
            }
            return View();
        }


    }
}