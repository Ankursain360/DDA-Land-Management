using Dto.Component;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CourtCasesManagement.Components
{
    public class PageHeaderViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string pageName)
        {
            await Task.Run(() =>
            {

            });
            return View("PageHeader", new PageHeaderDto() {
                PageName = pageName
            });
        }

    }
}
