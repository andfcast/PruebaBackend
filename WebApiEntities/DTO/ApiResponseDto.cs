﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiEntities.Models;

namespace WebApiEntities.DTO
{
    public class ApiResponseDto
    {
        public int page { get; set; }

        public int per_page { get; set; }

        public int total { get; set; }

        public int total_pages { get; set; }

        public List<UserDto> data { get; set; }
    }
}
