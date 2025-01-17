using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PhysiotherapyApplication.Application.Contracts.Persistence.Repositories;
using PhysiotherapyApplication.Domain.Entities;
using PhysiotherapyApplication.Persistence.Repositories.BaseRepository;

namespace PhysiotherapyApplication.Persistence.Repositories;

public class DoctorRepository(IdentityDbContext context) : GenericRepository<Doctor, IdentityDbContext>(context), IDoctorRepository
{
}
