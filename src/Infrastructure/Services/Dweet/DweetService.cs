using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using VoteApp.Application.Interfaces.Services;
using VoteApp.Application.Models.PollStartNotification;
using VoteApp.Application.Models.PollStopNotification;

namespace VoteApp.Infrastructure.Services.Dweet
{

	/// <summary>
	/// Taken from https://github.com/TobiasRoeddiger/DweetSharp
	/// </summary>
	public class DweetService : IPollStartNotificationService, IPollStopNotificationService
	{
		private static DweetSharpHttpClient _dweetIOClient = new DweetSharpHttpClient();

		/// <summary>
		/// https://dweet.io/get/latest/dweet/for/FeedbackAppDat250
		/// </summary>
		private const string dweetThing = "FeedbackAppDat250";

		public void Notify(PollStartNotificationMessage message)
		{
			_ = DweetFor(dweetThing, JsonSerializer.Serialize(message));


		}

		public void Notify(PollStopNotificationMessage message)
		{
			_ = DweetFor(dweetThing, JsonSerializer.Serialize(message));
		}


		public static async Task<bool> DweetFor(string thing, string JSONcontent, string key = null)
		{
			if (string.IsNullOrEmpty(thing)) { throw new ArgumentException("thing can't be null or empty"); }
			if (string.IsNullOrEmpty(JSONcontent)) { throw new ArgumentException("JSONcontent can't be null or empty"); }
			if (key != null && key == string.Empty) { throw new ArgumentException("key can't be an empty string"); }

			string uri = string.Format("https://dweet.io/dweet/for/{0}", thing) + ((key != null) ? "?key=" + key : null);
			return await _dweetIOClient.POSTWithDidSucceedReturned(uri, JSONcontent);
		}

		public class DweetSharpHttpClient
		{
			private HttpClient _httpClient;

			public DweetSharpHttpClient()
			{
				this._httpClient = new HttpClient();
			}

			public async Task<bool> POSTWithDidSucceedReturned(string uri, string json)
			{
				var stringContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
				var response = await _httpClient.PostAsync(uri, stringContent);

				return await ValidateResponse(response);
			}

			private async Task<bool> ValidateResponse(HttpResponseMessage response)
			{
				var responseContent = await response.Content.ReadAsStringAsync();

				//weird behaviour of dweet.io where on failure a 200 status code is returned, therefore have to check if JSON matches failure JSON.
				return response.IsSuccessStatusCode && !responseContent.StartsWith("{\"this\":\"failed\"");
			}
		}
	}
}
