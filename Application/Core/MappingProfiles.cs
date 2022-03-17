using Application.DTOs;
using AutoMapper;
using Domain;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Domain.Task, Domain.Task>();
            CreateMap<TaskDTO, Domain.Task>();
        }
    }
}