using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TecWebFest.Api.DTOs
{
    public class RegisterAttendeeDto
    {
        [Required] public string FullName { get; set; } = default!;
        [Required][EmailAddress] public string Email { get; set; } = default!;

        // Optional 1:1 profile
        public string? DocumentId { get; set; }
        public DateTime? BirthDate { get; set; }
    }

    public class BuyTicketDto
    {
        [Required] public int AttendeeId { get; set; }
        [Required] public int FestivalId { get; set; }
        [Required] public decimal Price { get; set; }
        public string Category { get; set; } = "General";
    }

    public class TicketDto
    {
        public int Id { get; set; }
        public string Festival { get; set; } = default!;
        public string Category { get; set; } = default!;
        public decimal Price { get; set; }
        public DateTime PurchasedAt { get; set; }
    }
}
