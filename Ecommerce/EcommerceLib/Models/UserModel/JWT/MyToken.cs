using System.Text.Json.Serialization;

namespace EcommerceLib.Models.UserModel.JWT;

public class MyToken
{
    public string AcssesToken { get; set; }
    public string RefreshToken { get; set; }
}
