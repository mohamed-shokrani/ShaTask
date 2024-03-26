using System.ComponentModel.DataAnnotations;

namespace ViewModel;
public class CashierUpdateAndCreateVM
{
    public int? ID { get; set; }

    [Required(ErrorMessage = "إسم الكاشير مطلوب"),MinLength(2,ErrorMessage ="اسم الكاشير مكون من حرفين على الاقل")]
    [Display(Name = "إسم الكاشير")]
    public string CashierName { get; set; }
    [Display(Name = "الفرع")]

    public string? BranchName { get; set; }

    [Display(Name = "الفرع")]
    [Required(ErrorMessage = "إسم الفرع مطلوب")]

    public int BranchID { get; set; }
    public IEnumerable<BranchVM>? Branches { get; set; } 
    public bool CashierExist { get; set; }
}
