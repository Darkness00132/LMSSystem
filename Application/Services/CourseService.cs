using Application.Common.Exceptions;
using Application.Dtos.Courses;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CourseService> _logger;
        private readonly IMemoryCache _cache;

        private const string PublishedCoursesCacheKey = "published_courses";

        private static readonly TimeSpan CourseCacheDuration =
            TimeSpan.FromMinutes(10);

        private static readonly TimeSpan CoursesCacheDuration =
            TimeSpan.FromMinutes(1);

        public CourseService(
            ICourseRepository courseRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<CourseService> logger,
            IMemoryCache cache)
        {
            _courseRepository = courseRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _cache = cache;
        }

        public async Task<Guid> CreateCourseAsync(CreateCourseRequest request)
        {
            _logger.LogInformation(
                "Creating course with title: {Title}",
                request.Title);

            var course = _mapper.Map<Course>(request);

            course.Id = Guid.NewGuid();
            course.Status = CourseStatus.Draft;

            await _courseRepository.CreateAsync(course);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation(
                "Course created successfully. CourseId: {CourseId}",
                course.Id);

            return course.Id;
        }

        public async Task<CourseDetailsDto> GetPublishedCourseAsync(Guid courseId)
        {
            var cacheKey = GetPublishedCourseCacheKey(courseId);

            if (_cache.TryGetValue(cacheKey, out CourseDetailsDto? cachedCourse)
                && cachedCourse is not null)
            {
                _logger.LogInformation(
                    "Published course retrieved from cache. CourseId: {CourseId}",
                    courseId);

                return cachedCourse;
            }

            _logger.LogInformation(
                "Fetching published course from database. CourseId: {CourseId}",
                courseId);

            var course = await _courseRepository.GetPublishedCourseAsync(courseId);

            if (course is null)
            {
                _logger.LogWarning(
                    "Published course not found. CourseId: {CourseId}",
                    courseId);

                throw new NotFoundException("Course not found.");
            }

            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = CourseCacheDuration
            };

            _cache.Set(cacheKey, course, cacheOptions);

            return course;
        }

        public async Task<IEnumerable<CourseDto>> GetPublishedCoursesAsync()
        {
            if (_cache.TryGetValue(
                    PublishedCoursesCacheKey,
                    out IEnumerable<CourseDto>? cachedCourses)
                && cachedCourses is not null)
            {
                _logger.LogInformation(
                    "Published courses retrieved from cache");

                return cachedCourses;
            }

            _logger.LogInformation(
                "Fetching published courses from database");

            var courses = await _courseRepository.GetPublishedCoursesAsync();

            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = CoursesCacheDuration
            };

            _cache.Set(
                PublishedCoursesCacheKey,
                courses,
                cacheOptions);

            return courses;
        }

        public async Task UpdateCourseAsync(
            Guid courseId,
            UpdateCourseRequest request)
        {
            var course = await GetCourseOrThrow(courseId);

            _mapper.Map(request, course);

            await _unitOfWork.SaveChangesAsync();

            RemoveCourseCache(courseId);

            _logger.LogInformation(
                "Course updated successfully. CourseId: {CourseId}",
                courseId);
        }

        public async Task SubmitCourseToAdminAsync(Guid courseId)
        {
            var course = await GetCourseOrThrow(courseId);

            course.Status = CourseStatus.UnderReview;

            await _unitOfWork.SaveChangesAsync();

            RemoveCourseCache(courseId);

            _logger.LogInformation(
                "Course submitted for review. CourseId: {CourseId}",
                courseId);
        }

        public async Task ReviewCourseAsync(
            Guid courseId,
            ReviewCourseRequest request)
        {
            var course = await GetCourseOrThrow(courseId);

            course.Status = request.Status;

            await _unitOfWork.SaveChangesAsync();

            RemoveCourseCache(courseId);

            _logger.LogInformation(
                "Course reviewed successfully. CourseId: {CourseId}, Status: {Status}",
                courseId,
                request.Status);
        }

        public async Task UnpublishCourseAsync(Guid courseId)
        {
            var course = await GetCourseOrThrow(courseId);

            course.Status = CourseStatus.Draft;

            await _unitOfWork.SaveChangesAsync();

            RemoveCourseCache(courseId);

            _logger.LogInformation(
                "Course unpublished successfully. CourseId: {CourseId}",
                courseId);
        }

        private async Task<Course> GetCourseOrThrow(Guid courseId)
        {
            var course = await _courseRepository.GetByIdAsync(courseId);

            if (course is null)
            {
                _logger.LogWarning(
                    "Course not found. CourseId: {CourseId}",
                    courseId);

                throw new NotFoundException("Course not found.");
            }

            return course;
        }

        private void RemoveCourseCache(Guid courseId)
        {
            var cacheKey = GetPublishedCourseCacheKey(courseId);

            _cache.Remove(cacheKey);

            _logger.LogInformation(
                "Course cache removed. CourseId: {CourseId}",
                courseId);
        }

        private static string GetPublishedCourseCacheKey(Guid courseId)
        {
            return $"published_course_{courseId}";
        }
    }
}