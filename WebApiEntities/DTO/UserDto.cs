using System.Security.Cryptography.X509Certificates;

namespace WebApiEntities.DTO
{
    /// <summary>
    /// Clase que describe un User
    /// </summary>
    public class UserDto
    {
        public int id { get; set; }
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string avatar { get; set; }
    }
}