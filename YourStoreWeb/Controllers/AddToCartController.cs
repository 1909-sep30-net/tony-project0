using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using YourStore.Library;
using YourStore.Library.Repo;
using YourStoreWeb.Models;


namespace YourStoreWeb.Controllers
{
    public class AddToCartController : Controller
    {
        public static int count = 0;
        private readonly ILogger<AddToCartController> _logger;
        private readonly IRepoBusLogic _repo;
        public AddToCartController(ILogger<AddToCartController> logger, IRepoBusLogic repo)
        {
            _logger = logger;
            _repo = repo;
        }


        [HttpGet]
        public IActionResult Cart()
        {
            _logger.LogInformation("AddToCart Controller: GetCart");

            return View();
        }

        [HttpPost]
        public IActionResult Cart(ViewStoreDetailModel Model)
        {
            _logger.LogInformation("AddToCart Controller: PostCart");

            var x = _repo.GetAllProducts().Where(a => a.ID == Model.ProductID).FirstOrDefault();
            /// pass to temp data 
            /// 
            Dictionary<Product, int> inven = new Dictionary<Product, int>();
            // pass Inventory
            try
            {

                Store PrevStore = DeserObjectStore(TempData["store"].ToString());
                Product item = PrevStore.ItemInventory.Where(f => f.Key.ID == x.ID).FirstOrDefault().Key;

                //validate same product
                



                if (PrevStore.ItemInventory[item] > Model.Quantity)
                {
                    PrevStore.ItemInventory[item] -= Model.Quantity;
                    TempData["Store"] = SeriObject(PrevStore);
                    Model.ViewOrderModel.Product = inven;
                    try 
                    {
                        inven = DeserObjectOrderDetail(TempData.Peek("OrderDetail").ToString());
                        inven.Add(x, Model.Quantity);
                        string y = SeriObject(inven);
                        TempData["OrderDetail"] = y;
                    }
                    catch
                    {
                        inven.Add(x, Model.Quantity);
                        string y = SeriObject(inven);
                        TempData["OrderDetail"] = y;
                    }

                }
                else
                {
                    Model.errorMessage = "Amount request is greater than inventory.";
                }

                
                //end pass   

                Model.Store = PrevStore;

                return View(Model);
            }
            catch
            {
                Model.errorMessage = "There is something with your purchase. Please wait. ";
                return View(Model);
            }
     


       

        }
        [HttpGet]
        public IActionResult FinishOrder()
        {
            _logger.LogInformation("AddToCart Controller: GetFinishOrder");
            ViewOrderDetailsModel od = new ViewOrderDetailsModel();
            od.Product = DeserObjectOrderDetail(TempData["OrderDetail"].ToString());
            od.Date = DateTime.Now;
            od.store = DeserObjectStore(TempData["Store"].ToString());

            Order o = new Order();

            o.Product = od.Product;
            o.Store = od.store;
            try
            {
                var allC = _repo.GetAllCustomer().Where(c => c.Username == TempData.Peek("Logged").ToString()).FirstOrDefault();
                o.Customer = allC;

                _repo.AddOrder(o);
                var allOrder = _repo.GetAllOrders().Count();
                foreach (Product x in od.Product.Keys)
                {
                    _repo.AddOrderDetail(od.Product[x], x.ID, allOrder);

                }

                foreach (Product x in od.Product.Keys)
                {
                    var item = od.store.ItemInventory.Where(a => a.Key.ID == x.ID).FirstOrDefault();

                    _repo.MakeInventoryChanges(od.store.StoreID, x.ID, item.Value);


                }
                return View(od);
            }
            catch
            {
                return RedirectToAction("AddCustomer", "Customer");
            }


        }
        [HttpPost]
        public IActionResult FinishOrder(ViewOrderDetailsModel od)
        {
            _logger.LogInformation("AddToCart Controller: PostFinishOrder");
            try
            {

                od.Product = DeserObjectOrderDetail(TempData["OrderDetail"].ToString());
            }
            catch
            {
                od.errorMessage = "You have not purchase an item.";
                return View(od.errorMessage);
            }
            od.Date = DateTime.Now;
            od.store = DeserObjectStore(TempData["Store"].ToString());

            Order o = new Order();

            o.Product = od.Product;
            o.Store = od.store;
            try
            {
                var allC = _repo.GetAllCustomer().Where(c => c.Username == TempData.Peek("Logged").ToString()).FirstOrDefault();
                o.Customer = allC;

                _repo.AddOrder(o);
                var allOrder = _repo.GetAllOrders().Count();
                foreach (Product x in od.Product.Keys)
                {
                    _repo.AddOrderDetail(od.Product[x], x.ID, allOrder);

                }

                foreach (Product x in od.Product.Keys)
                {
                    var item = od.store.ItemInventory.Where(a => a.Key.ID == x.ID).FirstOrDefault();

                    _repo.MakeInventoryChanges(od.store.StoreID, x.ID, item.Value);


                }
                return View(od);
            }
            catch
            {
                TempData["NotLogged"] = "true";
                return RedirectToAction("AddCustomer", "Customer");
            }


          
        }



