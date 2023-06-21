using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiEntities.DTO
{
    public class CredentialDto
    {
        [FromHeader]
        public string Client_secret { get; set; }
        [FromHeader]
        public string Client_id { get; set; }
    }
}
