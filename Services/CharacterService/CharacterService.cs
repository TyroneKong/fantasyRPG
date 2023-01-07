using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg2.Data;
using dotnet_rpg2.Dtos.Character;
using dotnet_rpg2.Models;

namespace dotnet_rpg2.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {

        private static List<Character> characters = new List<Character>{
            new Character(),
            new Character {
                Id = 1, Name = "Sauron",
                Defence = 100,
                Class = RpgClass.Mage
            }

        };

        private readonly IMapper _mapper;

        private readonly DataContext _context;
        public CharacterService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> CreateCharacter(AddCharacterDTO newCharacter)
        {

            var serviceResponse = new ServiceResponse<List<GetCharacterDTO>>();
            var character = _mapper.Map<Character>(newCharacter);
            character.Id = characters.Max(c => c.Id) + 1;
            characters.Add(character);
            serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDTO>>();
            var dbCharacters = await _context.Characters.ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDTO>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDTO>();
            var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
            serviceResponse.Data = _mapper.Map<GetCharacterDTO>(dbCharacter);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDTO>> UpdateCharacter(UpdateCharacterDTO updatecharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDTO>();

            try
            {
                var character = characters.FirstOrDefault(c => c.Id == updatecharacter.Id);
                if (character is null) throw new Exception($"Character with Id '{updatecharacter.Id}' not found");
                character.Name = updatecharacter.Name;
                character.Hitpoints = updatecharacter.Hitpoints;
                character.Strength = updatecharacter.Strength;
                character.Defence = updatecharacter.Defence;
                character.Intelligence = updatecharacter.Intelligence;
                character.Class = updatecharacter.Class;

                serviceResponse.Data = _mapper.Map<GetCharacterDTO>(character);
            }
            catch (Exception ex)
            {
                serviceResponse.success = false;
                serviceResponse.message = ex.Message;
            }

            return serviceResponse;

        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDTO>>();

            try
            {
                var character = characters.FirstOrDefault(c => c.Id == id);
                if (character is null) throw new Exception($"Character with Id '{id}' not found");
                characters.Remove(character);
                serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.success = false;
                serviceResponse.message = ex.Message;
            }

            return serviceResponse;

        }

    }




}