        [HttpPost]
        public IActionResult Index(ViewStoreProductModel model=null, ViewOrderDetailsModel x=null)
        {
            _logger.LogInformation("AddToCart Controller: PostIndex");
            if (!ModelState.IsValid) 
            {
                return RedirectToAction("Index", "Home");
            }

                int StoreID = model.StoreID;
                Store st = _repo.GetAllStores().Where(s => s.StoreID == StoreID).FirstOrDefault();
                string storeS = SeriObject(st);
                TempData["store"] = storeS;

                ViewStoreDetailModel stor = new ViewStoreDetailModel();
                stor.Store = st;

                if (st == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                return View(stor);
        }
        [HttpGet]
        public IActionResult Index()
        {
                 _logger.LogInformation("AddToCart Controller: PostIndex");

                return View();

            

        }
        [HttpPost]
        public IActionResult StoreShop(ViewStoreDetailModel model)
        {
            _logger.LogInformation("AddToCart Controller: StoreShop");


            int StoreID = model.Store.StoreID;
            Store st = _repo.GetAllStores().Where(s => s.StoreID == StoreID).FirstOrDefault();
           


            ViewStoreDetailModel stor = new ViewStoreDetailModel();
            stor.Store = st;

            if (st == null)
            {
                return RedirectToAction("Index", "Home");
            }


            return RedirectToAction("Index", "Home");

        }



        public string SeriObject<T>(T objects)
        {
            _logger.LogInformation("AddToCart Controller: SeriObject");

            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(T));
            MemoryStream msObj = new MemoryStream();
            js.WriteObject(msObj, objects);
            msObj.Position = 0;
            StreamReader sr = new StreamReader(msObj);

            // "{\"Description\":\"Share Knowledge\",\"Name\":\"C-sharpcorner\"}"  
            string json = sr.ReadToEnd();

            sr.Close();
            msObj.Close();
            return json;
        }
        /// <summary>
        /// have 2 deserOBj because i dont know how to use T :  public T DeserObjectStore<T>(string jsonS)
        /// </summary>
        /// <param name="jsonS"></param>
        /// <returns></returns>
        public Store DeserObjectStore(string jsonS)
        {
            _logger.LogInformation("AddToCart Controller: DeserObjectStoreObject");

            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(jsonS)))
            {
                // Deserialization from JSON  
                DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(Store));
                Store bsObj2 = (Store)deserializer.ReadObject(ms);
                return bsObj2;
                /* Response.Write("Name: " + bsObj2.Name); // Name: C-sharpcorner
                 Response.Write("Description: " + bsObj2.Description); // Description: Share Knowledge */
            }
        }
        public Dictionary<Product,int> DeserObjectOrderDetail(string jsonS)
        {
            _logger.LogInformation("AddToCart Controller: DeserObjectDictionary");

            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(jsonS)))
            {
                // Deserialization from JSON  
                DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(Dictionary<Product, int>));
                Dictionary<Product, int> bsObj2 = (Dictionary<Product, int>)deserializer.ReadObject(ms);
                return bsObj2;
                /* Response.Write("Name: " + bsObj2.Name); // Name: C-sharpcorner
                 Response.Write("Description: " + bsObj2.Description); // Description: Share Knowledge */
            }
        }
    }
}