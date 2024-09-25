using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLearn.Core.Dtos.Admin.Course
{
    public class CourseViewModel
    {
        public int CourseId { get; set; }
        public string ImageName { get; set; }
        public string CourseTitle { get; set; }
        public int CoursePrice { get; set; }
        public TimeSpan EpisodeTime { get; set; }
    }
}
