using Application.Dtos.Enrollments;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class EnrollementService : IEnrollmentService
    {
        private readonly IEnrollementRepository _enrollementRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private readonly ILogger<EnrollementService> _logger;

        public EnrollementService(IEnrollementRepository enrollementRepository, IUnitOfWork unitOfWork, IMapper mapper, IMemoryCache cache, ILogger<EnrollementService> logger)
        {
            _enrollementRepository = enrollementRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
            _logger = logger;
        }

        public Task ApproveEnrollmentAsync(Guid enrollmentId)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> EnrollCourseAsync(Guid studentId, Guid courseId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EnrollmentDto>> GetCourseEnrollmentsAsync(Guid courseId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EnrollmentDto>> GetMyEnrollmentsAsync(Guid studentId)
        {
            throw new NotImplementedException();
        }
    }
}
