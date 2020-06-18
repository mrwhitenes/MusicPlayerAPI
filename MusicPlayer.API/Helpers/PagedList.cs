using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace MusicPlayer.API.Helpers
{
    public class PagedList<T> : List<T>
    {
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public bool HasPrev => (CurrentPage > 1);
        public bool HasNext => (CurrentPage < TotalPages);

        private PagedList(List<T> ts, int count, int pageSize, int pageNumber)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(ts);
        }

        public static PagedList<T> Create(IQueryable<T> source, int pageSize, int pageNumber)
        {
            var count = source.Count();
            var items = source.Skip(pageSize * (pageNumber - 1))
                              .Take(pageSize).ToList();
            return new PagedList<T>(items, count, pageSize, pageNumber);
        }
    }
}
