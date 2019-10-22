using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using YourStore.Library;
using YourStore.Library.Repo;
using YourStoreWeb.Models;

namespace YourStoreWeb.Controllers
{
    public class CustomerController : Controller
    {

        private readonly ILogger<AddToCartController> _logger;
        private readonly IRepoBusLogic _repo;
        public CustomerController(ILogger<AddToCartController> logger, IRepoBusLogic repo)
        {
            _logger = logger;
            _repo = repo;
            
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Customer Controller: GetIndex");

            return View();
        }
        [HttpGet]
        public IActionResult AddCustomer()
        {
            _logger.LogInformation("Customer Controller: GetAddCustomer");

            return View(new ViewCustomerModel());
        }

        [HttpPost]
        public IActionResult AddCustomer(ViewCustomerModel m)
        {
       
                _logger.LogInformation("Customer Controller: PostAddCustomer");
            if (Regex.IsMatch(m.Customer.FirstName, @"^[a-zA-Z]+$") || Regex.IsMatch(m.Customer.LastName, @"^[a-zA-Z]+$"))
            {
                _repo.AddCustomer(m.Customer);
                
            }
            m.Customer = null;


            return View(m);

        }
        [HttpGet]
        public IActionResult DisplayCustomer()
        {
            var allC=_repo.GetAllCustomer();

            _logger.LogInformation("Customer Controller: GetDisplayCustomer");

            ViewCustomerModel v = new ViewCustomerModel();
            foreach(Customer x in allC)
            {
            
                v.AllCus.Add(new Customer 
                {

                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Zip = x.Zip,
                    Id = x.Id,
                    Username = x.Username,

                }
                );
            }
        


            return View(v);
        }

        [HttpPost]
        public IActionResult DisplayCustomer(ViewCustomerModel search)
        {
            _logger.LogInformation("Customer Controller: SearchDisplayCustomer");


            var allC = _repo.GetAllCustomer();
         
            if (search.SearchCustomer != null)
            {
                search.AllCus = allC.Where(x => x.FirstName == search.SearchCustomer).ToList();
                if (search.AllCus.Count == 0)
                {
                    search.AllCus = allC.Where(y => y.LastName == search.SearchCustomer).ToList();

                }
                else if (search.AllCus.Count == 0)
                {
                    search.AllCus = allC.Where(i => i.Username == search.SearchCustomer).ToList();
                }
                else if (search.AllCus.Count == 0)
                {
                    try
                    {
                        search.AllCus = allC.Where(i => i.Id == int.Parse(search.SearchCustomer)).ToList();
                    }
                    catch(FormatException ex)
                    {
                        search.AllCus = null;
                    }
                }
            }
            else
            {
                return RedirectToAction("AddCustomer");
            }
            return View(search);
        }
    }
}