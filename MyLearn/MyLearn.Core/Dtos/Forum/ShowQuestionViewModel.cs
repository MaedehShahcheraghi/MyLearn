using MyLearn.DataLayer.Entities.Forum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLearn.Core.Dtos.Forum
{
    public class ShowQuestionViewModel
    {

        public Question question { get; set; }
        public List<Answer>? Answers { get; set; }
    }
}
