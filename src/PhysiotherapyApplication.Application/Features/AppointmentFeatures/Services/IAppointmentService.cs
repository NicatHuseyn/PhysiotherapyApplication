using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using PhysiotherapyApplication.Application.Features.AppointmentFeatures.DTOs;
using PhysiotherapyApplication.Application.Paging;
using PhysiotherapyApplication.Application.Wrappers;

namespace PhysiotherapyApplication.Application.Features.AppointmentFeatures.Services;

public interface IAppointmentService
{
    Task<ServiceResult<Pagination<AppointmentDto>>> GetListPaginationAsync(
         Expression<Func<AppointmentDto, bool>>? predicate,
        Func<IQueryable<AppointmentDto>, IOrderedQueryable<AppointmentDto>>? orderBy = null,
        Func<IQueryable<AppointmentDto>, IIncludableQueryable<AppointmentDto, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
        );

    Task<ServiceResult<IEnumerable<AppointmentDto>>> GetAllAsync();

    Task<ServiceResult<AppointmentDto>> GetByIdAsync(string id);

    Task<ServiceResult<CreateAppointmentResponseDto>> AddAsync(CreateAppointmentRequestDto model);

    Task<ServiceResult> AddRangeAsync(ICollection<CreateAppointmentRequestDto> entities);

    Task<ServiceResult> UpdatedAsync(UpdateAppointmentDto model);

    Task<ServiceResult> UpdateRangeAsync(ICollection<UpdateAppointmentDto> entities);

    Task<ServiceResult> DeleteAsync(AppointmentDto model, bool permanent = false);

    Task<ServiceResult> DeleteRangeAsync(ICollection<AppointmentDto> entities, bool permanent = false);
}
