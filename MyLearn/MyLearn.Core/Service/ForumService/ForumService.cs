using Microsoft.EntityFrameworkCore;
using MyLearn.Core.Dtos.Forum;
using MyLearn.DataLayer.Context;
using MyLearn.DataLayer.Entities.Forum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLearn.Core.Service.ForumService
{
    public class ForumService : IForumService
    {
        private readonly MyLearnContext context;

        public ForumService(MyLearnContext context)
        {
            this.context = context;
        }

        public int AddAnswer(int QuestionId, string AnswerBody,int userId)
        {
            var answer = new Answer()
            {
                QuestionId = QuestionId,
                AnswerBody = AnswerBody,
                CreateAnswer = DateTime.Now,
                UserId = userId

            };
            context.Answers.Add(answer);
            context.SaveChanges();
            return answer.AnswerId;
        }

        public int AddQuestion(Question question)
        {
            context.Questions.Add(question);    
            context.SaveChanges();
            return question.QuestionId;
        }

        public void EditAnswer(Answer answer)
        {
            context.Answers.Update(answer); 
            context.SaveChanges();
        }

        public void EditQuestion(Question question)
        {
            context.Questions.Update(question);
            context.SaveChanges();
        }

        public List<Question> GetAllQuestions(int? courseId)
        {
            if (courseId!=null)
            {
                return context.Questions.Include(q => q.User).Include(q => q.Course).Where(q => q.CourseId == courseId).ToList();
            }
            return context.Questions.Include(q => q.User).Include(q=> q.Course).ToList();
        }

        public Answer GetAnswerByAnswerId(int answerId)
        {
            return context.Answers.FirstOrDefault(a => a.AnswerId == answerId);
        }

        public List<Question> GetPopularQuestion()
        {
            return context.Questions.Include(q=> q.Answers).OrderByDescending(q=> q.CreateQusetion).ToList();
        }

        public Question GetQuestionForEdit(int questionId)
        {
            return context.Questions.Where(q => q.QuestionId == questionId).FirstOrDefault();
        }

        public ShowQuestionViewModel GetQusetionByQuestionId(int questionId)
        {
            var qestion=context.Questions.Include(q => q.User).Where(q=> q.QuestionId==questionId).FirstOrDefault();   
            var answers=context.Answers.Where(a=> a.QuestionId==questionId).ToList();
            return new ShowQuestionViewModel()
            {
                Answers = answers,
                question=qestion
            };
        }
    }
}
