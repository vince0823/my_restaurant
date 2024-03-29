﻿namespace MyRestaurant.Models
{
    public class RefreshToken : MyRestaurantObject
    {
        public RefreshToken()
        {
            User = default!;
            Token = default!;
            CreatedByIp = default!;
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public DateTime Created { get; set; }
        public string CreatedByIp { get; set; }
        public DateTime? Revoked { get; set; }
        public string? RevokedByIp { get; set; }
        public string? ReplacedByToken { get; set; }
        public bool IsActive => Revoked == null && !IsExpired;

        public virtual User User { get; set; }
    }
}
