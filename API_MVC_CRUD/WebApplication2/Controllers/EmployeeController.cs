using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            if (Session["EMPLOYEEID"] == null)
            {
                return RedirectToAction("LoginUser");
            }
            IEnumerable<mvcEmployeeModel> emplist;
            mvcEmployeeModel ee;
            HttpResponseMessage response;
            if (Session["ROLENAME"].ToString() == "Admin")
            {
                response = GlobalVariables.WebApiClient.GetAsync("Employees").Result;
                emplist = response.Content.ReadAsAsync<IEnumerable<mvcEmployeeModel>>().Result;
                return View(emplist);
            }
            else
            {
                response = GlobalVariables.WebApiClient.GetAsync("Employees/" + Session["EMPLOYEEID"].ToString()).Result;
                ee = response.Content.ReadAsAsync<mvcEmployeeModel>().Result;
                return View("IndexUser", ee);
            }


        }
        public ActionResult IndexUser()
        {
            return View();
        }
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new mvcEmployeeModel());
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Employees/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<mvcEmployeeModel>().Result);
            }
        }
        [HttpPost]
        public ActionResult AddOrEdit(mvcEmployeeModel emp)
        {
            if (emp.EMPLOYEEID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Employees", emp).Result;
                TempData["SuccessInsert"] = "Saved Successfully";
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Employees/" + emp.EMPLOYEEID, emp).Result;
                TempData["SuccessUpdate"] = "Updated Successfully";
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Employees/" + id.ToString()).Result;
            TempData["SuccessDelete"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }
        public ActionResult LoginUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginUser(mvcEmployeeModel emp)
        {
            if (emp.USERNAME == "" && emp.USERPASSWORD == "")
            {
                TempData["Invalid"] = "Invalid Username or Password";
                return View();
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Account", emp).Result;
                mvcEmployeeModel ee = response.Content.ReadAsAsync<mvcEmployeeModel>().Result;

                if (ee==null)
                {
                    TempData["Invalid"] = "Username or Password is invalid please try with correct username or password";
                    return View();
                }
                else
                {
                    Session["EMPLOYEEID"] = ee.EMPLOYEEID;
                    Session["NAME"] = ee.NAME;
                    Session["ROLENAME"] = ee.ROLENAME;
                    return RedirectToAction("Index", "Employee");

                }
            }
        }
        public ActionResult LogOffUser()
        {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("LoginUser", "Employee");
        }

    }
}