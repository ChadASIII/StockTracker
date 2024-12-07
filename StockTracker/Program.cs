using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            int choice = 0;

            Console.WriteLine("What do you wish to do?");
            while (choice != 6)
            {
                try
                {
                    Console.WriteLine("1. Add item to storage\n2. Remove item from storage\n3. List all items\n4. Search for an item\n5. Restock an item\n6. Exit\n");
                    choice = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("\n...\n");
                    Thread.Sleep(500);
                    switch (choice)
                    {
                        case 1:
                            Item.NewItem();
                            break;
                        case 2:
                            Item.RemoveItem();
                            break;
                        case 3:
                            Item.ListItems();
                            break;
                        case 4:
                            Item.ListItem();
                            break;
                        case 5:
                            Shipment.OrderShipment();
                            break;
                    
                    }
                    if (choice < 1 || choice > 6)
                    {
                        Console.WriteLine("Please enter a number corresponding to the listed options.\n");
                    }
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Error: " + e.Message + "\n");
                    Console.WriteLine("Please enter a number corresponding to the listed options.\n");
                }
            }
        }
    }
}
