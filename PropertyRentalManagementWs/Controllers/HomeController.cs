using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PropertyRentalManagementWs.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PropertyRentalManagementWs.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        private readonly PropertyRentalManagementWebSiteDBContext _context;

        public HomeController(PropertyRentalManagementWebSiteDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(decimal rentPrice)
        {
            ViewData["currentFilter"] = rentPrice;
            var apartment = from b in _context.Apartment
                            select b;
            if (rentPrice != 0) { 
                apartment = apartment.Where(b => b.RentPrice.Equals(rentPrice));
                return View(await apartment.AsNoTracking().ToListAsync());
            }
            return View(await _context.Apartment.ToListAsync());
        }

        public async Task<ActionResult> Search(decimal rentPrice) {
            ViewData["currentFilter"] = rentPrice;
            var apartment = from b in _context.Apartment
                            select b;
            if (rentPrice != 0)
            {
                apartment = apartment.Where(b => b.RentPrice.Equals(rentPrice));
            }
            return View(await apartment.AsNoTracking().ToListAsync());
        }
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signup(PotentialTenant potentialTenant)
        {
            using (PropertyRentalManagementWebSiteDBContext context = new PropertyRentalManagementWebSiteDBContext())
            {
                context.PotentialTenant.Add(potentialTenant);
                context.SaveChanges();
            }
            ViewBag.PotentialTenant = potentialTenant;  


            return View("LoggedInPotentialTenant", await _context.Apartment.ToListAsync());
        }

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(PotentialTenant tenant, Administrator administrator, PropertyManager propertyManager, PropertyOwner propertyOwner)
        {
            using (PropertyRentalManagementWebSiteDBContext context = new PropertyRentalManagementWebSiteDBContext())
            {
                bool isValidUser = context.PotentialTenant.Any(user => user.Email.ToLower() ==
                tenant.Email.ToLower() && user.Password == tenant.Password);
                if (isValidUser)
                {
                    //FormsAuthentication.SetAuthCookie(model.Email, false);
                    return View("LoggedInPotentialTenant", await _context.Apartment.ToListAsync());
                }
                bool isValidAdmin = context.Administrator.Any(user => user.Email.ToLower() ==
                administrator.Email.ToLower() && user.Password == administrator.Password);

                if (isValidAdmin)
                {
                    return RedirectToAction("index", "Administrators");
                }

                bool isValidManager = context.PropertyManager.Any(user => user.Email.ToLower() ==
                propertyManager.Email && user.Password == propertyManager.Password);

                if (isValidManager) {
                    return RedirectToAction("index", "PropertyManagers");
                }

                bool isValidOwner = context.PropertyOwner.Any(user => user.Email.ToLower() == propertyOwner.Email.ToLower() && user.Password == propertyOwner.Password);

                if (isValidOwner) {
                    return RedirectToAction("index", "PropertyOwners");
                }
                ViewData["ValidateMessage"] = "Invalid username or password";
                //ModelState.AddModelError("", "Invalid username or password !");
                return View();
            }
        }


        public async Task<IActionResult> LoggedInPotentialTenant()
        {

            return View("LoggedInPotentialTenant", await _context.Apartment.ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult ContactUs() {
            return View();
        }

        [HttpPost]
        public IActionResult ContactUs(SendMailDto sendMailDto)
        {
            if (!ModelState.IsValid) {
                ViewBag.Error = "Errorrr";
                return View();
            };

            try { 
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("manueljuliocasanova@gmail.com");

                mail.To.Add("manuel_julio.casanova_reyes@lcieducation.net");

                mail.Subject = sendMailDto.Subject;

                mail.IsBodyHtml = true;

                string content = "Email : " + sendMailDto.Email;
                content += "<br/> Body :" + sendMailDto.Body;
                mail.Body = content;

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");

                NetworkCredential networkCredential = new NetworkCredential("manueljuliocasanova@gmail.com", "C4s4n0v@0920");
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = networkCredential;
                smtpClient.Port = 25;
                smtpClient.EnableSsl = true;
                smtpClient.Send(mail);

                ViewBag.Message = "Mail Send";

                ModelState.Clear();
            
            }catch (Exception ex) { 
                ViewBag.Message = ex.Message.ToString();   
            }
            
            return View();
        }
    }
}
