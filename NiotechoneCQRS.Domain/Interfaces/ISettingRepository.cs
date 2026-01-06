using NiotechoneCQRS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NiotechoneCQRS.Domain.Interfaces;

public interface ISettingRepository
{
    Task<IList<dynamic>> GetConfigurationList(long CompanyId, CancellationToken cancellationToken = default);
    Task<bool> UpdateConfiguration(ConfigManagerValue value, CancellationToken cancellationToken = default);
    Task<bool> SaveSystemConfigurationValue(IList<ConfigManagerValue> configList, CancellationToken cancellationToken);
    Task<ConfigManagerValue> GetConfigMasterByKey(string configMasterKey, long companyId, CancellationToken cancellationToken);
}
