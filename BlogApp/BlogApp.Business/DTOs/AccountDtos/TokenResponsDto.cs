﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.DTOs.AccountDtos
{
    public class TokenResponsDto
    {
        public string Token { get; set; }
        public string UserName { get; set; }
        public DateTime ExpariDate { get; set; }
    }
}