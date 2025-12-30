namespace NiotechoneCQRS.Application.DTOs.ResponseDTOs;

public class LoginApiResponse
{
    public string? Token { get; set; }
    public string? TokenType { get; set; }
    public DateTime? Expires { get; set; }
    public Domain.Entities.User? User { get; set; }
}
