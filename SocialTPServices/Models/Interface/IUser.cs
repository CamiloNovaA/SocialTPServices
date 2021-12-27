using System;

namespace SocialTPServices.Models.Interface
{
    interface IUser
    {
        int IdUser { get; set; }
        DateTime RegisterDate { get; set; }
        string UserName { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        byte[] ProfilePhoto { get; set; }
    }
}
