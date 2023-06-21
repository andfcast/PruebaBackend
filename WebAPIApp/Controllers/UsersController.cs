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
        public IActionResult Get([FromBody] PageInfoDto info)
        {
            List<UserDto> lstUsers = userBl.GetAll(info);
            if (lstUsers != null && lstUsers.Count > 0) {
                return Ok(new ApiResponseDto
                {
                    StatusCode = StatusCodes.Status200OK,
                    StatusName = "OK",
                    Page = info.Page,
                    PageSize = info.PageSize,
                    IsSuccess = true,
                    Data = lstUsers
                }); ;
            }
            else {
                return NotFound(new ApiResponseDto
                {
                    StatusCode = StatusCodes.Status204NoContent,
                    StatusName = "No content",
                    Page = info.Page,
                    PageSize = info.PageSize,
                    IsSuccess = false,
                    Message = "Items not found"
                });            
            }
            
        }

        /// <summary>
        /// Obtiene la información de un User específico
        /// </summary>
        /// <param name="id">Identificador de registro</param>
        /// <returns></returns>
        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            UserDto user = userBl.GetInfo(id);
            if (user != null)
            {
                return Ok(new ApiResponseDto
                {
                    StatusCode = StatusCodes.Status200OK,
                    StatusName = "OK",
                    IsSuccess = true,
                    Data = user
                });
            }
            else {
                return NotFound(new ApiResponseDto
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    StatusName = "Not Found",                    
                    IsSuccess = false,
                    Message = "Item not found"
                });
            }
                        
        }
        
        /// <summary>
        /// Permite crear un nuevo User
        /// </summary>
        /// <param name="value">User a ser creado</param>
        /// <param name="credentials">Credenciales de autenticación</param>        
        /// <returns></returns>
        // POST api/<UserController>        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]        
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromHeader] CredentialDto credentials ,[FromBody] UserDto value)
        {                        
            if (!ValidateAuth(credentials)) {
                return Unauthorized(new ApiResponseDto
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    StatusName = "Unauthorized",
                    Message = "Wrong Credentials!",
                    IsSuccess = false
                }) ;
            }
            if (string.IsNullOrEmpty(value.first_name) || string.IsNullOrEmpty(value.email))
            {
                return BadRequest(new ApiResponseDto
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    StatusName = "Bad Request",
                    Message = "Fields required. Please check and try again",
                    IsSuccess = false
                });                
            }
            else
            {
                if (userBl.Create(value))
                {
                    return Ok(new ApiResponseDto
                    {
                        StatusCode = StatusCodes.Status201Created,
                        StatusName = "OK",
                        IsSuccess = true,
                        Message = "User created successfully",                        
                    });
                }
                else {
                    return BadRequest(new ApiResponseDto
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        StatusName = "Bad Request",
                        IsSuccess = false,
                        Message = "Cannot create new user"
                    });
                }                
            }
            
        }

        /// <summary>
        /// Permite actualizar el registro de User
        /// </summary>
        /// <param name="id">Uer Id</param>
        /// <param name="value">Registro modificado</param>
        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Put(int id, [FromBody] UserDto value)
        {
            value.id = id;
            if ( id == 0 || string.IsNullOrEmpty(value.first_name) || string.IsNullOrEmpty(value.email))
            {
                return BadRequest(new ApiResponseDto
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    StatusName = "Bad Request",
                    Message = "Fields required. Please check and try again",
                    IsSuccess = false
                });
            }
            if (userBl.Update(value))
            {
                return Ok(new ApiResponseDto
                {
                    StatusCode = StatusCodes.Status202Accepted,
                    StatusName = "OK",
                    IsSuccess = true,
                    Message = "User updated successfully",
                });
            }
            else
            {
                return BadRequest(new ApiResponseDto
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    StatusName = "Bad Request",
                    IsSuccess = false,
                    Message = "Cannot update user"
                });
            }
            
        }

        /// <summary>
        /// Función de validación de credenciales.
        /// </summary>
        /// <param name="credential"></param>
        /// <returns></returns>
        private bool ValidateAuth(CredentialDto credential)
        {            
            if (credential.Client_secret != "F9730667-4137-4A3E-902C-E771F5AE8683" || credential.Client_id != "EFAF9902-0DE7-43AF-8B60-FCA5F70BC8E3")
                return false;
            return true;
        }

       
    }
}
