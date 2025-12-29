using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiotechoneCQRS.Application.DTOs.ResponseDTOs;

public class ResponseDTO<T>
{
    public bool IsSuccess { get; set; }
    public int StatusCode { get; set; }
    public string? ErrorMessage { get; set; }
    public T? Data { get; set; }

    public static ResponseDTO<T> Success(T data, int statusCode = 200)
        => new ResponseDTO<T> { IsSuccess = true, StatusCode = statusCode, Data = data };

    public static ResponseDTO<T> Failure(string error, int statusCode = 400)
        => new ResponseDTO<T> { IsSuccess = false, StatusCode = statusCode, ErrorMessage = error };
}

// shortcut for commands (no data needed)
public class ResponseDTO : ResponseDTO<object>
{
    public new static ResponseDTO Success(int statusCode = 200)
        => new ResponseDTO { IsSuccess = true, StatusCode = statusCode };

    public new static ResponseDTO Failure(string error, int statusCode = 400)
        => new ResponseDTO { IsSuccess = false, StatusCode = statusCode, ErrorMessage = error };
}
