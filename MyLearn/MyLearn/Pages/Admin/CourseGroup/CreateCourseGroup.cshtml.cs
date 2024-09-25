using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Build.ObjectModelRemoting;
using MyLearn.Core.Service.Course;

namespace MyLearn.Pages.Admin.CourseGroup
{
    public class CreateCourseGroupModel : PageModel
    {
        private readonly ICourseService courseService;

        public CreateCourseGroupModel(ICourseService courseService)
        {
            this.courseService = courseService;
        }
        [BindProperty]
        public MyLearn.DataLayer.Entities.Course.CourseGroup CourseGroup { get; set; }
        public void OnGet(int id=0)
        {
            if (id !=0)
            {
                CourseGroup=new DataLayer.Entities.Course.CourseGroup()
                {
                    ParentId = id
                };
            }
        }

       public IActionResult OnPost()
        {
            int courseId=courseService.AddCourseGroup(CourseGroup);
            return RedirectToPage("Index");
        }
    }
}
