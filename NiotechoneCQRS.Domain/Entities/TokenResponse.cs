using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiotechoneCQRS.Domain.Entities;

public class TokenResponse
{
    public string Token { get; set; } = null!;
    public string TokenType { get; set; } = "Bearer";
    public string Message { get; set; } = null!;
    public DateTime Expires { get; set; }
}
