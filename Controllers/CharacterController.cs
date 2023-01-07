using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg2.Dtos.Character;
using dotnet_rpg2.Models;
using dotnet_rpg2.Services.CharacterService;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {

        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDTO>>>> Get()
        {
            return Ok(await _characterService.GetAllCharacters());
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDTO>>> GetById(int id)
        {

            return Ok(await _characterService.GetCharacterById(id));

        }

        [HttpPost("createNewCharacter")]

        public async Task<ActionResult<ServiceResponse<List<GetCharacterDTO>>>> CreateCharacter(AddCharacterDTO newcharacter)
        {

            return Ok(await _characterService.CreateCharacter(newcharacter));
        }
        [HttpPut("updateCharacter")]

        public async Task<ActionResult<ServiceResponse<List<GetCharacterDTO>>>> UpdateCharacter(UpdateCharacterDTO updatecharacter)
        {

            var response = await _characterService.UpdateCharacter(updatecharacter);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok();
        }
        [HttpDelete("deleteCharacter")]

        public async Task<ActionResult<ServiceResponse<List<GetCharacterDTO>>>> DeleteCharacter(int id)
        {

            var response = await _characterService.DeleteCharacter(id);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok();
        }
    }
}