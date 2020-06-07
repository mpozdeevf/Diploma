namespace UniversityData.Entities
{
    public partial class StudentAuthData
    {
        public int StudentId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string RefreshToken { get; set; }

        public virtual Student Student { get; set; }
    }
}
