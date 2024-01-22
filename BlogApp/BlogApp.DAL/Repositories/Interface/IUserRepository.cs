﻿using BlogApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DAL.Repositories.Interface
{
    public interface IUserRepository
    {
        AppUser AddUser(AppUser user);
    }
}