using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CuaHangMayTinh.Models
{
    public class UserModel
    {
        private String username;
        private String password;

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }

        public string Username
        {
            get
            {
                return username;
            }

            set
            {
                username = value;
            }
        }
    }
}