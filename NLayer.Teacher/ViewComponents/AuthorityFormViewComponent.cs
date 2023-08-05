using Microsoft.AspNetCore.Mvc;

namespace NLayer.Teacher.ViewComponents
{
    public class AuthorityFormViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
