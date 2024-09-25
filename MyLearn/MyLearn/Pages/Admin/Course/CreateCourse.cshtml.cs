using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyLearn.Core.Service.Course;
using MyLearn.DataLayer.Entities.Course;

namespace MyLearn.Pages.Admin.Course
{
    public class CreateCourseModel : PageModel
    {
        private readonly ICourseService courseService;

        public CreateCourseModel(ICourseService courseService)
        {
            this.courseService = courseService;
        }
        [BindProperty]
        public MyLearn.DataLayer.Entities.Course.Course Course { get; set; }
        
        public void OnGet()
        {
            var coursegroup = courseService.GetGroupsForManageCourse();
            ViewData["Groups"] = new SelectList(coursegroup,"Value","Text");
            List<SelectListItem> SubGroup = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Value="0",
                    Text="لطفا انتخاب کنید"
                }
            };
            SubGroup.AddRange(courseService.GetSubGroupForManageCourse(int.Parse(coursegroup.First().Value.ToString())));
            ViewData["SubGroups"] =new SelectList(SubGroup,"Value","Text");
            var coursestatus=courseService.GetAllStatus();
            ViewData["status"] =new SelectList(coursestatus,"Value","Text");
            var courselevel=courseService.GetAllLevel();
            ViewData["levels"] =new SelectList (courselevel,"Value","Text");
            var teachers=courseService.GetTeachers();
            ViewData["teachers"] =new SelectList (teachers,"Value","Text");
            

        }
        public IActionResult OnPost(IFormFile imageCourseUp, IFormFile demoUp)
        {
            int courseid=courseService.AddCourse(Course,imageCourseUp,demoUp);
            return RedirectToPage("Index");
        }
    }
}
