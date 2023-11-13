using AutoMapper;
using ClinicMaster.Core.Dto;
using ClinicMaster.Core.Models;
using ClinicMaster.Core.ViewModel;

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
            CreateMap<AppointmentViewNodel, Appointment>().ReverseMap()
                .ForPath(dest => dest.Token, source => source.MapFrom(src => src.Patient.Token))
                .ForPath(dest => dest.Name, source => source.MapFrom(src => src.Patient.Name))
                .ForPath(dest => dest.Phone, source => source.MapFrom(src => src.Patient.Phone))
                .ForPath(dest => dest.DoctorName, source => source.MapFrom(src => src.Doctor.Name));
        }
    }
}
