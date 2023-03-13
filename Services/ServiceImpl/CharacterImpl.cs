using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace game_rpg.Services.ServiceImpl
{
    public class CharacterImpl : CharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        public CharacterImpl(IMapper mapper, DataContext context, IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            _context = context;
            _mapper = mapper;
            
        }

        private int GetUserId() => int.Parse(_contextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto addCharacterDto)
        {
            var response = new ServiceResponse<List<GetCharacterDto>>();
            var character = _mapper.Map<Character>(addCharacterDto);
            character.Users = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            response.Data = await _context.Characters.Where(u => u.Id == GetUserId()).Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
            response.Success = true;
            response.StatusCode = HttpStatusCode.Created;
            response.Message = "Character created to Characters table";
            return response;
        }

        public async Task<ServiceResponse<string>> DeleteCharacter(int characterId)
        {
            var response = new ServiceResponse<string>();

            var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == characterId && c.Users!.Id == GetUserId());
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
            var characters = await _context.Characters.Where(c => c.Users!.Id == GetUserId()).ToListAsync();
            response.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            response.StatusCode = HttpStatusCode.OK;
            response.Message = "Get all Character from Characters table";
            return response;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacter(int characterId)
        {
            var response = new ServiceResponse<GetCharacterDto>();
            var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == characterId && c.Users!.Id == GetUserId());
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

            var character = await _context.Characters.Include(c => c.Users).FirstOrDefaultAsync(c => c.Id == characterId && c.Users!.Id == GetUserId());

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

        public async Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto characterSkillDto)
        {
            var response = new ServiceResponse<GetCharacterDto>();

            try{
                var character = await _context.Characters.Include(c => c.Weapon).Include(c => c.Skills).FirstOrDefaultAsync(c => c.Id == characterSkillDto.CharacterId && c.Users!.Id == GetUserId());

                if(character is null) {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Character not found";
                    return response;
                }

                var skill = await _context.Skill.FirstOrDefaultAsync(s => s.Id == characterSkillDto.SkillId);

                if(skill is null) {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Skill not found";

                    return response;
                }

                character.Skills!.Add(skill);
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetCharacterDto>(character);
                response.StatusCode = HttpStatusCode.Created;
                response.Message = "Skill Created";
                return response;


            }catch(Exception ex) {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }
    }
}