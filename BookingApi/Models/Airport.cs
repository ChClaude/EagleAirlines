﻿using Microsoft.AspNetCore.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApi.Models
{
    public class Airport
    {
        public int ID { get; set; }
        
        [Required]
        [StringLength(150, MinimumLength = 4)]
        public string Name { get; set; }

        [Required]
        [StringLength(70, MinimumLength = 4)]
        public string City { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 4)]
        public string Country { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Iata { get; set; }
        
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Iciao { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Latitude { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Longitude { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Altitude { get; set; }


        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Timezone { get; set; }


        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Dst { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Tz { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string StationType { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Source { get; set; }
    }
}
