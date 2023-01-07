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
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();

            serviceResponse.Data = await _context.Characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToListAsync();
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
                var dbcharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == updatecharacter.Id);
                if (dbcharacter is null) throw new Exception($"Character with Id '{updatecharacter.Id}' not found");
                dbcharacter.Name = updatecharacter.Name;
                dbcharacter.Hitpoints = updatecharacter.Hitpoints;
                dbcharacter.Strength = updatecharacter.Strength;
                dbcharacter.Defence = updatecharacter.Defence;
                dbcharacter.Intelligence = updatecharacter.Intelligence;
                dbcharacter.Class = updatecharacter.Class;
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetCharacterDTO>(dbcharacter);
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
                var dbcharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
                if (dbcharacter is null) throw new Exception($"Character with Id '{id}' not found");
                characters.Remove(dbcharacter);
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