using ShaTask.Models;

namespace ShaTask.Interfaces;

public interface IUnitOfWork :IDisposable
{
    public IInvoiceRepository InvoiceDetails { get; }
    public IGenericRepository<InvoiceHeader> InvoiceHeaders { get; }
    public IGenericRepository<Cashier> Cashiers { get; }
    public IGenericRepository<Branch> Branchs { get;}
    public IGenericRepository<City> Cities { get; }
    Task<int> Complete();
}
