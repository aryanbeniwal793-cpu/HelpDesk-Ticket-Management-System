using HelpDesk.Api.Controllers;
using HelpDesk.Api.Models;
using HelpDesk.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace HelpDesk.Tests
{
    public class TicketControllerTests
    {
        private readonly Mock<ITicketRepository> _mockRepo;
        private readonly TicketController _controller;

        public TicketControllerTests()
        {
            _mockRepo = new Mock<ITicketRepository>();
            _controller = new TicketController(_mockRepo.Object);
        }

        // Test 1: GetAllTickets_ReturnsOkResult_WhenTicketsExist
        [Fact]
        public async Task GetAllTickets_ReturnsOkResult_WhenTicketsExist()
        {
            var testTickets = new List<Ticket> 
            { 
                new Ticket { Id = 1, Title = "Software Bug", Status = "Open" } 
            };
            _mockRepo.Setup(repo => repo.GetAllTicketsAsync()).ReturnsAsync(testTickets);

            var result = await _controller.GetAllTickets();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnTickets = Assert.IsType<List<Ticket>>(okResult.Value);
            Assert.Single(returnTickets);
        }

        // Test 2: GetTicketById_ReturnsOkResult_WhenTicketExists
        [Fact]
        public async Task GetTicketById_ReturnsOkResult_WhenTicketExists()
        {
            var testTicket = new Ticket { Id = 1, Title = "Network Issue" };
            _mockRepo.Setup(repo => repo.GetTicketByIdAsync(1)).ReturnsAsync(testTicket);

            var result = await _controller.GetTicketById(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnTicket = Assert.IsType<Ticket>(okResult.Value);
            Assert.Equal(1, returnTicket.Id);
        }

        // Test 3: GetTicketById_ReturnsNotFound_WhenTicketDoesNotExist
        [Fact]
        public async Task GetTicketById_ReturnsNotFound_WhenTicketDoesNotExist()
        {
            _mockRepo.Setup(repo => repo.GetTicketByIdAsync(99)).ReturnsAsync((Ticket?)null);

            var result = await _controller.GetTicketById(99);

            Assert.IsType<NotFoundResult>(result);
        }

        // Test 4: CreateTicket_ReturnsOkResult_WhenTicketIsCreatedSuccessfully
        [Fact]
        public async Task CreateTicket_ReturnsOkResult_WhenTicketIsCreatedSuccessfully()
        {
            var newTicket = new Ticket { Id = 1, Title = "Hardware Fault" };
            _mockRepo.Setup(repo => repo.CreateTicketAsync(newTicket)).ReturnsAsync(1);

            var result = await _controller.CreateTicket(newTicket);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(1, okResult.Value);
        }

        // Test 5: CreateTicket_ReturnsBadRequest_WhenTicketIsNull
        [Fact]
        public async Task CreateTicket_ReturnsBadRequest_WhenTicketIsNull()
        {
            var result = await _controller.CreateTicket(null);

            Assert.IsType<BadRequestResult>(result);
        }

        // Test 6: GetTicketsByStatus_ReturnsOkResult_WhenMatchingTicketsExist
        [Fact]
        public async Task GetTicketsByStatus_ReturnsOkResult_WhenMatchingTicketsExist()
        {
            var openTickets = new List<Ticket> 
            { 
                new Ticket { Id = 1, Status = "Open" } 
            };
            _mockRepo.Setup(repo => repo.GetTicketsByStatusAsync("Open")).ReturnsAsync(openTickets);

            var result = await _controller.GetTicketsByStatus("Open");

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnTickets = Assert.IsType<List<Ticket>>(okResult.Value);
            Assert.Single(returnTickets);
        }
    }
}