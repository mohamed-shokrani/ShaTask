using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class CashierVM
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Cashier name is required")]
        [Display(Name = "Cashier Name")]
        public string CashierName { get; set; }
        public string? BranchName { get; set; }


        [Display(Name = "Branch ID")]
        public int BranchID { get; set; } 
        public List<SelectListItem> Branches { get; set; }= new List<SelectListItem>();
    }
}
