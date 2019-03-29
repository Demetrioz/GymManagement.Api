using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using RestSharp;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using GymManagement.Api.Config;
using GymManagement.Api.Core;

namespace GymManagement.Api.Services
{
    public class MailchimpService
    {
        private ApiSettings ApiSettings { get; set; }

        public MailchimpService(ApiSettings settings) { ApiSettings = settings; }

        public IActionResult Read(string endpoint, string filter)
        {
            var queryString = filter != null
                ? $"filter={filter}"
                : String.Empty;

            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                var client = new RestClient(ApiSettings.ApiUris["MailChimp"]);

                var request = new RestRequest($"{endpoint}{queryString}", Method.GET);
                request.AddHeader("Authorization", $"apikey {ApiSettings.ApiKeys["MailChimp"]}");

                IRestResponse response = client.Execute(request);

                if(response.StatusCode == HttpStatusCode.OK)
                {
                    var responseData = JsonConvert.DeserializeObject(response.Content);
                    return ApiResponse.Success(responseData);
                }
                else
                {
                    var message = $"{response.StatusCode}: There was a problem with the request.";
                    return ApiResponse.Fail(message);
                }
            }
            catch(Exception ex)
            {
                var message = $"{ex.Message} {ex.InnerException.Message}";
                return ApiResponse.Fail(message);
            }
        }

        public ObjectResult Create()
        {
            return new ObjectResult(true);
        }

        public ObjectResult Update()
        {
            return new ObjectResult(true);
        }
    }
}
