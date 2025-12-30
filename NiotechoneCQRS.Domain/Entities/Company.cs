using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace NiotechoneCQRS.Domain.Entities;

public class Company
{
    /// <summary>
    /// Gets or sets the company identifier.
    /// </summary>
    /// <value>
    /// The company identifier.
    /// </value>
    public long CompanyId { get; set; }

    /// <summary>
    /// Gets or sets the encrypted company id.
    /// </summary>
    /// <value>
    /// The company identifier encrypted.
    /// </value>
    public string CompanyIdEncrypt { get; set; }
    /// <summary>
    /// Gets or sets the name of the company.
    /// </summary>
    /// <value>
    /// The name of the company.
    /// </value>
    public string CompanyName { get; set; }

    /// <summary>
    /// Gets or sets the address.
    /// </summary>
    /// <value>
    /// The address.
    /// </value>
    public string Address { get; set; }

    /// <summary>
    /// Gets or sets the po box.
    /// </summary>
    /// <value>
    /// The po box.
    /// </value>
    public string POBox { get; set; }

    /// <summary>
    /// Gets or sets the city.
    /// </summary>
    /// <value>
    /// The city.
    /// </value>
    public string City { get; set; }

    /// <summary>
    /// Gets or sets the country identifier.
    /// </summary>
    /// <value>
    /// The country identifier.
    /// </value>
    public int CountryId { get; set; }

    /// <summary>
    /// Gets or sets the phone.
    /// </summary>
    /// <value>
    /// The phone.
    /// </value>
    public string Phone { get; set; }

    /// <summary>
    /// Gets or sets the status identifier.
    /// </summary>
    /// <value>
    /// The status identifier.
    /// </value>
    public int StatusId { get; set; }

    public bool StatusBoolValue
    {
        get { return StatusId == 1; }
        set
        {
            if (value)
                StatusId = 1;
            else
                StatusId = 0;
        }
    }

    /// <summary>
    /// Gets or sets the name of the status.
    /// </summary>
    /// <value>
    /// The name of the status.
    /// </value>
    [NotMapped]
    public string StatusName { get; set; }

    /// <summary>
    /// Gets or sets the billable.
    /// </summary>
    /// <value>
    /// The billable.
    /// </value>
    public bool? Billable { get; set; }

    /// <summary>
    /// Gets or sets the work request.
    /// </summary>
    /// <value>
    /// The work request.
    /// </value>
    public bool? WorkRequest { get; set; }

    /// <summary>
    /// Gets or sets the work request URL.
    /// </summary>
    /// <value>
    /// The work request URL.
    /// </value>
    public string WorkRequestURL { get; set; }

    /// <summary>
    /// Gets or sets the currency identifier.
    /// </summary>
    /// <value>
    /// The currency identifier.
    /// </value>
    public int CurrencyId { get; set; }

    /// <summary>
    /// Gets or sets the threshold value.
    /// </summary>
    /// <value>
    /// The threshold value.
    /// </value>
    public int? ThresholdValue { get; set; }

    /// <summary>
    /// Gets or sets the Google Analytics Tracking Id.
    /// </summary>
    /// <value>
    /// The Google Analytics Tracking Id.
    /// </value>
    public string GoogleAnalyticsTrackingId { get; set; }

    /// <summary>
    /// Gets or sets the created date.
    /// </summary>
    /// <value>
    /// The created date.
    /// </value>
    public DateTime? CreatedDate { get; set; }

    /// <summary>
    /// Gets or sets the created by.
    /// </summary>
    /// <value>
    /// The created by.
    /// </value>
    public long? CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the modified date.
    /// </summary>
    /// <value>
    /// The modified date.
    /// </value>
    public DateTime? ModifiedDate { get; set; }

    /// <summary>
    /// Gets or sets the modify by.
    /// </summary>
    /// <value>
    /// The modify by.
    /// </value>
    public long? ModifyBy { get; set; }

