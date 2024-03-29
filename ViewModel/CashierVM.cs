﻿using System.ComponentModel.DataAnnotations;

namespace ViewModel;
public class CashierVM
{
    public int CashierID { get; set; }

    [Required(ErrorMessage = "Cashier name is required")]
    [Display(Name = "Cashier Name")]
    public string CashierName { get; set; }
    public string? BranchName { get; set; }


    [Display(Name = "Branch ID")]
    public int BranchID { get; set; } 
    public IEnumerable<BranchVM> Branches { get; set; }
    public bool? Selected { get; set; }
}
