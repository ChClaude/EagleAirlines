﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApi.Data.Util
{
    public class PagedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public int ItemCount { get; }

        private PagedList(IEnumerable<T> items, int itemCount, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            ItemCount = itemCount;
            TotalPages = (int)Math.Ceiling(itemCount / (double)pageSize);

            AddRange(items);
        }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
        
        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip(
                (pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            return new PagedList<T>(items, count, pageIndex, pageSize);
        }

    }
}