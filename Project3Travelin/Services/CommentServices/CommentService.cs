using AutoMapper;
using MongoDB.Driver;
using Project3Travelin.Dtos.CommentDtos;
using Project3Travelin.Entities;
using Project3Travelin.Settings;

namespace Project3Travelin.Services.CommentServices
{
    public class CommentService : ICommentService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Comment> _commentCollection;

        public CommentService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _commentCollection = database.GetCollection<Comment>(_databaseSettings.CommentCollectionName);
            _mapper = mapper;
            
        }
        public async Task CreateComment(CreateCommentDto createCommentDto)
        {
            var value = _mapper.Map<Comment>(createCommentDto);
            await _commentCollection.InsertOneAsync(value);
        }

        public async Task DeleteComment(string id)
        {
            await _commentCollection.DeleteOneAsync(x => x.CommentId == id);
        }

        public async Task<List<ResultCommentDto>> GetAllCommentsAsync()
        {
            var values = await _commentCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultCommentDto>>(values);
        }

        public async Task<GetCommentByIdDto> GetCommentById(string id)
        {
            var value = await _commentCollection.Find(x => x.CommentId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetCommentByIdDto>(value);
        }

        public async Task UpdateComment(UpdateCommentDto updateCommentDto)
        {
            var value = _mapper.Map<Comment>(updateCommentDto);
            await _commentCollection.FindOneAndReplaceAsync(x => x.CommentId == updateCommentDto.CommentId, value);
        }
    }
}
