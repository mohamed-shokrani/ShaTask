using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel;

public class InvoiceBasics
{
    public int BranchId { get; set; }

    public long InvoiceID { get; set; }

    [Required]
    [StringLength(200)]
    [DisplayName("إسم العميل *")]

    public string CustomerName { get; set; }
    [DisplayName("الفرع *")]
    public string BranchName { get; set; }
  


    [DataType(DataType.Date)]
    [DisplayName(" تاريخ الفاتورة  *")]

    public DateTime? InvoiceDate { get; set; }

    [Range(1, int.MaxValue)]
    public int? CashierID { get; set; }

    [Range(1, int.MaxValue)]
    public int? BranchID { get; set; }

    [DataType(DataType.Currency)]
    public decimal? TotalPrice { get; set; }
    [Required]
    [StringLength(200)]
    [DisplayName("  إسم المنتج * ")]

    public string ItemName { get; set; }

    [Range(0.01, double.MaxValue)]
    [DisplayName("الكمية *")]
    public double ItemCount { get; set; }
    [DisplayName(" السعر *")]


    [DataType(DataType.Currency)]
    public double ItemPrice { get; set; }
    public List<SelectListItem> BranchList { get; set; } = new List<SelectListItem>();
}
