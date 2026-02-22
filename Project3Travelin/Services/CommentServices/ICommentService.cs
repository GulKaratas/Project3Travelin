using Project3Travelin.Dtos.CommentDtos;

namespace Project3Travelin.Services.CommentServices
{
    public interface ICommentService
    {
        Task<List<ResultCommentDto>> GetAllCommentsAsync();
        Task<GetCommentByIdDto> GetCommentById(string id);
        Task CreateComment(CreateCommentDto createCommentDto);
        Task UpdateComment(UpdateCommentDto updateCommentDto);
        Task DeleteComment(string id);
    }
}
