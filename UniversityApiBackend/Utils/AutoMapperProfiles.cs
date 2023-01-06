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
            CreateMap<Course, CourseResDTO>().ReverseMap();
        }
    }
}
