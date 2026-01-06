using MediatR;
using Microsoft.AspNetCore.Http;
using NiotechoneCQRS.Application.DTOs.ResponseDTOs;
using NiotechoneCQRS.Domain.Interfaces;
using NiotechoneCQRS.Domain.Entities;
using NiotechoneCQRS.Utility.CommonResource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace NiotechoneCQRS.Application.User.Queries.GetUserById;

public record GetUserById(string id) : IRequest<ResponseDTO<Domain.Entities.User>>
{
}

//public class GetUserByIdQueryHandler : IRequestHandler<GetUserById, ResponseDTO<Domain.Entities.User>>
//{
//    private readonly IUserRepository _userRepository;

//    public GetUserByIdQueryHandler(IUserRepository userRepository)
//    {
//        _userRepository = userRepository;
//    }

//    public async Task<ResponseDTO<Domain.Entities.User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
//    {
//        var user = await _userRepository.GetUserById(request.id, cancellationToken);
//        if (user == null)
//        {
//            return ResponseDTO<Domain.Entities.User>.Failure(
//                string.Format(AppResource.Notfound, "User"),
//                (int)HttpStatusCode.NotFound
//            );
//        }

//        return ResponseDTO<Domain.Entities.User>.Success(user, (int)HttpStatusCode.OK);
//    }
//}
