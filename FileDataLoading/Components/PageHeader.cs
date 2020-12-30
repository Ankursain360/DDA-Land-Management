using Dto.Component;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FileDataLoading.Components
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
