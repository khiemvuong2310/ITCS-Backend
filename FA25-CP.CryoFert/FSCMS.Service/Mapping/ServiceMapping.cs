using FSCMS.Core.Entities;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;

namespace FSCMS.Service.Mapping
{
    public static class ServiceMapping
    {
        public static ServiceResponseModel ToResponseModel(this Core.Entities.Service entity)
        {
            return new ServiceResponseModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Price = entity.Price,
                Code = entity.Code,
                Unit = entity.Unit,
                Duration = entity.Duration,
                IsActive = entity.IsActive,
                Notes = entity.Notes,
                ServiceCategoryId = entity.ServiceCategoryId,
                ServiceCategoryName = entity.ServiceCategory?.Name ?? string.Empty,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public static Core.Entities.Service ToEntity(this ServiceCreateUpdateRequestModel request)
        {
            return new Core.Entities.Service(Guid.NewGuid(), request.Name, request.Price, request.ServiceCategoryId)
            {
                Description = request.Description,
                Code = request.Code,
                Unit = request.Unit,
                Duration = request.Duration,
                IsActive = request.IsActive,
                Notes = request.Notes
            };
        }

        public static void UpdateEntity(this Core.Entities.Service entity, ServiceCreateUpdateRequestModel request)
        {
            entity.Name = request.Name;
            entity.Description = request.Description;
            entity.Price = request.Price;
            entity.Code = request.Code;
            entity.Unit = request.Unit;
            entity.Duration = request.Duration;
            entity.IsActive = request.IsActive;
            entity.Notes = request.Notes;
            entity.ServiceCategoryId = request.ServiceCategoryId;
        }
    }
}
