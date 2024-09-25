using MyLearn.DataLayer.Entities.Course;
using MyLearn.DataLayer.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLearn.DataLayer.Entities.Forum
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        [Required]
        [MaxLength(300)]
        public string QuestionTitle { get; set; }
        [Required]
        public string QuestionBody { get; set; }
        public DateTime CreateQusetion { get; set; }

        #region Relation
        public User.User User { get; set; }
        public Course.Course Course { get; set; }
        public List<Answer> Answers { get; set; }

        #endregion



    }
}
