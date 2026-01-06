using NiotechoneCQRS.Domain.Entities;

namespace NiotechoneCQRS.Domain.Interfaces;

public interface IConfigManagerRepository
{
    Task<ConfigManagerValue?> GetConfigMasterByKey(string configMasterKey, long companyId);
}
