using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ViewModel;
public class InvoiceVM
{
    [Required]
    public int BranchID { get; set; }

    public long InvoiceID { get; set; }

    [Required]
    [StringLength(200)]
    [DisplayName("Customer Name *")]

    public string CustomerName { get; set; }
    [DisplayName("الفرع *")]
    public string BranchName { get; set; }
    [DisplayName("* City Name *")]
    [Required]

    public string CityName { get; set; }
    [DisplayName("   Cashier Name*")]

    public string CashierName{ get; set; }
    public string? ItemName { get; set; }
    public decimal? ItemPrice { get; set; }
    public int? ItemQuantity { get; set; }


    [DataType(DataType.Date)]
    [DisplayName("  Invoice Date  *")]

    public DateTime InvoiceDate { get; set; }

    [Range(1, int.MaxValue)]
    public int? CashierID { get; set; }

    [Range(1, int.MaxValue)]

    [DataType(DataType.Currency)]
    public decimal? TotalPrice { get; set; }
    public IEnumerable<InvoiceItemVM> InvoiceDetails { get; set; }
    public IEnumerable<BranchVM> BranchList { get; set; }
    public IEnumerable<CashierVM>? CashierList { get; set; }

}
