using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using NiotechoneCQRS.Application.DTOs.ResponseDTOs;
using NiotechoneCQRS.Application.User.Queries.GetAllUsers;
using NiotechoneCQRS.Domain.Interfaces;
using NiotechoneCQRS.Utility.CommonResource;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiotechoneCQRS.Application.Settings.Queries.GetConfigurationListByCompanyId;

public record GetConfigurationListQuery : IRequest<ResponseDTO<IList<object>>>
{
    public long CompanyId  { get; set; }
}


public class GetConfigurationListHandler : IRequestHandler<GetConfigurationListQuery, ResponseDTO<IList<object>>>
{
    private readonly ISettingRepository _settingRepository;

    public GetConfigurationListHandler(ISettingRepository settingRepository)
    {
        _settingRepository = settingRepository;
    }
    public async Task<ResponseDTO<IList<object>>> Handle(
        GetConfigurationListQuery request,
        CancellationToken cancellationToken)
    {
        var data = await _settingRepository
            .GetConfigurationList(request.CompanyId, cancellationToken);

        if (data == null || !data.Any())
        {
            return ResponseDTO<IList<object>>.Failure(
                string.Format(AppResource.Notfound, "Configuration List"),
                404
            );
        }

        return ResponseDTO<IList<object>>.Success(
            data.Cast<object>().ToList(),
            200
        );
    }
}