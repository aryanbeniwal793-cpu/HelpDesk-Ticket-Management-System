using System.Net.Http.Json;
using HelpDesk.Mvc.Models;

namespace HelpDesk.Mvc.Services
{
    public class TicketService
    {
        private readonly HttpClient _httpClient;

        public TicketService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Ticket>> GetAllTicketsAsync()
            => await _httpClient.GetFromJsonAsync<List<Ticket>>("api/Ticket/All") ?? new List<Ticket>();

        public async Task<Ticket?> GetTicketByIdAsync(int id)
            => await _httpClient.GetFromJsonAsync<Ticket>($"api/Ticket/{id}");

        public async Task<bool> CreateTicketAsync(Ticket ticket)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Ticket", ticket);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateTicketAsync(Ticket ticket)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Ticket/{ticket.Id}", ticket);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteTicketAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Ticket/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<Ticket>> GetTicketsByStatusAsync(string status)
            => await _httpClient.GetFromJsonAsync<List<Ticket>>($"api/Ticket/Status/{status}") ?? new List<Ticket>();
    }
}