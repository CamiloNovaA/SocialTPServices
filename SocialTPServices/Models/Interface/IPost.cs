using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialTPServices.Models.Interface
{
    interface IPost
    {
        int IdPost { get; set; }
        DateTime CreationDate { get; set; }
        string Title { get; set; }
        int UserId { get; set; }
        string Content { get; set; }
    }
}
