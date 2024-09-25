using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyLearn.Core.Service.Course;
using MyLearn.DataLayer.Entities.Course;

namespace MyLearn.Pages.Admin.Course
{
    public class CreateEpisodeModel : PageModel
    {
        private readonly ICourseService courseService;

        public CreateEpisodeModel(ICourseService courseService)
        {
            this.courseService = courseService;
        }
        [BindProperty]
        public CourseEpisode courseEpisode { get; set; }
        public void OnGet(int id)
        {
            courseEpisode = new CourseEpisode()
            {
                CourseId = id,

            };
            

        }
        public IActionResult OnPost(IFormFile Episodefile)
        {
            courseService.AddCourseEpisode(courseEpisode, Episodefile);
            return RedirectToPage("Index");
        }
    }
}
