using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyLearn.Core.Service.Course;
using MyLearn.Core.Service.ForumService;
using MyLearn.Core.Service.UserService;
using MyLearn.DataLayer.Entities.Course;
using MyLearn.DataLayer.Entities.Forum;
using System.Security.Claims;

namespace MyLearn.Controllers
{
    public class ForumController : Controller
    {
        private readonly IForumService forumService;
        private readonly ICourseService courseService;
        private readonly IUserService userService;

        public ForumController(IForumService forumService,ICourseService courseService,IUserService userService)
        {
            this.forumService = forumService;
            this.courseService = courseService;
            this.userService = userService;
        }
        public IActionResult Index(int? id)
        {
            if (id != null)
            ViewBag.CourseId = id;
            return View(forumService.GetAllQuestions(id));
        }
        public IActionResult CreateQuestion(int id)
        {
            var course=courseService.GetCourseByCourseId(id);
            if ((course.CoursePrice != 0 && userService.InUserUsed(id, User.Identity.Name)) || course.CoursePrice ==0)
            {
                ViewBag.courseId = id;
                return View();

            }
            else
            {
                return NotFound();
            }
        }
        [Authorize]
        [HttpPost]
        public IActionResult CreateQuestion(Question question)
        {
            var course=courseService.GetCourseByCourseId(question.CourseId);
            if ((course.CoursePrice != 0 && userService.InUserUsed(question.CourseId,User.Identity.Name)) || course.CoursePrice == 0)
            { 
                question.UserId=int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier.ToString()));
                question.CreateQusetion=DateTime.Now;
                int qusetionid=forumService.AddQuestion(question);
                return Redirect($"/Forum/ShowQuestion/{qusetionid}");

            }
            else
            {
                return BadRequest();
            }
        }
        public IActionResult ShowQuestion(int id)
        {
            var qusetion=forumService.GetQusetionByQuestionId(id);
            if(qusetion != null)
            {
                return View(qusetion);
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult AddAnswer(int QuestionId,string SendAnswer)
        {
            forumService.AddAnswer(QuestionId,SendAnswer, int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier.ToString())));

            return Redirect($"/Forum/ShowQuestion/{QuestionId}");

        }

        public IActionResult EditQuestion(int id)
        {
            return View(forumService.GetQuestionForEdit(id));
        }
        [HttpPost]
        public IActionResult EditQuestion(Question question)
        {
            if (User.Identity.IsAuthenticated && question.UserId == Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)))
            {
                forumService.EditQuestion(question);
                return Redirect($"/Forum/ShowQuestion/{question.QuestionId}");
            }
            else
            {
                return NotFound();
            }
           
        }

        public IActionResult EditAnswer(int id)
        {
            return View(forumService.GetAnswerByAnswerId(id));
        }
        [HttpPost]
        public IActionResult EditAnswer(Answer answer)
        {
            if (User.Identity.IsAuthenticated && answer.UserId == Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)))
            {
                forumService.EditAnswer(answer);
                return Redirect($"/Forum/ShowQuestion/{answer.QuestionId}");
            }
            else
            {
                return NotFound();
            }

        }
    }
}
