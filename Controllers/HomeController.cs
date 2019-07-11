using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ChefsAndDishes.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ChefsAndDishes.Controllers
{
    public class HomeController : Controller
    {
        private HomeContext dbContext;

        // here we can "inject" our context service into the constructor copy
        public HomeController(HomeContext context)
        {
            dbContext = context;
        }

        // Calculate the age of the chef
        DateTime now = DateTime.Today;
        private static int CalculateAge(DateTime dateOfBirth)
        {
            int age = DateTime.Now.Year - dateOfBirth.Year;
            if(DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
            {
                age -= 1;
            }
            return age;
        }

        // Index displays All Chefs
        [HttpGet("")]
        public IActionResult Index()
        {
            List<Chef> AllChefs = dbContext.Chefs
            .Include(chef => chef.CreatedDishes)
            .ToList();
            IEnumerable<Chef> OrderedChefs = AllChefs.OrderByDescending(chef => chef.CreatedAt);
            ViewBag.AllChefs = OrderedChefs;
            return View("Index");
        }

        // Dishes displays All Dishes
        [HttpGet("dishes")]
        public IActionResult Dishes()
        {
            List<Dish> AllDishes = dbContext.Dishes
            .Include(dish => dish.Creator)
            .ToList();
            IEnumerable<Dish> OrderedDishes = AllDishes.OrderByDescending(dish => dish.CreatedAt);
            ViewBag.AllDishes = OrderedDishes;
            return View("Dishes");
        }

        // NewChef displays the view for creating new chef
        [HttpGet("new/chef")]
        public IActionResult NewChef()
        {
            return View("NewChef");
        }

        // NewDish displays the view for creating new dish
        [HttpGet("new/dish")]
        public IActionResult NewDish()
        {
            List<Chef> AllChefs = dbContext.Chefs
            .Include(chef => chef.CreatedDishes)
            .ToList();
            IEnumerable<Chef> OrderedChefs = AllChefs.OrderByDescending(chef => chef.CreatedAt);
            ViewBag.AllChefs = OrderedChefs;
            return View("NewDish");
        }

        // This route is the post route for new chef
        [HttpPost("create/chef")]
        public IActionResult CreateChef(Chef newChef)
        {
            if(ModelState.IsValid)
            {
                int age = CalculateAge(newChef.Birthday);
                if (newChef.Birthday > now)
                {
                    ModelState.AddModelError("Birthday", "You can't be born in the future!");
                    return View("NewChef");
                }
                if(age < 18)
                {
                    ModelState.AddModelError("Birthday", "Must be at least 18!");
                    return View("NewChef");
                }
                newChef.Age = age;
                dbContext.Add(newChef);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("NewChef");
            }
        }

        // This route is the post route for new dish
        [HttpPost("create/dish")]
        public IActionResult CreateDish(Dish newDish)
        {
            if(ModelState.IsValid)
            {
                dbContext.Add(newDish);
                dbContext.SaveChanges();
                return RedirectToAction("Dishes");
            }
            else
            {
                return View("NewDish");
            }
        }

    }
}
