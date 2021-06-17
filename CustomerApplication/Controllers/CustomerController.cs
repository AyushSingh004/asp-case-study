using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CustomerApplication.Context;

namespace CustomerApplication.Controllers
{
    public class CustomerController : Controller
    {
        CustomerDetailsEntities obj = new CustomerDetailsEntities();
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        [Route("Customer/ViewCustomers")]
        public ActionResult ViewCustomers()
        {
            var customerList = obj.Customer_Infoo.ToList();
            return View(customerList);
        }

        [Route("Customer/AddCustomers")]
        public ActionResult AddCustomers()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCustomers(Customer_Infoo cust,string Business, string Delivery, string Visiting, string Others)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return Content("Error");
                }
                Customer_Infoo c = new Customer_Infoo();
                c.First_Name = cust.First_Name;
                c.Second_Name = cust.Second_Name;
                c.Email = cust.Email;
                c.Address1 = cust.Address1;
                c.Address2 = cust.Address2;
                c.Gender = cust.Gender;
                c.Phone = cust.Phone;
                c.isBusiness = (Business == "true" ? true : false);
                c.isDelivery = (Delivery == "true" ? true : false);
                c.isVisiting = (Visiting == "true" ? true : false);
                c.isOther    = (Others   == "true" ? true : false);
                c.State = cust.State;
                c.Country = cust.Country;
                c.City = cust.City;

                obj.Customer_Infoo.Add(c);
                obj.SaveChanges();
                                var customerList = obj.Customer_Infoo.ToList();
                return View("ViewCustomers",customerList);

            }
            catch(Exception e)
            {
                return Content("Account Already Exists");
            }
        }

        [HttpPost]
        public ActionResult EditCustomers(Customer_Infoo cust, string Business, string Delivery, string Visiting, string Others)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return Content("Error");
                }
                var c = obj.Customer_Infoo.Where(p => p.Customer_Id == cust.Customer_Id).First();
                c.First_Name = cust.First_Name;
                c.Second_Name = cust.Second_Name;
                c.Email = cust.Email;
                c.Address1 = cust.Address1;
                c.Address2 = cust.Address2;
                c.Gender = cust.Gender;
                c.Phone = cust.Phone;
                c.isBusiness = (Business == "true" ? true : false);
                c.isDelivery = (Delivery == "true" ? true : false);
                c.isVisiting = (Visiting == "true" ? true : false);
                c.isOther = (Others == "true" ? true : false);
                c.State = cust.State;
                c.Country = cust.Country;
                c.City = cust.City;

                obj.SaveChanges();
                var customerList = obj.Customer_Infoo.ToList();
                return View("ViewCustomers",customerList);
                

            }
            catch (Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound, "User Not Found");
            }
        }

        public ActionResult DeleteCustomer(int id)
        {
            try
            {
                var customer = obj.Customer_Infoo.Where(p => p.Customer_Id == id).First();
                if (customer != null)
                {
                    obj.Customer_Infoo.Remove(customer);
                    obj.SaveChanges();
                    var updatedCustomers = obj.Customer_Infoo.ToList();
                    return RedirectToAction("ViewCustomers");
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadGateway, "There is something wrong. please contact admin.");
                }
            }
            catch(Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadGateway, "There is something wrong. please contact admin.");
            }
        }

        public ActionResult EditCustomer(int id)
        {
            var customer = obj.Customer_Infoo.Where(p => p.Customer_Id == id).First();
            if (customer != null)
            {
                return View(customer);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadGateway, "There is something wrong. please contact admin.");
            }
        }

    }
}