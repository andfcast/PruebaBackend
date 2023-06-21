using System.Text.Json.Serialization;
using WebAPIBusiness.Interface;
using WebAPIDataLayer.Interfaces;
using WebApiEntities.DTO;
using Newtonsoft.Json;

namespace WebAPIBusiness.Implementacion
{
    public class UserBL : IUserBL
    {
        private IUserDAL _userDal;

        public UserBL(IUserDAL userDal)
        {
            _userDal = userDal;
        }

        public Boolean SaveToDB(int page) {
            ExternalApiResponseDto dto = CallService(page);
            return _userDal.SaveToDB(dto); ;
        }

        public List<UserDto> GetAll(PageInfoDto info) {
            return _userDal.GetAll(info);
        }

        public UserDto GetInfo(int id) {
            return _userDal.GetInfo(id);
        }

        public Boolean Create(UserDto objUser) { 
            return _userDal.Create(objUser);
        }

        public Boolean Update(UserDto objUser) {
            return _userDal.Update(objUser);
        }

        private ExternalApiResponseDto CallService(int page) {
            string urlService = "https://reqres.in/api/users?page=" + page.ToString();
            ExternalApiResponseDto res = new ExternalApiResponseDto();
            HttpClient client= new HttpClient();
            HttpResponseMessage response = client.GetAsync(urlService).Result;
            if (response.IsSuccessStatusCode) {
                res = JsonConvert.DeserializeObject<ExternalApiResponseDto>(response.Content.ReadAsStringAsync().Result);
            }
            return res;
        }
    }
}