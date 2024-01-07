using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel;

public class InvoiceItemVM
{
    [Required]
    [StringLength(200)]
    public string ItemName { get; set; }

    [Range(0.01, double.MaxValue)]
    public double ItemCount { get; set; }

    [DataType(DataType.Currency)]
    public double ItemPrice { get; set; }
}
