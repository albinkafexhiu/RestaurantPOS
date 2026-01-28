using Microsoft.AspNetCore.Mvc;
using RestaurantPOS.Domain.Dto;
using RestaurantPOS.Service.Interfaces;

namespace RestaurantPOS.Web.Controllers
{
    public class MealsController : Controller
    {
        private readonly IExternalMealService _externalMealService;

        public MealsController(IExternalMealService externalMealService)
        {
            _externalMealService = externalMealService;
        }

        public async Task<IActionResult> Index()
        {
            List<ExternalMealDto> meals = await _externalMealService.GetRandomMealsAsync(5);
            return View(meals);
        }
    }
}