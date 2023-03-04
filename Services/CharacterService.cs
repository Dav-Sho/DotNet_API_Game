using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace game_rpg.Services
{
    public interface CharacterService
    {
        Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacter();
        Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto addCharacterDto);
        Task<ServiceResponse<GetCharacterDto>> GetCharacter(int characterId);

        Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(int characterId, AddCharacterDto characterDto);
        Task<ServiceResponse<string>> DeleteCharacter(int characterId);
    }
}