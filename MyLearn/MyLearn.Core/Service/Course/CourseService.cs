using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyLearn.Core.Convertors;
using MyLearn.Core.Dtos.Admin.Course;
using MyLearn.Core.Generators;
using MyLearn.DataLayer.Context;
using MyLearn.DataLayer.Entities.Course;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLearn.Core.Service.Course
{
    public class CourseService : ICourseService
    {
        private readonly MyLearnContext context;

        public CourseService(MyLearnContext context)
        {
            this.context = context;
        }

        public int AddComment(int courseId, string comment, string userName)
        {
            var newComment = new CourseComment()
            {
                CourseId = courseId,
                Comment = comment,
                CreateComment = DateTime.Now,
                IsDelete = false,
                IaAdminRead = false,
                UserId = context.Users.FirstOrDefault(u => u.UserName == userName).UserId
            };
            context.CourseComments.Add(newComment);
            context.SaveChanges();
            return newComment.CommentId;
        }

        public int AddCourse(DataLayer.Entities.Course.Course course, IFormFile ImageDemoUp, IFormFile FileDemoFile)
        {
            course.CourseImageName = "no-pictures.png";
            if (ImageDemoUp != null)
            {
                string imagename = NameGenerator.GenerateUnicCode() + Path.GetExtension(ImageDemoUp.FileName);
                string mypath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/CourseImage", imagename);
                using (var stream = new FileStream(mypath, FileMode.Create))
                {
                    ImageDemoUp.CopyTo(stream);
                }
                string thumpPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/thump", imagename);
                ImageConvertor imageConvertor = new ImageConvertor();
                imageConvertor.Image_resize(mypath, thumpPath, 300);
                course.CourseImageName = imagename;

            }

            if (FileDemoFile != null)
            {
                string filename = NameGenerator.GenerateUnicCode() + Path.GetExtension(FileDemoFile.FileName);
                string mypath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/CourseDemo", filename);
                using (var stream = new FileStream(mypath, FileMode.Create))
                {
                    FileDemoFile.CopyTo(stream);
                }
                course.DemoFileName = filename;

            }
            course.CreateDate = DateTime.Now;
            context.Courses.Add(course);
            context.SaveChanges();
            return course.CourseId;
        }

        public int AddCourseEpisode(CourseEpisode courseEpisode, IFormFile epispdeFile)
        {
            string fileName = NameGenerator.GenerateUnicCode() + Path.GetExtension(epispdeFile.FileName);
            string episodePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/EpisodeFile", fileName);
            using (var stream = new FileStream(episodePath, FileMode.Create))
            {
                epispdeFile.CopyTo(stream);
            }
            courseEpisode.EpisodeFileName = fileName;
            context.CourseEpisodes.Add(courseEpisode);
            context.SaveChanges();
            return courseEpisode.CourseId;
        }

        public int AddCourseGroup(CourseGroup courseGroup)
        {
            context.CourseGroups.Add(courseGroup);
            context.SaveChanges();
            return courseGroup.GroupId;
        }

        public void AddVote(int courseId, string userName, bool vote)
        {
            int userid = context.Users.FirstOrDefault(x => x.UserName == userName).UserId;
            var oldVote = context.CourseVotes.FirstOrDefault(c => c.UserId == userid && c.CourseId == courseId);
            if (oldVote != null)
            {
                oldVote.Vote = vote;
                context.SaveChanges();
            }
            else
            {
                var newvote = new CourseVote()
                {
                    CourseId = courseId,
                    UserId = userid,
                    Vote = vote,
                    VoteDate = DateTime.Now
                };
                context.CourseVotes.Add(newvote);
                context.SaveChanges();
            }


          
        }

        public void EditCourse(DataLayer.Entities.Course.Course course, IFormFile ImageDemoUp, IFormFile FileDemoFile)
        {
            course.UpdateDate = DateTime.Now;

            if (ImageDemoUp != null)
            {
                if (course.CourseImageName != "no-pictures.png")
                {
                    string patholld = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/CourseImage", course.CourseImageName);
                    if (File.Exists(patholld))
                    {
                        File.Delete(patholld);
                    }
                    string thumpOld = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/thump", course.CourseImageName);
                    if (File.Exists(thumpOld))
                    {
                        File.Delete(thumpOld);
                    }
                }
                string imagename = NameGenerator.GenerateUnicCode() + Path.GetExtension(ImageDemoUp.FileName);
                string mypath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/CourseImage", imagename);
                using (var stream = new FileStream(mypath, FileMode.Create))
                {
                    ImageDemoUp.CopyTo(stream);
                }
                string thumpPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/thump", imagename);
                ImageConvertor imageConvertor = new ImageConvertor();
                imageConvertor.Image_resize(mypath, thumpPath, 150);
                course.CourseImageName = imagename;

            }

            if (FileDemoFile != null)
            {
                string filename = NameGenerator.GenerateUnicCode() + Path.GetExtension(FileDemoFile.FileName);
                string mypath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/CourseDemo", filename);
                using (var stream = new FileStream(mypath, FileMode.Create))
                {
                    FileDemoFile.CopyTo(stream);
                }
                course.DemoFileName = filename;

            }
            context.Courses.Update(course);
            context.SaveChanges();

        }

        public void EditCourseEpisode(CourseEpisode courseEpisode, IFormFile epispdeFile)
        {
            string oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwroot/Course/CourseEpisode", courseEpisode.EpisodeFileName);
            if (File.Exists(oldPath))
            {
                File.Delete(oldPath);
            }
            string fileName = NameGenerator.GenerateUnicCode() + Path.GetExtension(epispdeFile.FileName);
            string episodePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Course/EpisodeFile", fileName);
            using (var stream = new FileStream(episodePath, FileMode.Create))
            {
                epispdeFile.CopyTo(stream);
            }
            courseEpisode.EpisodeFileName = fileName;
            context.CourseEpisodes.Update(courseEpisode);
            context.SaveChanges();
        }

        public int EditCourseGroup(CourseGroup courseGroup)
        {
            context.CourseGroups.Update(courseGroup);
            context.SaveChanges();
            return courseGroup.GroupId;
        }

        public List<ShowCourseForAdminViewModel> GetAllCourse()
        {
            return context.Courses.Include(c => c.CourseEpisodes).Select(c => new ShowCourseForAdminViewModel()
            {
                CourseName = c.CourseTitle,
                ImageName = c.CourseImageName,
                EpisodeCount = c.CourseEpisodes.Count(),
                CourseId = c.CourseId
            }).ToList();
        }

        public List<SelectListItem> GetAllLevel()
        {
            return context.CourseLevels.Select(l => new SelectListItem()
            {
                Text = l.LevelTitle,
                Value = l.LevelId.ToString()
            }).ToList();
        }

        public List<SelectListItem> GetAllStatus()
        {
            return context.CourseStatuses.Select(l => new SelectListItem()
            {
                Text = l.StatusTitle,
                Value = l.StatusId.ToString()
            }).ToList();
        }

        public DataLayer.Entities.Course.Course GetCourseByCourseId(int courseId)
        {
            return context.Courses.Find(courseId);
        }

        public DataLayer.Entities.Course.Course GetCourseById(int courseId)
        {
            return context.Courses.Include(c => c.CourseEpisodes).Include(C => C.CourseStatus).
                Include(c => c.CourseLevel).Include(c => c.User).
                FirstOrDefault(c => c.CourseId == courseId);
        }


        public Tuple<List<CourseComment>, int> GetCourseComment(int courseId, int pageId)
        {
            var comments = context.CourseComments.Include(c => c.User).Where(c => c.CourseId == courseId).ToList();
            int take = 3;
            int skip = (pageId - 1) * take;
            int pagecount = comments.Count / take;
            if (comments.Count % take != 0)
            {
                pagecount++;
            }
            return Tuple.Create(comments.Skip(skip).Take(take).ToList(), pagecount);
        }

        public CourseEpisode GetCourseEpisodeByEpisodeId(int episodeId)
        {
            return context.CourseEpisodes.Where(e => e.EpisodeId == episodeId).First();
        }

        public List<CourseEpisode> GetCourseEpisodesByCouresId(int courseId)
        {
            return context.CourseEpisodes.Where(c => c.CourseId == courseId).ToList();
        }

        public List<string> GetCourseForSearchBox(string title)
        {
            return context.Courses.Where(c=> c.CourseTitle.Contains(title)).Select(c=> c.CourseTitle).ToList();
        }

        public List<CourseGroup> GetCourseGroup()
        {
            return context.CourseGroups.ToList();
        }

        public CourseGroup GetCourseGroupByGroupId(int groupId)
        {
            return context.CourseGroups.Find(groupId);
        }

        public List<CourseGroup> GetCourseGroupBySubGroup()
        {
            return context.CourseGroups.Include(c => c.courseGroups).ToList();
        }

        public Tuple<List<CourseViewModel>, int> GetCourses(int pageId = 1, string filter = "", string getType = "all", string orderByType = "date", int strartPrice = 0, int endPrrice = 0, List<int> SelectedGroups = null, int take = 6)
        {
            var courses = context.Courses.Include(c => c.CourseEpisodes).ToList();
            if (!string.IsNullOrEmpty(filter))
            {
                courses = courses.Where(c => c.CourseTitle.Contains(filter) || c.Tags.Contains(filter)).ToList();
            }
            switch (getType)
            {
                case "all":
                    break;
                case "price":
                    courses = courses.Where(c => c.CoursePrice > 0).ToList();
                    break;
                case "free":
                    courses = courses.Where(c => c.CoursePrice == 0).ToList();
                    break;

                default:
                    break;
            }

            switch (orderByType)
            {
                case "date":
                    {
                        courses = courses.OrderBy(c => c.CreateDate).ToList();
                        break;
                    }
                case "updatedate":
                    {
                        courses = courses.OrderByDescending(c => c.UpdateDate).ToList();
                        break;
                    }
            }
            if (strartPrice > 0)
            {
                courses = courses.Where(c => c.CoursePrice >= strartPrice).ToList();
            }

            if (endPrrice > 0)
            {
                courses = courses.Where(c => c.CoursePrice <= endPrrice).ToList();
            }
            if (SelectedGroups != null)
            {
                foreach (var item in SelectedGroups)
                {
                    courses = courses.Where(c => c.GroupId == item || c.SubGroupId == item).ToList();
                }
            }
            int skip = (pageId - 1) * take;
            int pagecounnt = courses.Count / take;
            if (courses.Count % take != 0)
            {
                pagecounnt++;
            }
            return Tuple.Create(courses.Skip(skip).Take(take).Select(c => new CourseViewModel()
            {
                CourseId = c.CourseId,
                ImageName = c.CourseImageName,
                CoursePrice = c.CoursePrice,
                CourseTitle = c.CourseTitle,
                EpisodeTime = new TimeSpan(c.CourseEpisodes.Sum(e => e.EpisodeTime.Ticks))
            }).ToList(), pagecounnt);
        }

        public List<DataLayer.Entities.Course.Course> GetCoursesForMaster(string userName)
        {
            int userId=context.Users.Where(u=> u.UserName == userName).First().UserId;
            return context.Courses.Include(c => c.CourseStatus).Include(c => c.CourseEpisodes).Where(c => c.TeacherId == userId).ToList();
        }

        public List<SelectListItem> GetGroupsForManageCourse()
        {
            return context.CourseGroups.Where(c => c.ParentId == null).Select(c => new SelectListItem()
            {
                Value = c.GroupId.ToString(),
                Text = c.GroupTitle.ToString()
            }).ToList();
        }

        public List<CourseViewModel> GetllCourseForShowIndex()
        {
            var courses = context.Courses.Include(c => c.CourseEpisodes).ToList();
            return courses.Select(c => new CourseViewModel()
            {
                CourseId = c.CourseId,
                ImageName = c.CourseImageName,
                CourseTitle = c.CourseTitle,
                CoursePrice = c.CoursePrice,
                EpisodeTime = new TimeSpan(c.CourseEpisodes.Sum(e => e.EpisodeTime.Ticks))
            }).ToList();
        }

        public List<CourseViewModel> GetPopularCourses()
        {
            var courses = context.Courses.Include(c => c.OrderDetails).Include(c => c.CourseEpisodes).ToList();
            return courses.Where
                (c => c.OrderDetails.Any()).OrderBy(c => c.OrderDetails.Sum(od => od.Count)).Select(c => new CourseViewModel()
                {
                    CourseId = c.CourseId,
                    ImageName = c.CourseImageName,
                    CourseTitle = c.CourseTitle,
                    CoursePrice = c.CoursePrice,
                    EpisodeTime = new TimeSpan(c.CourseEpisodes.Sum(c => c.EpisodeTime.Ticks))
                }).ToList();
        }

        public List<SelectListItem> GetSubGroupForManageCourse(int GroupId)
        {
            return context.CourseGroups.Where(c => c.ParentId == GroupId).Select(c => new SelectListItem()
            {
                Value = c.GroupId.ToString(),
                Text = c.GroupTitle.ToString()
            }).ToList();
        }

        public List<SelectListItem> GetTeachers()
        {
            return context.UserRoles.Include(u => u.User).Where(u => u.RoleId == 2).Select(u => new SelectListItem()
            {
                Value = u.User.UserId.ToString(),
                Text = u.User.UserName.ToString()
            }).ToList();
        }

        public Tuple<int, int> GetVoteCount(int courseId)
        {
            int trues = context.CourseVotes.Where(c => c.CourseId == courseId && c.Vote).Count();
            int flases = context.CourseVotes.Where(c => c.CourseId == courseId && !c.Vote).Count();
            return Tuple.Create(trues, flases);
        }

        public bool IsFreeCourse(int courseId)
        {
            return context.Courses.Any(c => c.CourseId == courseId && c.CoursePrice == 0);
        }
        public bool AddCourseEpisode(CourseEpisode courseEpisode, string username)
        {
            var userid = context.Users.FirstOrDefault(u => u.UserName == username).UserId;
            var course = GetCourseById(courseEpisode.CourseId);
            if (course == null && course.TeacherId != userid)
            {
                return false;
            }
            var episode = new CourseEpisode()
            {
                EpisodeTitle = courseEpisode.EpisodeTitle,
                EpisodeTime = courseEpisode.EpisodeTime,
                CourseId = courseEpisode.CourseId,
                EpisodeFileName = courseEpisode.EpisodeFileName,
                IsFree = courseEpisode.IsFree
            };
            context.CourseEpisodes.Add(episode);
            context.SaveChanges();
            return true;


        }
    }
}
