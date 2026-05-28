using Application.Dtos.Courses;
using Application.Interfaces.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CourseRepository(
            AppDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateAsync(Course course)
        {
            await _context.Courses.AddAsync(course);
        }

        public async Task<Course?> GetByIdAsync(Guid id)
        {
            return await _context.Courses
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<CourseDetailsDto?> GetPublishedCourseAsync(Guid courseId)
        {
            return await _context.Courses
                .AsNoTracking()
                .Where(c =>
                    c.Id == courseId &&
                    c.Status == CourseStatus.Published)
                .ProjectTo<CourseDetailsDto>(
                    _mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CourseDto>> GetPublishedCoursesAsync()
        {
            return await _context.Courses
                .AsNoTracking()
                .Where(c => c.Status == CourseStatus.Published)
                .ProjectTo<CourseDto>(
                    _mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}