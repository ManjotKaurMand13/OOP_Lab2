using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab2
{

    class Product
    {
        // getter and setter
        public string Name { get; set; }
        public double Price { get; set; }
        public string Code { get; set; }

        // constructor
        public Product(string name, double price, string code)
        {
            Name = name;
            Price = price;
            Code = code;
        }
    }

    class vendingMachine
    {
        // getter and setter
        public static int SerialNumber { get; set; }
        public Dictionary<int, int> MoneyFloat { get; set; }
        public Dictionary<Product, int> Inventory { get; set; }

        // declear Barcode as readonly
        public readonly string Barcode = "123456789";

        public vendingMachine(int serialNumber)
        {
            SerialNumber = serialNumber;
            MoneyFloat = new Dictionary<int, int>();
            Inventory = new Dictionary<Product, int>();
        }

        // static constructor
        static vendingMachine()
        {
            SerialNumber = 1;
            // increment the serial number each time a vending machine is created
            SerialNumber++;
        }

        public string StockItem(Product product, int quantity)
        {
            // check if the product is already in the inventory
            if (Inventory.ContainsKey(product))
            {
                Inventory[product] += quantity;
            }
            else
            {
                Inventory.Add(product, quantity);
            }
            return "Product " + product.Name + " " + product.Code + " " + product.Price + " " + quantity;
        }

        // method for stocking float
        public string StockFloat(int moneyDenomination, int quantity)
        {
            // check if the money denomination is valid
            if (MoneyFloat.ContainsKey(moneyDenomination))
            {
                MoneyFloat[moneyDenomination] += quantity;
            }
            else
            {
                MoneyFloat.Add(moneyDenomination, quantity);
            }
            return "Money " + moneyDenomination + " " + quantity;
        }

        // method for stocking float
        public string VendItem(string code, List<int> money)
        {
            Product product = Inventory.Keys.FirstOrDefault(x => x.Code == code);
            if (product == null)
            {
                return "Error, no item with code " + code;
            }
            if (Inventory[product] == 0)
            {
                return "Error: Item is out of stock";
            }
            if (money.Sum() < product.Price)
            {
                return "Error: insufficient money provided";
            }
            double change = money.Sum() - product.Price;
            Inventory[product]--;
            return "Please enjoy your " + product.Name + " and take your change of $" + change;
        }

        // method for stocking float
        public string GetSerialNumber()
        {
            return "Serial Number: " + SerialNumber;
        }

        public string GetBarcode()
        {
            return "Barcode: " + Barcode;
        }

        // method for stocking float
        public string GetInventory()
        {
            string inventory = "";
            foreach (var item in Inventory)
            {
                inventory += "Product " + item.Key.Name + " " + item.Key.Code + " " + item.Key.Price + " " + item.Value + "\n";
            }
            return inventory;
        }

        // method for stocking float
        public string GetMoneyFloat()
        {
            string moneyFloat = "";
            foreach (var item in MoneyFloat)
            {
                moneyFloat += "Money " + item.Key + " " + item.Value + "\n";
            }
            return moneyFloat;
        }

        // method for stocking float
        public string GetTotalInventory()
        {
            int total = 0;
            foreach (var item in Inventory)
            {
                total += item.Value;
            }
            return "Total Inventory: " + total;
        }

        // main method
        static void Main(string[] args)
        {
            // create a new vending machine
            vendingMachine machine = new vendingMachine(1);
            // add some products to the machine
            machine.StockItem(new Product("Coke", 1.50, "Coke"), 10);
            machine.StockItem(new Product("Pepsi", 1.50, "Pepsi"), 10);
            machine.StockItem(new Product("Sprite", 1.50, "Sprite"), 10);
            machine.StockItem(new Product("Dr. Pepper", 1.50, "Dr. Pepper"), 10);

            // add some money to the machine
            machine.StockFloat(1, 10);
            machine.StockFloat(5, 10);
            machine.StockFloat(10, 10);
            machine.StockFloat(20, 10);

            // print the inventory
            Console.WriteLine(machine.GetInventory());
            Console.WriteLine(machine.GetMoneyFloat());
            Console.WriteLine(machine.GetTotalInventory());
            Console.WriteLine(machine.GetSerialNumber());

            // print the barcode
            Console.WriteLine(machine.GetBarcode());
        }
    }
}



