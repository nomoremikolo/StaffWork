namespace StaffWork.Server.Utils.Models
{
    public class HashWithSaltModel
    {
        public string Hash { get; set; }
        public string Salt { get; set; }
        public HashWithSaltModel(string hash, string salt)
        {
            Hash = hash;
            Salt = salt;
        }
    }
}
