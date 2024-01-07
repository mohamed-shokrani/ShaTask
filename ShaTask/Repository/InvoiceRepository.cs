using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShaTask.Data;
using ShaTask.Models;
using ShaTask.Repository;
using ViewModel;

namespace ShaTask.Interfaces
{
    public class InvoiceRepository : GenericRepository<InvoiceDetail> , IInvoiceRepository
    {
        private readonly AppDbContext _context;

        public InvoiceRepository(AppDbContext context) :base(context) {
            _context = context;
        }
      
        public async Task<decimal> CalculateTotalPrice(long invoiceHeaderID)
        {
            var invoiceHeader = await _context.InvoiceHeaders.FindAsync(invoiceHeaderID);
            if (invoiceHeader == null) return 0;

            decimal totalPrice = invoiceHeader.InvoiceDetails.Sum(detail => (decimal)(detail.ItemCount * detail.ItemPrice));
            return totalPrice;
        }
        
        public async Task<List<InvoiceVM>> GetAllInvoices()
        {
            List<SelectListItem>
            var list = await (from i in _context.InvoiceDetails
                                  join ih in _context.InvoiceHeaders on i.InvoiceHeaderId equals ih.Id
                                  join c in _context.Cashiers on ih.CashierId equals c.Id
                                  join b in _context.Branches on ih.BranchId equals b.Id
                                  join city in _context.Cities on b.CityId equals city.Id
                                  select new { i, ih, c, b, city }
                                       ).GroupBy(x => x.ih.Id)
                                       .Select(group => new InvoiceVM
                                       {
                                           InvoiceID = group.Key,
                                           CashierName = group.Select(x => x.c.CashierName).FirstOrDefault(),
                                           BranchName = group.Select(x => x.b.BranchName).FirstOrDefault(),
                                           CustomerName = group.Select(x => x.ih.CustomerName).FirstOrDefault(),
                                           CityName = group.Select(x => x.city.CityName).FirstOrDefault(),
                                           Invoicedate = group.Select(x => x.ih.Invoicedate).FirstOrDefault(),
                                           TotalPrice = group.Sum(x => (decimal)(x.i.ItemCount * x.i.ItemPrice)),
                                           Items = group.Select(g => new InvoiceItemVM
                                           {
                                               ItemName = g.i.ItemName,
                                               ItemCount = g.i.ItemCount,
                                               ItemPrice = g.i.ItemPrice
                                           }).ToList()
                                       }).AsNoTracking()
                                         .ToListAsync() ?? new List<InvoiceVM>();

            return list;
        }


    }
}
