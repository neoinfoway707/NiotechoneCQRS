namespace NiotechoneCQRS.Application.DTOs.ResponseDTOs;

public class CompanyListModelDTO
{
    public bool isSuccess { get; set; }
    public int statusCode { get; set; }
    public object errorMessage { get; set; }
    public List<CompanyData> data { get; set; }
}

public class CompanyData
{
    public long CompanyId { get; set; }
    public string CompanyName { get; set; }
    public string Address { get; set; }
    public string POBox { get; set; }
    public string City { get; set; }
    public int CountryId { get; set; }
    public string Timezone { get; set; }
    public string Phone { get; set; }
    public int StatusId { get; set; }
    public Nullable<bool> Billable { get; set; }
    public Nullable<bool> WorkRequest { get; set; }
    public string WorkRequestURL { get; set; }
    public int CurrencyId { get; set; }
    public Nullable<int> ThresholdValue { get; set; }
    public Nullable<int> WorkingHour { get; set; }
    public string GoogleAnalyticsTrackingId { get; set; }
    public Nullable<System.DateTime> CreatedDate { get; set; }
    public Nullable<long> CreatedBy { get; set; }
    public Nullable<System.DateTime> ModifiedDate { get; set; }
    public Nullable<long> ModifyBy { get; set; }
    public Nullable<int> LanguageId { get; set; }
    public Nullable<long> AllowForMobileUser { get; set; }
    public Nullable<int> CompanyType { get; set; }
    public Nullable<int> NoOfCompanies { get; set; }
    public Nullable<int> NoOfRequestors { get; set; }
    public Nullable<int> NoOfAdmins { get; set; }
    public string PurchaseReqEmails { get; set; }
    public Nullable<bool> IssuanceExist { get; set; }
    public Nullable<int> AssetListConfiguration { get; set; }
    public Nullable<bool> IsSubscription { get; set; }
    public Nullable<short> SubscriptionModelType { get; set; }
    public Nullable<bool> IsCompanySwitchingEnabled { get; set; }
    public Nullable<int> NoOfSupervisors { get; set; }
    public Nullable<decimal> VAT { get; set; }
    public string TaxRegistrationNo { get; set; }
    public Nullable<int> NoOfClients { get; set; }
}
