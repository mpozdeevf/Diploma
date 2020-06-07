namespace UniversityData.Entities
{
    public partial class StaffAuthData
    {
        public int StaffId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string RefreshToken { get; set; }

        public virtual Staff Staff { get; set; }
    }
}
