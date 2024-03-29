﻿using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.DTO
{
    public class CategoryResDTO : CategoryReqDTO
    {
        public int Id { get; set; }
        public virtual ICollection<CourseMinDTO> Courses { get; set; } = new List<CourseMinDTO>();
    }
}
