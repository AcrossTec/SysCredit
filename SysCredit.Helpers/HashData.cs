namespace SysCredit.Helpers;

using System.Security.Cryptography;
using System.Text;

public static class HashData
{
    public static byte[] ComputeHashSha512(this string ToBeHashed)
    {
        using var Sha512 = SHA512.Create();
        return Sha512.ComputeHash((Encoding.UTF8.GetBytes(ToBeHashed)));
    }
}
