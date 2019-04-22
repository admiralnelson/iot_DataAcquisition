using Microsoft.EntityFrameworkCore;
public class MyDbContext : DbContext
{

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }

    public DbSet<Device> Devices { get; set; }
    public DbSet<DeviceLog> DeviceLogs { get; set; }
    

}