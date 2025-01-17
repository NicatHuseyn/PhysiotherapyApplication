﻿using PhysiotherapyApplication.Application.Contracts.Persistence.Repositories.BaseRepository;
using PhysiotherapyApplication.Domain.Entities;

namespace PhysiotherapyApplication.Application.Contracts.Persistence.Repositories;

public interface IPatientRepository:IGenericRepository<Patient>
{
}
