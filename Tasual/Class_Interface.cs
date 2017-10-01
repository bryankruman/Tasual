﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace Tasual
{
	class Interface
	{
		/// <summary>
		/// Address used for API communication
		/// </summary>
		public static string ServerAddress = "http://tasual.org";

		/// <summary>
		/// Interface #1: Version check and information
		/// </summary>
		public class VersionCheck
		{
			/// <summary>
			/// Client-to-Server: Client Information
			/// </summary>
			/// <remarks>
			/// Request sent to the server when starting up the client.
			/// </remarks>
			public class RequestObject
			{
				/// <summary>Integer: interface: Interface version</summary>
				[JsonProperty("interface")]
				public int Interface { get; set; }

				/// <summary>String: type: Client type (application, service, etc)</summary>
				[JsonProperty("type")]
				public string Type { get; set; }

				/// <summary>String: platform: Client platform (Windows version, Linux version, mobile OS, etc)</summary>
				[JsonProperty("platform")]
				public string Platform { get; set; }

				/// <summary>String: version: Client version (from assembly info, major.minor.patch)</summary>
				[JsonProperty("version")]
				public string Version { get; set; }

				/// <summary>String: hash: Client unique hash (created and stored in the settings.json file) to identify this client</summary>
				[JsonProperty("hash")]
				public string Hash { get; set; }

				/// <summary>Boolean: autoupdate: Auto-update enabled</summary>
				[JsonProperty("autoupdate")]
				public bool AutoUpdate { get; set; }

				public RequestObject(
					int Interface,
					string Type,
					string Platform,
					string Version,
					string Hash,
					bool AutoUpdate)
				{
					this.Interface = Interface;
					this.Type = Type;
					this.Platform = Platform;
					this.Version = Version;
					this.Hash = Hash;
					this.AutoUpdate = AutoUpdate;
				}
			}

			/// <summary>
			/// Server-to-Client: Server Information
			/// </summary>
			/// <remarks>
			/// Response from server containing compatibility/update information.
			/// </remarks>
			public class ResponseObject
			{
				/// <summary>Integer: interface: Interface version (perhaps redundant?)</summary>
				[JsonProperty("")]
				public int Interface { get; set; }

				/// <summary>String: serverversion: Server version (from assembly info, major.minor.patch)</summary>
				[JsonProperty("")]
				public string ServerVersion { get; set; }

				/// <summary>String: latestversion: Latest release version (from assembly info of client type, major.minor.patch)</summary>
				[JsonProperty("")]
				public string LatestVersion { get; set; }

				/// <summary>Boolean: shouldupdate: Should client update (checks whether update is available for client)</summary>
				[JsonProperty("")]
				public bool ShouldUpdate { get; set; }

				/// <summary>String: updateurl: URL for update package</summary>
				[JsonProperty("")]
				public string UpdateURL { get; set; }
			}

			public static void Handler(IRestResponse Response)
			{
				Console.WriteLine(Response.Content);
				var Content = JsonConvert.DeserializeObject<ResponseObject>(Response.Content);

				// TODO: Store the latest version and list it in the about dialog
				// TODO: Do we really need the server version? 

				if (Content.ShouldUpdate)
				{
					// TODO: Mark internal flag in settings to initiate update
					// TODO: Store update package URL in settings
				}
			}

			public static void Request()
			{
				var Client = new RestClient(ServerAddress);
				var Request = new RestRequest("api/versioncheck", Method.POST);

				Request.AddHeader("Content-type", "application/json");
				Request.RequestFormat = DataFormat.Json;

				Request.AddObject(new RequestObject(
					1,
					"Application",
					"Windows 10",
					"1.1",
					"asdf",
					true
				));

				//Request.AddJsonBody
				//Request.AddParameter("interface", "1");
				//Request.AddParameter("type", "application");
				//Request.AddParameter("platform", "Windows 10"); // TODO: Properly determine platform
				//Request.AddParameter("version", "1.1"); // TODO: Properly acquire version info
				//Request.AddParameter("hash", "asdf"); // TODO: Properly create a unique identifier (pull from arrayinfo)
				//Request.AddParameter("autoupdate", true);

				Client.ExecuteAsync(Request, Response => Handler(Response));
			}
		}

		/// <summary>
		/// Interface #2: Registration
		/// </summary>
		/// <remarks>
		/// Incoming (from client): Registration application
		/// Sent from client when they submit the registration form to create an account.
		/// We cannot always assume that #1 has already been sent to the server, so lets include identification information.
		/// TODO: Lay out design to use open authentication instead, this really isn't secure enough for my liking
		/// TODO: Should we really even use this or just rely upon web registration and provide an auth token?
		///    1. Integer: interface: Interface version
		///    2. String: type: Client type (application, service, etc)
		///    3. String: platform: Client platform
		///    4: String: version: Client version
		///    5: String: hash: Client unique hash
		///    6. String: name: Real name of user to register
		///    7: String: email: Email address of user to register
		///    8: String: password: Password of user to register
		/// 
		/// Outgoing (from server): Registration response
		/// Sent from server to inform the client of the registration status
		/// </remarks>
		public class Registration
		{
			public class RequestObject
			{
				/// <summary>Integer: interface: Interface version</summary>
				[JsonProperty("interface")]
				public int Interface { get; set; }

				/// <summary>String: type: Client type (application, service, etc)</summary>
				[JsonProperty("type")]
				public string Type { get; set; }

				/// <summary>String: platform: Client platform</summary>
				[JsonProperty("platform")]
				public string Platform { get; set; }

				/// <summary>String: version: Client version</summary>
				[JsonProperty("version")]
				public string Version { get; set; }

				/// <summary>String: hash: Client unique hash</summary>
				[JsonProperty("hash")]
				public string Hash { get; set; }

				/// <summary>String: name: Real name of user to register</summary>
				[JsonProperty("name")]
				public string Name { get; set; }

				/// <summary>String: email: Email address of user to register</summary>
				[JsonProperty("email")]
				public string Email { get; set; }

				/// <summary>String: password: Password of user to register</summary>
				[JsonProperty("password")]
				public string Password { get; set; }

				public RequestObject(
					int Interface,
					string Type,
					string Platform,
					string Version,
					string Hash,
					string Name,
					string Email,
					string Password)
				{
					this.Interface = Interface;
					this.Type = Type;
					this.Platform = Platform;
					this.Version = Version;
					this.Hash = Hash;
					this.Name = Name;
					this.Email = Email;
					this.Password = Password;
				}
			}

			public static void Handler(IRestResponse Response)
			{
				Console.WriteLine(Response.Content);

				// TODO: Read response type/message
			}

			public static void Request()
			{
				var Client = new RestClient(ServerAddress);
				var Request = new RestRequest("api/registration", Method.POST);

				Request.AddHeader("Content-type", "application/json");
				Request.RequestFormat = DataFormat.Json;

				Request.AddObject(new RequestObject(
					1,
					"Application",
					"Windows 10",
					"1.1",
					"asdf",
					"Foobar",
					"foo@bar.com",
					"password"
				));

				Client.ExecuteAsync(Request, Response => Handler(Response));
			}
		}

		/// <summary>
		/// Interface #3: Sign-in
		/// </summary>
		/// <remarks>
		/// Incoming (from client): Login information
		/// If the client has an account registered in its settings, send the following login information
		///    1. Integer: interface: Interface version
		///    2: String: hash: Client unique hash
		///    3: String: email: Email address of user
		///    4: String: password: Password of user
		///    5. String: localmodified: Date and time of last modification to the local database
		/// 
		/// Outgoing (from server): Login response
		///    1. Boolean: success: Successful login
		///    2. *String: error: If login is not successful, error reason is listed here
		///    3. String: session: Session hash ID generated by server to identify this login instance between the server and the client
		///    4. String: name: Real name of user (this may have been changed by the user on the website)
		///    5. Boolean: shouldsync: Whether or not the client should send its data to the server and the server should send its data to the client
		/// </remarks>
		public static void SignIn()
		{

		}

		/// <summary>
		/// Interface #4: Keepalive
		/// </summary>
		/// <remarks>
		/// Incoming (from client): Pulse
		/// NOTE: We can safely assume that the interface will be correct as there would not be an established session if the interfaces mismatched
		///    1. String: session: Session hash ID retrieved earlier from the server
		///    2. String: localmodified: Date and time of last modification to the local database
		/// 
		/// Outgoing(from server): Pulse response
		///    1. Boolean: active: Session is still active
		///    2. *String: error: If not active, error reason is listed here
		///    3. Boolean: shouldsync: Whether or not the client should send its data to the server and the server should send its data to the client
		/// </remarks>
		public static void KeepAlive()
		{

		}

		/// <summary>
		/// Interface #5: Sync
		/// </summary>
		/// <remarks>
		/// Incoming (from client): Local task array
		/// NOTE: Any additional settings (like rules on how the server should merge the database or such) should get sent from the client here and processed by the server
		/// NOTE: We can safely assume that the interface will be correct as there would not be an established session if the interfaces mismatched
		///    1. String: session: Session hash ID retrieved earlier from the server
		///    2. String: timestamp: Date and time of when this sync was sent from the client
		///    3. TaskArray: localdatabase: Entire local database of tasks
		/// 
		/// Outgoing (from server): Remote task array
		///    1. String: timestamp: Date and time of when this sync was completed and sent back from the server to the client
		///    2. Boolean: success: Whether or not the merge of databases was successful
		///    3. *String: error: If not successful, error reason is listed here
		///    4. TaskArray: remotedatabase: Updated and merged database from server
		/// </remarks>
		public static void Sync()
		{

		}
	}
}
