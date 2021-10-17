using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lee_Stephens_Assignment1_COMP2084.Models
{
    public class InventoryItem
    {
        public int InventoryItemId { get; set; }

        [Required]
        [MaxLength(30)]
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Required]
        [Range(0, 100)]
        public int Quantity { get; set; }

        [Display(Name = "In Stock")]
        public bool InStock { get; set; }

        [Display(Name = "Department")]
        public int ItemId { get; set; } // Foreign Key

        //parent refference
        public Item Item { get; set; }
    }
}
