using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ShaTask.Interfaces;
using ShaTask.Models;
using ViewModel;

namespace ShaTask.Controllers
{
    public class CashiersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CashiersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _unitOfWork.Cashiers.GetAll(x => x.IsActive,b=>b.Branch)
                                    .Select(x => new CashierVM
                                    {
                                        ID = x.Id,
                                        BranchName = x.Branch.BranchName,
                                        CashierName = x.CashierName,
                                    }).ToListAsync();
            return View(list);
        }
        public async Task<IActionResult> Create()
        {
            var vm = new CashierUpdateAndCreateVM();
            vm.Branches = await _unitOfWork.InvoiceDetails.BranchSelectList();

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CashierUpdateAndCreateVM vM)
        {
            var cashierExists = await _unitOfWork.Cashiers.CheckEntityExistsAsync<Cashier>(x => x.CashierName == vM.CashierName);

            if (cashierExists)
            {
                ModelState.AddModelError(string.Empty, "The cashier already exists.");
                vM.Branches = await _unitOfWork.InvoiceDetails.BranchSelectList();
                return View(vM);
            }

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
        public async Task<IActionResult> Edit(int id)
        {
            var cashier = await _unitOfWork.Cashiers.GetByIdAsync(id);
            if (cashier == null) return RedirectToAction("Index");
            var vm = new CashierVM
            {
                ID = id,
                CashierName = cashier.CashierName,
                BranchID = cashier.BranchId,

            };
            vm.Branches = await _unitOfWork.InvoiceDetails.BranchSelectList();
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CashierVM vM)
        {
            vM.Branches = await _unitOfWork.InvoiceDetails.BranchSelectList();

            if (ModelState.IsValid)
            {
                var cashier = await _unitOfWork.Cashiers.GetByIdAsync(vM.ID);
                cashier.CashierName = vM.CashierName;
                cashier.BranchId = vM.BranchID;

                await _unitOfWork.Cashiers.UpdateAsync(cashier);
                await _unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            return View(vM);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var cashierToDelete = await _unitOfWork.Cashiers.GetByIdAsync(id);

                if (cashierToDelete == null)
                {
                    return NotFound(); // Or handle not found scenario
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
        [HttpPost]
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
}
