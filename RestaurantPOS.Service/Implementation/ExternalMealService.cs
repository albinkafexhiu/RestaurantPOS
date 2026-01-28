using System.Text.Json;
using RestaurantPOS.Domain.Dto;
using RestaurantPOS.Service.Interfaces;

namespace RestaurantPOS.Service.Implementation
{
    public class ExternalMealService : IExternalMealService
    {
        private readonly HttpClient _httpClient;

        public ExternalMealService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress ??= new Uri("https://www.themealdb.com/");
        }

        public async Task<List<ExternalMealDto>> GetRandomMealsAsync(int count = 5)
        {
            var result = new List<ExternalMealDto>();

            for (int i = 0; i < count; i++)
            {
                var response = await _httpClient.GetAsync("api/json/v1/1/random.php");
                if (!response.IsSuccessStatusCode)
                {
                    continue;
                }

                var json = await response.Content.ReadAsStringAsync();

                var data = JsonSerializer.Deserialize<TheMealDbResponse>(json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                var meal = data?.Meals?.FirstOrDefault();
                if (meal == null)
                {
                    continue;
                }

                var ingredients = ExtractIngredients(meal);

                var dto = new ExternalMealDto
                {
                    Name = meal.strMeal ?? string.Empty,
                    Category = meal.strCategory ?? string.Empty,
                    Area = meal.strArea ?? string.Empty,
                    ShortInstructions = BuildShortInstructions(meal.strInstructions),
                    Ingredients = ingredients
                };

                result.Add(dto);
            }

            return result;
        }

        private static List<string> ExtractIngredients(MealItem meal)
        {
            var list = new List<string?>()
            {
                meal.strIngredient1, meal.strIngredient2, meal.strIngredient3,
                meal.strIngredient4, meal.strIngredient5, meal.strIngredient6,
                meal.strIngredient7, meal.strIngredient8, meal.strIngredient9,
                meal.strIngredient10, meal.strIngredient11, meal.strIngredient12,
                meal.strIngredient13, meal.strIngredient14, meal.strIngredient15,
                meal.strIngredient16, meal.strIngredient17, meal.strIngredient18,
                meal.strIngredient19, meal.strIngredient20
            };

            return list
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x!.Trim())
                .ToList();
        }

        private static string BuildShortInstructions(string? full)
        {
            if (string.IsNullOrWhiteSpace(full))
                return string.Empty;

            var trimmed = full.Trim();
            if (trimmed.Length <= 180)
                return trimmed;

            return trimmed.Substring(0, 180) + "...";
        }

        // internal classes for JSON mapping (service only)
        private class TheMealDbResponse
        {
            public List<MealItem>? Meals { get; set; }
        }

        private class MealItem
        {
            public string? strMeal { get; set; }
            public string? strCategory { get; set; }
            public string? strArea { get; set; }
            public string? strInstructions { get; set; }

            public string? strIngredient1 { get; set; }
            public string? strIngredient2 { get; set; }
            public string? strIngredient3 { get; set; }
            public string? strIngredient4 { get; set; }
            public string? strIngredient5 { get; set; }
            public string? strIngredient6 { get; set; }
            public string? strIngredient7 { get; set; }
            public string? strIngredient8 { get; set; }
            public string? strIngredient9 { get; set; }
            public string? strIngredient10 { get; set; }
            public string? strIngredient11 { get; set; }
            public string? strIngredient12 { get; set; }
            public string? strIngredient13 { get; set; }
            public string? strIngredient14 { get; set; }
            public string? strIngredient15 { get; set; }
            public string? strIngredient16 { get; set; }
            public string? strIngredient17 { get; set; }
            public string? strIngredient18 { get; set; }
            public string? strIngredient19 { get; set; }
            public string? strIngredient20 { get; set; }
        }
    }
}
