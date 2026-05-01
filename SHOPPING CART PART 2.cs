using System;
using System.Collections.Generic;

namespace CALEB
{
    class CartItem
    {
        public int CartItemID;
        public string CartItemName;
        public int CartItemQuantity;
        public double CartItemSubTotal;
    }

    class Product
    {
        public int Id;
        public string Name;
        public double Price;
        public int RemainingStock;
        public string Category;

        // Display Product Info
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

    class Order
    {
        public int ReceiptNumber;
        public double FinalTotal;
    }

    internal class Program
    {
        static int receiptCounter = 1;
        static List<Order> orderHistory = new List<Order>();

        // Y/N Validation
        public static string GetYesNo(string message)
        {
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine().ToUpper();

                if (input == "Y" || input == "N")
                    return input;

                Console.WriteLine("Invalid input. Please enter Y or N only.");
            }
        }

        // Validation
        public static int GetInt(string message)
        {
            int value;
            while (true)
            {
                Console.Write(message);
                if (int.TryParse(Console.ReadLine(), out value))
                    return value;

                Console.WriteLine("Invalid number.");
            }
        }

        // Validation
        public static double GetDouble(string message)
        {
            double value;
            while (true)
            {
                Console.Write(message);
                if (double.TryParse(Console.ReadLine(), out value))
                    return value;

                Console.WriteLine("Invalid number.");
            }
        }

        public static Product FindProduct(Product[] products, int id)
        {
            for (int i = 0; i < products.Length; i++)
            {
                if (products[i] != null && products[i].Id == id)
                    return products[i];
            }
            return null;
        }

        // Product Search
        public static void SearchProducts(Product[] products)
        {
            Console.Write("Search product: ");
            string keyword = Console.ReadLine().ToLower();

            bool found = false;

            Console.WriteLine("\nSearch Results:");
            for (int i = 0; i < products.Length; i++)
            {
                if (products[i] != null &&
                    products[i].Name.ToLower().Contains(keyword))
                {
                    products[i].DisplayProduct();
                    found = true;
                }
            }

            if (!found)
                Console.WriteLine("No matching products found.");
        }

        // Category Filter
        public static void CategoryFilter(Product[] products)
        {
            Console.WriteLine("\nCATEGORY FILTER");
            Console.WriteLine("1. Shoes");
            Console.WriteLine("2. Volleyball");
            Console.WriteLine("3. Kneepads");
            Console.WriteLine("4. Sports Bag");
            Console.WriteLine("5. All");

            string choice;

            while (true)
            {
                Console.Write("Choose (1-5): ");
                choice = Console.ReadLine();

                if (choice == "1" || choice == "2" || choice == "3" ||
                    choice == "4" || choice == "5")
                    break;

                Console.WriteLine("Invalid input.");
            }

            string category = "";

            if (choice == "1") category = "Shoes";
            else if (choice == "2") category = "Volleyball";
            else if (choice == "3") category = "Kneepads";
            else if (choice == "4") category = "Sports Bag";

            Console.WriteLine("\nFILTERED PRODUCTS:");

            for (int i = 0; i < products.Length; i++)
            {
                if (products[i] != null &&
                    (choice == "5" || products[i].Category == category))
                {
                    products[i].DisplayProduct();
                }
            }
        }

        // Cart System
        public static void ManageCart(List<CartItem> cart, Product[] products)
        {
            while (true)
            {
                Console.WriteLine("\nCART MENU");
                Console.WriteLine("1. View Cart");
                Console.WriteLine("2. Remove Item");
                Console.WriteLine("3. Update Quantity");
                Console.WriteLine("4. Clear Cart");
                Console.WriteLine("5. Exit Cart");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        if (cart.Count == 0)
                            Console.WriteLine("Cart is empty.");
                        else
                            foreach (var item in cart)
                                Console.WriteLine($"{item.CartItemName} x{item.CartItemQuantity} = {item.CartItemSubTotal} PHP");
                        break;

                    case "2":
                        int rid = GetInt("Enter ID: ");
                        var removeItem = cart.Find(i => i.CartItemID == rid);

                        if (removeItem != null)
                        {
                            Product p = FindProduct(products, rid);
                            if (p != null)
                                p.RemainingStock += removeItem.CartItemQuantity;

                            cart.Remove(removeItem);
                            Console.WriteLine("Removed.");
                        }
                        break;

                    case "3":
                        int uid = GetInt("Enter ID: ");
                        var updateItem = cart.Find(i => i.CartItemID == uid);

                        if (updateItem != null)
                        {
                            Product p = FindProduct(products, uid);

                            int qty = GetInt("New qty: ");

                            p.RemainingStock += updateItem.CartItemQuantity;

                            if (p.HasEnoughStock(qty))
                            {
                                p.DeductStock(qty);
                                updateItem.CartItemQuantity = qty;
                                updateItem.CartItemSubTotal = p.GetItemTotal(qty);
                            }
                        }
                        break;

                    case "4":
                        foreach (var item in cart)
                        {
                            Product p = FindProduct(products, item.CartItemID);
                            if (p != null)
                                p.RemainingStock += item.CartItemQuantity;
                        }

                        cart.Clear();
                        Console.WriteLine("Cart cleared.");
                        break;

                    case "5":
                        return;
                }
            }
        }

