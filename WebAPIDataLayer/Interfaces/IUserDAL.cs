using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiEntities;
using WebApiEntities.DTO;

namespace WebAPIDataLayer.Interfaces
{
    /// <summary>
    /// Interface de las operaciones de acceso a datos
    /// </summary>
    public interface IUserDAL
    {
        List<UserDto> GetAll(PageInfoDto info);
        UserDto GetInfo(int id);

        bool SaveToDB(ExternalApiResponseDto dto);
        bool Create(UserDto user);

        bool Update(UserDto user);
    }
}
