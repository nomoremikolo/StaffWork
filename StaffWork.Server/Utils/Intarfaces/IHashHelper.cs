using StaffWork.Server.Utils.Models;

namespace StaffWork.Server.Utils.Intarfaces
{
    public interface IHashHelper
    {
        HashWithSaltModel GenerateSaltedHash(string password);
        bool VerifyHash(string password, string storedHash, string storedSalt);
    }
}
