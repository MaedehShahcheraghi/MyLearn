using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyLearn.Core.Security;
using MyLearn.Core.Service.Course;
using MyLearn.Core.Service.UserService;
using MyLearn.DataLayer.Entities.Course;
using MyLearn.Pages.Admin.Course;

namespace MyLearn.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [PermissionChecker(14)]
    public class MasterController : Controller
    {
        private readonly ICourseService courseService;
        private readonly IUserService userService;

        public MasterController(ICourseService courseService,IUserService userService)
        {
            this.courseService = courseService;
            this.userService = userService;
        }
        [Route("UserPanel/ListCourses")]
        public IActionResult Index()
        {
            return View(courseService.GetCoursesForMaster(User.Identity.Name));
        }
        [Route("UserPanel/ShowCourseEpisode")]
        public IActionResult ShowCourseEpisode(int id)
        {
            ViewBag.courseId=id;
            var episodes = courseService.GetCourseEpisodesByCouresId(id);
            return View(episodes);
        }
        [HttpGet("add-episode/{courseId}")]
        public IActionResult AddEpisode(int courseId)
        {
            var course = courseService.GetCourseById(courseId);

            if (course == null)
            {
                return NotFound();
            }

            var userId = userService.GetUserIdByUserName(User.Identity.Name);

            if (course.TeacherId != userId)
            {
                return RedirectToAction("MasterCoursesList", "Master");
            }

            var result = new CourseEpisode
            {
                CourseId = courseId,
                IsFree = true
            };

            return View(result);
        }
        [HttpPost("add-episode/{courseId}")]
        public IActionResult AddEpisode(CourseEpisode courseEpisode)
        {
            

            if (string.IsNullOrEmpty(courseEpisode.EpisodeFileName))
            {
                return View(courseEpisode);
            }

            var result = courseService.AddCourseEpisode(courseEpisode,User.Identity.Name);

            if (result)
            {
                return Redirect($"/Master/ShowCourseEpisode?id={courseEpisode.CourseId}");
            }

            return View(courseEpisode);
        }

        public IActionResult DropzoneTarget(List<IFormFile> files, int courseId)
        {
            if (files != null && files.Any())
            {
                foreach (var file in files)
                {
                    var fileName = $"{courseId}-{Guid.NewGuid().ToString()}" + Path.GetExtension(file.FileName);

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/EpisodeFile/");

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    var finalPath = path + fileName;

                    using (var stream = new FileStream(finalPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    return new JsonResult(new { data = fileName, status = "Success" });
                }
            }

            return new JsonResult(new { status = "Error" });
        }
    }
}
