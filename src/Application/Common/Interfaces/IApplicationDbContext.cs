using SampleProject.Domain.Entities;

namespace SampleProject.Application.Common.Interfaces;

public interface IApplicationDbContext
{ 
    public DbSet<ApplicationUser> ApplicationUsers { get;  }
    public DbSet<ApplicationRole> ApplicationRoles { get;  }
    public DbSet<ApplicationUserRole> ApplicationUserRoles { get;  }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
