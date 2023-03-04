using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace game_rpg.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Character: ControllerBase
    {
        private readonly CharacterService _characterService;

        // dependency Injection
     public Character(CharacterService characterService)
     {
        _characterService = characterService;
        
     }

    [HttpGet("GetAll")]
     public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> GetCharacters() {
        return Ok(await _characterService.GetAllCharacter());
     }

     [HttpGet]
     public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetCharacter(int characterId) {
        return Ok(await _characterService.GetCharacter(characterId));
     }

    [HttpPost]
     public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddCharacter(AddCharacterDto characterDto) {
        return Ok(await _characterService.AddCharacter(characterDto));
     }

     [HttpPut]
     public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> UpdateCharacter(int characterId, AddCharacterDto characterDto) {
        return Ok(await _characterService.UpdateCharacter(characterId, characterDto));
     }

     [HttpDelete]
     public async Task<ActionResult<ServiceResponse<string>>> DeleteCharacter(int characterId) {
        return Ok(await _characterService.DeleteCharacter(characterId));
     }

    }
}