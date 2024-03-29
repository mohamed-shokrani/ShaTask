﻿using Microsoft.AspNetCore.Mvc.Rendering;
using ShaTask.Models;
using ViewModel;

namespace ShaTask.Interfaces;
public interface IInvoiceRepository :IGenericRepository<InvoiceDetail>
{
    Task<IReadOnlyCollection<InvoiceVM>> GetAllInvoices();
    Task<IEnumerable<BranchVM>> BranchSelectList(int id =0);
}
