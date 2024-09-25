using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyLearn.Core.Service.Course;
using MyLearn.DataLayer.Entities.Course;

namespace MyLearn.Pages.Admin.Course
{
    public class IndexEpisodeModel : PageModel
    {
        private readonly ICourseService courseService;

        public IndexEpisodeModel(ICourseService courseService)
        {
            this.courseService = courseService;
        }
        [BindProperty]
        public List<CourseEpisode> courseEpisodes { get; set; }
        public void OnGet(int id)
        {
            courseEpisodes=courseService.GetCourseEpisodesByCouresId(id);
            ViewData["CourseId"] = id;
        }
    }
}
