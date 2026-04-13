using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CALEB
{
    class Product
    {
        public int Id;
        public string Name;
        public double Price;
        public int RemainingStock;

        public void DisplayProduct()
        {
            Console.WriteLine($"{Id}. {Name} - {Price} PHP (Stock: {RemainingStock})");
        }

        public double GetItemTotal(int quantity)
        {
            return Price * quantity;
        }

        public bool HasEnoughStock(int quantity)
        {
            return quantity <= RemainingStock;
        }

        public void DeductStock(int quantity)
        {
            RemainingStock -= quantity;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Product[] products = new Product[5];

            products[0] = new Product { Id = 1, Name = "Asics Netburner Ballistic FF4", Price = 9500, RemainingStock = 5 };
            products[1] = new Product { Id = 2, Name = "Li-ning Way of Wade Shadow 6 v2", Price = 6500, RemainingStock = 5 };
            products[2] = new Product { Id = 3, Name = "Li-ning 808 5 Ultra", Price = 9000, RemainingStock = 5 };
            products[3] = new Product { Id = 4, Name = "Asics Metarise 2", Price = 14000, RemainingStock = 5 };
            products[4] = new Product { Id = 5, Name = "Asics Netburner Ballistic MT FF4", Price = 9500, RemainingStock = 5 };

            Console.Write("Product List: \n");
            for (int i = 0; i < products.Length; i++)
            {
                products[i].DisplayProduct();
            }
        }
    }
}
