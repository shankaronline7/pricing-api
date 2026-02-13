namespace Application.DTOs.UserManagement
{
    public class UpdateUserDto
    {
        public string? Username { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? EmailId { get; set; }
        public string? Password { get; set; }
        public int? Status { get; set; }   // Nullable
    }

}
