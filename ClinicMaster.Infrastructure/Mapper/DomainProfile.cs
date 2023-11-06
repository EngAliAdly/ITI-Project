using AutoMapper;
using ClinicMaster.Core.Dto;
using ClinicMaster.Core.Models;

namespace ClinicMaster.Web.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Patient, PatientDto>();
            CreateMap<City, CityDto>();
            CreateMap<Doctor, DoctorDto>();
            CreateMap<Specialization, SpecializationDto>();
        }
    }
}
