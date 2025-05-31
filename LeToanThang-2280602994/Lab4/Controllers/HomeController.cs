using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Lab4.Models;
using Lab4.Repositories;
using Lab4.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;

namespace Lab4.Controllers;

public class HomeController : Controller
{
    private readonly IProductRepository productRepository;
    private IProductRepository _productRepository;

    public HomeController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _productRepository.GetAllAsync();
        return View(products);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
