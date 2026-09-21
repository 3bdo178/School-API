using AutoMapper;
using Practical_Exam.Dtos;
using Practical_Exam.Models;

namespace Practical_Exam.Profiles
{
    public class TeacherProfile : Profile
    {
        public TeacherProfile()
        {
            CreateMap<Teacher, TeacherDto>()
                .ForMember(dis=>dis.FullName,
                  opt => opt.MapFrom(src=> $"{src.FirstName} {src.LastName}"));

            CreateMap<CreateTeacherDto, Teacher>()
                .ForMember(dest => dest.FirstName,
                opt => opt.MapFrom(src => src.FullName.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)[0]))
             .ForMember(dest => dest.LastName,
                opt => opt.MapFrom(src => src.FullName.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)[1]));
            CreateMap<UpdateTeacherDto, Teacher>().ForMember(dist => dist.FirstName,
                opt => opt.MapFrom(src => src.FullName.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0]))
                .ForMember(dist => dist.LastName,
                opt => opt.MapFrom(src => src.FullName.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1]));
        }
    }
}
