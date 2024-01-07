using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ViewModel;
public class InvoiceVM
{

    public long InvoiceID { get; set; }

    [Required]
    [StringLength(200)]
    public string CustomerName { get; set; }
    public string BranchName { get; set; }
    public string CityName { get; set; }
    public string CashierName{ get; set; }


    [DataType(DataType.Date)]
    public DateTime Invoicedate { get; set; }

    [Range(1, int.MaxValue)]
    public int? CashierID { get; set; }

    [Range(1, int.MaxValue)]
    public int? BranchID { get; set; }

    [DataType(DataType.Currency)]
    public decimal? TotalPrice { get; set; }
    public List<InvoiceItemVM> Items { get; set; }
    public List<SelectListItem> BranchList { get; set; } = new List<SelectListItem>();

}
