using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MyParcelApi.Net.Helpers;
using MyParcelApi.Net.Models;
using MyParcelApi.Net.Wrappers;

namespace MyParcelApi.Net
{
    public class MyParcelApiClient
    {
        private const string ApiBaseUrl = "https://api.myparcel.nl/";
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Instantiates a new MyParcelApiClient
        /// </summary>
        /// <param name="apiKey">API key which can be generated on myparcel.nl</param>
        public MyParcelApiClient(string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentException("Parameter apiKey needs a value");

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(ApiBaseUrl)
            };

            _httpClient.DefaultRequestHeaders.Add("Authorization",
                $"basic {Convert.ToBase64String(Encoding.UTF8.GetBytes(apiKey))}");
        }

        /// <summary>
        /// Add shipments allows you to create standard shipments.
        /// </summary>
        /// <param name="shipments"></param>
        /// <returns>Array of ShipmentIds is returned. The ids in the ShipmentIds array will be in the same order they where sent.</returns>
        public async Task<ObjectId[]> AddShipment(Shipment[] shipments)
        {
            var apiWrapper = new ApiWrapper
            {
                Data = new DataWrapper
                {
                    Shipments = shipments
                }
            };
            var content = new StringContent(JsonHelper.Serialize(apiWrapper));
            content.Headers.Clear();
            content.Headers.Add("Content-Type", "application/vnd.shipment+json;charset=utf-8");
            var response = await _httpClient.PostAsync("shipments", content).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper>(jsonResult).Data.Ids;
            }
            var errors = JsonHelper.Deserialize<ApiWrapper>(jsonResult).Errors;
            var message = string.Join("\n", errors.Select(obj => string.Join("\n", obj.Human)));
            throw new Exception(message);
        }

        /// <summary>
        /// Add return shipments allows you to create related return shipments.
        /// </summary>
        /// <param name="returnShipments"></param>
        /// <returns>Array of ShipmentIds is returned. The ids in the ShipmentIds array will be in the same order they where sent.</returns>
        public async Task<ObjectId[]> AddReturnShipment(ReturnShipment[] returnShipments)
        {
            return await AddReturnShipment(returnShipments, false).ConfigureAwait(false);
        }

        /// <summary>
        /// Add unrelated return shipments allows you to create unrelated return shipments.
        /// </summary>
        /// <param name="returnShipments"></param>
        /// <returns>Array of ShipmentIds is returned. The ids in the ShipmentIds array will be in the same order they where sent.</returns>
        public async Task<ObjectId[]> AddUnrelatedReturnShipment(ReturnShipment[] returnShipments)
        {
            return await AddReturnShipment(returnShipments, true).ConfigureAwait(false);
        }

        private async Task<ObjectId[]> AddReturnShipment(ReturnShipment[] returnShipments, bool unrelated)
        {
            var apiWrapper = new ApiWrapper
            {
                Data = new DataWrapper
                {
                    ReturnShipments = returnShipments
                }
            };
            var content = new StringContent(JsonHelper.Serialize(apiWrapper));
            content.Headers.Clear();
            content.Headers.Add("Content-Type", $"application/vnd.{(unrelated ? "unrelated_" : string.Empty)}return_shipment+json; charset=utf-8");
            var response = await _httpClient.PostAsync("shipments", content).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper>(jsonResult).Data.Ids;
            }
            return null;
        }

        /// <summary>
        /// Use this function to remove shipments. You can specify multiple shipment ids. Only shipments with status 'pending - concept' can be deleted. 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns>This method returns true if successful.</returns>
        public async Task<bool> DeleteShipment(int[] ids)
        {
            var response = await _httpClient.DeleteAsync("shipments/" + string.Join(";", ids)).ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// This function is for external parties to facilitate return shipments on a dedicated part of their website, mainly when offering reverse logistics e.g. repair services. It will allow the consumer to send packages to the merchant directly from the merchant's website.
        /// </summary>
        /// <returns></returns>
        public async Task<DownloadUrl> GenerateUnrelatedReturnShipment()
        {
            var content = new StringContent(string.Empty);
            var response = await _httpClient.PostAsync("return_shipments", content).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper>(jsonResult).Data.DownloadUrl;
            }
            return null;
        }

        /// <summary>
        /// With this function you can get shipments. You can use the 'q' query parameter to search for shipments. Multiple shipment ids can be specified.
        /// </summary>
        /// <param name="ids">This is the shipment id. You can specify multiple shipment ids.</param>
        /// <param name="page">Page number. Maximum value is 1000 and minimum is 1. Defaults to 1.</param>
        /// <param name="size">Items per page. Maximum value is 200 and minimum is 30. Defaults to 30.</param>
        /// <param name="sort">Shipment object field to sort on. See Shipment</param>
        /// <param name="order">Sort order for sort filter. Defaults to ASC.</param>
        /// <param name="q">Use this parameter to search true all the fields of a shipment object including the embedded objects.</param>
        /// <param name="dropoffToday">Use this parameter to filter for shipments that need to dropped at a PostNL location today.</param>
        /// <param name="status">Use this parameter to specify the shipment status to filter on. You can specify multiple status.</param>
        /// <param name="from">Use this parameter to filter on the shipment creation date. This filter will set the lower bound of the date search range.</param>
        /// <param name="to">Use this parameter to filter on the shipment creation date. This filter will set the upper bound of the date search range.</param>
        /// <returns>Upon success a array of Shipment objects is returned.</returns>
        public async Task<Shipment[]> GetShipment(int[] ids = null, int size = 30, int page = 1, string sort = "", string order = "ASC", string q = "", bool? dropoffToday = null, ShipmentStatus[] status = null, DateTime? from = null, DateTime? to = null)
        {
            if (page <= 0)
                throw new ArgumentOutOfRangeException("Parameter page cannot 0 or less");

            if (size <= 0)
                throw new ArgumentOutOfRangeException("Parameter size cannot 0 or less");

            // TODO maybe use an enum?
            if (order.ToLowerInvariant() != "desc" && order.ToLowerInvariant() != "asc")
                throw new ArgumentOutOfRangeException("Parameter order can only be asc or desc");

            var urlBuilder = new StringBuilder("shipments/");

            if (ids != null && ids.Length > 0)
                urlBuilder.Append($"{string.Join(";", ids)}");

            var parameters = new Dictionary<string, string>
            {
                { "page", page.ToString() },
                { "size", size.ToString() }
            };
            if (!string.IsNullOrWhiteSpace(sort))
                parameters.Add("sort", sort);
            if (!string.IsNullOrWhiteSpace(order))
                parameters.Add("order", order);
            if (!string.IsNullOrWhiteSpace(q))
                parameters.Add("q", q);
            if (dropoffToday.HasValue)
                parameters.Add("dropoff_today", Convert.ToInt32(dropoffToday.Value).ToString());
            if (status != null && status.Length > 0)
                parameters.Add("status", string.Join(";", status.Select(s => (int)s)));
            if (from.HasValue)
                parameters.Add("from", from.Value.ToString("yyyy-MM-dd"));
            if (to.HasValue)
                parameters.Add("to", to.Value.ToString("yyyy-MM-dd"));
            urlBuilder.Append(GetQueryString(parameters));

            var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper>(jsonResult).Data.Shipments;
            }
            return null;
        }

        /// <summary>
        /// Get shipment label. You can specify label format and starting position of labels on the first page with the FORMAT and POSITION query parameters. The POSITION parameter only works when you specify the A4 format and is only applied on the first page with labels. 
        /// </summary>
        /// <param name="ids">This is the shipment id. You can specify multiple shipment ids by semi-colon separating them on the URI.</param>
        /// <param name="format">The paper size of the PDF. Currently A4 and A6 are supported. When A4 is chosen you can specify the label position. When requesting the label for a shipment that contains a custom form, you can only request a A4 format.</param>
        /// <param name="positions">The position of the label on an A4 sheet. You can specify multiple positions by semi-colon separating them on the URI. Positioning is only applied on the first page with labels. All subsequent pages will use the default positioning 1,2,3,4.</param>
        /// <returns>Shipment label PDF or PaymentInstructions (when payment is required)</returns>
        public async Task<object> GetShipmentLabel(int[] ids, string format = "", int[] positions = null)
        {
            var urlBuilder = new StringBuilder("shipment_labels/");

            if (ids != null && ids.Length > 0)
                urlBuilder.Append($"{string.Join(";", ids)}");

            var parameters = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(format))
                parameters.Add("format", format);
            if (positions != null && positions.Length > 0)
                parameters.Add("positions", string.Join(";", positions));
            urlBuilder.Append(GetQueryString(parameters));

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/pdf"));

            var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return stream;
            }
            return JsonHelper.Deserialize<ApiWrapper>(jsonResult).Data.PaymentInstructions;

            //var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            //var data = await _httpClient.GetByteArrayAsync(urlBuilder.ToString()).ConfigureAwait(false);
            //return data;
            //return JsonHelper.Deserialize<ApiWrapper>(jsonResult).Data.Shipments;
        }

        /// <summary>
        /// Get shipment label. You can specify label format and starting position of labels on the first page with the FORMAT and POSITION query parameters. The POSITION parameter only works when you specify the A4 format and is only applied on the first page with labels. 
        /// </summary>
        /// <param name="ids">This is the shipment id. You can specify multiple shipment ids by semi-colon separating them on the URI.</param>
        /// <param name="format">The paper size of the PDF. Currently A4 and A6 are supported. When A4 is chosen you can specify the label position. When requesting the label for a shipment that contains a custom form, you can only request a A4 format.</param>
        /// <param name="positions">The position of the label on an A4 sheet. You can specify multiple positions by semi-colon separating them on the URI. Positioning is only applied on the first page with labels. All subsequent pages will use the default positioning 1,2,3,4.</param>
        /// <returns>ShipmentLabelDownloadLink or PaymentInstructions (when payment is required)</returns>
        public async Task<object> GetShipmentLabelDownloadLink(int[] ids, string format = "", int[] positions = null)
        {
            var urlBuilder = new StringBuilder("shipment_labels/");

            if (ids != null && ids.Length > 0)
                urlBuilder.Append($"{string.Join(";", ids)}");

            var parameters = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(format))
                parameters.Add("format", format);
            if (positions != null && positions.Length > 0)
                parameters.Add("positions", string.Join(";", positions));
            urlBuilder.Append(GetQueryString(parameters));

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper>(jsonResult).Data.DownloadLink;
            }
            return JsonHelper.Deserialize<ApiWrapper>(jsonResult).Data.PaymentInstructions;
        }

        /// <summary>
        /// Get detailed track and trace information for a shipment.
        /// </summary>
        /// <param name="ids">This is the shipment id. You can specify multiple shipment ids.</param>
        /// <param name="page">Page number. Maximum value is 1000 and minimum is 1. Defaults to 1.</param>
        /// <param name="size">Items per page. Maximum value is 200 and minimum is 30. Defaults to 30.</param>
        /// <param name="sort">TrackTrace object field to sort on.</param>
        /// <param name="order">Sort order for sort filter. Defaults to ASC.</param>
        /// <returns>Upon success an array of TrackTrace objects is returned</returns>
        public async Task<TrackTrace[]> TrackShipment(int[] ids, int page = 1, int size = 30, string sort = "",
            string order = "ASC")
        {
            if (page <= 0)
                throw new ArgumentOutOfRangeException("Parameter page cannot 0 or less");

            if (size <= 0)
                throw new ArgumentOutOfRangeException("Parameter size cannot 0 or less");

            // TODO maybe use an enum?
            if (order.ToLowerInvariant() != "desc" && order.ToLowerInvariant() != "asc")
                throw new ArgumentOutOfRangeException("Parameter order can only be asc or desc");

            var urlBuilder = new StringBuilder("tracktraces/");

            if (ids != null && ids.Length > 0)
                urlBuilder.Append($"{string.Join(";", ids)}");

            var parameters = new Dictionary<string, string>
            {
                { "page", page.ToString() },
                { "size", size.ToString() }
            };
            if (!string.IsNullOrWhiteSpace(sort))
                parameters.Add("sort", sort);
            if (!string.IsNullOrWhiteSpace(order))
                parameters.Add("order", order);
            urlBuilder.Append(GetQueryString(parameters));

            var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper>(jsonResult).Data.TrackTraces;
            }
            return null;
        }

        /// <summary>
        /// Get the delivery options for a given location and carrier. If none of the optional parameters are specified then the following default will be used: If a request is made for the delivery options between Friday after the default cutoff_time (15h30) and Monday before the default cutoff_time (15h30) then Tuesday will be shown as the next possible delivery date.
        /// </summary>
        /// <param name="countryCode">The country code for which to fetch the delivery options</param>
        /// <param name="postalCode">The postal code for which to fetch the delivery options.</param>
        /// <param name="number">The street number for which to fetch the delivery options.</param>
        /// <param name="carier">The carrier for which to fetch the delivery options.</param>
        /// <param name="deliveryTime">The time on which a package has to be delivered. Note: This is only an indication of time the package will be delivered on the selected date.</param>
        /// <param name="deliveryDate">The date on which the package has to be delivered.</param>
        /// <param name="cutoffTime">This option allows the Merchant to indicate the latest cut-off time before which a consumer order will still be picked, packed and dispatched on the same/first set dropoff day, taking into account the dropoff-delay. Default time is 15h30. For example, if cutoff time is 15h30, Monday is a delivery day and there's no delivery delay; all orders placed Monday before 15h30 will be dropped of at PostNL on that same Monday in time for the Monday collection.</param>
        /// <param name="dropoffDays">This options allows the Merchant to set the days she normally goes to PostNL to hand in her parcels. By default Saturday and Sunday are excluded.</param>
        /// <param name="mondayDelivery">Monday delivery is only possible when the package is delivered before 15.00 on Saturday at the designated PostNL locations. Click here for more information concerning Monday delivery and the locations. Note: To activate Monday delivery, value 6 must be given with dropoff_days, value 1 must be given by monday_delivery.And on Saturday the cutoff_time must be before 15:00 (14:30 recommended) so that Monday will be shown.</param>
        /// <param name="dropoffDelay">This options allows the Merchant to set the number of days it takes her to pick, pack and hand in her parcels at PostNL when ordered before the cutoff time. By default this is 0 and max is 14.</param>
        /// <param name="deliverydaysWindow">This options allows the Merchant to set the number of days into the future for which she wants to show her consumers delivery options. For example, if set to 3 in her check-out, a consumer ordering on Monday will see possible delivery options for Tuesday, Wednesday and Thursday (provided there is no drop-off delay, it's before the cut-off time and she goes to PostNL on Mondays). Min is 1. and max. is 14.</param>
        /// <param name="excludeDeliveryType">This options allows the Merchant to exclude delivery types from the available delivery options. You can specify multiple delivery types by semi-colon separating them. The standard delivery type cannot be excluded.</param>
        /// <returns>Upon success two arrays are returned one for DeliveryOptions and one for PickupOptions objects is returned. This object contains delivery date, time and pricing. Upon error an Error object is returned.</returns>
        public async Task<DataWrapper> GetDeliveryOptions(string countryCode, string postalCode, string number,
            Carrier carier, TimeSpan? deliveryTime = null, DateTime? deliveryDate = null, TimeSpan? cutoffTime = null,
            DayOfWeek[] dropoffDays = null, bool? mondayDelivery = null, int? dropoffDelay = null, int? deliverydaysWindow = null,
            DeliveryType[] excludeDeliveryType = null)
        {
            if (dropoffDelay.HasValue && (dropoffDelay.Value < 0 || dropoffDelay.Value > 14))
                throw new ArgumentOutOfRangeException("Parameter dropoffDays must be between 0 and 14");

            if (deliverydaysWindow.HasValue && (deliverydaysWindow.Value < 1 || deliverydaysWindow.Value > 14))
                throw new ArgumentOutOfRangeException("Parameter deliverydaysWindow must be between 1 and 14");

            var urlBuilder = new StringBuilder("delivery_options/");

            var parameters = new Dictionary<string, string>
            {
                {"cc", countryCode},
                {"postal_code", postalCode},
                {"number", number},
                {"carrier", carier.ToString().ToLower()}
            };
            if (deliveryTime.HasValue)
                parameters.Add("delivery_time", deliveryTime.Value.ToString("HH:mm:ss"));
            if (deliveryDate.HasValue)
                parameters.Add("delivery_time", deliveryDate.Value.ToString("yyyy-MM-dd"));
            if (cutoffTime.HasValue)
                parameters.Add("cutoff_time", cutoffTime.Value.ToString(@"hh\:mm\:ss"));
            if (dropoffDays != null && dropoffDays.Length > 0)
                parameters.Add("dropoff_days", string.Join(";", dropoffDays.Select(dd => (int)dd)));
            if (mondayDelivery.HasValue)
                parameters.Add("monday_delivery", Convert.ToInt32(mondayDelivery.Value).ToString());
            if (dropoffDelay.HasValue)
                parameters.Add("dropoff_delay", dropoffDelay.Value.ToString());
            if (deliverydaysWindow.HasValue)
                parameters.Add("deliverydays_window", deliverydaysWindow.Value.ToString());
            if (excludeDeliveryType != null && excludeDeliveryType.Length > 0)
                parameters.Add("exclude_delivery_type", string.Join(";", excludeDeliveryType.Select(edt => (int)edt)));
            urlBuilder.Append(GetQueryString(parameters));

            var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper>(jsonResult).Data;
            }
            var errors = JsonHelper.Deserialize<ApiWrapper>(jsonResult).Errors;
            var message = string.Join("\n", errors.Select(obj => obj.Message));
            throw new Exception(message);
        }

        /// <summary>
        /// Use this function to subscribe to an event in the API. 
        /// </summary>
        /// <param name="subscriptions"></param>
        /// <returns>Upon success an array with subscription ids is returned</returns>
        public async Task<ObjectId[]> AddSubscription(Subscription[] subscriptions)
        {
            var apiWrapper = new ApiWrapper
            {
                Data = new DataWrapper
                {
                    WebhookSubscriptions = subscriptions
                }
            };
            var content = new StringContent(JsonHelper.Serialize(apiWrapper));
            content.Headers.Clear();
            content.Headers.Add("Content-Type", "application/json;charset=utf-8");
            var response = await _httpClient.PostAsync("webhook_subscriptions", content).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper>(response.Content.ReadAsStringAsync().Result).Data.Ids;
            }
            return null;
        }


        /// <summary>
        /// Use this function to delete webhook subscriptions. You specify multiple subscription ids 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns>This method returns true if successful.</returns>
        public async Task<bool> DeleteSubscription(int[] ids)
        {
            var response = await _httpClient.DeleteAsync("webhook_subscriptions/" + string.Join(";", ids)).ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Use this function to fetch webhook subscriptions. You can also filter by account and shop id that you have access to.
        /// </summary>
        /// <param name="ids">This is the susbcription id. You can specify multiple subscription ids.</param>
        /// <returns>Upon success an array of Subscription objects is returned. </returns>
        public async Task<Subscription[]> GetSubscription(int[] ids)
        {
            var urlBuilder = new StringBuilder("webhook_subscriptions/");

            if (ids != null && ids.Length > 0)
                urlBuilder.Append($"{string.Join(";", ids)}");

            var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper>(jsonResult).Data.WebhookSubscriptions;
            }
            return null;
        }

        private string GetQueryString(Dictionary<string, string> parameters)
        {
            var queryString = string.Join("&",
                parameters.Select(
                    p =>
                        string.IsNullOrEmpty(p.Value)
                            ? Uri.EscapeDataString(p.Key)
                            : $"{Uri.EscapeDataString(p.Key)}={Uri.EscapeDataString(p.Value)}"));
            return !string.IsNullOrWhiteSpace(queryString) ? "?" + queryString : string.Empty;
        }
    }
}
