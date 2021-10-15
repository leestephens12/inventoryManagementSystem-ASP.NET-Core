using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lee_Stephens_Assignment1_COMP2084.Models
{
    public class InventoryItem
    {
        public int InventoryItemId { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public bool InStock { get; set; }
        public int ItemId { get; set; } // Foreign Key

        //parent refference
        public Item Item { get; set; }
    }
}
