using System;

namespace BrassAndPoem
{
    public class Program
    {
        public static void Main()
        {
            List<ProductType> productTypes = new List<ProductType>
            {
                new ProductType { Id = 1, Title = "Electronics" },
                new ProductType { Id = 2, Title = "Furniture" }
            };

            List<Product> products = new List<Product>
            {
                new Product { Name = "Laptop", Price = 999.99m, ProductTypeId = 1 },
                new Product { Name = "Phone", Price = 699.99m, ProductTypeId = 1 },
                new Product { Name = "Table", Price = 299.99m, ProductTypeId = 2 },
                new Product { Name = "Chair", Price = 99.99m, ProductTypeId = 2 },
                new Product { Name = "Monitor", Price = 199.99m, ProductTypeId = 1 }
            };

            Console.WriteLine("Welcome to the Brass and Poem Inventory System!");

            bool exit = false;
            while (!exit)
            {
                DisplayMenu();
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DisplayAllProducts(products, productTypes);
                        break;
                    case "2":
                        DeleteProduct(products, productTypes);
                        break;
                    case "3":
                        AddProduct(products, productTypes);
                        break;    
                    case "4":
                        UpdateProduct(products, productTypes);
                        break;
                    case "5":
                        exit = true;
                        Console.WriteLine("Exiting the program. Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please choose a number between 1 and 5.");
                        break;
                }

                if (!exit)
                {
                    Console.WriteLine("Press any key to return to the menu...");
                    Console.ReadKey();
                }
            }
        }   

        static void DisplayMenu()
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Display all products");
            Console.WriteLine("2. Delete a product");
            Console.WriteLine("3. Add a new product");
            Console.WriteLine("4. Update product properties");
            Console.WriteLine("5. Exit");
        }

        static void DisplayAllProducts(List<Product> products, List<ProductType> productTypes)
        {
            Console.WriteLine("Products:");
            if (products.Count == 0)
            {
                Console.WriteLine("No products available.");
            }
            else
            {
                for (int i = 0; i < products.Count; i++)
                {
                    var product = products[i];
                    var productType = productTypes.FirstOrDefault(pt => pt.Id == product.ProductTypeId)?.Title ?? "Unknown";
                    Console.WriteLine($"{i + 1}. {product.Name} - ${product.Price} ({productType})");
                }
            }
        }

        static void DeleteProduct(List<Product> products, List<ProductType> productTypes)
        {
            DisplayAllProducts(products, productTypes);
            Console.Write("Enter the index of the product to delete: ");

            if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index <= products.Count)
            {
                products.RemoveAt(index - 1);
                Console.WriteLine("Product deleted successfully.");
            }
            else
            {
                Console.WriteLine("Invalid index. No product was deleted.");
            }
        }

        static void AddProduct(List<Product> products, List<ProductType> productTypes)
        {
            Console.Write("Enter the name of the new product: ");
            string name = Console.ReadLine();

            Console.Write("Enter the price of the new product: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                Console.WriteLine("Invalid price. Product not added.");
                return;
            }

            Console.WriteLine("Product Types:");
            for (int i = 0; i < productTypes.Count; i++)
            {
                Console.WriteLine($"{productTypes[i].Id}. {productTypes[i].Title}");
            }

            Console.Write("Choose a product type: ");
            if (!int.TryParse(Console.ReadLine(), out int typeId) || !productTypes.Any(pt => pt.Id == typeId))
            {
                Console.WriteLine("Invalid product type. Product not added.");
                return;
            }

            products.Add(new Product { Name = name, Price = price, ProductTypeId = typeId });
            Console.WriteLine("Product added successfully.");
        }

        static void UpdateProduct(List<Product> products, List<ProductType> productTypes)
        {
            if (products.Count == 0)
            {
                Console.WriteLine("No products available to update.");
                return;
            }

            DisplayAllProducts(products, productTypes);
            Console.Write("Enter the index of the product to update: ");

            if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index <= products.Count)
            {
                Product product = products[index - 1];

                Console.Write($"Enter a new name for {product.Name} (or press Enter to keep it): ");
                string newName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newName))
                {
                    product.Name = newName;
                }

                Console.Write($"Enter a new price for {product.Price} (or press Enter to keep it): ");
                string priceInput = Console.ReadLine();
                if (decimal.TryParse(priceInput, out decimal newPrice))
                {
                    product.Price = newPrice;
                }

                Console.WriteLine("Product Types:");
                for (int i = 0; i < productTypes.Count; i++)
                {
                    Console.WriteLine($"{productTypes[i].Id}. {productTypes[i].Title}");
                }
                Console.Write($"Choose a new product type for {product.Name} (or press Enter to keep it): ");
                string typeInput = Console.ReadLine();
                if (int.TryParse(typeInput, out int newTypeId) && productTypes.Any(pt => pt.Id == newTypeId))
                {
                    product.ProductTypeId = newTypeId;
                }

                Console.WriteLine("Product updated successfully.");
            }
            else
            {
                Console.WriteLine("Invalid index. No product was updated.");
            }
        }
    }
}
