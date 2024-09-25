using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyLearn.Core.Service.Course;
using MyLearn.DataLayer.Entities.Course;

namespace MyLearn.Pages.Admin.Course
{
    public class EditEpisodeModel : PageModel
    {
        private readonly ICourseService courseService;

        public EditEpisodeModel(ICourseService courseService)
        {
            this.courseService = courseService;
        }
        [BindProperty]
        public CourseEpisode courseEpisode { get; set; }
        public void OnGet(int id)
        {
           courseEpisode= courseService.GetCourseEpisodeByEpisodeId(id);
        }
        public IActionResult OnPost(IFormFile Episodefile)
        {
            courseService.EditCourseEpisode(courseEpisode, Episodefile);
            return RedirectToPage("IndexEpisode");
        }
    }
}
