using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShaTask.Data;
using ShaTask.Models;
using ShaTask.Repository;
using ViewModel;

namespace ShaTask.Interfaces
{
    public class InvoiceRepository : GenericRepository<InvoiceDetail>, IInvoiceRepository
    {
        private readonly AppDbContext _context;

        public InvoiceRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<InvoiceVM>> GetAllInvoices()
        {
            var list = await (from i in _context.InvoiceDetails
                              join ih in _context.InvoiceHeaders on i.InvoiceHeaderId equals ih.Id
                              join c in _context.Cashiers on ih.CashierId equals c.Id into cashierJoin
                              from cashier in cashierJoin.DefaultIfEmpty() 
                              join b in _context.Branches on ih.BranchId equals b.Id
                              join city in _context.Cities on b.CityId equals city.Id
                              where !ih.IsDeleted
                              select new { i, ih, cashier, b, city }
                                       ).GroupBy(x => x.ih.Id)
                                       .Select(group => new InvoiceVM
                                       {
                                           InvoiceID = group.Key,
                                           CashierName = group.Select(x => x.cashier.CashierName).FirstOrDefault(),
                                           BranchName = group.Select(x => x.b.BranchName).FirstOrDefault(),
                                           CustomerName = group.Select(x => x.ih.CustomerName).FirstOrDefault(),
                                           CityName = group.Select(x => x.city.CityName).FirstOrDefault(),
                                           InvoiceDate = group.Select(x => x.ih.Invoicedate).FirstOrDefault(),
                                           TotalPrice = group.Sum(x => (decimal)(x.i.ItemCount * x.i.ItemPrice)),
                                           InvoiceDetails = group.Select(g => new InvoiceItemVM
                                           {
                                               ItemName = g.i.ItemName,
                                               ItemCount = g.i.ItemCount,
                                               ItemPrice = g.i.ItemPrice
                                           }).ToList()
                                       }).AsNoTracking()
                                         .ToListAsync() ?? new List<InvoiceVM>();

            return list;
        }
        public async Task<List<SelectListItem>> BranchSelectList()
        {
            return await _context.Branches.Select(x => new SelectListItem
            {
                Text = x.BranchName,
                Value = x.Id.ToString(),
            }).ToListAsync();
        }
    }
}
