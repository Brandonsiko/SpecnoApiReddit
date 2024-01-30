using Microsoft.EntityFrameworkCore;
using SpecnoApiReddit.Models;

namespace SpecnoApiReddit.Data;

public class ApplicationDbContext:DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { }

    public DbSet<SpecnoUser> specnoUsersCtx { get; set; }
    public DbSet<Post> postsSet { get; set; }

    public DbSet<Likes> likesSet { get; set; }
}
