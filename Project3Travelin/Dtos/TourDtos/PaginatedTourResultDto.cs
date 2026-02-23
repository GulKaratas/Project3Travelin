namespace Project3Travelin.Dtos.TourDtos
{
    public class PaginatedTourResultDto
    {
        public List<ResultTourDto> Tours { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
    }
}
