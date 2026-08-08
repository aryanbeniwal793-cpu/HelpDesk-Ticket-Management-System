using HelpDesk.Mvc.Models;
using HelpDesk.Mvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Mvc.Controllers
{
    public class TicketController : Controller
    {
        private readonly TicketService _ticketService;

        public TicketController(TicketService ticketService)
        {
            _ticketService = ticketService;
        }

        // Dashboard
        public async Task<IActionResult> Index()
        {
            var tickets = await _ticketService.GetAllTicketsAsync();
            ViewBag.TotalTickets = tickets.Count;
            ViewBag.OpenTickets = tickets.Count(t => string.Equals(t.Status, "Open", StringComparison.OrdinalIgnoreCase));
            ViewBag.ClosedTickets = tickets.Count(t => string.Equals(t.Status, "Closed", StringComparison.OrdinalIgnoreCase));
            return View();
        }

        // View All Tickets
        public async Task<IActionResult> List()
        {
            var tickets = await _ticketService.GetAllTicketsAsync();
            return View(tickets);
        }

        // View Ticket Details
        public async Task<IActionResult> Details(int id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);
            if (ticket == null) return NotFound();
            return View(ticket);
        }

        // Raise New Ticket
        public IActionResult Create() => View(new Ticket { Status = "Open" });

        [HttpPost]
        public async Task<IActionResult> Create(Ticket ticket)
        {
            ticket.Status = "Open"; // Hardcoded as required by assignment
            ticket.CreatedDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                await _ticketService.CreateTicketAsync(ticket);
                return RedirectToAction(nameof(List));
            }
            return View(ticket);
        }

        // Edit Ticket
        public async Task<IActionResult> Edit(int id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);
            if (ticket == null) return NotFound();
            return View(ticket);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                await _ticketService.UpdateTicketAsync(ticket);
                return RedirectToAction(nameof(List));
            }
            return View(ticket);
        }

        // Delete Ticket
        public async Task<IActionResult> Delete(int id)
        {
            await _ticketService.DeleteTicketAsync(id);
            return RedirectToAction(nameof(List));
        }

        // Filter Tickets by Status
        public async Task<IActionResult> Filter(string status)
        {
            ViewBag.SelectedStatus = status;
            if (string.IsNullOrEmpty(status))
                return View(new List<Ticket>());

            var tickets = await _ticketService.GetTicketsByStatusAsync(status);
            return View(tickets);
        }
    }
}
