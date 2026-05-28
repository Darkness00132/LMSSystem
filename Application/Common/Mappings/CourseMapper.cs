using Application.Dtos.Courses;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings
{
    public class CourseMapper : Profile
    {
        public CourseMapper()
        {
            CreateMap<CreateCourseRequest, Course>();
            CreateMap<Course, CourseDetailsDto>();
            CreateMap<Course, CourseDto>();
        }
    }
}
