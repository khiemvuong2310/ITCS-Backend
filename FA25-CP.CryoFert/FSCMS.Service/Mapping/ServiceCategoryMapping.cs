using FSCMS.Core.Entities;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;

namespace FSCMS.Service.Mapping
{
    public static class ServiceCategoryMapping
    {
        public static ServiceCategoryResponseModel ToResponseModel(this ServiceCategory entity)
        {
            return new ServiceCategoryResponseModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Code = entity.Code,
                IsActive = entity.IsActive,
                DisplayOrder = entity.DisplayOrder,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                ServiceCount = entity.Services?.Count ?? 0
            };
        }

        public static ServiceCategory ToEntity(this ServiceCategoryRequestModel request)
        {
            return new ServiceCategory(Guid.NewGuid(), request.Name)
            {
                Description = request.Description,
                Code = request.Code,
                IsActive = request.IsActive,
                DisplayOrder = request.DisplayOrder
            };
        }

        public static void UpdateEntity(this ServiceCategory entity, ServiceCategoryRequestModel request)
        {
            entity.Name = request.Name;
            entity.Description = request.Description;
            entity.Code = request.Code;
            entity.IsActive = request.IsActive;
            entity.DisplayOrder = request.DisplayOrder;
        }
    }
}
