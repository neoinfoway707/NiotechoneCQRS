using MediatR;
using NiotechoneCQRS.Application.DTOs.ResponseDTOs;
using NiotechoneCQRS.Application.Permission.Queries.GetKPIList;
using NiotechoneCQRS.Domain.Entities;
using NiotechoneCQRS.Domain.Interfaces;
using NiotechoneCQRS.Utility.CommonResource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NiotechoneCQRS.Application.Parameter.Queries.GetAllParameterValues;

public class GetAllParameterValuesQuery : IRequest<ResponseDTO<List<ParameterValue>>>
{
    public int ParameterValuesId { get; set; }
    public string Name { get; set; }
}

public class GetAllParameterValuesHandler : IRequestHandler<GetAllParameterValuesQuery, ResponseDTO<List<ParameterValue>>>
{
    private readonly IParameterRepository _ParameterRepository;
    public GetAllParameterValuesHandler(IParameterRepository ParameterRepository)
    {
        _ParameterRepository = ParameterRepository;
    }

    public async Task<ResponseDTO<List<ParameterValue>>> Handle(GetAllParameterValuesQuery parameterName, CancellationToken cancellationToken)
    {
        var parameterList = await _ParameterRepository.GetParameterValues(parameterName.Name, cancellationToken);
        if (parameterList == null)
        {
            return ResponseDTO<List<ParameterValue>>.Failure(
                string.Format(AppResource.Notfound, "KPI List"),
                (int)HttpStatusCode.NotFound
            );
        }

        return ResponseDTO<List<ParameterValue>>.Success(parameterList, (int)HttpStatusCode.OK);
    }
}
