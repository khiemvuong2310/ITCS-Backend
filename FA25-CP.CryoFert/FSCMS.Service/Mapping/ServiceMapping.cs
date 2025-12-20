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

        public static Core.Entities.Service ToEntity(this ServiceCreateRequestModel request)
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

        /// <summary>
        /// Partial update - only updates fields that are provided (not null)
        /// </summary>
        public static void UpdateEntity(this Core.Entities.Service entity, ServiceUpdateRequestModel request)
        {
            // Name: Only update if a non-empty value is provided (Name is required, cannot be cleared)
            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                entity.Name = request.Name;
            }

            // Description: Update if provided (null = keep existing, empty string = clear, value = update)
            if (request.Description != null)
            {
                entity.Description = request.Description;
            }

            // Price: Update if provided (null = keep existing, value = update)
            if (request.Price.HasValue)
            {
                entity.Price = request.Price.Value;
            }

            // Code: Update if provided (null = keep existing, empty string = clear, value = update)
            if (request.Code != null)
            {
                entity.Code = request.Code;
            }

            // Unit: Update if provided (null = keep existing, empty string = clear, value = update)
            if (request.Unit != null)
            {
                entity.Unit = request.Unit;
            }

            // Duration: Update if provided (null = keep existing, value = update)
            if (request.Duration.HasValue)
            {
                entity.Duration = request.Duration.Value;
            }

            // IsActive: Update if provided (null = keep existing, value = update)
            if (request.IsActive.HasValue)
            {
                entity.IsActive = request.IsActive.Value;
            }

            // Notes: Update if provided (null = keep existing, empty string = clear, value = update)
            if (request.Notes != null)
            {
                entity.Notes = request.Notes;
            }

            // ServiceCategoryId: Update if provided (null = keep existing, value = update)
            // Note: Validation should be done in service layer before calling this method
            if (request.ServiceCategoryId.HasValue)
            {
                entity.ServiceCategoryId = request.ServiceCategoryId.Value;
            }
        }
    }
}
