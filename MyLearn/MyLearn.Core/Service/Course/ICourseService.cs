using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyLearn.Core.Dtos.Admin.Course;
using MyLearn.DataLayer.Entities.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLearn.Core.Service.Course
{
    public interface ICourseService
    {
        #region Admin
        List<SelectListItem> GetGroupsForManageCourse();
        List<SelectListItem> GetSubGroupForManageCourse(int GroupId);
        List<SelectListItem> GetAllStatus();
        List<SelectListItem> GetAllLevel();
        List<SelectListItem> GetTeachers();
        int AddCourse(MyLearn.DataLayer.Entities.Course.Course course, IFormFile ImageDemoUp, IFormFile FileDemoFile);
        List<ShowCourseForAdminViewModel> GetAllCourse();
        void EditCourse(MyLearn.DataLayer.Entities.Course.Course course, IFormFile ImageDemoUp, IFormFile FileDemoFile);
        MyLearn.DataLayer.Entities.Course.Course GetCourseByCourseId(int courseId);
        List<CourseEpisode> GetCourseEpisodesByCouresId(int courseId);
        int AddCourseEpisode(CourseEpisode courseEpisode,IFormFile epispdeFile);  
        CourseEpisode GetCourseEpisodeByEpisodeId(int episodeId);
        void EditCourseEpisode(CourseEpisode courseEpisode, IFormFile epispdeFile);
        int AddCourseGroup(CourseGroup courseGroup);
        int EditCourseGroup(CourseGroup courseGroup);
        CourseGroup GetCourseGroupByGroupId(int groupId);   
        #endregion
        #region Comment
        int AddComment(int courseId,string comment,string userName);
        Tuple<List<CourseComment>,int> GetCourseComment(int courseId, int pageId);
        #endregion
        #region CourseVote
        void AddVote(int courseId, string userName, bool vote);
        Tuple<int, int> GetVoteCount(int courseId);
        #endregion
        #region Master
        List<MyLearn.DataLayer.Entities.Course.Course> GetCoursesForMaster(string userName);
        #endregion
        List<CourseViewModel> GetllCourseForShowIndex();
        Tuple<List<CourseViewModel>, int> GetCourses(int pageId=1, string filter = "", string getType = "all", string orderByType = "date",
          int strartPrice = 0, int endPrrice = 0, List<int> SelectedGroups=null, int take = 6);
        List<CourseGroup> GetCourseGroup();
        MyLearn.DataLayer.Entities.Course.Course GetCourseById(int courseId);

        List<CourseViewModel> GetPopularCourses();
        List<CourseGroup> GetCourseGroupBySubGroup();
        bool IsFreeCourse(int courseId);
        List<string> GetCourseForSearchBox(string title);
         bool AddCourseEpisode(CourseEpisode courseEpisode, string username);

    }
}
