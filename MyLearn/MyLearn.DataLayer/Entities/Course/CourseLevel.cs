using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLearn.DataLayer.Entities.Course
{
    public class CourseLevel
    {
        [Key]
        public int LevelId { get; set; }
        [MaxLength(100)]
        [Required]
        public string LevelTitle { get; set; }
        #region relations
        public List<Course> Courses { get; set; }
        #endregion
    }
}
