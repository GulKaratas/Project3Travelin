using AutoMapper;
using MongoDB.Driver;
using Project3Travelin.Dtos.TourDtos;
using Project3Travelin.Entities;
using Project3Travelin.Settings;

namespace Project3Travelin.Services.TourServices
{
    public class TourService : ITourService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Tour> _tourCollection;

        public TourService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _tourCollection = database.GetCollection<Tour>(_databaseSettings.TourCollectionName);
            _mapper = mapper;
           
        }
        public async Task CreateTourAsync(CreateTourDto createTourDto)
        {
            var tour = _mapper.Map<Tour>(createTourDto);
            await _tourCollection.InsertOneAsync(tour);
        }

        public async Task DeleteTourAsync(string id)
        {
            await _tourCollection.DeleteOneAsync(x=>x.TourId == id);
        }

        public async Task<List<ResultTourDto>> GetAllToursAsync()
        {
            var values = await _tourCollection.Find(tour => true).ToListAsync();
            return _mapper.Map<List<ResultTourDto>>(values);
        }

        public async Task<PaginatedTourResultDto> GetPaginatedToursAsync(int page, int pageSize)
        {
            var totalCount = await _tourCollection.CountDocumentsAsync(tour => true);
            var tours = await _tourCollection.Find(tour => true)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();

            return new PaginatedTourResultDto
            {
                Tours = _mapper.Map<List<ResultTourDto>>(tours),
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                TotalCount = (int)totalCount,
                PageSize = pageSize
            };
        }

        public async Task<GetTourByIdDto> GetTourByIdAsync(string id)
        {
            var values = await _tourCollection.Find(tour => tour.TourId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetTourByIdDto>(values);
        }

        public async Task UpdateTourAsync(UpdateTourDto updateTourDto)
        {
            var values = _mapper.Map<Tour>(updateTourDto);
            await _tourCollection.FindOneAndReplaceAsync(x => x.TourId == updateTourDto.TourId, values);
            
        }
    }
}
