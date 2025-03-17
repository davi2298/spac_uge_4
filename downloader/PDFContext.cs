using Microsoft.EntityFrameworkCore;

class PDFContext : DbContext
{
    private static PDFContext? instanse;
    public static PDFContext GetInstanse => instanse ??= new();
    private PDFContext() { }
    public DbSet<PDF> PDF { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connString = $"server=localhost;database={Environment.GetEnvironmentVariable("DBNAME")};user={Environment.GetEnvironmentVariable("DBUSER")};password={Environment.GetEnvironmentVariable("DBPASSWORD")}";
        optionsBuilder.UseMySQL(connString);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<PDF>().HasKey(e => e.BRnum);
        // Microsoft.EntityFrameworkCore.Metadata.Builders.PropertyBuilder<Uri>.HasConversion(Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter)converter;
        modelBuilder.Entity<PDF>().Property(e => e.Pdf_URL).HasConversion(v => v == null ? null : v.ToString(), v => new Uri(v));

    }
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
        // var tmp = configurationBuilder.Properties<PDF>(e => );
    }
}