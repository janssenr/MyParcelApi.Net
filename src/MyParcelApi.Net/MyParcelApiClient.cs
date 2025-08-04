using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MyParcelApi.Net.Exceptions;
using MyParcelApi.Net.Helpers;
using MyParcelApi.Net.Models;
using MyParcelApi.Net.Wrappers;
using Newtonsoft.Json.Linq;

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
        /// <param name="cultureInfo">You can translate the endpoint by sending the correct header</param>
        public MyParcelApiClient(string apiKey, CultureInfo cultureInfo = null)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentException("Parameter apiKey needs a value");

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(ApiBaseUrl)
            };

            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Convert.ToBase64String(Encoding.UTF8.GetBytes(apiKey))}");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "CustomApiCall/2");
            if (cultureInfo != null)
            {
                _httpClient.DefaultRequestHeaders.Add("Accept-Language", cultureInfo.Name);
            }
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
            content.Headers.Add("Content-Type", "application/vnd.shipment+json;charset=utf-8;version=1.1");
            var response = await _httpClient.PostAsync("shipments", content).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper>(jsonResult).Data.Ids;
            }
            return await HandleResponseError<ObjectId[]>(response);
        }

        /// <summary>
        /// Update shipments allows you to update standard shipments.
        /// </summary>
        /// <param name="shipments"></param>
        /// <returns>Array of ShipmentIds is returned. The ids in the ShipmentIds array will be in the same order they where sent.</returns>
        public async Task<ObjectId[]> UpdateShipment(Shipment[] shipments)
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
            content.Headers.Add("Content-Type", "application/vnd.shipment+json;charset=utf-8;version=1.1");
            var response = await _httpClient.PutAsync("shipments", content).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper>(jsonResult).Data.Ids;
            }
            return await HandleResponseError<ObjectId[]>(response);
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
            return await HandleResponseError<ObjectId[]>(response);
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
            return await HandleResponseError<DownloadUrl>(response);
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
            //if (!string.IsNullOrWhiteSpace(q))
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
            return await HandleResponseError<Shipment[]>(response);
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
            //return await HandleResponseError<object[]>(response);
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
            //return await HandleResponseError<object[]>(response);
        }

        /// <summary>
        /// Get detailed track and trace information for a shipment.
        /// </summary>
        /// <param name="ids">This is the shipment id. You can specify multiple shipment ids.</param>
        /// <param name="page">Page number. Maximum value is 1000 and minimum is 1. Defaults to 1.</param>
        /// <param name="size">Items per page. Maximum value is 200 and minimum is 30. Defaults to 30.</param>
        /// <param name="sort">TrackTrace object field to sort on.</param>
        /// <param name="order">Sort order for sort filter. Defaults to ASC.</param>
        /// <param name="extraInfo">Only the delivery_moment option is available. Delivery moment is not included by default for performance reasons.</param>
        /// <returns>Upon success an array of TrackTrace objects is returned</returns>
        public async Task<TrackTrace[]> TrackShipment(int[] ids, int page = 1, int size = 30, string sort = "", string order = "ASC", string extraInfo = null)
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
            if (!string.IsNullOrWhiteSpace(extraInfo))
                parameters.Add("extra_info", extraInfo);
            urlBuilder.Append(GetQueryString(parameters));

            var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper>(jsonResult).Data.TrackTraces;
            }
            return await HandleResponseError<TrackTrace[]>(response);
        }

        /// <summary>
        /// Get detailed track and trace information.
        /// </summary>
        /// <param name="barcode">The barcode for which to fetch the track and trace information.</param>
        /// <param name="postalCode">The postal code for which to fetch the track and trace information.</param>
        /// <param name="country">The country for which to fetch the track and trace information.</param>
        /// <returns>Upon success an array of TrackTrace objects is returned</returns>
        public async Task<TrackTrace[]> TrackShipment(string barcode, string postalCode, string country = null)
        {
            var urlBuilder = new StringBuilder("tracktraces/");

            var parameters = new Dictionary<string, string>
            {
                { "barcode", barcode.ToString() },
                { "postal_code", postalCode.ToString() }
            };
            if (!string.IsNullOrWhiteSpace(country))
                parameters.Add("cc", country);
            urlBuilder.Append(GetQueryString(parameters));

            _httpClient.DefaultRequestHeaders.Remove("Authorization");

            var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper>(jsonResult).Data.TrackTraces;
            }
            return await HandleResponseError<TrackTrace[]>(response);
        }

        /// <summary>
        /// Get the delivery options for a given location and carrier. If none of the optional parameters are specified then the following default will be used: If a request is made for the delivery options between Friday after the default cutoff_time (15h30) and Monday before the default cutoff_time (15h30) then Tuesday will be shown as the next possible delivery date.
        /// </summary>
        /// <param name="countryCode">The country code for which to fetch the delivery options</param>
        /// <param name="postalCode">The postal code for which to fetch the delivery options.</param>
        /// <param name="number">The street number for which to fetch the delivery options.</param>
        /// <param name="platform">The platform where you want the data from</param>
        /// <param name="carier">The carrier for which to fetch the delivery options.</param>
        /// <param name="deliveryTime">The time on which a package has to be delivered. Note: This is only an indication of time the package will be delivered on the selected date.</param>
        /// <param name="deliveryDate">The date on which the package has to be delivered.</param>
        /// <param name="cutoffTime">This option allows the Merchant to indicate the latest cut-off time before which a consumer order will still be picked, packed and dispatched on the same/first set dropoff day, taking into account the dropoff-delay. Default time is 15h30. For example, if cutoff time is 15h30, Monday is a delivery day and there's no delivery delay; all orders placed Monday before 15h30 will be dropped of at PostNL on that same Monday in time for the Monday collection.</param>
        /// <param name="dropoffDays">This options allows the Merchant to set the days she normally goes to PostNL to hand in her parcels. By default Saturday and Sunday are excluded.</param>
        /// <param name="mondayDelivery">Monday delivery is only possible when the package is delivered before 15.00 on Saturday at the designated PostNL locations. Click here for more information concerning Monday delivery and the locations. Note: To activate Monday delivery, value 6 must be given with dropoff_days, value 1 must be given by monday_delivery.And on Saturday the cutoff_time must be before 15:00 (14:30 recommended) so that Monday will be shown.</param>
        /// <param name="dropoffDelay">This options allows the Merchant to set the number of days it takes her to pick, pack and hand in her parcels at PostNL when ordered before the cutoff time. By default this is 0 and max is 14.</param>
        /// <param name="deliverydaysWindow">This options allows the Merchant to set the number of days into the future for which she wants to show her consumers delivery options. For example, if set to 3 in her check-out, a consumer ordering on Monday will see possible delivery options for Tuesday, Wednesday and Thursday (provided there is no drop-off delay, it's before the cut-off time and she goes to PostNL on Mondays). Min is 1. and max. is 14.</param>
        /// <param name="excludeDeliveryType">This options allows the Merchant to exclude delivery types from the available delivery options. You can specify multiple delivery types by semi-colon separating them. The standard delivery type cannot be excluded.</param>
        /// <param name="latitude">This provides the ability to display the postNL locations through the coordinates. If only latitude or longitude is passed as a parameter, it will be ignored and will simply use zip code for searching locations.</param>
        /// <param name="longitude">This provides the ability to display the postNL locations through the coordinates. If only latitude or longitude is passed as a parameter, it will be ignored and will simply use zip code for searching locations.</param>
        /// <returns>Upon success two arrays are returned one for DeliveryOptions and one for PickupOptions objects is returned. This object contains delivery date, time and pricing. Upon error an Error object is returned.</returns>
        public async Task<DataWrapper> GetDeliveryOptions(string countryCode, string postalCode, string number,
                Platform? platform = null, Carrier? carier = null, TimeSpan? deliveryTime = null, DateTime? deliveryDate = null, TimeSpan? cutoffTime = null,
                DayOfWeek[] dropoffDays = null, bool? mondayDelivery = null, int? dropoffDelay = null, int? deliverydaysWindow = null,
                DeliveryType[] excludeDeliveryType = null, double? latitude = null, double? longitude = null)
        {
            if (dropoffDelay.HasValue && (dropoffDelay.Value < 0 || dropoffDelay.Value > 14))
                throw new ArgumentOutOfRangeException("Parameter dropoffDays must be between 0 and 14");

            if (deliverydaysWindow.HasValue && (deliverydaysWindow.Value < 1 || deliverydaysWindow.Value > 14))
                throw new ArgumentOutOfRangeException("Parameter deliverydaysWindow must be between 1 and 14");

            var urlBuilder = new StringBuilder("delivery_options");

            var parameters = new Dictionary<string, string>
            {
                {"cc", countryCode},
                {"postal_code", postalCode},
                {"number", number}
            };
            if (platform.HasValue)
                parameters.Add("platform", platform.ToString().ToLower());
            if (carier.HasValue)
                parameters.Add("carrier", carier.ToString().ToLower());
            if (deliveryTime.HasValue)
                parameters.Add("delivery_time", deliveryTime.Value.ToString("HH:mm:ss"));
            if (deliveryDate.HasValue)
                parameters.Add("delivery_date", deliveryDate.Value.ToString("yyyy-MM-dd"));
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
            if (latitude.HasValue)
                parameters.Add("latitude", latitude.Value.ToString(CultureInfo.InvariantCulture));
            if (longitude.HasValue)
                parameters.Add("longitude", longitude.Value.ToString(CultureInfo.InvariantCulture));
            urlBuilder.Append(GetQueryString(parameters));

            var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper>(jsonResult).Data;
            }
            return await HandleResponseError<DataWrapper>(response);
        }

        /// <summary>
        /// Get the delivery options for a given location and carrier. If none of the optional parameters are specified then the following default will be used: If a request is made for the delivery options between Friday after the default cutoff_time (15h30) and Monday before the default cutoff_time (15h30) then Tuesday will be shown as the next possible delivery date.
        /// </summary>
        /// <param name="countryCode">The country code for which to fetch the delivery options</param>
        /// <param name="postalCode">The postal code for which to fetch the delivery options.</param>
        /// <param name="number">The street number for which to fetch the delivery options.</param>
        /// <param name="platform">The platform where you want the data from</param>
        /// <param name="carier">The carrier for which to fetch the delivery options.</param>
        /// <param name="deliveryTime">The time on which a package has to be delivered. Note: This is only an indication of time the package will be delivered on the selected date.</param>
        /// <param name="deliveryDate">The date on which the package has to be delivered.</param>
        /// <param name="cutoffTime">This option allows the Merchant to indicate the latest cut-off time before which a consumer order will still be picked, packed and dispatched on the same/first set dropoff day, taking into account the dropoff-delay. Default time is 15h30. For example, if cutoff time is 15h30, Monday is a delivery day and there's no delivery delay; all orders placed Monday before 15h30 will be dropped of at PostNL on that same Monday in time for the Monday collection.</param>
        /// <param name="dropoffDays">This options allows the Merchant to set the days she normally goes to PostNL to hand in her parcels. By default Saturday and Sunday are excluded.</param>
        /// <param name="mondayDelivery">Monday delivery is only possible when the package is delivered before 15.00 on Saturday at the designated PostNL locations. Click here for more information concerning Monday delivery and the locations. Note: To activate Monday delivery, value 6 must be given with dropoff_days, value 1 must be given by monday_delivery.And on Saturday the cutoff_time must be before 15:00 (14:30 recommended) so that Monday will be shown.</param>
        /// <param name="dropoffDelay">This options allows the Merchant to set the number of days it takes her to pick, pack and hand in her parcels at PostNL when ordered before the cutoff time. By default this is 0 and max is 14.</param>
        /// <param name="deliverydaysWindow">This options allows the Merchant to set the number of days into the future for which she wants to show her consumers delivery options. For example, if set to 3 in her check-out, a consumer ordering on Monday will see possible delivery options for Tuesday, Wednesday and Thursday (provided there is no drop-off delay, it's before the cut-off time and she goes to PostNL on Mondays). Min is 1. and max. is 14.</param>
        /// <param name="excludeDeliveryType">This options allows the Merchant to exclude delivery types from the available delivery options. You can specify multiple delivery types by semi-colon separating them. The standard delivery type cannot be excluded.</param>
        /// <param name="excludeParcelLockers">This option allows to filter out pickup locations that are parcel lockers.</param>
        /// <param name="latitude">This provides the ability to display the postNL locations through the coordinates. If only latitude or longitude is passed as a parameter, it will be ignored and will simply use zip code for searching locations.</param>
        /// <param name="longitude">This provides the ability to display the postNL locations through the coordinates. If only latitude or longitude is passed as a parameter, it will be ignored and will simply use zip code for searching locations.</param>
        /// <returns>Upon success two arrays are returned one for DeliveryOptions and one for PickupOptions objects is returned. This object contains delivery date, time and pricing. Upon error an Error object is returned.</returns>
        public async Task<Delivery[]> GetDeliveryOptionsV2(string countryCode, string postalCode, string number,
                Platform? platform = null, Carrier? carier = null, TimeSpan? deliveryTime = null, DateTime? deliveryDate = null, TimeSpan? cutoffTime = null,
                DayOfWeek[] dropoffDays = null, bool? mondayDelivery = null, int? dropoffDelay = null, int? deliverydaysWindow = null,
                DeliveryType[] excludeDeliveryType = null, bool? excludeParcelLockers = null, double? latitude = null, double? longitude = null)
        {
            if (dropoffDelay.HasValue && (dropoffDelay.Value < 0 || dropoffDelay.Value > 14))
                throw new ArgumentOutOfRangeException("Parameter dropoffDays must be between 0 and 14");

            if (deliverydaysWindow.HasValue && (deliverydaysWindow.Value < 1 || deliverydaysWindow.Value > 14))
                throw new ArgumentOutOfRangeException("Parameter deliverydaysWindow must be between 1 and 14");

            var urlBuilder = new StringBuilder("delivery_options");

            var parameters = new Dictionary<string, string>
            {
                {"cc", countryCode},
                {"postal_code", postalCode},
                {"number", number}
            };
            if (platform.HasValue)
                parameters.Add("platform", platform.ToString().ToLower());
            if (carier.HasValue)
                parameters.Add("carrier", carier.ToString().ToLower());
            if (deliveryTime.HasValue)
                parameters.Add("delivery_time", deliveryTime.Value.ToString("HH:mm:ss"));
            if (deliveryDate.HasValue)
                parameters.Add("delivery_date", deliveryDate.Value.ToString("yyyy-MM-dd"));
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
            if (excludeParcelLockers.HasValue)
                parameters.Add("exclude_parcel_lockers", Convert.ToInt32(excludeParcelLockers.Value).ToString());
            if (latitude.HasValue)
                parameters.Add("latitude", latitude.Value.ToString(CultureInfo.InvariantCulture));
            if (longitude.HasValue)
                parameters.Add("longitude", longitude.Value.ToString(CultureInfo.InvariantCulture));
            urlBuilder.Append(GetQueryString(parameters));

            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json;charset=utf-8;version=2.0");
            var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper>(jsonResult).Data.Deliveries;
            }
            return await HandleResponseError<Delivery[]>(response);
        }

        /// <summary>
        /// Get the delivery options for a given location and carrier. If none of the optional parameters are specified then the following default will be used: If a request is made for the delivery options between Friday after the default cutoff_time (15h30) and Monday before the default cutoff_time (15h30) then Tuesday will be shown as the next possible delivery date.
        /// </summary>
        /// <param name="countryCode">The country code for which to fetch the delivery options</param>
        /// <param name="postalCode">The postal code for which to fetch the delivery options.</param>
        /// <param name="number">The street number for which to fetch the delivery options.</param>
        /// <param name="platform">The platform where you want the data from</param>
        /// <param name="carier">The carrier for which to fetch the delivery options.</param>
        /// <param name="deliveryTime">The time on which a package has to be delivered. Note: This is only an indication of time the package will be delivered on the selected date.</param>
        /// <param name="deliveryDate">The date on which the package has to be delivered.</param>
        /// <param name="cutoffTime">This option allows the Merchant to indicate the latest cut-off time before which a consumer order will still be picked, packed and dispatched on the same/first set dropoff day, taking into account the dropoff-delay. Default time is 15h30. For example, if cutoff time is 15h30, Monday is a delivery day and there's no delivery delay; all orders placed Monday before 15h30 will be dropped of at PostNL on that same Monday in time for the Monday collection.</param>
        /// <param name="dropoffDays">This options allows the Merchant to set the days she normally goes to PostNL to hand in her parcels. By default Saturday and Sunday are excluded.</param>
        /// <param name="mondayDelivery">Monday delivery is only possible when the package is delivered before 15.00 on Saturday at the designated PostNL locations. Click here for more information concerning Monday delivery and the locations. Note: To activate Monday delivery, value 6 must be given with dropoff_days, value 1 must be given by monday_delivery.And on Saturday the cutoff_time must be before 15:00 (14:30 recommended) so that Monday will be shown.</param>
        /// <param name="dropoffDelay">This options allows the Merchant to set the number of days it takes her to pick, pack and hand in her parcels at PostNL when ordered before the cutoff time. By default this is 0 and max is 14.</param>
        /// <param name="deliverydaysWindow">This options allows the Merchant to set the number of days into the future for which she wants to show her consumers delivery options. For example, if set to 3 in her check-out, a consumer ordering on Monday will see possible delivery options for Tuesday, Wednesday and Thursday (provided there is no drop-off delay, it's before the cut-off time and she goes to PostNL on Mondays). Min is 1. and max. is 14.</param>
        /// <param name="excludeDeliveryType">This options allows the Merchant to exclude delivery types from the available delivery options. You can specify multiple delivery types by semi-colon separating them. The standard delivery type cannot be excluded.</param>
        /// <param name="latitude">This provides the ability to display the postNL locations through the coordinates. If only latitude or longitude is passed as a parameter, it will be ignored and will simply use zip code for searching locations.</param>
        /// <param name="longitude">This provides the ability to display the postNL locations through the coordinates. If only latitude or longitude is passed as a parameter, it will be ignored and will simply use zip code for searching locations.</param>
        /// <returns>Upon success two arrays are returned one for DeliveryOptions and one for PickupOptions objects is returned. This object contains delivery date, time and pricing. Upon error an Error object is returned.</returns>
        public async Task<PickupLocation[]> GetPickupLocationsV2(string countryCode, string postalCode, string number,
                Platform? platform = null, Carrier? carier = null, TimeSpan? deliveryTime = null, DateTime? deliveryDate = null, TimeSpan? cutoffTime = null,
                DayOfWeek[] dropoffDays = null, bool? mondayDelivery = null, int? dropoffDelay = null, int? deliverydaysWindow = null,
                DeliveryType[] excludeDeliveryType = null, double? latitude = null, double? longitude = null)
        {
            if (dropoffDelay.HasValue && (dropoffDelay.Value < 0 || dropoffDelay.Value > 14))
                throw new ArgumentOutOfRangeException("Parameter dropoffDays must be between 0 and 14");

            if (deliverydaysWindow.HasValue && (deliverydaysWindow.Value < 1 || deliverydaysWindow.Value > 14))
                throw new ArgumentOutOfRangeException("Parameter deliverydaysWindow must be between 1 and 14");

            var urlBuilder = new StringBuilder("pickup_locations");

            var parameters = new Dictionary<string, string>
            {
                {"cc", countryCode},
                {"postal_code", postalCode},
                {"number", number}
            };
            if (platform.HasValue)
                parameters.Add("platform", platform.ToString().ToLower());
            if (carier.HasValue)
                parameters.Add("carrier", carier.ToString().ToLower());
            if (deliveryTime.HasValue)
                parameters.Add("delivery_time", deliveryTime.Value.ToString("HH:mm:ss"));
            if (deliveryDate.HasValue)
                parameters.Add("delivery_date", deliveryDate.Value.ToString("yyyy-MM-dd"));
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
            if (latitude.HasValue)
                parameters.Add("latitude", latitude.Value.ToString(CultureInfo.InvariantCulture));
            if (longitude.HasValue)
                parameters.Add("longitude", longitude.Value.ToString(CultureInfo.InvariantCulture));
            urlBuilder.Append(GetQueryString(parameters));

            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json;charset=utf-8;version=2.0");
            var response = await _httpClient.GetAsync(urlBuilder.ToString()).ConfigureAwait(false);
            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return JsonHelper.Deserialize<ApiWrapper>(jsonResult).Data.PickupLocations;
            }
            return await HandleResponseError<PickupLocation[]>(response);
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
            return await HandleResponseError<ObjectId[]>(response);
        }

        /// <summary>
        /// Use this function to fetch your active webhook subscriptions.
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
            return await HandleResponseError<Subscription[]>(response);
        }

        /// <summary>
        /// Use this function to delete webhook subscriptions. You specify multiple subscription ids 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns>This method returns true if successful.</returns>
        public async Task<bool> DeleteSubscription(int[] ids)
        {
            var urlBuilder = new StringBuilder("webhook_subscriptions/");

            if (ids != null && ids.Length > 0)
                urlBuilder.Append($"{string.Join(";", ids)}");
            var response = await _httpClient.DeleteAsync(urlBuilder.ToString()).ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }

        private async Task<T> HandleResponseError<T>(HttpResponseMessage response)
        {
            string message = string.Empty;
            switch (response.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    message = "Page not found";
                    break;
                default:
                    var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    //var result = JsonHelper.Deserialize<ApiWrapper>(responseBody);
                    //message = string.Join("\n", result.Errors.Select(obj => string.Join("\n", obj.Human)));
                    //if (string.IsNullOrEmpty(message))
                    //{
                    //    message = result.Message;
                    //}
                    JObject result = JObject.Parse(responseBody);
                    if (result.ContainsKey("message"))
                    {
                        message = result["message"].ToString();
                    }

                    if (result.ContainsKey("errors"))
                    {
                        JArray errors = (JArray)result["errors"];
                        foreach (JObject error in errors)
                        {
                            if (error.ContainsKey("fields") && error.ContainsKey("human"))
                            {
                                message += GetMessages(error);
                            }
                            else
                            {
                                foreach (JProperty childError in error.Children())
                                {
                                    switch (childError.Type)
                                    {
                                        case JTokenType.Array:
                                        case JTokenType.Object:
										case JTokenType.Property:
											foreach (JObject child in childError.Children())
                                            {
                                                if (child.ContainsKey("fields") && child.ContainsKey("human"))
                                                {
                                                    message += GetMessages(child);
                                                }
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                    }
                    break;
            }
            throw new MyParcelException(message);
        }

        private string GetMessages(JObject error)
        {
            string messages = string.Empty;
            JArray fields = (JArray)error["fields"];
            JArray human = (JArray)error["human"];
            for (int i = 0; i < human.Count; i++)
            {
                messages += "\n" + human[i];
            }
            return messages;
        }

        private string GetQueryString(Dictionary<string, string> parameters)
        {
            var queryString = string.Join("&",
                parameters.Select(p =>
                    $"{Uri.EscapeDataString(p.Key)}={(!string.IsNullOrEmpty(p.Value) ? Uri.EscapeDataString(p.Value) : "")}"));
            return !string.IsNullOrWhiteSpace(queryString) ? "?" + queryString : string.Empty;
        }
    }
}
