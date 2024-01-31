using Microsoft.EntityFrameworkCore;
using SpecnoApiReddit.Models;

namespace SpecnoApiReddit.Data;

public class ApplicationDbContext:DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { }

    

    public DbSet<SpecnoUser> SpecnoUsers { get; set; }
    public DbSet<Post> Posts { get; set; }

    public DbSet<Likes> Likes { get; set; }
    public DbSet<Comment> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       
        // Relationships:
       

   
       /* modelBuilder.Entity<Likes>()
            .HasOne(l => l.Post)
            .WithMany(p => p.MyLikes)
            .HasForeignKey(l => l.PostId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.PostPost)
            .WithMany(p => p.Comments)
            .HasForeignKey(c => c.PostpostId)
            .OnDelete(DeleteBehavior.Cascade);*/
    }
}
