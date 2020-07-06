using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CuaHangMayTinh.Models
{
    public class UserDTO
    {
        private String username;
        private UserType type;

        public UserDTO() { }
        public UserDTO(TaiKhoan user)
        {
            this.Username = user.tai_khoan;
            switch (user.loai_tai_khoan)
            {
                case 0: this.Type = UserType.ADMIN; break;
                case 1: this.Type = UserType.STAFF; break;
                case 2: this.Type = UserType.USER; break;
                default:
                    break;
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

        public string email
        {
            get { return email; }
            set { email = value; }
        }
        public string mat_khau
        {
            get { return mat_khau; }
            set { mat_khau = value; }
        }
        public UserType Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }
    }
}