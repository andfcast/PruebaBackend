using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiEntities.DTO
{
    /// <summary>
    /// Clase que encapsula las condiciones de paginación deseadas
    /// </summary>
    public class PageInfoDto
    {
        public int Page { get; set; }

        public int PageSize { get; set; }
    }
}
