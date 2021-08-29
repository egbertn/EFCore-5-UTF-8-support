using Microsoft.EntityFrameworkCore;

namespace EFCore_5_UTF_8_support
{
	public class TestDBContext : DbContext
	{
		public TestDBContext(DbContextOptions<TestDBContext> options)
		  : base(options)
		{
			this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;	
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			
			base.OnConfiguring(optionsBuilder);

		}
		public DbSet<Entities.MyTable> MyTable { get; set; }
		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Entities.MyTable>(p => 
			{
				p.HasKey(k => k.Id);
				p.Property(prop => prop.Param).IsUnicode(false).UseCollation("Latin1_General_100_BIN2_UTF8").HasMaxLength(255);

			});
		}
	}
}
