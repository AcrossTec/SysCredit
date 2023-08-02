namespace SysCredit.Api.Extensions;

public static class TypeExtensions
{
    public static Type GetUnderlyingType(this Type Type)
    {
        static Type NullableGetUnderlyingType(Type Type) => Nullable.GetUnderlyingType(Type) ?? Type;

        if (Type.IsEnum)
        {
            return Enum.GetUnderlyingType(NullableGetUnderlyingType(Type));
        }

        return NullableGetUnderlyingType(Type);
    }
}
