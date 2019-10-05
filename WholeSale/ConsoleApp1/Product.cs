namespace Logic
{
    public class Product
    {
        public string Name { get; set; }
        public string Vendor { get; set; }
        public int Amount { get; set; }

        public bool InventoryLow() {

            return true;
        }
        public bool Restockable()
        {
            return true;
        }
        public void Refill()
        {

        }
        public void DecreaseInventory(int num)
        {

        }


    }
}