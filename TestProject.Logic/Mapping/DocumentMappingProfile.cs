using AutoMapper;
using TestProject.Logic.Services.Document.Models;
using TestProject.Repositories.Entities;

namespace TestProject.Logic.Mapping;

public class DocumentMappingProfile : Profile
{
    public DocumentMappingProfile()
    {
        CreateMap<DocumentModel, DbDocument>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(c => c.UserId))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(c => c.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(c => c.LastName))
            .ForMember(dest => dest.ContractNumber, opt => opt.MapFrom(c => c.ContractNumber))
            .ForMember(dest => dest.WithdrawalAmount, opt => opt.MapFrom(c => c.WithdrawalAmount))
            .ReverseMap();
    }
}

