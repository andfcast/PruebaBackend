using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiEntities.DTO;

namespace WebAPIBusiness.Interface
{
    /// <summary>
    /// Interface de las operaciones de lógica de negocio
    /// </summary>
    public interface IUserBL
    {
        List<UserDto> GetAll(PageInfoDto info);
        UserDto GetInfo(int id);

        Boolean SaveToDB(int page);
 

         Boolean Create(UserDto objUser);

        Boolean Update(UserDto objUser);
    }
}
