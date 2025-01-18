using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using PhysiotherapyApplication.Application.Wrappers;
using PhysiotherapyApplication.Domain.Entities;
using PhysiotherapyApplication.Application.Paging;
using PhysiotherapyApplication.Application.Features.AppointmentFeatures.DTOs;

namespace PhysiotherapyApplication.Application.Features.AppointmentFeatures.Services;

public interface IAppointmentService
{
    Task<ServiceResult<AppointmentDto>> GetAsync(
        Expression<Func<AppointmentDto, bool>> predicate,
        Func<IQueryable<AppointmentDto>, IIncludableQueryable<AppointmentDto, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
        );

    Task<ServiceResult<Pagination<AppointmentDto>>> GetListAsync(
         Expression<Func<AppointmentDto, bool>> predicate,
        Func<IQueryable<AppointmentDto>, IOrderedQueryable<AppointmentDto>>? orderBy = null,
        Func<IQueryable<AppointmentDto>, IIncludableQueryable<AppointmentDto, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
        );

    Task<ServiceResult<bool>> AnyAsync(
        Expression<Func<AppointmentDto, bool>>? predicate = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
        );

    Task<ServiceResult<AppointmentDto>> AnyAsync(string Id);

    Task<ServiceResult<CreateAppointmentDto>> AddAsync(CreateAppointmentDto model);

    Task<ServiceResult<ICollection<CreateAppointmentDto>>> AddRangeAsync(ICollection<CreateAppointmentDto> entities);

    ServiceResult<UpdateAppointmentDto> Udpated(UpdateAppointmentDto model);

    ServiceResult<ICollection<UpdateAppointmentDto>> UpdateRange(ICollection<UpdateAppointmentDto> entities);

    Task<ServiceResult<AppointmentDto>> DeleteAsync(AppointmentDto model, bool permanent = false);

    Task<ServiceResult<ICollection<AppointmentDto>>> DeleteRangeAsync(ICollection<AppointmentDto> entities, bool permanent = false);
}
