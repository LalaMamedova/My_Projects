namespace EcommerceLib.Models.UserModel.JWT;

public class TokenResponse
{
    public MyToken Token { get; set; }
    public DateTime TokenValid { get; set; }
}
