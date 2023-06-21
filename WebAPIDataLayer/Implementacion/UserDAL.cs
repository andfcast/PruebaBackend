using Microsoft.Extensions.DependencyInjection;
using Utilities;
using WebAPIDataLayer.Interfaces;
using WebApiEntities;
using WebApiEntities.DTO;
using WebApiEntities.Models;
using WebApiEntities.Context;
using Microsoft.EntityFrameworkCore;

namespace WebAPIDataLayer.Implementacion
{
    public class UserDAL: IUserDAL
    {
        private DbUsersContext _context = new WebApiEntities.Context.DbUsersContext();

        public UserDAL(DbUsersContext context)
        {
            _context = context;
        }

        public Boolean SaveToDB(ExternalApiResponseDto response) {
            if (response.data.Count > 0) {
                List<User> lstUsers = new List<User>();
                foreach (UserDto dto in response.data)
                {
                    lstUsers.Add(Converter.ConvertToEntity(dto));
                }
                try
                {
                    _context.Users.AddRange(lstUsers);
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }                
            }
            return false;
        }

        public List<UserDto> GetAll(PageInfoDto info) {
            List<UserDto> lstUsers = new List<UserDto>();
            User[] users = _context.Users.ToArray();
            if (users.Length > 0) {
                for (int i = (info.Page * info.PageSize - info.PageSize); i < (info.Page * info.PageSize); i++)
                {
                    if (i < users.Length) {
                        lstUsers.Add(Converter.ConvertToDto(users[i]));
                    }
                }
            }
            
            return lstUsers;
        }

        public UserDto GetInfo(int id) {
            User user = _context.Users.FirstOrDefault(x => x.Id == id);
            if(user == null)
            {
                return null;
            }
            return Converter.ConvertToDto(user);
        }

        public bool Create(UserDto user) {
            long id = 1;
            if (_context.Users.Count() > 0) { 
                id = _context.Users.OrderByDescending(x => x.Id).First().Id + 1;
            }            
            User newUser = Converter.ConvertToEntity(user);
            newUser.Id = id;
            _context.Users.Add(newUser);
            _context.SaveChanges();
            return true;
        }

        public bool Update(UserDto user) {
            User updatedUser = Converter.ConvertToEntity(user);
            _context.Users.Update(updatedUser);
            _context.SaveChanges();
            return true;
        }
    }
}