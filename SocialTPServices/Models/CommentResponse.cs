using SocialTPServices.Models.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialTPServices.Models
{
    public class CommentResponse : IComment
    {
        [Key]
        public int IdComment { get; set; }
        public int IdPost { get; set; }
        public int IdUser { get; set; }
        public string Content { get; set; }
        public string UserName { get; set; }
    }
}