    /// <summary>
    /// Gets or sets the country.
    /// </summary>
    /// <value>
    /// The country.
    /// </value>
    public IEnumerable<Country> Country { get; set; }

    /// <summary>
    /// Gets or sets the currency.
    /// </summary>
    /// <value>
    /// The currency.
    /// </value>
    public IEnumerable<ParameterValue> Currency { get; set; }

    /// <summary>
    /// Gets or sets the working hour.
    /// </summary>
    /// <value>
    /// The working hour.
    /// </value>
    public int WorkingHour { get; set; }

    /// <summary>
    /// Gets or sets the language id.
    /// </summary>
    /// <value>The language id.</value>
    public int? LanguageId { get; set; }

    /// <summary>
    /// Gets or sets the allow personal for mob.
    /// </summary>
    /// <value>
    /// The allow personal for mob.
    /// </value>
    public Nullable<long> AllowForMobileUser { get; set; }

    public int? CompanyType { get; set; }

    public int? NoOfCompanies { get; set; }

    public int? NoOfRequestors { get; set; }

    public int? NoOfAdmins { get; set; }

    public int? NoOfSupervisors { get; set; }

    public int? NoOfClients { get; set; }

    public string Timezone { get; set; }

    public IFormFile LogoFile { get; set; }
    public IFormFile CompanyHeaderFile { get; set; }
    public IFormFile CompanyFooterFile { get; set; }

    public string PurchaseReqEmails { get; set; }

    public IEnumerable<ConfigManager> Configuration { get; set; }

    public bool IssuanceExist { get; set; }

    public bool? IsSubscription { get; set; }
    public short? SubscriptionModelType { get; set; }
    public string CompanyLogoPath { get; set; }
    public string CompanyHeaderFilePath { get; set; }
    public string CompanyFooterFilePath { get; set; }
    public string LanguageName { get; set; }
    /// <summary>
    /// value added Tax added for Sales Module
    /// </summary>
    public decimal VAT { get; set; }
    public string TaxRegistrationNo { get; set; }

    public string PrimaryColor { get; set; }
    public string LogoURL { get; set; }
    public string CompanyHeaderURL { get; set; }
    public string CompanyFooterURL { get; set; }
}

public class WorderOrderReminderSettingsModel
{
    public long ReminderId { get; set; }
    public long CompanyId { get; set; }
    public long HoursBeforeWO { get; set; }
    public bool IsActive { get; set; }
    public long UserId { get; set; }
    public DateTime CurrentDateTime { get; set; }
    public bool PushNotifications { get; set; }
    public bool EmailNotifications { get; set; }

}

public class WorkOrderServiceReportConfigurationModel
{
    public long ConfigurationId { get; set; }
    public long CompanyId { get; set; }
    public string ConfigurationKey { get; set; }
    public int ConfigurationKeyId { get; set; }

    public bool ConfigurationValue { get; set; }
    public string ConfigurationValueString { get; set; }

}
public class RFIDConfigurationModel
{
    public int CompanyID { get; set; }

    // Hexadecimal QR code option
    public bool IsHexaCode { get; set; } = false;

    // Metallic Label Settings
    public bool Is92BitForMetallic { get; set; } = false;
    public string Label92BitForMetallic { get; set; }
    public string ZPL92BitForMetallic { get; set; }

    public bool Is128BitForMetallic { get; set; } = false;
    public string Label128BitForMetallic { get; set; }
    public string ZPL128BitForMetallic { get; set; }

    // Non-Metallic Label Settings
    public bool Is92BitForNonMetallic { get; set; } = false;
    public string Label92BitForNonMetallic { get; set; }
    public string ZPL92BitForNonMetallic { get; set; }

    public bool Is128BitForNonMetallic { get; set; } = false;
    public string Label128BitForNonMetallic { get; set; }
    public string ZPL128BitForNonMetallic { get; set; }
}
public class WorkOrderCompletionThresholdsConfigurationModel
{
    public long ConfigurationId { get; set; }
    public long CompanyId { get; set; }
    public string ConfigurationKey { get; set; }
    public int ConfigurationKeyId { get; set; }
    public int ConfigurationValue { get; set; }
    public string ConfigurationValueString { get; set; }

}

