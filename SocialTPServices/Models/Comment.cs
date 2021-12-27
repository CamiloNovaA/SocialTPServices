using SocialTPServices.Models.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialTPServices.Models
{
    public class Comment : IComment
    {
        [Key]
        public int IdComment { get; set; }
        [Required]
        public int IdPost { get; set; }
        [Required]
        public int IdUser { get; set; }
        [Required]
        [Column(TypeName = "varchar(200)")]
        public string Content { get; set; }
}
}
