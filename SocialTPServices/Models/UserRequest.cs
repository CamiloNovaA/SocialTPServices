using System;
using SocialTPServices.Models.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialTPServices.Models
{
    public class UserRequest : IUser
    {
        [Key]
        public int IdUser { get; set; }
        public DateTime RegisterDate { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public byte[] ProfilePhoto { get; set; }
    }
}
