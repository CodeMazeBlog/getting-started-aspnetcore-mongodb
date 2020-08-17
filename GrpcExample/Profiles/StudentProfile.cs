using AutoMapper;
namespace GrpcExample.Profiles
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Models.Course, StudentGrpcService.Course>();
            CreateMap<Models.Student, StudentGrpcService.Student>()
                .ForMember(dest => dest.Courses, opt => opt.MapFrom(src => src.CourseList));
        }
    }
}