using AutoMapper;
using UniversityApiBackend.DTO;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Utils
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Category, CategoryResDTO>().ReverseMap();
            CreateMap<CategoryReqDTO, Category>();
            CreateMap<Category, CategoryMinDTO>();
            CreateMap<Course, CourseResDTO>().ReverseMap();
            CreateMap<Course, CourseMinDTO>().ReverseMap();
            CreateMap<CourseReqDTO, Course>();
            CreateMap<Student, StudentResDTO>().ReverseMap();
            CreateMap<Student, StudentMinDTO>();
        }
    }
}
