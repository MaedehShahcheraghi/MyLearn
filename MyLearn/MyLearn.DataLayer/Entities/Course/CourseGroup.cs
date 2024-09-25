using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLearn.DataLayer.Entities.Course
{
    public class CourseGroup
    {
        [Key]
        public int GroupId { get; set; }
        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "لطفا {0} کمتر از {1} باشد")]
        public string GroupTitle { get; set; }
        [Required]
        public bool IsDelete { get; set; }
        public int? ParentId { get; set; }

        #region Rleations
        [ForeignKey("ParentId")]
        public List<CourseGroup> courseGroups { get; set; }
        [InverseProperty("CourseGroup")]
        public List<Course> Courses { get; set; }
        [InverseProperty("SubGroup")]
        public List<Course> SubGroup { get; set; }


        #endregion
    }
}
