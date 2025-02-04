using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using PhysiotherapyApplication.Domain.Options;

namespace PhysiotherapyApplication.Persistence.Contexts;

//public class PhysiothreapyApplicationDesignTimeDbContextFactory(IConfiguration configuration) : IDesignTimeDbContextFactory<PhysiotherapyApplicationDbContext>
//{
//    public PhysiotherapyApplicationDbContext CreateDbContext(string[] args)
//    {

//        var connectionString = configuration.GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>();

//        var optionsBuilder = new DbContextOptionsBuilder<PhysiotherapyApplicationDbContext>();

//        optionsBuilder.UseSqlServer(connectionString!.SqlServer);

//        return new PhysiotherapyApplicationDbContext(optionsBuilder.Options);
//    }
//}
