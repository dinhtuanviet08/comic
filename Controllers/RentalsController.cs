using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComicSystem.Data;
using ComicSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ComicSystem.Controllers
{
    public class RentalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RentalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rentals
        public async Task<IActionResult> Index()
        {
            var rentals = await _context.Rentals.Include(r => r.Customer).ToListAsync();
            return View(rentals);
        }

        // GET: Rentals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var rental = await _context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.RentalDetails)
                    .ThenInclude(rd => rd.ComicBook)
                .FirstOrDefaultAsync(m => m.RentalId == id);

            if (rental == null) return NotFound();

            return View(rental);
        }

        // GET: Rentals/Create
        public IActionResult Create()
        {
            // Check if there are any customers or comic books available
            if (_context.Customers == null || !_context.Customers.Any())
            {
                // Handle the case where there are no customers
                ViewBag.CustomerId = new SelectList(Enumerable.Empty<object>());
            }
            else
            {
                ViewBag.CustomerId = new SelectList(_context.Customers, "Id", "FullName");
            }

            if (_context.ComicBooks == null || !_context.ComicBooks.Any())
            {
                // Handle the case where there are no comic books
                ViewBag.ComicBooks = new List<ComicBook>();
            }
            else
            {
                ViewBag.ComicBooks = _context.ComicBooks.ToList();
            }

            return View();
        }

        // POST: Rentals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Rental rental, List<RentalDetail> details)
        {
            if (ModelState.IsValid)
            {
                // Add rental to database
                _context.Add(rental);
                await _context.SaveChangesAsync();

                // Initialize the details list if it is null
                if (details == null)
                {
                    details = new List<RentalDetail>();
                }

                // Add rental details
                foreach (var detail in details)
                {
                    if (detail.ComicBookId != 0 && detail.Quantity > 0)
                    {
                        detail.RentalId = rental.RentalId;
                        _context.RentalDetails.Add(detail);
                    }
                }

                // Save changes to the database
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // If the model state is not valid, return to the form with errors
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Fullname", rental.CustomerId);
            ViewData["ComicBooks"] = _context.ComicBooks.ToList();
            return View(rental);
        }

        // GET: Rentals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var rental = await _context.Rentals.FindAsync(id);
            if (rental == null) return NotFound();

            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Fullname", rental.CustomerId);
            return View(rental);
        }

        // POST: Rentals/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Rental rental)
        {
            if (id != rental.RentalId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rental);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentalExists(rental.RentalId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Fullname", rental.CustomerId);
            return View(rental);
        }

        // GET: Rentals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var rental = await _context.Rentals
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(m => m.RentalId == id);

            if (rental == null) return NotFound();

            return View(rental);
        }

        // POST: Rentals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rental = await _context.Rentals.FindAsync(id);

            var rentalDetails = _context.RentalDetails.Where(rd => rd.RentalId == id);
            _context.RentalDetails.RemoveRange(rentalDetails);

            _context.Rentals.Remove(rental);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RentalExists(int id)
        {
            return _context.Rentals.Any(e => e.RentalId == id);
        }
    }
}
