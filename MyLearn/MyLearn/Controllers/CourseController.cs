using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using MyLearn.Core.Service.Course;
using MyLearn.Core.Service.OrderService;
using MyLearn.Core.Service.UserService;
using MyLearn.DataLayer.Entities.Course;
using SharpCompress.Archives;

namespace MyLearn.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService courseService;
        private readonly IOrderService orderService;
        private readonly IUserService userService;
        private readonly FileExtensionContentTypeProvider fileExtension;

        public CourseController(ICourseService courseService, IOrderService orderService, IUserService userService, FileExtensionContentTypeProvider fileExtension)
        {
            this.courseService = courseService;
            this.orderService = orderService;
            this.userService = userService;
            this.fileExtension = fileExtension;
        }
        public IActionResult Index(int pageId = 1, string filter = "", string getType = "all", string orderByType = "date", int strartPrice = 0, int endPrrice = 0, List<int> SelectedGroups = null, int take = 6)
        {
            ViewBag.Groups = courseService.GetCourseGroup();
            ViewBag.selectedGroups = SelectedGroups;
            ViewBag.gettype = getType;
            ViewBag.pageId = pageId;
            ViewBag.startprice = strartPrice;
            ViewBag.endprice = endPrrice;
            return View(courseService.GetCourses(pageId, filter, getType, orderByType, strartPrice, endPrrice, SelectedGroups, take));
        }


        public IActionResult ShowCourse(int id, int episode = 0)
        {
            var course = courseService.GetCourseById(id);
            string filePath = "";
            string fullPath = "";
            if (episode != 0)
            {
                var courseEpisode = courseService.GetCourseEpisodeByEpisodeId(episode);
                if (courseEpisode != null)
                {
                    ViewBag.episode = courseEpisode;

                    if (courseEpisode.IsFree)
                    {
                        filePath = "/CourseFreeOnline/" + courseEpisode.EpisodeFileName.Replace(".rar", ".mp4");
                        fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CourseFreeOnline",
                            courseEpisode.EpisodeFileName.Replace(".rar", ".mp4"));
                    }
                    else
                    {
                        if (!userService.InUserUsed(courseEpisode.CourseId, User.Identity.Name))
                        {
                            return BadRequest();
                        }
                        filePath = "/CourseOnline/" + courseEpisode.EpisodeFileName.Replace(".rar", ".mp4");
                        fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CourseOnline",
                            courseEpisode.EpisodeFileName.Replace(".rar", ".mp4"));
                    }

                    if (!System.IO.File.Exists(fullPath))
                    {
                        var targetPath = Directory.GetCurrentDirectory();
                        if (courseEpisode.IsFree)
                        {
                            targetPath = Path.Combine(targetPath, "wwwroot/CourseFreeOnline");
                        }
                        else
                        {
                            targetPath = Path.Combine(targetPath, "wwwroot/CourseOnline");

                        }
                        var fileepisode = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/EpisodeFile", courseEpisode.EpisodeFileName);
                        var rarfile = ArchiveFactory.Open(fileepisode);
                        var Entries = rarfile.Entries.OrderBy(x => x.Key.Length);
                        foreach (var en in Entries)
                        {
                            if (Path.GetExtension(en.Key) == ".mp4")
                            {
                                using (var stream = new FileStream(Path.Combine(targetPath, courseEpisode.EpisodeFileName.Replace(".rar", ".mp4")), FileMode.Create))
                                {
                                    en.WriteTo(stream);
                                }
                            }
                        }
                    }
                    ViewBag.filepath = filePath;
                }
            }




            return View(course);
        }
        [Authorize]
        public IActionResult BuyCourse(int id)
        {
            string username = User.Identity.Name.ToString();
            int orderId = orderService.AddOrder(username, id);
            return Redirect("/UserPanel/MyOrder/ShowOrder/" + orderId);
        }
        [Authorize]

        public IActionResult DownloadFile(int id)
        {
            var episode = courseService.GetCourseEpisodeByEpisodeId(id);
            if (episode == null)
                return NotFound();
            string pathfile = "";
            byte[] bytefile = null;
            if (episode.IsFree)
            {
                pathfile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/EpisodeFile", episode.EpisodeFileName);
                if (System.IO.File.Exists(pathfile))
                {
                    bytefile = System.IO.File.ReadAllBytes(pathfile);
                    string filename = episode.EpisodeTitle;
                    if (!fileExtension.TryGetContentType(pathfile, out var contentType))
                    {
                        contentType = "application/force-download";
                    }
                    return File(bytefile, contentType, episode.EpisodeFileName);

                }


            }
            else
            {
                if (userService.InUserUsed(episode.CourseId, User.Identity.Name))
                {
                    bytefile = System.IO.File.ReadAllBytes(pathfile);
                    string filename = episode.EpisodeTitle;
                    if (!fileExtension.TryGetContentType(pathfile, out var contentType))
                    {
                        contentType = "application/force-download";
                    }
                    return File(bytefile, contentType, episode.EpisodeFileName);

                }
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult CreateComment(int courseId, string comment)
        {
            int commentId = courseService.AddComment(courseId, comment, User.Identity.Name);
            return View("ShowComment", courseService.GetCourseComment(courseId, 1));
        }
        public IActionResult ShowComment(int id, int pageId = 1)
        {
            return View(courseService.GetCourseComment(id, pageId));
        }

        public IActionResult CourseVote(int id)
        {
            if ((User.Identity.IsAuthenticated && userService.InUserUsed(id,User.Identity.Name)) || courseService.IsFreeCourse(id) )
            {
                ViewBag.IsAccess = true;
            }
           
            return PartialView(courseService.GetVoteCount(id));
        }

        public IActionResult AddVote(int id,bool vote)
        {

            if ((User.Identity.IsAuthenticated && userService.InUserUsed(id, User.Identity.Name)) || courseService.IsFreeCourse(id))
            {
                ViewBag.IsAccess = true;  
                courseService.AddVote(id,User.Identity.Name,vote);
            }
          
          
            return PartialView("CourseVote", courseService.GetVoteCount(id));
        }
    }
}
