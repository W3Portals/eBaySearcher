using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eBaySearch.Finding;
using System.Net;

namespace eBaySearch.Models
{
    public class CustomFindingService
    {
        //protected override WebRequest GetWebRequest(Uri uri)
        //{
        //    try
        //    {
        //        HttpWebRequest request = (HttpWebRequest)base.(uri);
        //        request.Headers.Add("X-EBAY-SOA-SECURITY-APPNAME", "W3Portal-00d0-4af0-a2fc-0bdc68668695");
        //        request.Headers.Add("X-EBAY-SOA-OPERATION-NAME", "findItemsByKeywords");
        //        request.Headers.Add("X-EBAY-SOA-SERVICE-NAME", "FindingService");
        //        request.Headers.Add("X-EBAY-SOA-MESSAGE-PROTOCOL", "SOAP11");
        //        request.Headers.Add("X-EBAY-SOA-SERVICE-VERSION", "1.0.0");
        //        request.Headers.Add("X-EBAY-SOA-GLOBAL-ID", "EBAY-US");
        //        return request;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}