using LegacyApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp
{
    public class UserDataAccessWrapper : IUserDataAccessWrapper
    {
        IUser _user;
        readonly IUserDataAccessWrapper _userAccessWrapper;
        
        public UserDataAccessWrapper()
        {

        }
        public void AddUser(IUser user)
        {
            _user = user;
        }
    }
}
