using System;
using System.Collections.Generic;

namespace UniversityData.Entities
{
    public partial class News
    {
        public News()
        {
            NewsReceivers = new HashSet<NewsReceiver>();
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        public int? AuthorStudentId { get; set; }
        public int? AuthorStaffId { get; set; }
        public DateTime Date { get; set; }

        public virtual Staff AuthorStaff { get; set; }
        public virtual Student AuthorStudent { get; set; }
        public virtual ICollection<NewsReceiver> NewsReceivers { get; set; }
    }
}
