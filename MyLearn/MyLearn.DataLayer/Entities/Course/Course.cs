using MyLearn.DataLayer.Entities.Forum;
using MyLearn.DataLayer.Entities.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyLearn.DataLayer.Entities.Course
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        [Required]
        public int GroupId { get; set; }
        public int? SubGroupId { get; set; }
        [Required]
        public int TeacherId { get; set; }
        [Required]
        public int LevelId { get; set; }
        [Required]
        public int StatusId { get; set; }
        [Display(Name = "عنوان دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(400, ErrorMessage = "لطفا {0} کمتر از {1} باشد")]
        public string CourseTitle { get; set; }
        [Display(Name = "شرح دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string CourseDescreption { get; set;}
        [Display(Name = "قیمت دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int CoursePrice { get; set; }
        [MaxLength(100)]
        public string CourseImageName { get; set; }
        [MaxLength(500)]
        public string Tags { get; set; }
        [MaxLength(100)]
        public string DemoFileName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        #region Relations
        [ForeignKey("TeacherId")]
        public User.User User { get; set; }
        [ForeignKey("GroupId")]
        public CourseGroup CourseGroup { get; set; }
        [ForeignKey("SubGroupId")]
        public CourseGroup SubGroup { get; set; }
        [ForeignKey("LevelId")]
        public CourseLevel CourseLevel { get; set; }
        [ForeignKey("StatusId")]
        public CourseStatus CourseStatus { get; set; }
        public List<CourseEpisode> CourseEpisodes { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public List<CourseComment> CourseComments { get; set; }
        public List<CourseVote> CourseVotes { get; set; }
        public List<Question> Questions { get; set; }

        #endregion

    }
}
