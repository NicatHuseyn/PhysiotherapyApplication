using PhysiotherapyApplication.Application.Contracts.Persistence.Repositories;
using PhysiotherapyApplication.Domain.Entities;
using PhysiotherapyApplication.Persistence.Contexts;
using PhysiotherapyApplication.Persistence.Repositories.BaseRepository;

namespace PhysiotherapyApplication.Persistence.Repositories;

public class PatientDetailRepository(PhysiotherapyApplicationDbContext context) : GenericRepository<PatientDetail>(context), IPatientDetailRepository
{
}
