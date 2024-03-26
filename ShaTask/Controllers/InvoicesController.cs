using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ShaTask.Interfaces;
using ShaTask.Models;
using ViewModel;

namespace ShaTask.Controllers;
[Route("invoices")]
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
    [Route("index1")]
    public async Task<IActionResult> Index1()
    {
        var invoices = await _unitOfWork.InvoiceDetails.GetAllInvoices();

        return View(invoices);
    }
    [Route("create")]

    public async Task<IActionResult> Create()
    {
        var vm = new InvoiceVM();
        vm.BranchList =await _unitOfWork.InvoiceDetails.BranchSelectList();
        var list = await _unitOfWork.Cashiers.GetAllAsync();
        vm.CashierList = list.Select(x => new CashierVM
        {
            CashierID = x.Id,
            CashierName = x.CashierName,
        });
        return View(vm);
    }
    [HttpPost("create")]
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
    [HttpGet("update/{id}")]

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
    [HttpPost("updateInvoice")]

    public async Task<IActionResult> Update(InvoiceUpdateVM vm, string invoiceDetailsJson)
    {
        var invoice = await _unitOfWork.InvoiceHeaders.GetByIdAsyncWithInclude(x => x.Id == vm.Id, i => i.InvoiceDetails);

        if (invoice != null)
        {
            invoice.BranchId = vm.BranchID;
            invoice.CustomerName = vm.CustomerName;

            var updatedDetails = JsonConvert.DeserializeObject<List<InvoiceItemVM>>(invoiceDetailsJson);

            ManageInvoiceDetails(invoice, updatedDetails);

            await _unitOfWork.Complete();

            return RedirectToAction("index");
        }

        return View(vm);
    }


    [HttpPost("delete/{id}")]
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
    private void ManageInvoiceDetails(InvoiceHeader invoice, List<InvoiceItemVM> updatedDetails)
    {
        var existingDetails = invoice.InvoiceDetails.ToList(); // ToList to avoid modifying collection while iterating
        var detailsToAdd = updatedDetails.Where(ud => !existingDetails.Any(ed => ed.Id == ud.Id)).ToList();
        var detailsToRemove = existingDetails.Where(ed => !updatedDetails.Any(ud => ud.Id == ed.Id)).ToList();
        var detailsToUpdate = existingDetails.Where(ed => updatedDetails.Any(ud => ud.Id == ed.Id)).ToList();

        foreach (var detailToAdd in detailsToAdd)
        {
            invoice.InvoiceDetails.Add(new InvoiceDetail
            {
                ItemName = detailToAdd.ItemName,
                ItemCount = detailToAdd.ItemCount,
                ItemPrice = detailToAdd.ItemPrice
            });
        }

        foreach (var detailToUpdate in detailsToUpdate)
        {
            var updatedDetail = updatedDetails.First(ud => ud.Id == detailToUpdate.Id);
            detailToUpdate.ItemName = updatedDetail.ItemName;
            detailToUpdate.ItemCount = updatedDetail.ItemCount;
            detailToUpdate.ItemPrice = updatedDetail.ItemPrice;
        }

        foreach (var detailToRemove in detailsToRemove)
        {
            _unitOfWork.InvoiceDetails.Delete(detailToRemove);
        }
    }
}
