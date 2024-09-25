using MyLearn.Core.Dtos.Forum;
using MyLearn.DataLayer.Entities.Forum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLearn.Core.Service.ForumService
{
    public interface IForumService
    {
        #region Qusetion
        int AddQuestion(Question question);
        ShowQuestionViewModel GetQusetionByQuestionId(int questionId);
        List<Question> GetAllQuestions(int? courseId);
        List<Question> GetPopularQuestion();
        Question GetQuestionForEdit(int questionId);
        void EditQuestion(Question question);
        #endregion
        #region Answers
        int AddAnswer(int QuestionId, string AnswerBody, int userId);
        Answer GetAnswerByAnswerId(int answerId);
        void EditAnswer(Answer answer);
        #endregion



    }
}
