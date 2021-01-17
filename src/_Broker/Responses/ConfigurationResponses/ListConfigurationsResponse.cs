using System.Collections.Generic;

namespace Communications.Responses
{
    public interface ListConfigurationsResponse
    {
        int Page { get; set; }
        int PageSize { get; set; }
        int RecordsInPage { get; set; }
        int TotalPages { get; set; }
        List<ConfigurationResponse> Items { get; set; }
    }
}