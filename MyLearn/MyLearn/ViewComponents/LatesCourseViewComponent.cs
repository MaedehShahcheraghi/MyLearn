using Microsoft.AspNetCore.Mvc;
using MyLearn.Core.Service.Course;

namespace MyLearn.ViewComponents
{
    public class LatesCourseViewComponent : ViewComponent
    {
        private readonly ICourseService courseService;

        public LatesCourseViewComponent(ICourseService  courseService)
        {
            this.courseService = courseService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("LatesCourse", courseService.GetllCourseForShowIndex()));
        }
    }
}
