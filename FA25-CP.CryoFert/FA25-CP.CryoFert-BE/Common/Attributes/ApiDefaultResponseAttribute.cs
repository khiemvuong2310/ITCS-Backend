using System;
namespace FA25_CP.CryoFert_BE.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class ApiDefaultResponseAttribute : Attribute
    {
        public Type ResponseModelType { get; }

        /// <summary>
        /// If true, use DynamicResponse&lt;T&gt; as wrapper for 200 OK instead of BaseResponse&lt;T&gt;
        /// </summary>
        public bool UseDynamicWrapper { get; set; } = true;
        public bool IncludeForbidden { get; set; } = false;

        public ApiDefaultResponseAttribute(Type responseModelType)
        {
            ResponseModelType = responseModelType ?? throw new ArgumentNullException(nameof(responseModelType));
        }
    }
}
