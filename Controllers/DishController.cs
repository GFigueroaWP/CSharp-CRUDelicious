using Microsoft.AspNetCore.Mvc;
using CRUDelicious.Models;

namespace CRUDelicious.Controllers;

public class DishController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private MyContext _context;

    public DishController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }
    [HttpGet("dishes/new")]
    public IActionResult New()
    {
        return View();
    }
    [HttpPost("dishes/create")]
    public IActionResult Create(Dish dish)
    {
        if (ModelState.IsValid)
        {
            _context.Add(dish);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        else
        {
            return View("New");
        }
    }
    [HttpGet("dishes/{dishId}")]
    public IActionResult Show(int dishId)
    {
        Dish? dish = _context.Dishes.FirstOrDefault(d => d.DishId == dishId);
        return View(dish);
    }

    [HttpPost("dishes/{DishId}/destroy")]
    public IActionResult DestroyDish(int DishId)
    {
        Dish? dish = _context.Dishes.FirstOrDefault(d => d.DishId == DishId);
        _context.Remove(dish);
        _context.SaveChanges();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet("dishes/{DishId}/edit")]
    public IActionResult Edit(int DishId)
    {
        Dish? dish = _context.Dishes.FirstOrDefault(d => d.DishId == DishId);
        return View(dish);
    }

    [HttpPost("dishes/{DishId}/update")]
    public IActionResult Update(int DishId, Dish dish)
    {
        Dish? dishToUpdate = _context.Dishes.FirstOrDefault(d => d.DishId == DishId);
        dishToUpdate.Name = dish.Name;
        dishToUpdate.Chef = dish.Chef;
        dishToUpdate.Calories = dish.Calories;
        dishToUpdate.Tastiness = dish.Tastiness;
        dishToUpdate.Description = dish.Description;
        dishToUpdate.UpdatedAt = DateTime.Now;
        _context.SaveChanges();
        return RedirectToAction("Index", "Home");
    }
}
