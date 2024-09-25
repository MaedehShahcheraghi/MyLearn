using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyLearn.Core.Service.Course;

namespace MyLearn.Pages.Admin.CourseGroup
{
    public class EditCourseGroupModel : PageModel
    {
        private readonly ICourseService courseService;

        public EditCourseGroupModel(ICourseService courseService)
        {
            this.courseService = courseService;
        }
        [BindProperty]
        public MyLearn.DataLayer.Entities.Course.CourseGroup CourseGroup { get; set; }
        public void OnGet(int id=0)
        {
          CourseGroup=courseService.GetCourseGroupByGroupId(id);
        }

        public IActionResult OnPost()
        {
            courseService.EditCourseGroup(CourseGroup);
            return RedirectToPage("Index");
        }
    }
}
