using System;
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
		// ==============
		//  Declarations
		// ==============

		/// <summary>
		/// Address used for API communication
		/// </summary>
		public static string ServerAddress = "http://tasual.org";


		// ============================
		//  Interface #1: VersionCheck
		// ============================

		/// <summary>
		/// Class for the VersionCheck interface which checks for updates from the server.
		/// </summary>
		public static class VersionCheck
		{
			/// <summary>
			/// Client-to-Server: Request sent to the server when starting up the client.
			/// </summary>
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
			/// Server-to-Client: Response from server containing compatibility/update information.
			/// </summary>
			public class ResponseObject
			{
				/// <summary>Integer: interface: Interface version (perhaps redundant?)</summary>
				[JsonProperty("interface")]
				public int Interface { get; set; }

				/// <summary>String: serverversion: Server version (from assembly info, major.minor.patch)</summary>
				[JsonProperty("serverversion")]
				public string ServerVersion { get; set; }

				/// <summary>String: latestversion: Latest release version (from assembly info of client type, major.minor.patch)</summary>
				[JsonProperty("latestversion")]
				public string LatestVersion { get; set; }

				/// <summary>Boolean: shouldupdate: Should client update (checks whether update is available for client)</summary>
				[JsonProperty("shouldupdate")]
				public bool ShouldUpdate { get; set; }

				/// <summary>String: updateurl: URL for update package</summary>
				[JsonProperty("updateurl")]
				public string UpdateURL { get; set; }

				public ResponseObject(
					int Interface,
					string ServerVersion,
					string LatestVersion,
					bool ShouldUpdate,
					string UpdateURL)
				{
					this.Interface = Interface;
					this.ServerVersion = ServerVersion;
					this.LatestVersion = LatestVersion;
					this.ShouldUpdate = ShouldUpdate;
					this.UpdateURL = UpdateURL;
				}
			}

			/// <summary>
			/// Generate the request to send to the server for VersionCheck.
			/// </summary>
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

				Client.ExecuteAsync(Request, Response => Handler(Response));
			}

			/// <summary>
			/// Handles response sent from server to the client regarding the VersionCheck request.
			/// </summary>
			/// <param name="Response">Response with IRestResponse formatting.</param>
			public static void Handler(IRestResponse Response)
			{
				// Debug
				Console.WriteLine(Response.Content);

				// Handle response by status 
				switch (Response.StatusCode)
				{
					case System.Net.HttpStatusCode.OK:
						{
							var Content = JsonConvert.DeserializeObject<ResponseObject>(Response.Content);

							// TODO: Store the latest version and list it in the about dialog
							// TODO: Do we really need the server version? 

							if (Content.ShouldUpdate)
							{
								// TODO: Mark internal flag in settings to initiate update
								// TODO: Store update package URL in settings
							}
							break;
						}
					case System.Net.HttpStatusCode.Forbidden:
						{
							break;
						}
				}
			}
		}


		// ============================
		//  Interface #2: Registration
		// ============================

		/// <summary>
		/// Class for the Registration interface which registers an account on the server.
		/// </summary>
		/// <remarks>
		/// We cannot always assume that #1 has already been sent to the server, so lets include identification information.
		/// TODO: Lay out design to use open authentication instead, this really isn't secure enough for my liking
		/// TODO: Should we really even use this or just rely upon web registration and provide an auth token?
		/// </remarks>
		public static class Registration
		{
			/// <summary>
			/// Client-to-Server: Sent from client when they submit the registration form to create an account.
			/// </summary>
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

			/// <summary>
			/// Generate the request to send to the server for Registration.
			/// </summary>
			/// <param name="Name">Real name of user to register.</param>
			/// <param name="Email">Email address of user to register.</param>
			/// <param name="Password">Password of user to register.</param>
			public static void Request(string Name, string Email, string Password)
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
					Name,
					Email,
					Password
				));

				Client.ExecuteAsync(Request, Response => Handler(Response));
			}

			/// <summary>
			/// Handles response sent from server to the client regarding the Registration request.
			/// </summary>
			/// <param name="Response">Response with IRestResponse formatting.</param>
			public static void Handler(IRestResponse Response)
			{
				// Debug
				Console.WriteLine(Response.Content);

				// Handle response by status 
				switch (Response.StatusCode)
				{
					case System.Net.HttpStatusCode.OK:
						{
							break;
						}
					case System.Net.HttpStatusCode.Forbidden:
						{
							break;
						}
				}
			}
		}


		// ======================
		//  Interface #3: SignIn
		// ======================

		/// <summary>
		/// Class for the SignIn interface which authenticates and starts a session with the server.
		/// </summary>
		public static class SignIn
		{
			/// <summary>
			/// Client-to-Server: Request sent to the server with the user information to start a new session.
			/// </summary>
			public class RequestObject
			{
				/// <summary>Integer: interface: Interface version</summary>
				[JsonProperty("interface")]
				public int Interface { get; set; }

				/// <summary>String: hash: Client unique hash</summary>
				[JsonProperty("hash")]
				public string Hash { get; set; }

				/// <summary>String: email: Email address of user to register</summary>
				[JsonProperty("email")]
				public string Email { get; set; }

				/// <summary>String: password: Password of user to register</summary>
				[JsonProperty("password")]
				public string Password { get; set; }

				/// <summary>DateTime: localmodified: Date and time of last modification to the local database</summary>
				[JsonProperty("localmodified")]
				public DateTime LocalModified { get; set; }

				public RequestObject(
					int Interface,
					string Hash,
					string Email,
					string Password,
					DateTime LocalModified)
				{
					this.Interface = Interface;
					this.Hash = Hash;
					this.Email = Email;
					this.Password = Password;
					this.LocalModified = LocalModified;
				}
			}

			/// <summary>
			/// Server-to-Client: Response from server containing session information.
			/// </summary>
			public class ResponseObject
			{
				/// <summary>String: session: Session hash ID generated by server to identify this login instance between the server and the client</summary>
				[JsonProperty("session")]
				public string Session { get; set; }

				/// <summary>String: name: Real name of user (this may have been changed by the user on the website)</summary>
				[JsonProperty("name")]
				public string Name { get; set; }

				/// <summary>Boolean: shouldsync: Whether or not the client should send its data to the server and the server should send its data to the client</summary>
				[JsonProperty("shouldsync")]
				public bool ShouldSync { get; set; }

				public ResponseObject(
					string Session,
					string Name,
					bool ShouldSync)
				{
					this.Session = Session;
					this.Name = Name;
					this.ShouldSync = ShouldSync;
				}
			}

			/// <summary>
			/// Generate the request to send to the server for SignIn.
			/// </summary>
			/// <param name="Email">Email address of user to signin.</param>
			/// <param name="Password">Password of user to signin.</param>
			public static void Request(string Email, string Password)
			{
				var Client = new RestClient(ServerAddress);
				var Request = new RestRequest("api/signin", Method.POST);

				Request.AddHeader("Content-type", "application/json");
				Request.RequestFormat = DataFormat.Json;

				Request.AddObject(new RequestObject(
					1,
					"asdf",
					Email,
					Password,
					DateTime.Now
				));

				Client.ExecuteAsync(Request, Response => Handler(Response));
			}

			/// <summary>
			/// Handles response sent from server to the client regarding the SignIn request.
			/// </summary>
			/// <param name="Response">Response with IRestResponse formatting.</param>
			public static void Handler(IRestResponse Response)
			{
				// Debug
				Console.WriteLine(Response.Content);

				// Handle response by status 
				switch (Response.StatusCode)
				{
					case System.Net.HttpStatusCode.OK:
						{
							break;
						}
					case System.Net.HttpStatusCode.Forbidden:
						{
							break;
						}
				}
			}
		}


		// =========================
		//  Interface #4: KeepAlive
		// =========================

		/// <summary>
		/// Class for the KeepAlive interface which keeps the session active and checks whether the database needs to sync.
		/// </summary>
		public static class KeepAlive
		{
			/// <summary>
			/// Client-to-Server: Request sent to the server when checking session info and whether or not to sync databases.
			/// </summary>
			public class RequestObject
			{
				/// <summary>String: session: Session hash ID retrieved earlier from the server</summary>
				[JsonProperty("session")]
				public string Session { get; set; }

				/// <summary>DateTime: localmodified: Date and time of last modification to the local database</summary>
				[JsonProperty("localmodified")]
				public DateTime LocalModified { get; set; }

				public RequestObject(
					string Session,
					DateTime LocalModified)
				{
					this.Session = Session;
					this.LocalModified = LocalModified;
				}
			}

			/// <summary>
			/// Generate the request to send to the server for KeepAlive.
			/// </summary>
			public static void Request()
			{
				var Client = new RestClient(ServerAddress);
				var Request = new RestRequest("api/keepalive", Method.POST);

				Request.AddHeader("Content-type", "application/json");
				Request.RequestFormat = DataFormat.Json;

				Request.AddObject(new RequestObject(
					"asdf",
					DateTime.Now
				));

				Client.ExecuteAsync(Request, Response => Handler(Response));
			}

			/// <summary>
			/// Handles response sent from server to the client regarding the KeepAlive request.
			/// </summary>
			/// <param name="Response">Response with IRestResponse formatting.</param>
			public static void Handler(IRestResponse Response)
			{
				// Debug
				Console.WriteLine(Response.Content);

				// Handle response by status 
				switch (Response.StatusCode)
				{
					case System.Net.HttpStatusCode.OK:
						{
							// "connection still good, no need to sync" response
							break;
						}
					case System.Net.HttpStatusCode.Continue:
						{
							// "connection still good, and also we should sync" response
							break;
						}
					case System.Net.HttpStatusCode.Forbidden:
						{
							// "connection isn't good anymore, reconnect"
							break;
						}

					default:
					case System.Net.HttpStatusCode.BadRequest:
						{
							// "missing content data, retry"
							// 
							break;
						}
				}
			}
		}


		// ====================
		//  Interface #5: Sync
		// ====================

		/// <summary>
		/// Class for the Sync interface which synchronizes data between the client and the server.
		/// </summary>
		/// <remarks>
		/// NOTE: Any additional settings (like rules on how the server should merge the database or such) should get sent from the client here and processed by the server
		/// NOTE: We can safely assume that the interface will be correct as there would not be an established session if the interfaces mismatched
		/// </remarks>
		public static class Sync
		{
			/// <summary>
			/// Client-to-Server: Request sent to the server when initiating a database synchronization.
			/// </summary>
			public class RequestObject
			{
				/// <summary>String: session: Session hash ID retrieved earlier from the server</summary>
				[JsonProperty("session")]
				public string Session { get; set; }

				/// <summary>String: timestamp: Date and time of when this sync was sent from the client</summary>
				[JsonProperty("timestamp")]
				public DateTime Timestamp { get; set; }

				/// <summary>TaskArray: localdatabase: Entire local database of tasks</summary>
				[JsonProperty("localdatabase")]
				public List<Task> LocalDatabase { get; set; }

				public RequestObject(
					string Session,
					DateTime Timestamp,
					List<Task> LocalDatabase)
				{
					this.Session = Session;
					this.Timestamp = Timestamp;
					this.LocalDatabase = LocalDatabase;
				}
			}

			/// <summary>
			/// Server-to-Client: Response from server containing the newly synchronized database.
			/// </summary>
			public class ResponseObject
			{
				/// <summary>DateTime: timestamp: Date and time of when this sync was completed and sent back from the server to the client</summary>
				[JsonProperty("timestamp")]
				public DateTime Timestamp { get; set; }

				/// <summary>TaskArray: remotedatabase: Updated and merged database from server</summary>
				[JsonProperty("remotedatabase")]
				public List<Task> RemoteDatabase { get; set; }

				public ResponseObject(
					DateTime Timestamp, 
					List<Task> RemoteDatabase)
				{
					this.Timestamp = Timestamp;
					this.RemoteDatabase = RemoteDatabase;
				}
			}

			/// <summary>
			/// Generate the request to send to the server for synchronization.
			/// </summary>
			public static void Request()
			{
				var Client = new RestClient(ServerAddress);
				var Request = new RestRequest("api/keepalive", Method.POST);

				Request.AddHeader("Content-type", "application/json");
				Request.RequestFormat = DataFormat.Json;

				Request.AddObject(new RequestObject(
					"asdf",
					DateTime.Now,
					new List<Task>()
				));

				Client.ExecuteAsync(Request, Response => Handler(Response));
			}

			/// <summary>
			/// Handles response sent from server to the client regarding the Sync request.
			/// </summary>
			/// <param name="Response">Response with IRestResponse formatting.</param>
			public static void Handler(IRestResponse Response)
			{
				// Debug
				Console.WriteLine(Response.Content);

				// Handle response by status 
				switch (Response.StatusCode)
				{
					case System.Net.HttpStatusCode.OK:
						{
							// "connection still good, no need to sync" response
							break;
						}
					case System.Net.HttpStatusCode.Continue:
						{
							// "connection still good, and also we should sync" response
							break;
						}
					case System.Net.HttpStatusCode.Forbidden:
						{
							// "connection isn't good anymore, reconnect"
							break;
						}

					default:
					case System.Net.HttpStatusCode.BadRequest:
						{
							// "missing content data, retry"
							break;
						}
				}
			}
		}
	}
}
