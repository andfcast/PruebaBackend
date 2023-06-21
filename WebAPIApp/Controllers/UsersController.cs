using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPIBusiness.Interface;
using WebApiEntities;
using WebApiEntities.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserBL userBl;
        public UsersController(IUserBL userBL) { 
            this.userBl = userBL;       
        }
        // GET: api/<UserController>
        /// <summary>
        /// Devuelve los resultados paginados de una consulta
        /// </summary>
        /// <param name="info">Parámetro con los datos de la página y el tamaño que se va a manejar</param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<UserDto> Get([FromBody] PageInfoDto info)
        {
            return userBl.GetAll(info);
        }

        /// <summary>
        /// Obtiene la información de un User específico
        /// </summary>
        /// <param name="id">Identificador de registro</param>
        /// <returns></returns>
        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public UserDto Get(int id)
        {
            return userBl.GetInfo(id);
        }

        /// <summary>
        /// Permite crear un nuevo User
        /// </summary>
        /// <param name="value">User a ser creado</param>
        /// <returns></returns>
        // POST api/<UserController>        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]        
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] UserDto value)
        {
            if (string.IsNullOrEmpty(value.first_name) || string.IsNullOrEmpty(value.email))
            {
                return BadRequest();                
            }
            else
            {
                return Ok(userBl.Create(value));
            }
            
        }

        /// <summary>
        /// Permite actualizar el registro de User
        /// </summary>
        /// <param name="id">Uer Id</param>
        /// <param name="value">Registro modificado</param>
        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] UserDto value)
        {
            value.id = id;
            userBl.Update(value);
        }

    }
}
