using ShaTask.Models;
using ViewModel;

namespace ShaTask.Interfaces;
public interface IInvoiceRepository :IGenericRepository<InvoiceDetail>
{
    Task<decimal> CalculateTotalPrice(long invoiceHeaderID);
    Task<List<InvoiceVM>> GetAllInvoices();
}
