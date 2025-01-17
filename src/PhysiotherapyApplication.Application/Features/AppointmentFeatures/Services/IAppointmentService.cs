using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using PhysiotherapyApplication.Application.Wrappers;
using PhysiotherapyApplication.Domain.Entities;
using PhysiotherapyApplication.Application.Paging;

namespace PhysiotherapyApplication.Application.Features.AppointmentFeatures.Services;

public interface IAppointmentService
{
    Task<ServiceResult<Appointment>> GetAsync(
        Expression<Func<Appointment, bool>> predicate,
        Func<IQueryable<Appointment>, IIncludableQueryable<Appointment, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
        );

    Task<ServiceResult<Pagination<Appointment>>> GetListAsync(
         Expression<Func<Appointment, bool>> predicate,
        Func<IQueryable<Appointment>, IOrderedQueryable<Appointment>>? orderBy = null,
        Func<IQueryable<Appointment>, IIncludableQueryable<Appointment, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
        );

    Task<ServiceResult<bool>> AnyAsync(
        Expression<Func<Appointment, bool>>? predicate = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
        );

    Task<ServiceResult<Appointment>> AddAsync(Appointment model);

    Task<ServiceResult<ICollection<Appointment>>> AddRangeAsync(ICollection<Appointment> entities);

    ServiceResult<Appointment> Udpated(Appointment model);

    ServiceResult<ICollection<Appointment>> UpdateRange(ICollection<Appointment> entities);

    Task<ServiceResult<Appointment>> DeleteAsync(Appointment model, bool permanent = false);

    Task<ServiceResult<ICollection<Appointment>>> DeleteRangeAsync(ICollection<Appointment> entities, bool permanent = false);
}
