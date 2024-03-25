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
    [Route("invoices")]
    public async Task< IActionResult> Index()
    {
        var invoices = await _unitOfWork.InvoiceDetails.GetAllInvoices();
                                             
        return View(invoices);
    }
    [Route("invoices/index1")]
    public async Task<IActionResult> Index1()
    {
        var invoices = await _unitOfWork.InvoiceDetails.GetAllInvoices();

        return View(invoices);
    }
    [Route("invoices/create")]

    public async Task<IActionResult> Create()
    {
        var vm = new InvoiceVM();
        vm.BranchList =await _unitOfWork.InvoiceDetails.BranchSelectList();

        return View(vm);
    }
    [HttpPost,Route("invoices/create")]
    public async Task<IActionResult> Create(InvoiceVM vm, string invoiceDetailsJson)
    {
        vm.BranchList = await _unitOfWork.InvoiceDetails.BranchSelectList();
        if (!string.IsNullOrEmpty( vm.CustomerName) &&vm.BranchID >0&& invoiceDetailsJson != "[]")
        {
            var invoiceDetails = JsonConvert.DeserializeObject<List<InvoiceItemVM>>(invoiceDetailsJson);
            var invoice = new InvoiceHeader
            {
                BranchId = vm.BranchID,
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
    [HttpGet("invoices/update/{id}")]

    public async Task<IActionResult> Update(int ID)
    {
        var invoice = await _unitOfWork.InvoiceHeaders.GetByIdAsyncWithInclude(x => x.Id == ID, include => include.InvoiceDetails );
        var bracnhcList = await _unitOfWork.InvoiceDetails.BranchSelectList(invoice.BranchId);
        var check = bracnhcList
                .Where(x => x.BranchID > 0 && x.Selected == true).Select(x => x.BranchID).FirstOrDefault().ToString();
        if (invoice is not null)
        {
            var vm = new InvoiceUpdateVM
            {
                CustomerName = invoice.CustomerName,
                BranchList = bracnhcList,
                BranchID = invoice.BranchId,
                Id = invoice.Id,
                InvoiceDetails = invoice.InvoiceDetails.Select(i => new InvoiceItemVM
                {
                    ItemCount = i.ItemCount,
                    ItemName = i.ItemName,
                    ItemPrice = i.ItemPrice
                })
            };
            return View(vm);
        }



        return NotFound();

    }
    [HttpPost("invoices/updateupdate")]

    public async Task<IActionResult> updateupdate(InvoiceUpdateVM vm, string invoiceDetailsJson)
    {
        var invoice = await _unitOfWork.InvoiceHeaders.GetByIdAsync(vm.Id);
        var invoiceDetails = JsonConvert.DeserializeObject<List<InvoiceItemVM>>(invoiceDetailsJson);
        if (invoice !=null )
        {
            invoice.BranchId = vm.BranchID;
            invoice.CustomerName = vm.CustomerName;
            invoice.InvoiceDetails = invoiceDetails.ToList().Select(x=> new InvoiceDetail
                                                            {
                                                                ItemCount = x.ItemCount,
                                                                ItemName = x.ItemName,
                                                                ItemPrice = x.ItemPrice

                                                            }).ToList();
            await _unitOfWork.InvoiceHeaders.UpdateAsync(invoice);
            await _unitOfWork.Complete();
            return RedirectToAction("index");

        }

        return View(vm);
    }
    [HttpPost("invoices/delete/{id}")]
    public async Task<IActionResult> delete(int id)
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
