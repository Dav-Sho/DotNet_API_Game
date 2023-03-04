using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

namespace game_rpg.Services.ServiceImpl
{
    public class CharacterImpl : CharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public CharacterImpl(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
            
        }
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto addCharacterDto)
        {
            var response = new ServiceResponse<List<GetCharacterDto>>();
            var character = _mapper.Map<Character>(addCharacterDto);
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            response.Data = await _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
            response.Success = true;
            response.StatusCode = HttpStatusCode.Created;
            response.Message = "Character created to Characters table";
            return response;
        }

        public async Task<ServiceResponse<string>> DeleteCharacter(int characterId)
        {
            var response = new ServiceResponse<string>();
            var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == characterId);
            if(character is null) {
                response.StatusCode = HttpStatusCode.NotFound;
                response.Success = false;
                response.Message = $"Character with the id {characterId} not found";
                return response;
            }

            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();

                response.StatusCode = HttpStatusCode.OK;
                response.Message = $"Character with the id {characterId} deleted";
                return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacter()
        {
            var response = new ServiceResponse<List<GetCharacterDto>>();
            var characters = await _context.Characters.ToListAsync();
            response.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            response.StatusCode = HttpStatusCode.OK;
            response.Message = "Get all Character from Characters table";
            return response;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacter(int characterId)
        {
            var response = new ServiceResponse<GetCharacterDto>();
            var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == characterId);
            if(character is null) {
                response.StatusCode = HttpStatusCode.NotFound;
                response.Success = false;
                response.Message = $"Character with the id {characterId} not found";
                return response;
            }

            response.Data = _mapper.Map<GetCharacterDto>(character);
            response.StatusCode = HttpStatusCode.OK;
            response.Message = $"Character found with the id of {characterId}";
            return response;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(int characterId, AddCharacterDto characterDto)
        {
            var response = new ServiceResponse<GetCharacterDto>();

            var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == characterId);

            if(character is null) {
                response.StatusCode = HttpStatusCode.NotFound;
                response.Success = false;
                response.Message = $"Character with the id {characterId} not found";
                return response;
            }

            character.Name = characterDto.Name;
            character.Strength = characterDto.Strength;
            character.Defense = characterDto.Defense;
            character.HitPoints = characterDto.HitPoints;
            character.Intelligence = characterDto.Intelligence;
            character.rpgClass = characterDto.rpgClass;

            await _context.SaveChangesAsync();

            response.Data = _mapper.Map<GetCharacterDto>(character);
            response.StatusCode = HttpStatusCode.OK;
            response.Message = $"Character found with the id of {characterId} upgrade";
            return response;
        }
    }
}