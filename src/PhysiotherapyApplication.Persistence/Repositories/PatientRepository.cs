using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PhysiotherapyApplication.Application.Contracts.Persistence.Repositories;
using PhysiotherapyApplication.Domain.Entities;
using PhysiotherapyApplication.Persistence.Repositories.BaseRepository;

namespace PhysiotherapyApplication.Persistence.Repositories;

public class PatientRepository(IdentityDbContext context) : GenericRepository<Patient, IdentityDbContext>(context), IPatientRepository
{
}
