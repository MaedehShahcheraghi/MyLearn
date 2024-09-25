using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyLearn.Core.Dtos.Admin.Course;
using MyLearn.Core.Service.Course;

namespace MyLearn.Pages.Admin.Course
{
    public class IndexModel : PageModel
    {
        private readonly ICourseService courseService;

        public IndexModel(ICourseService courseService)
        {
            this.courseService = courseService;
        }
        public List<ShowCourseForAdminViewModel> showCourses { get; set; }
        public void OnGet()
        {
            showCourses = courseService.GetAllCourse();
        }
    }
}
