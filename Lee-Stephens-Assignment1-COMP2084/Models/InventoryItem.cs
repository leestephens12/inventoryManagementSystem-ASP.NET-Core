using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lee_Stephens_Assignment1_COMP2084.Models
{
    public class InventoryItem
    {
        //All table items for InventoryItem
        public int InventoryItemId { get; set; }

        //Data annotations for validation
        [Required]
        [MaxLength(30)]
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        //Data annotations for validation
        [Required]
        [Range(1, 100)]
        public int Quantity { get; set; }

        //Data annotations for validation
        [Display(Name = "In Stock")]
        public bool InStock { get; set; }

        [Required]
        [Display(Name = "Store Location")]
        public string StoreLocation { get; set; }

        //Data annotations for validation
        [Display(Name = "Department")]
        public int ItemId { get; set; } // Foreign Key

        //parent refference
        public Item Item { get; set; }
    }
}
