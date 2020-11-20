using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Utility.Helper;

namespace EncroachmentDemolition.Components
{
    public class PagerViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int totalCount, int pageNumber, int pageSize = 10)
        {

            PagerHelper pageHelper = new PagerHelper(totalCount, pageNumber, pageSize);
            await Task.Run(() =>
            {

            });
            return View("Pager", pageHelper);
        }
    }
}
