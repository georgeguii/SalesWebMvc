using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;

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

}
