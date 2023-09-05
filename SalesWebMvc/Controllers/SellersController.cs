﻿using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers;
public class SellersController : Controller
{
    private readonly SellerService _sellerService;

    public SellersController(SellerService sellerService)
    {
        _sellerService = sellerService;
    }

    public async Task<IActionResult> Index()
    {
        var list = await _sellerService.FindAll();
        return View(list);
    }
}