public class SalesModulePrefixConfigurationModel
{
    public long ConfigurationId { get; set; }
    public long CompanyId { get; set; }
    public string ConfigurationKey { get; set; }
    public int ConfigurationKeyId { get; set; }
    public int ConfigurationValue { get; set; }
    public string ConfigurationValueString { get; set; }

}

public class RentalJobModulePrefixConfigurationModel
{
    public long ConfigurationId { get; set; }
    public long CompanyId { get; set; }
    public string ConfigurationKey { get; set; }
    public int ConfigurationKeyId { get; set; }
    public int ConfigurationValue { get; set; }
    public string ConfigurationValueString { get; set; }

}

public class InvoicingConfigurationModel
{
    public long ConfigurationId { get; set; }
    public long CompanyId { get; set; }
    public string ConfigurationKey { get; set; }
    public int ConfigurationKeyId { get; set; }
    public int ConfigurationValue { get; set; }
    public string ConfigurationValueString { get; set; }

}

public class GeoFenceThresholdsConfigurationModel
{
    public long ConfigurationId { get; set; }
    public long CompanyId { get; set; }
    public string ConfigurationKey { get; set; }
    public int ConfigurationKeyId { get; set; }
    public int ConfigurationValue { get; set; }
    public string ConfigurationValueString { get; set; }

}
public class ZohoAccountConfigurationModel
{
    public long ConfigurationId { get; set; }
    public long CompanyId { get; set; }
    public string ConfigurationKey { get; set; }
    public int ConfigurationKeyId { get; set; }
    public int ConfigurationValue { get; set; }
    public string ConfigurationValueString { get; set; }

}
public class CompanyConfigModel
{
    public long CompanyId { get; set; }
    public bool IsBabirusConfigEnabled { get; set; }
    public bool IsTimeInDaysConfigEnabled { get; set; }
    public bool IsGeoFenceConfigEnabled { get; set; }
    public bool IsSalesModuleConfigEnabled { get; set; }
    public bool IsRentalJobModuleConfigEnabled { get; set; }
    public bool IsInvoicingModuleConfigEnabled { get; set; }
    public bool IsLDAPModuleConfigEnabled { get; set; }
    public bool IsCustomNotificationsEnabled { get; set; }
    public bool IsClientInstallationEnabled { get; set; }
    public bool IsSLAConfigurationConfigEnabled { get; set; }
    public bool IsLHCIntegrationConfigEnabled { get; set; }
    public bool IsAdditionalFieldsConfigEnabled { get; set; }
}
public class LDAPConfigurationModel
{
    public long ConfigurationId { get; set; }
    public long CompanyId { get; set; }
    public string ConfigurationKey { get; set; }
    public int ConfigurationKeyId { get; set; }
    public int ConfigurationValue { get; set; }
    public string ConfigurationValueString { get; set; }
}
public class AssetTagDesignConfigurationModel
{
    public long ConfigurationId { get; set; }
    public long CompanyId { get; set; }
    public string ConfigurationKey { get; set; }
    public int ConfigurationKeyId { get; set; }
    public int ConfigurationValue { get; set; }
    public string ConfigurationValueString { get; set; }

}
public class KeyValueConfigurationModel
{
    public long ConfigurationId { get; set; }
    public long CompanyId { get; set; }
    public string ConfigurationKey { get; set; }
    public int ConfigurationKeyId { get; set; }
    public bool ConfigurationValue { get; set; }
    public string ConfigurationValueString { get; set; }

}
public class LHCConfigurationModel
{
    public long ConfigurationId { get; set; }
    public long CompanyId { get; set; }
    public string ConfigurationKey { get; set; }
    public int ConfigurationKeyId { get; set; }
    public int ConfigurationValue { get; set; }
    public string ConfigurationValueString { get; set; }
}
