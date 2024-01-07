using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShaTask.Interfaces;
using ShaTask.Models;
using ViewModel;

namespace ShaTask.Controllers;
public class InvoicesController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public InvoicesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task< IActionResult> Index()
    {
        var invoices = await _unitOfWork.InvoiceDetails.GetAllInvoices();
                                             
        return View(invoices);
    }
    public async Task<IActionResult> Create()
    {
        var vm = new InvoiceVM();
      

        return View(vm);
    }
}
