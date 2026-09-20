using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

namespace bankLIB.Data;

// Interface used only by the 'dotnet ef' CL tool at design time for migrations, not the running app
// allows a way for the tool to construct the BankDbContext w/o need to first run Program.cs

public class BankDbContextFactory : IDesignTimeDbContextFactory<BankDbContext>
{
    public BankDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BankDbContext>();

    // hardcoded path is only used by migration tooling

    optionsBuilder.UseSqlServer(
    "Server=localhost;" +
    "Database=BankDb;" +
    "Trusted_Connection=True;" +
    "TrustServerCertificate=True;");

    return new BankDbContext(optionsBuilder.Options);
    }
}