using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StockTracker
{
    public class Shipment
    {
        private static Random random = new Random();
        private int shipmentID;
        private string name;
        private double price;
        private int units;
        private static bool result;
        public static List<Shipment> shipments = new List<Shipment>();
        public static Dictionary<int, Shipment> orderContents = new Dictionary<int, Shipment>();
        public static List<string> shipmentNames = new List<string>();

        public static void OrderShipment()
        {
            int end = 0;
            int purchaseAmount;
            double total = 0;

            UpdateShipments(shipments);

            while (end != 2 && end != 3)
            {
                Console.WriteLine("\nFetching shipment information for stocked items...\n");
                Thread.Sleep(1000);
                ListShipments();
                int choice = -9999;
                while (choice != 0)
                {
                    try
                    {
                        Console.Write("Enter the ID of the shipment you wish to add to your order or 0 to review order: ");
                        choice = Convert.ToInt32(Console.ReadLine());
                        foreach (Shipment shipment in shipments)
                        {
                            if (shipment.shipmentID == choice)
                            {
                                Shipment tempShipment = new Shipment(shipment);
                                Console.WriteLine("How many shipments of " + shipment.name + " would you like to add to your order?: ");
                                purchaseAmount = Convert.ToInt32(Console.ReadLine());
                                tempShipment.price = tempShipment.price * purchaseAmount;
                                tempShipment.units = tempShipment.units * purchaseAmount;
                                Console.WriteLine("\nAdding to order...\n");
                                if (orderContents.ContainsKey(tempShipment.shipmentID))
                                {
                                    orderContents[tempShipment.shipmentID].units += tempShipment.units;
                                    orderContents[tempShipment.shipmentID].price += tempShipment.price;
                                }
                                else
                                {
                                    orderContents[tempShipment.shipmentID] = tempShipment;
                                }
                                total += tempShipment.price;
                            }
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Please enter an integer ID.");
                    }
                    if (choice > shipments.Count || choice < 0)
                    {
                        Console.WriteLine("Please enter a value that matches a shipments ID or exit.");
                    }
                }
                Console.Write("Order Details: \n");
                foreach (var shipment in orderContents.Values)
                {
                    Console.WriteLine("Item: " + shipment.name + "\nAmount: " + shipment.units + "\nPrice: " + shipment.price + "\n\n");
                }
                try
                {
                    Console.Write("1. Continue Shopping\n");
                    Console.Write("2. Complete Purchase\n");
                    Console.Write("3. Cancel Order\n\n");
                    end = Convert.ToInt32(Console.ReadLine());
                    switch (end)
                    {
                        case 2:
                            Console.WriteLine("Processing Order...\n\n");
                            Thread.Sleep(500);
                            Console.WriteLine("Success! You have been charged $" + total + "\n\n");
                            orderContents.Clear();
                            break;
                        case 3:
                            Console.WriteLine("Order Cancelled.");
                            orderContents.Clear();
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please enter an integer ID.");
                }
                if (end > 3 || end < 1)
                {
                    Console.WriteLine("Please enter a valid integer.");
                }
            }
        }

        public static List<Shipment> UpdateShipments(List<Shipment> shipmentList)
        {
            List<Item> inventory = Item.GetInventory();
            Shipment newShipment;
            string itemName;
            int itemID;
            foreach (Shipment shipment in shipmentList)
            {
                itemName = shipment.name;
                if (!shipmentNames.Contains(itemName))
                {
                    shipmentNames.Add(itemName);
                }
            }
            foreach (Item item in inventory)
            {
                itemName = item.name;
                itemID = item.itemID;
                if (!shipmentNames.Contains(itemName))
                {
                    newShipment = CreateShipment(itemName, itemID);
                    shipmentList.Add(newShipment);
                }
            }

            return shipmentList;
        }

        public static void ListShipments()
        {
            foreach(Shipment shipment in shipments)
            Console.WriteLine("Shipment ID: " + shipment.shipmentID + "\nItem: " + shipment.name + "\nShipment Amount: " + shipment.units + "\nPrice: " + shipment.price + "\n");
        }

        public static Shipment CreateShipment(string name, int id)
        {
            Shipment newShipment = new()
            {
                name = name,
                shipmentID = id,
                units = random.Next(10, 20),
                price = Math.Round((random.NextDouble() * random.Next(100, 1000)), 2)
            };
            return newShipment;
        }

        public Shipment() { }

        public Shipment(Shipment other)
        {
            this.shipmentID = other.shipmentID;
            this.name = other.name;
            this.price = other.price;
            this.units = other.units;
        }
    }
}