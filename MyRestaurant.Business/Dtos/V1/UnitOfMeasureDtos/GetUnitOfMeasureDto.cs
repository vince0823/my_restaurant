﻿namespace MyRestaurant.Business.Dtos.V1
{
    public class GetUnitOfMeasureDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = default!;
        public string Description { get; set; } = default!;
    }
}
