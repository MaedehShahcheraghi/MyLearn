using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLearn.DataLayer.Entities.Course
{
    public class CourseStatus
    {
        [Key]
        public int StatusId { get; set; }
        [Required]
        [MaxLength(150)]
        public string StatusTitle { get; set; }

        #region relations
        public List<Course> Courses { get; set; }
        #endregion
    }

}
