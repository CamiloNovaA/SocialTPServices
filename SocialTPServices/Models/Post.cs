using System;
using SocialTPServices.Models.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialTPServices.Models
{
    public class Post : IPost
    {
        [Key]
        public int IdPost { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Title { get; set; }
        [Required]
        [Column(TypeName = "varchar(200)")]
        public string Content { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
    }
}
