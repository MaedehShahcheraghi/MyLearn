using MyLearn.DataLayer.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLearn.DataLayer.Entities.Course
{
    public class CourseVote
    {
        [Key]
        public int VoteId { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public bool Vote { get; set; }
        public DateTime VoteDate { get; set; }
        #region Relations
        public User.User User { get; set; }
        public Course Course { get; set; }
        #endregion
    }
}
