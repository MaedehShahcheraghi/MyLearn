using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyLearn.Core.Service.Course;
using MyLearn.DataLayer.Entities.Course;

namespace MyLearn.Pages.Admin.CourseGroup
{
    public class IndexModel : PageModel
    {
        private readonly ICourseService courseService;

        public IndexModel(ICourseService courseService)
        {
            this.courseService = courseService;
        }
        public List<MyLearn.DataLayer.Entities.Course.CourseGroup> CourseGroups { get; set; }
        public void OnGet()
        {
            CourseGroups = courseService.GetCourseGroupBySubGroup();
        }
    }
}
