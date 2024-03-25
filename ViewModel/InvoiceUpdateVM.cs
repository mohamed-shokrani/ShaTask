namespace ViewModel;

public class InvoiceUpdateVM
{
    public IEnumerable<InvoiceItemVM> InvoiceDetails { get; set; } = new List<InvoiceItemVM>();
    public IEnumerable<BranchVM> BranchList { get; set; }= new List<BranchVM>();
    public string CustomerName { get; set; }
    public string BranchName { get; set; }
    public int BranchID { get; set; }
    public long Id { get; set; }

}
