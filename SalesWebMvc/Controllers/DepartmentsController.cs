using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;

namespace SalesWebMvc.Controllers;
public class DepartmentsController : Controller
{
    public IActionResult Index()
    {
        List<Departament> departaments = new()
        {
            new Departament { Id = 1, Name = "Eletronics" },
            new Departament { Id = 2, Name = "Fashion" }
        };

        return View(departaments);
    }
}
