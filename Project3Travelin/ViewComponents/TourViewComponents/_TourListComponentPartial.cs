using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Services.TourServices;

namespace Project3Travelin.ViewComponents.TourViewComponents
{
    public class _TourListComponentPartial : ViewComponent
    {
        private readonly ITourService _tourService;

        public _TourListComponentPartial(ITourService tourService)
        {
            _tourService = tourService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int pageSize = 3)
        {
            int page = 1;
            if (HttpContext.Request.Query.ContainsKey("page"))
            {
                int.TryParse(HttpContext.Request.Query["page"], out page);
                if (page < 1) page = 1;
            }
            
            var paginatedTours = await _tourService.GetPaginatedToursAsync(page, pageSize);
            return View(paginatedTours);
        }
    }
}