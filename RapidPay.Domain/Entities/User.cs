namespace RapidPay.Domain.Entities;

public class User(string userName, string email, string password)
{
    public string UserName { get; set; } = userName;

    public string Email { get; set; } = email;

    public string Password { get; set; } = password;
}
