using Pricing.Domain.Constants;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.UserManagement
{
    [Table("users")] // optional but recommended for EF Core
    public class users
    {
        public static string PasswordHash { get; set; }

        [Column("id")]
        public long Id { get; set; }

        [Column("username")]
        public string Username { get; set; }

        [Column("firstname")]
        public string Firstname { get; set; }

        [Column("lastname")]
        public string Lastname { get; set; }

        [Column("password_hash")]
        public string Password { get; set; }

        [Column("email_id")]
        public string EmailId { get; set; }

        [Column("status")]
        public UserStatus Status { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; set; }

        [Column("created_by")]
        public string? CreatedBy { get; set; }
    }
}
