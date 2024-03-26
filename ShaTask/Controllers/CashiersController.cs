using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShaTask.Interfaces;
using ShaTask.Models;
using ViewModel;

namespace ShaTask.Controllers;

[Route("cashiers")]
public class CashiersController(IUnitOfWork unitOfWork) : Controller
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var list = await _unitOfWork.Cashiers.GetAll(x => x.IsActive,b=>b.Branch)
                                .Select(x => new CashierVM
                                {
                                    CashierID = x.Id,
                                    BranchName = x.Branch.BranchName,
                                    CashierName = x.CashierName,
                                }).ToListAsync();
        return View(list);
    }
    [HttpGet("create")]
    public async Task<IActionResult> Create()
    {
        var vm = new CashierUpdateAndCreateVM();
        vm.Branches = await _unitOfWork.InvoiceDetails.BranchSelectList();

        return View(vm);
    }
    [HttpPost("create")]
    public async Task<IActionResult> Create(CashierUpdateAndCreateVM vM)
    {
       

        vM.Branches = await _unitOfWork.InvoiceDetails.BranchSelectList();
        if (ModelState.IsValid)
        {

            var model = new Cashier
            {
                BranchId = vM.BranchID,
                CashierName = vM.CashierName
            };
            await _unitOfWork.Cashiers.CreateAsync(model);
            await _unitOfWork.Complete();
            return RedirectToAction("Index");

        }
        return View(vM);
    }
    [HttpGet("update/{id}")]
    public async Task<IActionResult> Update(int id)
    {
        var cashier = await _unitOfWork.Cashiers.GetByIdAsync(id);
        if (cashier == null) return RedirectToAction("Index");
        var vm = new CashierUpdateAndCreateVM
        {
            ID = id,
            CashierName = cashier.CashierName,
            BranchID = cashier.BranchId,

        };
        vm.Branches = await _unitOfWork.InvoiceDetails.BranchSelectList();
        return View(vm);
    }
    [HttpPost("update/{id}")]
    public async Task<IActionResult> Update(int id, CashierUpdateAndCreateVM model)
    {
        model.Branches = await _unitOfWork.InvoiceDetails.BranchSelectList();

        if (ModelState.IsValid)
        {
            var cashier = await _unitOfWork.Cashiers.GetByIdAsync(id);
            cashier.CashierName = model.CashierName;
            cashier.BranchId = model.BranchID;

            await _unitOfWork.Cashiers.UpdateAsync(cashier);
            await _unitOfWork.Complete();
            return RedirectToAction("Index");
        }
        return View(model);
    }
    [HttpPost ("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var cashierToDelete = await _unitOfWork.Cashiers.GetByIdAsync(id);

            if (cashierToDelete == null)
            {
                return NotFound(); 
            }

            // Delete the Cashier
            await _unitOfWork.Cashiers.DeleteAsync(x => x.Id == id);
            await _unitOfWork.Complete();

            return Json(new { success = true });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, error = ex.Message });
        }

    }
    [HttpPost("dactivateCashier/{id}")]
    public async Task<IActionResult> DeActivateCashier(int id)
    {
        var deactivateCashier = await _unitOfWork.Cashiers.GetByIdAsync(id);

        if (deactivateCashier != null)
        {

            deactivateCashier.IsActive = false ;
          

            await _unitOfWork.Cashiers.UpdateAsync(deactivateCashier);
            await _unitOfWork.Complete();
            return Json(new { success = true });
            
        }
        return Json(new { success = false });

    }
}
