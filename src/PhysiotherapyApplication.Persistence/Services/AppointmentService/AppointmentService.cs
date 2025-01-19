using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;
using PhysiotherapyApplication.Application.Contracts.Persistence.Repositories;
using PhysiotherapyApplication.Application.Contracts.Persistence.UnitOfWork;
using PhysiotherapyApplication.Application.Features.AppointmentFeatures.DTOs;
using PhysiotherapyApplication.Application.Features.AppointmentFeatures.Services;
using PhysiotherapyApplication.Application.Paging;
using PhysiotherapyApplication.Application.Wrappers;
using PhysiotherapyApplication.Domain.Entities;

namespace PhysiotherapyApplication.Persistence.Services.AppointmentService;

public class AppointmentService(IAppointmentRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
    : IAppointmentService

{
    public async Task<ServiceResult<CreateAppointmentResponseDto>> AddAsync(CreateAppointmentRequestDto model)
    {
        try
        {
            var appointment = mapper.Map<Appointment>(model);
            await repository.AddAsync(appointment);
            await unitOfWork.CommitAsync();

            return ServiceResult<CreateAppointmentResponseDto>.SuccessAsCreated(new CreateAppointmentResponseDto(appointment.Id), $"api/products/{appointment.Id}");
        }
        catch (Exception ex)
        {
            return ServiceResult<CreateAppointmentResponseDto>.Fail(ex.Message);
        }
    }

    public async Task<ServiceResult> AddRangeAsync(ICollection<CreateAppointmentRequestDto> entities)
    {
        try
        {
            var appointments = mapper.Map<ICollection<Appointment>>(entities);
            await repository.AddRangeAsync(appointments);
            await unitOfWork.CommitAsync();

            return ServiceResult.Success();
        }
        catch (Exception ex)
        {
            return ServiceResult.Fail(ex.Message);
        }
    }

    public async Task<ServiceResult> DeleteAsync(AppointmentDto model, bool permanent = false)
    {
        try
        {
            var appointment = await repository.GetByIdAsync(model.Id);
            if (appointment is null)
                return ServiceResult.Fail("Appointment Not Found");

            await repository.DeleteAsync(appointment);
            await unitOfWork.CommitAsync();
            return ServiceResult.Success();
        }
        catch (Exception ex)
        {
            return ServiceResult.Fail(ex.Message);
        }
    }

    public async Task<ServiceResult> DeleteRangeAsync(ICollection<AppointmentDto> entities, bool permanent = false)
    {
        try
        {
            var appointments = mapper.Map<ICollection<Appointment>>(entities);
            await repository.DeleteRangeAsync(appointments);
            await unitOfWork.CommitAsync();
            return ServiceResult.Success();
        }
        catch (Exception ex)
        {
            return ServiceResult.Fail(ex.Message);
        }
    }

    public async Task<ServiceResult<IEnumerable<AppointmentDto>>> GetAllAsync()
    {
        try
        {
            var appointments = await repository.GetAllAsync();

            var appointmentAsDto = mapper.Map<IEnumerable<AppointmentDto>>(appointments);

            return ServiceResult<IEnumerable<AppointmentDto>>.Success(appointmentAsDto);
        }
        catch (Exception ex)
        {
            return ServiceResult<IEnumerable<AppointmentDto>>.Fail(ex.Message);
        }
    }


    public async Task<ServiceResult<AppointmentDto>> GetByIdAsync(string id)
    {
        try
        {
            var appointment = await repository.GetByIdAsync(Guid.Parse(id));
            if (appointment is null)
                return ServiceResult<AppointmentDto>.Fail("Appointment Not Found");

            var appointmentAsDto = mapper.Map<AppointmentDto>(appointment);

            return ServiceResult<AppointmentDto>.Success(appointmentAsDto);
        }
        catch (Exception ex)
        {
            return ServiceResult<AppointmentDto>.Fail(ex.Message);
        }
    }

    public async Task<ServiceResult<Pagination<AppointmentDto>>> GetListPaginationAsync(Expression<Func<AppointmentDto, bool>>? predicate, Func<IQueryable<AppointmentDto>, IOrderedQueryable<AppointmentDto>>? orderBy = null, Func<IQueryable<AppointmentDto>, IIncludableQueryable<AppointmentDto, object>>? include = null, int index = 0, int size = 10, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        try
        {
            var appointment = await repository.GetListPaginationAsync();
            var appointmentsAsDto = mapper.Map<Pagination<AppointmentDto>>(appointment);

            return ServiceResult<Pagination<AppointmentDto>>.Success(appointmentsAsDto);
        }
        catch (Exception ex)
        {
            return ServiceResult<Pagination<AppointmentDto>>.Fail(ex.Message);
        }
    }

    public async Task<ServiceResult> UpdatedAsync(UpdateAppointmentDto model)
    {
        try
        {
            var appointment = await repository.GetByIdAsync(model.Id);
            if (appointment is null)
                return ServiceResult.Fail("Appointment Not Found");

            appointment = mapper.Map(model, appointment);

            repository.Udpated(appointment);
            await unitOfWork.CommitAsync();

            return ServiceResult.Success();
        }
        catch (Exception ex)
        {
            return ServiceResult.Fail(ex.Message);
        }
    }

    public async Task<ServiceResult> UpdateRangeAsync(ICollection<UpdateAppointmentDto> entities)
    {
        try
        {
            //var appointments = await repository.GetAllAsync();
            //appointments = mapper.Map(entities,appointments);
            //repository.UpdateRange(appointments.ToList());

            var idToUpdate = entities.Select(e=>e.Id).ToList();

            var appointments = await repository.GetWhereAsync(a=>idToUpdate.Contains(a.Id));

            foreach (Appointment appointment in appointments)
            {
                var dto = entities.FirstOrDefault(d=>d.Id == appointment.Id);

                if (dto is not null)
                {
                    mapper.Map(dto, appointment);
                }
            }

            repository.UpdateRange(appointments.ToList());
            await unitOfWork.CommitAsync();

            return ServiceResult.Success();
        }
        catch (Exception ex)
        {
            return ServiceResult.Fail(ex.Message);
        }
    }
}
