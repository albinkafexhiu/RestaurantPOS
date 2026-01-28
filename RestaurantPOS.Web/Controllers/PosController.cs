using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantPOS.Domain.Entities;
using RestaurantPOS.Domain.Enums;
using RestaurantPOS.Service.Interfaces;
using RestaurantPOS.Web.Models;
using RestaurantPOS.Web.Infrastructure;


namespace RestaurantPOS.Web.Controllers
{
    [PosAuthorize]
    public class PosController : Controller
    {
        private readonly ITableService _tableService;
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly IWaiterService _waiterService;

        public PosController(
            ITableService tableService,
            IOrderService orderService,
            IProductService productService,
            IWaiterService waiterService)
        {
            _tableService = tableService;
            _orderService = orderService;
            _productService = productService;
            _waiterService = waiterService;
        }

        // Show tables for POS usage
        public IActionResult Index()
        {
            var tables = _tableService.GetAll();
            return View(tables);
        }

        // Show POS screen for a specific table
        public IActionResult Table(Guid id)
        {
            var table = _tableService.GetById(id);
            if (table == null)
            {
                return NotFound();
            }

            var vm = BuildOrderViewModel(table);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult StartOrder(Guid tableId, Guid waiterId)
        {
            if (waiterId == Guid.Empty)
            {
                return RedirectToAction("Table", new { id = tableId });
            }

            _orderService.OpenOrderForTable(tableId, waiterId);

            return RedirectToAction("Table", new { id = tableId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddItem(Guid tableId, Guid orderId, Guid productId, int quantity)
        {
            if (orderId == Guid.Empty || productId == Guid.Empty || quantity <= 0)
            {
                return RedirectToAction("Table", new { id = tableId });
            }

            _orderService.AddItem(orderId, productId, quantity);

            return RedirectToAction("Table", new { id = tableId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveItem(Guid tableId, Guid orderItemId)
        {
            _orderService.RemoveItem(orderItemId);

            return RedirectToAction("Table", new { id = tableId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CloseOrder(Guid tableId, Guid orderId, PaymentMethod paymentMethod)
        {
            if (orderId != Guid.Empty)
            {
                _orderService.CloseOrder(orderId, paymentMethod);
            }

            return RedirectToAction("Index");
        }

        private OrderDetailsViewModel BuildOrderViewModel(RestaurantTable table)
        {
            var openOrder = _orderService.GetOpenOrderForTable(table.Id);

            var products = _productService.GetAll()
                .Where(p => p.IsAvailable)
                .ToList();

            var waiters = _waiterService.GetAll()
                .Where(w => w.IsActive)
                .ToList();

            var vm = new OrderDetailsViewModel
            {
                TableId = table.Id,
                TableNumber = table.TableNumber,
                Products = products,
                Waiters = waiters
                    .Select(w => new SelectListItem
                    {
                        Value = w.Id.ToString(),
                        Text = w.FullName
                    })
                    .ToList()
            };

            if (openOrder != null)
            {
                vm.OrderId = openOrder.Id;

                var items = _orderService.GetItemsForOrder(openOrder.Id).ToList();

                vm.Items = items
                    .Select(i =>
                    {
                        var product = products.FirstOrDefault(p => p.Id == i.ProductId);
                        return new OrderItemDisplay
                        {
                            OrderItemId = i.Id,
                            ProductName = product?.Name ?? "Unknown",
                            Quantity = i.Quantity,
                            UnitPrice = i.UnitPrice,
                            LineTotal = i.LineTotal
                        };
                    })
                    .ToList();

                vm.Total = vm.Items.Sum(x => x.LineTotal);
            }

            return vm;
        }
    }
}
