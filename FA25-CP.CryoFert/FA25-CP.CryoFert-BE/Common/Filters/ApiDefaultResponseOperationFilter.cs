using FA25_CP.CryoFert_BE.Common.Attributes;
using FSCMS.Service.ReponseModel;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FA25_CP.CryoFert_BE.Common.Filters
{
    public class ApiDefaultResponseOperationFilter : IOperationFilter
    {

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            //này là attribute trên method hoặc controller
            var methodInfo = context.MethodInfo;

            var attr = methodInfo.GetCustomAttributes(true)
                .OfType<ApiDefaultResponseAttribute>()
                .FirstOrDefault()
                ?? methodInfo.DeclaringType.GetCustomAttributes(true)
                .OfType<ApiDefaultResponseAttribute>()
                .FirstOrDefault();
            if (attr == null)
            {
                return;
            }

            var modelType = attr.ResponseModelType;

            //wrapper type -dùng thay cho namespace/type đang build
            var baseResponseOpen = typeof(BaseResponse<>);
            var dynamicResponseOpen = typeof(DynamicResponse<>);

            Type okResponseType;
            if (attr.UseDynamicWrapper)
                okResponseType = dynamicResponseOpen.MakeGenericType(modelType);
            else
                okResponseType = baseResponseOpen.MakeGenericType(modelType);

            var badResponseType = baseResponseOpen.MakeGenericType(modelType);

            // helper: tạo reference schema
            var schemaGenerator = context.SchemaGenerator;
            var schemaRepository = context.SchemaRepository;

            var okSchema = schemaGenerator.GenerateSchema(okResponseType, schemaRepository);
            var errorSchema = schemaGenerator.GenerateSchema(badResponseType, schemaRepository);
            if (attr.IncludeForbidden)
            {
                AddOrReplaceResponse(operation, "403", "Forbidden", errorSchema);
            }
            // thêm/ghi đè 200, 400, 401, 404, 500
            AddOrReplaceResponse(operation, "200", "OK", okSchema);
            //AddOrReplaceResponse(operation, "400", "Bad Request", errorSchema);
            //AddOrReplaceResponse(operation, "401", "Unauthorized", errorSchema);
            //AddOrReplaceResponse(operation, "404", "Not Found", errorSchema);
            //AddOrReplaceResponse(operation, "500", "Internal Server Error", errorSchema);
        }

        private void AddOrReplaceResponse(OpenApiOperation operation, string statusCode, string description, OpenApiSchema schema)
        {
            var cotent = new OpenApiMediaType
            {
                Schema = schema
            };

            var responese = new OpenApiResponse
            {
                Description = description,
                Content =
                {
                    ["application/json"] = cotent
                }
            };

            operation.Responses[statusCode] = responese;
        }
    }
}
