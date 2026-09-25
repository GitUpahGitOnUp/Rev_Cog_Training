using Microsoft.EntityFrameworkCore;
using bankLIB.Entities;

namespace bankLIB.Data;

// this is the core EF Core class that represents a session/connection
// to the DB, and ea. DbSet<T> property represents one TABLE mapped directly
// from a C# class, using a Code-First strategy

public class BankDbContext : DbContext
{
    // each of these becomes a table 
    #region Tables
    public DbSet<User> Users {get; set;}

    public DbSet<Accounts> Accounts {get; set;}

    public DbSet<Transaction> Transactions {get; set;}

    public DbSet<ServiceRequest> ServiceRequests {get; set;}

    #endregion

    // Constructor that supplies the connection details vs BankDbContext hardcoding them itself
    // ex of 'Constructor Injection' 

    public BankDbContext(DbContextOptions<BankDbContext> options) : base(options)
    {
        
    }

    // OnModelCreating called once by EF Core at app startup to build its internal schema snapshot
    // this allows for customization for things ef core doesn't handle on its own
    // this is where Fluent API config lives
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    
    // Table-Per_Hierarchy. Customer + Admin both : User, this tells EF how to store both in the User table
    // allowing a hidden discriminator column to tell EF which rows are cust. and which are admin as SQL does not do this natively
    modelBuilder.Entity<User>().HasDiscriminator<string>("UserType").HasValue<Customer>("Customer").HasValue<Admin>("Admin");

    // tells EF Core that Transaction.AccNo IS the real foreign key to Accounts.AccNo,
    // which prevents EF Core create its own hidden AccountsAccNo shadow column
    modelBuilder.Entity<Transaction>()
        .HasOne<Accounts>() // Transaction has no 'public Accounts Account get/set navigations property pointing back thus no navigation property
                                // so <Accounts> explicitly lists the target type  instead

        .WithMany(a => a.Transactions) 

        .HasForeignKey(t => t.AccNo); // explicityly insturcts EF Core to use Transaction's own AccNo property as the FK column instead of generating its own

    modelBuilder.Entity<ServiceRequest>()
        .HasOne<Accounts>()
        .WithMany(a => a.ServiceRequests)
        .HasForeignKey(sr => sr.AccNo);

    
    base.OnModelCreating(modelBuilder);

    }


}