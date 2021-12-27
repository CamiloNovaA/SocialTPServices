using System;
using SocialTPServices.Models.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialTPServices.Models
{
    public class User : IUser
    {
        [Key]
        public int IdUser { get; set; }
        [Required]
        public DateTime RegisterDate { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string UserName { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string Email { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Password { get; set; }
        [Column(TypeName = "varbinary(MAX)")]
        public byte[] ProfilePhoto { get; set; }
    }
}
