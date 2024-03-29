﻿namespace MyRestaurant.Services.Common
{
    public class CollectionEnvelop<T>
    {
        public CollectionEnvelop()
        {
            Items = default(T[])!;
        }
        public IEnumerable<T> Items { get; set; }
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalPages()
        {
            return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
        }
    }
}
