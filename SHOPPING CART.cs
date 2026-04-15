using System;

namespace CALEB
{
    class Product
    {
        public int Id;
        public string Name;
        public double Price;
        public int RemainingStock;

        // Display Product Info
        public void DisplayProduct()
        {
            Console.WriteLine($"{Id}. {Name} - {Price} PHP (Stock: {RemainingStock})");
        }

        // Get Total Price
        public double GetItemTotal(int quantity)
        {
            return Price * quantity;
        }

        // Check Stock Availability
        public bool HasEnoughStock(int quantity)
        {
            return quantity <= RemainingStock;
        }

        // Deduct Stock
        public void DeductStock(int quantity)
        {
            RemainingStock -= quantity;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // Product List
            Product[] products = new Product[5];

            products[0] = new Product { Id = 1, Name = "Asics Netburner Ballistic FF4", Price = 9500, RemainingStock = 5 };
            products[1] = new Product { Id = 2, Name = "Li-ning Way of Wade Shadow 6 v2", Price = 6500, RemainingStock = 5 };
            products[2] = new Product { Id = 3, Name = "Li-ning 808 5 Ultra", Price = 9000, RemainingStock = 5 };
            products[3] = new Product { Id = 4, Name = "Asics Metarise 2", Price = 14000, RemainingStock = 5 };
            products[4] = new Product { Id = 5, Name = "Asics Netburner Ballistic MT FF4", Price = 9500, RemainingStock = 5 };

            // Show Product List
            Console.Write("Product List:\n");
            for (int i = 0; i < products.Length; i++)
            {
                products[i].DisplayProduct();
            }

            // Cart Arrays
            int[] CartIds = new int[5];
            int[] CartQty = new int[5];
            double[] CartSubTotal = new double[5];
            int CartCount = 0;

           
            string Choice;

            while (true)
            {
                Console.WriteLine("\nDo you want to purchase a product? (Y or N)");
                Choice = Console.ReadLine();

                if (!string.IsNullOrEmpty(Choice) &&
                    (Choice.ToUpper() == "Y" || Choice.ToUpper() == "N"))
                {
                    break;
                }

                Console.WriteLine("Invalid input! Please enter Y or N only.");
            }

            // Main Shopping Loop
            while (Choice.ToUpper() == "Y")
            {
                // Show Products
                Console.WriteLine("\nProduct List:");
                for (int i = 0; i < products.Length; i++)
                {
                    products[i].DisplayProduct();
                }

                // Input Product ID
                Console.WriteLine("Enter Product ID: ");
                string input = Console.ReadLine();

                if (!int.TryParse(input, out int ProductNumber))
                {
                    Console.WriteLine("Invalid Product Number!");
                    continue;
                }

                if (ProductNumber < 1 || ProductNumber > products.Length)
                {
                    Console.WriteLine("Invalid Product Number!");
                    continue;
                }

                Product chosen = products[ProductNumber - 1];

                // Input Quantity
                Console.WriteLine("Input Quantity: ");
                string qtyInput = Console.ReadLine();

                if (!int.TryParse(qtyInput, out int quantity) || quantity <= 0)
                {
                    Console.WriteLine("Invalid Quantity Input!");
                    continue;
                }

                // Stock Check
                if (chosen.RemainingStock <= 0)
                {
                    Console.WriteLine("Out of Stock!");
                    continue;
                }

                if (!chosen.HasEnoughStock(quantity))
                {
                    Console.WriteLine("Not Enough Stock Available!");
                    continue;
                }

                // Check if Item Already in Cart
                bool ItemFound = false;

                for (int i = 0; i < CartCount; i++)
                {
                    if (CartIds[i] == chosen.Id)
                    {
                        CartQty[i] += quantity;
                        CartSubTotal[i] += chosen.GetItemTotal(quantity);
                        ItemFound = true;
                        break;
                    }
                }

                // Add New Item to Cart
                if (!ItemFound)
                {
                    if (CartCount >= CartIds.Length)
                    {
                        Console.WriteLine("Your Cart is Full!");
                        continue;
                    }

                    CartIds[CartCount] = chosen.Id;
                    CartQty[CartCount] = quantity;
                    CartSubTotal[CartCount] = chosen.GetItemTotal(quantity);
                    CartCount++;
                }

                // Deduct Stock
                chosen.DeductStock(quantity);

                // Ask to Continue
                Console.WriteLine("\nDo You Want to Add More Products? (Y or N)");
                Choice = Console.ReadLine();
            }

            // Receipt
            double TotalAmount = 0;

            Console.WriteLine("\nReceipt:");
            for (int i = 0; i < CartCount; i++)
            {
                Console.WriteLine($"Product ID: {CartIds[i]} | Qty: {CartQty[i]} | Subtotal: {CartSubTotal[i]}");
                TotalAmount += CartSubTotal[i];
            }

            // Discount
            double Discount = 0;

            if (TotalAmount > 5000)
            {
                Discount = TotalAmount * 0.10;
            }

            double FinalAmount = TotalAmount - Discount;

            // Totals
            Console.WriteLine($"\nTotal Amount: {TotalAmount}");
            Console.WriteLine($"Discount: {Discount}");
            Console.WriteLine($"Final Amount: {FinalAmount}");

            // Updated Stock
            Console.WriteLine("\nUpdated Remaining Stock:");
            for (int i = 0; i < products.Length; i++)
            {
                products[i].DisplayProduct();
            }
        }
    }
}