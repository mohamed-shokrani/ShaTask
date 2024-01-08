using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
        vm.BranchList =await _unitOfWork.InvoiceDetails.BranchSelectList();

        return View(vm);
    }
    [HttpPost]
    public async Task<IActionResult> Create(InvoiceVM vm, string invoiceDetailsJson)
    {
        vm.BranchList = await _unitOfWork.InvoiceDetails.BranchSelectList();
        if (!string.IsNullOrEmpty( vm.CustomerName) &&vm.BranchId >0&& invoiceDetailsJson != "[]")
        {
            var invoiceDetails = JsonConvert.DeserializeObject<List<InvoiceItemVM>>(invoiceDetailsJson);
            var invoice = new InvoiceHeader
            {
                BranchId = vm.BranchId,
                CustomerName = vm.CustomerName,
                InvoiceDetails = invoiceDetails.Select(x => new InvoiceDetail
                                                {
                                                    ItemCount = x.ItemCount,
                                                    ItemName = x.ItemName,
                                                    ItemPrice = x.ItemPrice
                                                }).ToList(),
            };
            await _unitOfWork.InvoiceHeaders.CreateAsync(invoice);
            await _unitOfWork.Complete();


            return RedirectToAction("Index");
        }
        return NotFound();
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var vm = new InvoiceVM();
        try
        {
            await _unitOfWork.InvoiceHeaders.Update(x => x.Id == id, l => l.SetProperty(x => x.IsDeleted, true));
            return Json("تم الحذف بنجاح");
        }
        catch (Exception)
        {
            return Json("عفوا حدث خطا");

        }


    }
}
