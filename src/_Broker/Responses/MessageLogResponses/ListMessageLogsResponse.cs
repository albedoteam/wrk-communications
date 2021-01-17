using System.Collections.Generic;

namespace Communications.Responses
{
    public interface ListMessageLogsResponse
    {
        int Page { get; set; }
        int PageSize { get; set; }
        int RecordsInPage { get; set; }
        int TotalPages { get; set; }
        List<MessageLogResponse> Items { get; set; }
    }
}