using System;
using SocialTPServices.Models.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialTPServices.Models
{
    public class PostResponse : IPost
    {
        [Key]
        public int IdPost { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public string UserName { get; set; }
    }
}
