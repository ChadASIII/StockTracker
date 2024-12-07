using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracker
{
    public class Item
    {
        public String? name;
        private String choice;
        public int itemID;
        public int amount;
        private int limit;
        private int lowStock;
        private static int option;
        private static int ID = 1;
        private static int targetID;
        private static Item targetItem;
        public static List<Item> itemList = new List<Item>();

        public static void NewItem()
        {
            try
            {
                Item newItem = new Item();
                Console.Write("What is this item's name?: ");
                newItem.name = Console.ReadLine();
                newItem.name ??= "Item " + ID;
                Console.Write("How many units of this item are being initially stored?: ");
                newItem.amount = Convert.ToInt32(Console.ReadLine());
                Console.Write("What is the max amount of units that can be stored?: ");
                newItem.limit = Convert.ToInt32(Console.ReadLine());
                newItem.itemID = ID;
                option = 0;
                while(option != 1 && option != 2)
                {
                    Console.WriteLine("Do you wish to set a warning amount for the item?\nThis will alert you when the stock for the item drops below this amount.\n\t1. Yes\n\t2. No\n");
                    option = Convert.ToInt32(Console.ReadLine());
                    if(option == 1)
                    {
                        Console.Write("Enter a stock warning threshold for this item: ");
                        newItem.lowStock = Convert.ToInt32(Console.ReadLine());
                    }
                }
                ID++;
                itemList.Add(newItem);
            }
            catch (FormatException e)
            {
                Console.WriteLine("Error: " + e.Message + "\n");
                Console.WriteLine("Please enter a number corresponding to the listed options.\n");
            }
        }

        public static void RemoveItem()
        {
            Console.Write("What is the ID of the item you wish to remove?: ");
            targetID = Convert.ToInt32(Console.ReadLine());
            targetItem = GetItem(targetID);
            Console.WriteLine("Removing " + targetItem.name + " from storage list...");
            foreach (Item item in itemList)
            {
                if (item.itemID == targetItem.itemID)
                {
                    itemList.Remove(item);
                    Console.WriteLine("Success!");
                    break;
                }
            }

        }

        public static void ListItem()
        {
            Console.Write("What is the ID of the item you wish to find?: ");
            targetID = Convert.ToInt32(Console.ReadLine());
            targetItem = GetItem(targetID);
            if (targetItem != null)
            {
                Console.WriteLine("Name: " + targetItem.name + "\nID: " + targetItem.itemID + "\nCurrent Amount: " + targetItem.amount + "\nWarning Threshold: " + targetItem.lowStock + "\nStorage Limit: " + targetItem.limit + "\n");
            }
        }

        public static Item? GetItem(int searchID)
        {
            foreach (Item item in itemList)
            {
                if (item.itemID == searchID)
                {
                    return item;
                }
            }
            Console.WriteLine("Item cannot be found.\n");
            return null;
        }

        public static void ListItems()
        {
            option = 0;
            while (option != 1 && option != 2)
            {
                try
                {
                    Console.WriteLine("Would you like to:\n\t1. List All Items\n\t2. List Low Stock Items\n");
                    option = Convert.ToInt32(Console.ReadLine());
                    if (option == 1)
                    {
                        foreach (Item item in itemList)
                        {
                            Console.WriteLine("Name: " + item.name + "\nID: " + item.itemID + "\nCurrent Amount: " + item.amount + "\nWarning Threshold: " + item.lowStock + "\nStorage Limit: " + item.limit + "\n");
                        }
                    }
                    else
                    {
                        foreach(Item item in itemList)
                        {
                            if(item.amount < item.lowStock)
                            {
                                Console.WriteLine("Name: " + item.name + "\nID: " + item.itemID + "\nCurrent Amount: " + item.amount + "\nWarning Threshold: " + item.lowStock + "\nStorage Limit: " + item.limit + "\n");
                            }
                        }
                    }
                    if (option < 1 || option > 2)
                    {
                        Console.WriteLine("Please enter a number corresponding to the listed options.\n");
                    }
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Please enter a number corresponding to the listed options.");
                }
            }
        }

        public static List<Item> GetInventory()
        {
            return itemList;
        }
    }
}
