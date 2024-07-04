using AutoMapper;
using HelloDoc_Entities.DataModels;
using HelloDoc_Entities.DTOs.Request;

namespace HelloDoc_BusinessAccessLayer.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Register Patient
            CreateMap<SubmitRegisterPatientRequest, User>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Zip, opt => opt.MapFrom(src => src.Zip))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.Ignore())
                .ForMember(dest => dest.OTP, opt => opt.Ignore())
                .ForMember(dest => dest.OtpExpiryTime, opt => opt.Ignore())
                .ForMember(dest => dest.Gender, opt => opt.Ignore())
                .ForMember(dest => dest.Avatar, opt => opt.Ignore());

            CreateMap<SubmitRegisterPatientRequest, PatientDetails>()
           .ForMember(dest => dest.EmergencyContactName, opt => opt.MapFrom(src => src.EmergencyContactName))
           .ForMember(dest => dest.EmergencyContactNumber, opt => opt.MapFrom(src => src.EmergencyContactNumber))
           .ForMember(dest => dest.MedicalHistory, opt => opt.MapFrom(src => src.MedicalHistory))
           .ForMember(dest => dest.Allergies, opt => opt.MapFrom(src => src.Allergies))
           .ForMember(dest => dest.CurrentMedications, opt => opt.MapFrom(src => src.CurrentMedications))
           .ForMember(dest => dest.BloodTypeId, opt => opt.MapFrom(src => src.BloodTypeId))
           .ForMember(dest => dest.Document, opt => opt.Ignore())
           .ForMember(dest => dest.UserId, opt => opt.Ignore())
           .ForMember(dest => dest.Users, opt => opt.Ignore())
           .ForMember(dest => dest.BloodType, opt => opt.Ignore());

        }
    }
}