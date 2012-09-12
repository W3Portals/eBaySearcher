using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eBaySearch.Models;
using eBaySearch.Finding;
using eBaySearch.Services.Common;
using eBaySearch.Services;

namespace eBaySearch.Controllers
{
    public class HomeController : Controller
    {
        public static string appID = "xxxGetYourOwnxxxxx";
        public static string findingServerAddress = "http://svcs.ebay.com/services/search/FindingService/v1";
        public ClientConfig config = new ClientConfig(appID, findingServerAddress);
        
        [HttpPost]
        public ActionResult Index(string Id, string zip)
        {
            FindingServicePortTypeClient client = FindingServiceClientFactory.getServiceClient(config);

            ViewBag.Message = Id;
            ViewBag.Zip = zip;
           
            SearchItem[] items = null;
            try
            {
                // Create request object
                FindItemsByKeywordsRequest request = new FindItemsByKeywordsRequest();
         
                // Set request parameters
                request.keywords =Id;
                if (!string.IsNullOrEmpty(zip))
                {
                    request.buyerPostalCode = zip;
                    ItemFilter[] ifilter = { new ItemFilter { name = ItemFilterType.MaxDistance, value = new string[] { "50" }}
                                            // ,  new ItemFilter { name = ItemFilterType.Seller, value = new string[] { "easypeaches" }} 
                                           };

                   // ItemFilter[] ifilter2 = { new ItemFilter { name = ItemFilterType.Seller, value = new string[] { "easypeaches" } } };
                    request.itemFilter = ifilter;
                }
                request.sortOrder = SortOrderType.BidCountMost;
                //request.sortOrderSpecified = true;
                OutputSelectorType[] outputs = { OutputSelectorType.SellerInfo };
                request.outputSelector = outputs;

                PaginationInput pi = new PaginationInput();
                pi.entriesPerPage = 105;
                pi.entriesPerPageSpecified = true;
                request.paginationInput = pi;

                // Call the service
                FindItemsByKeywordsResponse response = client.findItemsByKeywords(request);
   
                // Show output
                if (response.searchResult != null && response.searchResult.item != null)
                {
                     items = response.searchResult.item;
                     ViewBag.resultcount = response.searchResult.count;
                     ViewBag.totalitems = response.paginationOutput.totalEntries;
                    // var groupeditems = items.GroupBy(i => i.sellerInfo);
                     //var itemsBySeller =
                     //    from i in items
                     //    group i by i.sellerInfo into s
                     //    select new { SellerName = s.Key.sellerUserName, ItemCount = s.Count(), Items=s };

                     return View(items);
                }

            }
            catch (Exception ex)
            {
               var errorText = ex.Message;
            }

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Index()
        {
            ViewBag.Zip = "19460";
            ViewBag.Message = "lacoste";
            return View();
        }

        public ActionResult Users(string Id)
        {
            FindItemsByKeywordsRequest req = new FindItemsByKeywordsRequest();
            FindItemsByKeywordsResponse resp = new FindItemsByKeywordsResponse();
            
            return View();
        }

        public ActionResult SellerItems(string Id)
        {
            FindingServicePortTypeClient client = FindingServiceClientFactory.getServiceClient(config);
            ViewBag.Seller = Id;
            SearchItem[] items = null;
            try
            {
                // Create request object
              FindItemsAdvancedRequest  request = new FindItemsAdvancedRequest();

                ItemFilter[] ifilter = { new ItemFilter { name = ItemFilterType.Seller, value = new string[] { Id } } };
                request.itemFilter = ifilter;
                
                PaginationInput pi = new PaginationInput();
                pi.entriesPerPage = 105;
                pi.entriesPerPageSpecified = true;
                request.paginationInput = pi;

                // Call the service
                FindItemsAdvancedResponse response = client.findItemsAdvanced(request);

                // Show output
                if (response.searchResult != null && response.searchResult.item != null)
                {
                    items = response.searchResult.item;
                    ViewBag.resultcount = response.searchResult.count;
                    ViewBag.totalitems = response.paginationOutput.totalEntries;
                    ViewBag.location = items[0].location;
                    return View(items);
                }

            }
            catch (Exception ex)
            {
                var errorText = ex.Message;
            }

            return View();
        }
    }
}
