using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orders.Models;

namespace Orders.Controllers
{
    public class OrdersController : Controller
    {
        private readonly OrdersContext _context;
        private readonly Random _random = new Random();

        public OrdersController(OrdersContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Orders.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateOrderViewModel orderVm)
        {
            Order order = new Order();

            if (ModelState.IsValid)
            {
                order.AddressFrom = orderVm.AddressFrom;
                order.AddressTo = orderVm.AddressTo;
                order.Phone = orderVm.Phone;
                order.Price = _random.Next(50, 101);

                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(order);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,AddressFrom,AddressTo,Phone,Price")] Order newOrder)
        { 
            if (id != newOrder.Id)
            {
                return NotFound();
            }
            var order = _context.Orders.Find(id);

            if (order == null)
            {
                return NotFound();
            }

            //todo(max): use ViewModel to avoid this ugly properties assigning
            if (ModelState.IsValid)
            {
                order.AddressFrom = newOrder.AddressFrom;
                order.AddressTo = newOrder.AddressTo;
                order.Phone = newOrder.Phone;
                order.Price = newOrder.Price;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }
        
        [HttpPost]
        public async Task<IActionResult> Cancel([FromBody] int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (order == null)
            {
                return NotFound();
            }

            order.Cancelled = true;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
       
    }
}
