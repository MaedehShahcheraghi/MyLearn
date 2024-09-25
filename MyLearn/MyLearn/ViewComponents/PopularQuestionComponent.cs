using Microsoft.AspNetCore.Mvc;
using MyLearn.Core.Service.ForumService;

namespace MyLearn.ViewComponents
{
    public class PopularQuestionComponent:ViewComponent
    {
        private readonly IForumService forumService;

        public PopularQuestionComponent(IForumService forumService)
        {
            this.forumService = forumService;
        }

        public  async Task<IViewComponentResult> InvokeAsync() {

            return await Task.FromResult((IViewComponentResult)View("PopularQuestionComponent", forumService.GetPopularQuestion()));     
        }
    }
}
