using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg2.Dtos.Character;
using dotnet_rpg2.Models;

namespace dotnet_rpg2.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<GetCharacterDTO>>> GetAllCharacters();
        Task<ServiceResponse<GetCharacterDTO>> GetCharacterById(int id);
        Task<ServiceResponse<List<GetCharacterDTO>>> CreateCharacter(AddCharacterDTO newCharacter);
        Task<ServiceResponse<GetCharacterDTO>> UpdateCharacter(UpdateCharacterDTO updateCharacter);
        Task<ServiceResponse<List<GetCharacterDTO>>> DeleteCharacter(int id);

    }
}