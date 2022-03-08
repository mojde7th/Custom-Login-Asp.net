using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Login.Models;
using System.Data.SqlClient;
using System.Data;

namespace Login.Controllers
{
  
    public class AccountController : Controller
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
       
        // GET: Account
        [HttpGet]
       
        public ActionResult Login()
        {
            return View();
        }
        void connectionString()
        {
            con.ConnectionString = "Data Source=(local);Initial Catalog=of1;Integrated Security=True";
        }
        [HttpPost]
        public ActionResult verify(UserAccounts acc)
        {
            DataTable dt = new DataTable();
            connectionString();
            con.Open();
            com.Connection = con;
            var ff = acc.Username;
            TempData["idd"] = ff;
            com.CommandText = "SELECT * FROM [of1].[dbo].[User] where Username='"+acc.Username+"' and Pass='"+acc.Pass+"'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                con.Close();
                return View("Create");
            }
            else
            {
                con.Close();
                return View("Error");
            }
            
        }
        public ActionResult but()
        {
            var Userid = TempData["idd"];
            DataTable dt = new DataTable();
            Entities2 db = new Entities2();
            var query = from Employees in db.Employees
                        where
                              (from Users in db.Users
                               where
                     Users.Username == "admin"
                               select new
                               {
                                   Users.CompanyCode
                               }).Contains(new { CompanyCode = Employees.CompanyCodee })
                        select Employees;
            //var nel = sd.UserAccounts.Where(x => x.Username == Userid).ToList();
            return View(query);
            //sda.Fill(dt);
          
        }
    }
}