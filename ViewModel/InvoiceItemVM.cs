using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel;

public class InvoiceItemVM
{
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
}
