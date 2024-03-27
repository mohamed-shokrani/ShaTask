using System.ComponentModel.DataAnnotations;

namespace ViewModel;

public class InvoiceUpdateVM
{
    public IEnumerable<InvoiceItemVM> InvoiceDetails { get; set; } = new List<InvoiceItemVM>();
    public IEnumerable<BranchVM> BranchList { get; set; }= new List<BranchVM>();
    public IEnumerable<CashierVM > CashierList { get; set; } = new List<CashierVM>();

    [Required (ErrorMessage = "اسم العميل مطلوب"), Display(Name = "اسم العميل")]
    public string CustomerName { get; set; }

    [Required, Display(Name = "اسم BranchName")]
    public string BranchName { get; set; }
    public DateTime InvoiceDate { get; set; }
    public int BranchID { get; set; }
    public int CashierID {  get; set; }
    public long Id { get; set; }

}
