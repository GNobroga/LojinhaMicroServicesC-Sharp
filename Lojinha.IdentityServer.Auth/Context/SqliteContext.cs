using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lojinha.IdentityServer.Auth.Context;

public class SqliteContext : IdentityDbContext<ApplicationUser>
{

    public SqliteContext(DbContextOptions<SqliteContext> options) : base (options) {}

}