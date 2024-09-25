using MyLearn.DataLayer.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLearn.DataLayer.Entities.Course
{
    public class CourseComment
    {
        [Key]
        public int CommentId { get; set; }
        public int CourseId { get; set; }
        public int UserId { get; set; }
        [MaxLength(700)]
        public string Comment { get; set; }
        public DateTime CreateComment { get; set; }
        public bool IsDelete { get; set; }
        public bool IaAdminRead { get; set; }
        #region Relations
        public Course Course { get; set; }
        public User.User User { get; set; }
        #endregion

    }
}
