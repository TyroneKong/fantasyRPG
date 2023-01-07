using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg2.Dtos.Character;
using dotnet_rpg2.Models;

namespace dotnet_rpg2
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDTO>();
            CreateMap<AddCharacterDTO, Character>();
        }
    }
}