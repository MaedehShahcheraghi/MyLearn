using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLearn.Core.Dtos.Admin.Course
{
    public class ShowCourseForAdminViewModel
    {
        public int CourseId { get; set; }
        public string ImageName { get; set; }
        public string CourseName { get; set; }
        public int EpisodeCount { get; set; }

    }
}
