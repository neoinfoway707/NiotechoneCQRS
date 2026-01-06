using MediatR;
using NiotechoneCQRS.Application.DTOs.ResponseDTOs;
using NiotechoneCQRS.Domain.Entities;
using NiotechoneCQRS.Domain.Interfaces;
using NiotechoneCQRS.Utility.CommonResource;
using System.Net;

namespace NiotechoneCQRS.Application.Permission.Queries.GetKPIList;

public class GetKpiListQuery : IRequest<ResponseDTO<List<KPIList>>>
{
    public long UserRoleId { get; set; }
    public long CompanyId { get; set; }
}

public class GetKpiListQueryHandler : IRequestHandler<GetKpiListQuery, ResponseDTO<List<KPIList>>>
{
    private readonly IPermissionRepository _permissionRepository;
    public GetKpiListQueryHandler(IPermissionRepository permissionRepository)
    {
        _permissionRepository = permissionRepository;
    }

    public async Task<ResponseDTO<List<KPIList>>> Handle(GetKpiListQuery request, CancellationToken cancellationToken)
    {
        var kpiList = await _permissionRepository.GetKpiList(request.UserRoleId, request.CompanyId, cancellationToken);
        if (kpiList == null)
        {
            return ResponseDTO<List<KPIList>>.Failure(
                string.Format(AppResource.Notfound, "KPI List"),
                (int)HttpStatusCode.NotFound
            );
        }

        return ResponseDTO<List<KPIList>>.Success(kpiList, (int)HttpStatusCode.OK);
    }
}
