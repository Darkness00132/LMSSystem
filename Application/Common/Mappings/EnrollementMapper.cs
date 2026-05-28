using Application.Dtos.Enrollments;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings
{
    public class EnrollementMapper : Profile
    {
        public EnrollementMapper()
        {
            CreateMap<Enrollment, EnrollmentDto>();
        }
    }
}
