using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyLearn.Core.Service.Course;

namespace MyLearn.Pages.Admin.Course
{
    public class EditCourseModel : PageModel
    {
        private readonly ICourseService courseService;

        public EditCourseModel(ICourseService courseService)
        {
            this.courseService = courseService;
        }
        [BindProperty]
        public MyLearn.DataLayer.Entities.Course.Course Course { get; set; }

        public void OnGet(int id)
        {
            Course=courseService.GetCourseByCourseId(id);
            var coursegroup = courseService.GetGroupsForManageCourse();
            ViewData["Groups"] = new SelectList(coursegroup, "Value", "Text",Course.GroupId);
            List<SelectListItem> SubGroup = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Value="0",
                    Text="لطفا انتخاب کنید"
                }
            };
            SubGroup.AddRange(courseService.GetSubGroupForManageCourse(Course.GroupId));
            ViewData["SubGroups"] = new SelectList(SubGroup, "Value", "Text",Course.SubGroupId);
            var coursestatus = courseService.GetAllStatus();
            ViewData["status"] = new SelectList(coursestatus, "Value", "Text",Course.StatusId);
            var courselevel = courseService.GetAllLevel();
            ViewData["levels"] = new SelectList(courselevel, "Value", "Text",Course.LevelId);
            var teachers = courseService.GetTeachers();
            ViewData["teachers"] = new SelectList(teachers, "Value", "Text",Course.TeacherId);


        }

        public IActionResult OnPost(IFormFile imageCourseUp, IFormFile demoUp)
        {
            courseService.EditCourse(Course, imageCourseUp, demoUp);
            return RedirectToPage("Index");
        }
    }
}
