﻿using BookingApi.Dtos.SeatDto;

namespace BookingApi.Dtos.PassengerDto
{
    public class PassengerReadDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Surname { get; set; }
        public int Age { get; set; }

        public string Title { get; set; }

        public string PassportNumber { get; set; }
        
        public string Citizenship { get; set; }
        public int BookingId { get; set; }

        public SeatReadDto Seat { get; set; }
    }
}