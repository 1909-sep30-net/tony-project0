using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using YourStore.Library.Repo;
using YourStoreWeb.Models;

namespace YourStoreWeb.Controllers
{
    public class StoreController : Controller
    {


        private readonly ILogger<StoreController> _logger;
        private readonly IRepoBusLogic _repo;
        public StoreController(ILogger<StoreController> logger, IRepoBusLogic repo)
        {
            _logger = logger;
            _repo = repo;
        }
        public IActionResult Index()
        {
            _logger.LogInformation("Store Controller: GetIndex");

            return View(new ViewOrderHistoryModel());
        }
        [HttpPost]
        public IActionResult Index(ViewOrderHistoryModel model)
        {
            _logger.LogInformation("Store Controller: PostIndex");

            string SearchOrder = model.SearchOrder;
            string SearchOrderDetail = model.SearchOrderDetail;
            string SearchCustomerHistory = model.SearchCustomerHistory;



            var y = _repo.GetAllOrders();
                if (SearchOrder != null)
                {
                    var orderStoreHistory = y.Where(a => a.Store.StoreID == int.Parse(SearchOrder));
                    var xx = orderStoreHistory.ToList();
                    model.Order = (xx);
                }
                if (SearchOrderDetail != null)
                {
                    var orderDetail = y.Where(a => a.Id == int.Parse(SearchOrderDetail)).FirstOrDefault();
                    var xx = orderDetail;
                    model.OrderDetail = (xx);
                }
                if (SearchCustomerHistory != null)
                {
                    var orderDetail = y.Where(a => a.Customer.Username == SearchCustomerHistory);
                    var xx = orderDetail.ToList();
                    model.CusOrdHistory = (xx);
                }


            return View(model);
        }
    }
}