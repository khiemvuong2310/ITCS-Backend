using System;
namespace FA25_CP.CryoFert_BE.AppStarts
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class ApiDefaultResponseAttribute : Attribute
    {
        public Type ResponseModelType { get; }
        public bool UseDynamicWrapper { get; set; } = true;

        public ApiDefaultResponseAttribute(Type responseModelType)
        {
            ResponseModelType = responseModelType ?? throw new ArgumentNullException(nameof(responseModelType));
        }
    }
}
