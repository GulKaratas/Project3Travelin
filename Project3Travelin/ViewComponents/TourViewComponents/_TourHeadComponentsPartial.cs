using Microsoft.AspNetCore.Mvc;

namespace Project3Travelin.ViewComponents.TourViewComponents
{
    public class _TourHeadComponentsPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
