using FSCMS.Core.Entities;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using FSCMS.Core.Enum;

namespace FSCMS.Service.Mapping
{
    public static class ServiceRequestMapping
    {
        public static ServiceRequestResponseModel ToResponseModel(this ServiceRequest entity)
        {
            return new ServiceRequestResponseModel
            {
                Id = entity.Id,
                AppointmentId = entity.AppointmentId,
                RequestDate = entity.RequestDate,
                Status = entity.Status,
                StatusName = entity.Status.ToString(),
                TotalAmount = entity.TotalAmount,
                Notes = entity.Notes,
                ApprovedDate = entity.ApprovedDate,
                ApprovedBy = entity.ApprovedBy,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                ServiceDetails = entity.ServiceRequestDetails?.Select(srd => srd.ToResponseModel()).ToList() ?? new List<ServiceRequestDetailResponseModel>()
            };
        }

        public static ServiceRequestDetailResponseModel ToResponseModel(this ServiceRequestDetails entity)
        {
            return new ServiceRequestDetailResponseModel
            {
                Id = entity.Id,
                ServiceRequestId = entity.ServiceRequestId,
                ServiceId = entity.ServiceId,
                ServiceName = entity.Service?.Name ?? string.Empty,
                ServiceCode = entity.Service?.Code,
                ServiceUnit = entity.Service?.Unit,
                Quantity = entity.Quantity,
                UnitPrice = entity.UnitPrice,
                Discount = entity.Discount,
                TotalPrice = entity.TotalPrice,
                Notes = entity.Notes
            };
        }

        public static ServiceRequest ToEntity(this ServiceRequestCreateRequestModel request)
        {
            return new ServiceRequest(Guid.NewGuid(), request.RequestDate)
            {
                AppointmentId = request.AppointmentId,
                Notes = request.Notes,
                Status = ServiceRequestStatus.Pending
            };
        }

        public static ServiceRequestDetails ToEntity(this ServiceRequestDetailCreateRequestModel request, Guid serviceRequestId)
        {
            var totalPrice = (request.UnitPrice * request.Quantity) - (request.Discount ?? 0);
            return new ServiceRequestDetails(Guid.NewGuid(), serviceRequestId, request.ServiceId, request.Quantity, request.UnitPrice)
            {
                Discount = request.Discount,
                TotalPrice = totalPrice,
                Notes = request.Notes
            };
        }

        public static void UpdateEntity(this ServiceRequest entity, ServiceRequestUpdateRequestModel request)
        {
            entity.AppointmentId = request.AppointmentId;
            entity.RequestDate = request.RequestDate;
            entity.Status = request.Status;
            entity.Notes = request.Notes;
            entity.ApprovedBy = request.ApprovedBy;
            
            if (request.Status == ServiceRequestStatus.Approved && entity.ApprovedDate == null)
            {
                entity.ApprovedDate = DateTime.Now;
            }
        }

        public static void UpdateEntity(this ServiceRequestDetails entity, ServiceRequestDetailUpdateRequestModel request)
        {
            entity.Quantity = request.Quantity;
            entity.UnitPrice = request.UnitPrice;
            entity.Discount = request.Discount;
            entity.TotalPrice = (request.UnitPrice * request.Quantity) - (request.Discount ?? 0);
            entity.Notes = request.Notes;
        }
    }
}