        // Low Stock Alert
        public static void LowStockAlert(Product[] products)
        {
            Console.WriteLine("\nLOW STOCK ALERT:");

            bool found = false;

            for (int i = 0; i < products.Length; i++)
            {
                if (products[i] != null && products[i].RemainingStock <= 5)
                {
                    Console.WriteLine($"{products[i].Name} has only {products[i].RemainingStock} stocks left.");
                    found = true;
                }
            }

            if (!found)
                Console.WriteLine("No low stock items.");
        }

        static void Main(string[] args)
        {
            Product[] products = new Product[13];

            // Shoes
            products[0] = new Product { Id = 1, Name = "Asics Netburner Ballistic FF4", Price = 9500, RemainingStock = 5, Category = "Shoes" };
            products[1] = new Product { Id = 2, Name = "Li-ning Way of Wade Shadow 6 v2", Price = 6500, RemainingStock = 5, Category = "Shoes" };
            products[2] = new Product { Id = 3, Name = "Li-ning 808 5 Ultra", Price = 9000, RemainingStock = 5, Category = "Shoes" };
            products[3] = new Product { Id = 4, Name = "Asics Metarise 2", Price = 14000, RemainingStock = 5, Category = "Shoes" };
            products[4] = new Product { Id = 5, Name = "Asics Netburner Ballistic MT FF4", Price = 9500, RemainingStock = 5, Category = "Shoes" };

            // Volleyballs
            products[5] = new Product { Id = 6, Name = "Mikasa V200W Pro Volleyball", Price = 4500, RemainingStock = 10, Category = "Volleyball" };
            products[6] = new Product { Id = 7, Name = "Molten V5M5000 Volleyball", Price = 3800, RemainingStock = 10, Category = "Volleyball" };
            products[7] = new Product { Id = 8, Name = "Mikasa V300W Training Volleyball", Price = 2500, RemainingStock = 15, Category = "Volleyball" };

            //Kneepads
            products[8] = new Product { Id = 9, Name = "ASICS Kneepads", Price = 1200, RemainingStock = 15, Category = "Kneepads" };
            products[9] = new Product { Id = 10, Name = "Mizuno Kneepads", Price = 1800, RemainingStock = 12, Category = "Kneepads" };

            // Bags
            products[10] = new Product { Id = 11, Name = "Nike Elite Sports Backpack", Price = 2200, RemainingStock = 10, Category = "Sports Bag" };

            List<CartItem> cart = new List<CartItem>();

            Console.WriteLine("PRODUCT MENU");
            Console.WriteLine("1. View");
            Console.WriteLine("2. Search");

            string choice = Console.ReadLine();

            if (choice == "2")
                SearchProducts(products);
            else
                for (int i = 0; i < products.Length; i++)
                    if (products[i] != null)
                        products[i].DisplayProduct();

            Console.WriteLine("\nFilter? (Y/N)");
            if (GetYesNo("") == "Y")
                CategoryFilter(products);

            string buy = "Y";

            while (buy.ToUpper() == "Y")
            {
                int id = GetInt("Product ID: ");
                Product p = FindProduct(products, id);

                if (p == null)
                    continue;

                int qty = GetInt("Qty: ");

                if (!p.HasEnoughStock(qty))
                {
                    Console.WriteLine("Not enough stock.");
                    continue;
                }

                p.DeductStock(qty);

                cart.Add(new CartItem
                {
                    CartItemID = p.Id,
                    CartItemName = p.Name,
                    CartItemQuantity = qty,
                    CartItemSubTotal = p.GetItemTotal(qty)
                });

                if (GetYesNo("Cart menu? (Y/N): ") == "Y")
                    ManageCart(cart, products);

                buy = GetYesNo("Continue buying products? (Y/N): ");
            }

            double total = 0;

            Console.WriteLine("\nReceipt:");
            foreach (var item in cart)
            {
                Console.WriteLine($"{item.CartItemName} x{item.CartItemQuantity} = {item.CartItemSubTotal} PHP");
                total += item.CartItemSubTotal;
            }

            Console.WriteLine($"TOTAL: {total} PHP");

            // Payment Validation
            double payment;
            while (true)
            {
                payment = GetDouble("Enter payment: ");

                if (payment >= total)
                    break;

                Console.WriteLine("Insufficient payment.");
            }

            double change = payment - total;

            // Receipt Info
            Console.WriteLine($"\nReceipt No: {receiptCounter.ToString("D4")}");
            Console.WriteLine($"Date: {DateTime.Now}");
            Console.WriteLine($"Final Total: {total} PHP");
            Console.WriteLine($"Payment: {payment} PHP");
            Console.WriteLine($"Change: {change} PHP");

            orderHistory.Add(new Order { ReceiptNumber = receiptCounter, FinalTotal = total });
            receiptCounter++;

            if (GetYesNo("View order history? (Y/N): ") == "Y")
            {
                Console.WriteLine("\nORDER HISTORY");
                foreach (var o in orderHistory)
                    Console.WriteLine($"Receipt #{o.ReceiptNumber.ToString("D4")} - Final Total: {o.FinalTotal} PHP");
            }

            LowStockAlert(products);
        }
    }
}