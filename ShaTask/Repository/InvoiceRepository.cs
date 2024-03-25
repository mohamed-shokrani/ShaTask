using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShaTask.Data;
using ShaTask.Models;
using ShaTask.Repository;
using System.Security.Policy;
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

        public async Task<IReadOnlyCollection<InvoiceVM>> GetAllInvoices()
        {
            var list =     await  ( from i in _context.InvoiceDetails 
                              join ih in  _context.InvoiceHeaders on i.InvoiceHeaderId equals ih.Id 
                              join cash in  _context.Cashiers on ih.CashierId equals cash.Id into cashierJoin
                              from cash in cashierJoin.DefaultIfEmpty()
                              join b in _context.Branches on ih.BranchId equals b.Id
                              join c in _context.Cities on b.CityId equals c.Id
                              where !ih.IsDeleted
                              select new {i,ih, cash,c, b}
                              ).GroupBy(x=>x.ih.Id)
                              .Select(group => new InvoiceVM
                              {  
                                  CustomerName = group.Select(invoice =>invoice.ih.CustomerName).FirstOrDefault(),
                                  InvoiceID = group.Key,
                                  CashierName = group.Select(x=>x.cash.CashierName).FirstOrDefault(),
                                  BranchName = group.Select(x=>x.b.BranchName).FirstOrDefault(),
                                  CityName = group.Select(x=>x.c.CityName).FirstOrDefault(),
                                  InvoiceDate = group.Select(x=>x.ih.Invoicedate).First(),
                                  
                                  TotalPrice = group.Sum(x=>(decimal)( x.i.ItemCount * x.i.ItemPrice)),
                                  InvoiceDetails  = group.Select(x => new InvoiceItemVM
                                  {
                                      ItemPrice = x.i.ItemPrice,
                                      ItemCount = x.i.ItemCount,
                                      ItemName = x.i.ItemName,
                                  }).ToList()
                              }).AsNoTracking().ToListAsync() ??[];
                              
          

            return list;
        }
        public async Task<IEnumerable<BranchVM>> BranchSelectList(int BranchID =0)
        {
            return await _context.Branches.Select(x => new BranchVM
            {
                BranchName = x.BranchName,
                BranchID = x.Id,
                Selected = BranchID > 0 && x.Id == BranchID
            }).ToListAsync() ?? [];
        }
    }
}
