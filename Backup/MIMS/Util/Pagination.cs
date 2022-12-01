using System;
using System.Collections.Generic;
using MvcContrib.Pagination;

namespace MIMS.Util
{
    public class Pagination<TEntity> : IPagination<TEntity>
    {
        private readonly IEnumerable<TEntity> _entities;

        public Pagination(IEnumerable<TEntity> entities, int pageNumber, int pageSize, int totalItems)
        {
            _entities = entities;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalItems = totalItems;
        }

        public int FirstItem
        {
            get { return PageNumber == 1 ? 1 : PageSize * (PageNumber - 1) + 1; }
        }

        public bool HasNextPage
        {
            get { return PageNumber < TotalPages; }
        }

        public bool HasPreviousPage
        {
            get { return PageNumber > 1; }
        }

        public int LastItem
        {
            get { return HasNextPage ? (FirstItem + PageSize - 1) : TotalItems; }
        }

        public int PageNumber
        {
            get;
            private set;
        }

        public int PageSize
        {
            get;
            private set;
        }

        public int TotalItems
        {
            get;
            private set;
        }

        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); }
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return _entities.GetEnumerator();
        }

        IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator()
        {
            return _entities.GetEnumerator();
        }
    }
}