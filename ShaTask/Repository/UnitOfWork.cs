using ShaTask.Data;
using ShaTask.Interfaces;
using ShaTask.Models;

namespace ShaTask.Repository;
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public IInvoiceRepository InvoiceDetails {  get;private set; }

    public IGenericRepository<InvoiceHeader> InvoiceHeaders { get; private set; }
    public IGenericRepository<Cashier>  Cashiers { get; private set; }
    public IGenericRepository<Branch> Branchs { get; private set; }
    public IGenericRepository<City> Cities { get; private set; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        InvoiceDetails = new InvoiceRepository(_context);
        InvoiceHeaders = new GenericRepository<InvoiceHeader>(_context);
        Cashiers = new GenericRepository<Cashier>(_context);
        Branchs = new GenericRepository<Branch>(_context);
        Cities = new GenericRepository<City>(_context);

    }

    public async Task<int> Complete()
    {
      return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
       _context.Dispose();
    }
}
