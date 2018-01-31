using System;
using AutoMapper;
using BaseAPI.DataTransferObject;
using BaseAPI.Models;

namespace BaseAPI.Common
{
    public class AutomapperConfigurationProfile:Profile
    {
        public AutomapperConfigurationProfile(){
            CreateMap<Student,StudentDto>();
            CreateMap<StudentDto, Student>();
        }
        
    }
}
