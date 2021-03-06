﻿using BookingApi.Data.Util;
using BookingApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApi.Data.Repository.AirportRepo
{
    public class AirportRepo : IAirportRepo
    {
        public AirportRepo(BookingContext context)
        {
            _context = context;
        }

        private BookingContext _context { get; }

        public async Task<IEnumerable<Airport>> GetAllAsync(QueryStringParameters queryStringParameters)
        {
            IQueryable<Airport> airportsIq;

            // search
            if (!string.IsNullOrEmpty(queryStringParameters.SearchString))
            {
                var searchString = queryStringParameters.SearchString;
                airportsIq = _context.Airports.Where(a => a.Name.ToUpper().Contains(searchString.ToUpper())
                                                          || a.Country.ToUpper().Contains(searchString.ToUpper())
                                                          || a.City.ToUpper().Contains(searchString.ToUpper()));
            }
            else
            {
                airportsIq = from a in _context.Airports select a;
            }

            // page
            IEnumerable<Airport> airports =
                await PaginatedList<Airport>.CreateAsync(airportsIq, queryStringParameters.PageNumber, queryStringParameters.PageSize);
            
            // sort string not set
            if (string.IsNullOrEmpty(queryStringParameters.SortString)) return airports;
            
            // sort
            var sort = queryStringParameters.SortString;
            
            var count = ((PaginatedList<Airport>) airports).ItemCount;
            var index = ((PaginatedList<Airport>) airports).PageIndex;
            var size = ((PaginatedList<Airport>) airports).PageSize;

            airports = sort switch
            {
                "name_desc" => airports.OrderByDescending(a => a.Name),
                "country" => airports.OrderBy(a => a.Country),
                "country_desc" => airports.OrderByDescending(a => a.Country),
                "city" => airports.OrderBy(a => a.City),
                "city_desc" => airports.OrderByDescending(a => a.City),
                _ => airports.OrderBy(a => a.Name)
            };

            return PaginatedList<Airport>.ParsePaginatedList(airports, count, index, size);
        }

        public async Task<Airport> GetByIdAsync(int id)
        {
            return await _context.Airports.FindAsync(id);
        }

        public async Task CreateAsync(Airport airport)
        {
            if (airport == null)
            {
                throw new ArgumentNullException(nameof(airport));
            }

            await _context.Airports.AddAsync(airport);
        }
        
        public void Update(Airport airport)
        {
            _context.Entry(airport).State = EntityState.Modified;
        }

        public void Delete(Airport airport)
        {
            if (airport == null)
            {
                throw new ArgumentNullException(nameof(airport));
            }

            _context.Airports.Remove(airport);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

    }
}