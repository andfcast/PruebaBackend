using System.Reflection.Metadata;
using WebApiEntities.Models;
using WebApiEntities.DTO;

namespace Utilities
{
    public static class Converter
    {
        public static User ConvertToEntity(UserDto dto) {
            return new User
            {
                Id = dto.id,
                Email = dto.email,
                FirstName = dto.first_name,
                LastName = dto.last_name,
                Avatar = dto.avatar,
            };
        }

        public static UserDto ConvertToDto(User entity)
        {
            return new UserDto
            {
                id = Convert.ToInt32(entity.Id),
                email = entity.Email,
                first_name = entity.FirstName,
                last_name = entity.LastName,
                avatar = entity.Avatar,
            };
        }

    }
}