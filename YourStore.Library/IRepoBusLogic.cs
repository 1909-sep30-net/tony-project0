using System.Collections.Generic;
using System.Threading.Tasks;
using YourStore.Library;

namespace YourStore.Library.Repo
{
    public interface IRepoBusLogic
    {
        IEnumerable<Customer> GetAllCustomer();
        IEnumerable<Order> GetAllOrders();

        IEnumerable<Employee> GetAllEmployees();

        IEnumerable<Product> GetAllProducts();
        IEnumerable<Store> GetAllStores();





        void AddEmployee(Employee emp);
        void AddCustomer(Customer customer);
        void AddOrder(Order order);
        void AddOrderDetail(int quantity, int product, int orderID);
        void AddInventory(int quantity, Product product, Store Store);
        void AddProduct(Product product);
        void MakeInventoryChanges(int storeid, int productid, int quantity);



    }


}