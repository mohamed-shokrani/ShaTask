using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ViewModel;
public class InvoiceVM
{
    [Required]
    public int BranchId { get; set; }

    public long InvoiceID { get; set; }

    [Required]
    [StringLength(200)]
    [DisplayName("إسم العميل *")]

    public string CustomerName { get; set; }
    [DisplayName("الفرع *")]
    public string BranchName { get; set; }
    [DisplayName("* المدينة *")]
    [Required]

    public string CityName { get; set; }
    [DisplayName("  إسم الكاشير *")]

    public string CashierName{ get; set; }


    [DataType(DataType.Date)]
    [DisplayName(" تاريخ الفاتورة  *")]

    public DateTime InvoiceDate { get; set; }

    [Range(1, int.MaxValue)]
    public int? CashierID { get; set; }

    [Range(1, int.MaxValue)]
    public int? BranchID { get; set; }

    [DataType(DataType.Currency)]
    public decimal? TotalPrice { get; set; }
    public List<InvoiceItemVM> InvoiceDetails { get; set; }
    public List<SelectListItem> BranchList { get; set; } = new List<SelectListItem>();

}
