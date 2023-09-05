using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Controllers;
public class SellersController : Controller
{
    private readonly SellerService _sellerService;
    private readonly DepartmentService _departmentService;

    public SellersController(SellerService sellerService, DepartmentService departmentService)
    {
        _sellerService = sellerService;
        _departmentService = departmentService;
    }

    public async Task<IActionResult> Index()
    {
        var list = await _sellerService.FindAll();
        return View(list);
    }

    public async Task<IActionResult> Create()
    {
        var departments = await _departmentService.FindAll();
        var viewModel = new SellerFormViewModel { Departments = departments };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Seller seller)
    {
        await _sellerService.Insert(seller);
        return RedirectToAction(nameof(Index));
    }


    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var obj = await _sellerService.FindById(id.Value);
        if (obj == null)
        {
            return NotFound();
        }

        return View(obj);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var obj = await _sellerService.FindById(id.Value);
        if (obj == null)
        {
            return NotFound();
        }

        return View(obj);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _sellerService.Remove(id);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var obj = await _sellerService.FindById(id.Value);
        if (obj == null)
        {
            return NotFound();
        }

        var departments = await _departmentService.FindAll();
        SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Seller seller)
    {
        try
        {

            if (id != seller.Id)
            {
                return BadRequest();
            }

            await _sellerService.Update(seller);

            return RedirectToAction(nameof(Index));
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { details = ex.Message });
        }
        catch (DbConcurrencyException ex)
        {
            return BadRequest(new { details = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { details = ex.Message });
        }
    }


}
