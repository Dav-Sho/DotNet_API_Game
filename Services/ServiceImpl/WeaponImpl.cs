using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace game_rpg.Services.ServiceImpl
{
    public class WeaponImpl : WeaponService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;

        public WeaponImpl(DataContext context, IHttpContextAccessor contextAccessor, IMapper mapper)
        {
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _context = context;
            
        }
        public async Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto addWeaponDto)
        {
            var response = new ServiceResponse<GetCharacterDto>();

            try{
                var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == addWeaponDto.CharacterId && c.Users!.Id == int.Parse(_contextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!));

                if(character is null) {
                    response.Success = false;
                    response.Message = "Character not fond.";
                    return response;
                }

                var weapon = new Weapon{
                    Name = addWeaponDto.Name,
                    Damage = addWeaponDto.Damage,
                    Character = character
                };

                _context.Weapon.Add(weapon);
                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetCharacterDto>(character);
                response.StatusCode = HttpStatusCode.Created;
                response.Message = "Weapon Created";
                return response;
            }catch(Exception ex) {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }
    }
}