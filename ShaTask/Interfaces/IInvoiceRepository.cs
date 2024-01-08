using Microsoft.AspNetCore.Mvc.Rendering;
using ShaTask.Models;
using ViewModel;

namespace ShaTask.Interfaces;
public interface IInvoiceRepository :IGenericRepository<InvoiceDetail>
{
    Task<List<InvoiceVM>> GetAllInvoices();
    Task<List<SelectListItem>> BranchSelectList();
}
