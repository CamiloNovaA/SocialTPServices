using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialTPServices.Models.Interface
{
    interface IComment
    {
        int IdComment { get; set; }
        int IdPost { get; set; }
        int IdUser { get; set; }
        string Content { get; set; }
    }
}
