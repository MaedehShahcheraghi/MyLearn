using MyLearn.DataLayer.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLearn.DataLayer.Entities.Forum
{
    public class Answer
    {
        [Key]
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        public int UserId { get; set; }
        [Required]
        [MaxLength(300)]
        public string AnswerBody { get; set; }
        public DateTime CreateAnswer { get; set; }

        #region Relations
        public Question Question { get; set; }
        public User.User User { get; set; }
        #endregion


    }
}
