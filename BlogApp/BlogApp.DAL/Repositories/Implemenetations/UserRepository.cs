using BlogApp.Core.Entities;
using BlogApp.DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DAL.Repositories.Implemenetations
{
    public class UserRepository : IUserRepository
    {
        private readonly List<AppUser> _users = new List<AppUser>();

        public AppUser AddUser(AppUser user)
        {
            user.Id = (_users.Count + 1).ToString();
            _users.Add(user);
            return user;
        }
    }
}
