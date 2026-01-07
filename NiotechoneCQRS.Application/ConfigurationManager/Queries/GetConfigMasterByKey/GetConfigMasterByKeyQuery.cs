using MediatR;
using NiotechoneCQRS.Application.DTOs.ResponseDTOs;
using NiotechoneCQRS.Domain.Entities;
using NiotechoneCQRS.Domain.Interfaces;
using NiotechoneCQRS.Utility.CommonResource;
using System.Net;

namespace NiotechoneCQRS.Application.ConfigurationManager.Queries.GetConfigMasterByKey;

public class GetConfigMasterByKeyQuery : IRequest<ResponseDTO<ConfigManagerValue>>
{
    public string ConfigMasterKey {  get; set; }
    public long CompanyId { get; set; }
}

public class GetConfigMasterByKeyQueryHandler : IRequestHandler<GetConfigMasterByKeyQuery, ResponseDTO<ConfigManagerValue>>
{
    private readonly IConfigManagerRepository _managerRepository;
    public GetConfigMasterByKeyQueryHandler(IConfigManagerRepository managerRepository)
    {
        _managerRepository = managerRepository;
    }

    public async Task<ResponseDTO<ConfigManagerValue>> Handle(GetConfigMasterByKeyQuery query, CancellationToken cancellationToken)
    {
        var config = await _managerRepository.GetConfigMasterByKey(query.ConfigMasterKey, query.CompanyId);

        if (config == null)
        {
            return ResponseDTO<ConfigManagerValue>.Failure(
                string.Format(AppResource.Notfound, "Configuration"),
                (int)HttpStatusCode.NotFound
            );
        }

        return ResponseDTO<ConfigManagerValue>.Success(config, (int)HttpStatusCode.OK);
    }
}
