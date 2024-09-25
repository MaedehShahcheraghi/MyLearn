using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyLearn.Core.Service.Course;

namespace MyLearn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseApiController : ControllerBase
    {
        private readonly ICourseService courseService;

        public CourseApiController(ICourseService courseService)
        {
            this.courseService = courseService;
        }
        [Produces("application/json")]
        [HttpGet("search")]
        public  IActionResult Search()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var courseTitle =  courseService.GetCourseForSearchBox(term);
                return Ok(courseTitle);
            }
            catch
            {
                return BadRequest();
            }
        } 
    }
}
