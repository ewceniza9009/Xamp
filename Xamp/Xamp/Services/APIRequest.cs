using System.Net.Http;

namespace Xamp.Services
{
    public class APIRequest
    {
        public static string requestUriBase = " http://10.0.2.2:80";

        public APIRequest()
        {
        }

        public static string GetLoginRequestUri(string user, string pass)
        {
            var requestUri = string.Format("{0}/tplsite/api/login/{1}/{2}", requestUriBase, user, pass);

            return requestUri;
        }

        public static string GetInventorySummaryRequestUri(string search)
        {

            var requestUri = string.Format("{0}/tplsite/api/storageinventory/json/inventorysummary/{1}", requestUriBase, search);

            return requestUri;
            
        }

        public static string GetOrderList()
        {
            var requestUri = string.Format("{0}/tplsite/api/withdrawalorders/json/withdrawalorder", requestUriBase);

            return requestUri;
        }

        public static string GetOrderDetail(int Id)
        {
            var requestUri = string.Format("{0}/tplsite/api/withdrawalorders/json/withdrawaldetail/{1}", requestUriBase, Id);

            return requestUri;
        }

        public static string ApproveWithdrawalOrder(int Id)
        {
            var requestUri = string.Format("{0}/tplsite/api/withdrawalorders/approvewithdrawalorder/{1}", requestUriBase, Id);

            return requestUri;
        }

        public static string GetOrderListA()
        {
            var requestUri = string.Format("{0}/tplsite/api/withdrawalorders/json/withdrawalordera", requestUriBase);

            return requestUri;
        }
    }
}