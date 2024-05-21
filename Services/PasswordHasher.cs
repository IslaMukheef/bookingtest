using System.Security.Cryptography;
using System.Text;
namespace backend.Services;

public class PasswordHasher : IPasswordHasher
{
  private byte[] ToByte(string str) => Encoding.UTF8.GetBytes(str);
  public string HashPassword(string password)
  {
    var provider = MD5.Create();
    var bytes = ToByte(password);
    var computedBytes = provider.ComputeHash(bytes);
    var computedHash = Convert.ToBase64String(computedBytes);

    return computedHash;
  }

  public bool VerifyPassword(string password, string hash)
  {
    return HashPassword(password) == hash;
  }
}