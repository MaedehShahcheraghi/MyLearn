using Microsoft.AspNetCore.Mvc;
using MyLearn.Core.Service.Course;
using System.Threading.Tasks;

namespace TopLearn.Web.ViewComponents
{
    public class CourseGroupComponents:ViewComponent
    {
        private readonly ICourseService service;

        public CourseGroupComponents(ICourseService service)
        {
            this.service = service;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult) View("CourseGroup",service.GetCourseGroup()));
        }
    }
}
