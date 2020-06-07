namespace UniversityData.Entities
{
    public partial class StaffRequisite
    {
        public int StaffId { get; set; }
        public string EMail { get; set; }
        public string MobilePhone { get; set; }
        public string HomePhone { get; set; }

        public virtual Staff Staff { get; set; }
    }
}
