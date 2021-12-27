using Microsoft.EntityFrameworkCore;
using SocialTPServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialTPServices.Data
{
    public class SocialTPDBContext : DbContext
    {
        public SocialTPDBContext(DbContextOptions<SocialTPDBContext> options): base(options)
        {

        }

        public DbSet<Post> Post { get; set; }
        public DbSet<PostResponse> PostResponse { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserRequest> UserRequest { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<CommentResponse> CommentResponse { get; set; }        
    }
}
