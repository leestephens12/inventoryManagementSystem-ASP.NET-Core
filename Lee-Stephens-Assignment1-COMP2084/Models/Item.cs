using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lee_Stephens_Assignment1_COMP2084.Models
{
    public class Item
    {
        public int ItemId { get; set; }

        [Required]
        [MaxLength(25)]
        [Display(Name = "Department")]
        public string Section { get; set; }

        [Required]
        [MaxLength(20)]
        public string Brand { get; set; }

        [Required]
        [MaxLength(35)]
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        //child refferences
        public List<InventoryItem> InventoryItems { get; set; }
    }
}
