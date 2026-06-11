using KYC.Models.ResponseModels;
using Microsoft.EntityFrameworkCore;

namespace KYC.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<KycVerificationResposne> KycVerification { get; set; }
    }
}
