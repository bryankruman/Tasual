﻿// ===========================================
//  Copyright (c) 2017 Bryan Kruman
//
//  See LICENSE.rtf file in the project root
//  for full license information.
// ===========================================

using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Tasual
{
	public class Interface
	{
		// ==============
		//  Declarations
		// ==============

		/// <summary>
		/// Address used for API communication
		/// </summary>
		public static string ServerAddress = "http://localhost:58315/";


		// ================================
		//  Interface #1: v1: VersionCheck
		// ================================

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
				/// <summary>type: Client type (application, service, etc)</summary>
				[JsonProperty("type")]
				public string Type { get; set; }

				/// <summary>platform: Client platform (Windows version, Linux version, mobile OS, etc)</summary>
				[JsonProperty("platform")]
				public string Platform { get; set; }

				/// <summary>version: Client version (from assembly info, major.minor.build)</summary>
				[JsonProperty("version")]
				public string Version { get; set; }

				/// <summary>hash: Client unique hash (created and stored in the settings.json file) to identify this client</summary>
				[JsonProperty("hash")]
				public string Hash { get; set; }

				public RequestObject(
					string Type,
					string Platform,
					string Version,
					string Hash)
				{
					this.Type = Type;
					this.Platform = Platform;
					this.Version = Version;
					this.Hash = Hash;
				}
			}

			/// <summary>
			/// Server-to-Client: Response from server containing compatibility/update information.
			/// </summary>
			public class ResponseObject
			{
				/// <summary>serverversion: Server version (from assembly info, major.minor.build)</summary>
				[JsonProperty("serverversion")]
				public string ServerVersion { get; set; }

				/// <summary>latestversion: Latest release version (from assembly info of client type, major.minor.build)</summary>
				[JsonProperty("latestversion")]
				public string LatestVersion { get; set; }

				/// <summary>updateurl: URL for update package</summary>
				[JsonProperty("updateurl")]
				public string UpdateURL { get; set; }

				public ResponseObject(
					string ServerVersion,
					string LatestVersion,
					string UpdateURL)
				{
					this.ServerVersion = ServerVersion;
					this.LatestVersion = LatestVersion;
					this.UpdateURL = UpdateURL;
				}
			}

			/// <summary>
			/// Result given to callback delegates by the handler function.
			/// </summary>
			public enum RequestResult
			{
				/// <summary>An update was found.</summary>
				UpdateFound,
				/// <summary>Already up to date.</summary>
				UpToDate,
				/// <summary>For some reason or another, the request/response was not completed.</summary>
				NotCompleted,
				/// <summary>For some reason or another, the request was marked as bad.</summary>
				BadRequest
			}

			/// <summary>
			/// Generate the request to send to the server for VersionCheck.
			/// </summary>
			public static void Request(Action<RequestResult> Callback)
			{
				var Client = new RestClient(ServerAddress);
				//Client.Authenticator = new HttpBasicAuthenticator("foo", "bar"); // VersionCheck doesn't need authentication

				var Request = new RestRequest("api/v1/versioncheck", Method.POST);
				Request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
				Request.RequestFormat = DataFormat.Json;

				Request.AddJsonBody(new RequestObject(
					AssemblyInfo.Product,
					Environment.OSVersion.VersionString,
					AssemblyInfo.Version,
					Settings.Config.Hash
				));

				Console.WriteLine(String.Format(
					"API: VersionCheck (v1): Request: Sending to {0}", 
					ServerAddress
				));

				Client.ExecuteAsync(Request, Response => Handler(Callback, Response));
			}

			/// <summary>
			/// Handles response sent from server to the client regarding the VersionCheck request.
			/// </summary>
			/// <param name="Callback">Callback delegate to call when a result is determined.</param>
			/// <param name="Response">Response with IRestResponse formatting.</param>
			public static void Handler(Action<RequestResult> Callback, IRestResponse Response)
			{
				if (Response.ResponseStatus != ResponseStatus.Completed)
				{
					Console.WriteLine(String.Format(
						"API: VersionCheck (v1): Handler: Response status '{0}'", 
						Response.ResponseStatus)
					);

					Callback(RequestResult.NotCompleted);
					return;
				}

				switch (Response.StatusCode)
				{
					case System.Net.HttpStatusCode.OK:
						{
							var Content = JsonConvert.DeserializeObject<ResponseObject>(Response.Content);

							Settings.Config.ServerVersion = Content.ServerVersion;
							Settings.Config.LatestVersion = Content.LatestVersion;
							Settings.Config.UpdateURL = Content.UpdateURL;
							Settings.Save();

							var LatestVersion = new Version(Content.LatestVersion);
							var CurrentVersion = new Version(AssemblyInfo.Version);

							int Result = LatestVersion.CompareTo(CurrentVersion);

							if (Result > 0)
							{
								Console.WriteLine(String.Format(
									"API: VersionCheck (v1): Handler: Latest ({0}) is newer than current ({1})!",
									LatestVersion.ToString(3),
									CurrentVersion.ToString(3)
								));

								Callback(RequestResult.UpdateFound);
							}
							else if (Result < 0)
							{
								Console.WriteLine(String.Format(
									"API: VersionCheck (v1): Handler: Current ({0}) is newer than latest ({1})!",
									CurrentVersion.ToString(3),
									LatestVersion.ToString(3)
								));

								Callback(RequestResult.UpToDate);
							}
							else
							{
								Console.WriteLine(String.Format(
									"API: VersionCheck (v1): Handler: Current version is up to date! ({0}))",
									CurrentVersion.ToString(3)
								));

								Callback(RequestResult.UpToDate);
							}
							return;
						}

					default:
					case System.Net.HttpStatusCode.BadRequest:
						{
							Console.WriteLine(String.Format(
								"API: VersionCheck (v1): Handler: BadRequest '{0}'",
								Response.StatusDescription)
							);

							Callback(RequestResult.BadRequest);
							return;
						}
				}
			}
		}


		// ================================
		//  Interface #2: v1: Registration
		// ================================

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
				/// <summary>type: Client type (application, service, etc)</summary>
				[JsonProperty("type")]
				public string Type { get; set; }

				/// <summary>platform: Client platform</summary>
				[JsonProperty("platform")]
				public string Platform { get; set; }

				/// <summary>version: Client version</summary>
				[JsonProperty("version")]
				public string Version { get; set; }

				/// <summary>hash: Client unique hash</summary>
				[JsonProperty("hash")]
				public string Hash { get; set; }

				/// <summary>name: Real name of user to register</summary>
				[JsonProperty("name")]
				public string Name { get; set; }

				/// <summary>email: Email address of user to register</summary>
				[JsonProperty("email")]
				public string Email { get; set; }

				/// <summary>password: Password of user to register</summary>
				[JsonProperty("password")]
				public string Password { get; set; }

				public RequestObject(
					string Type,
					string Platform,
					string Version,
					string Hash,
					string Name,
					string Email,
					string Password)
				{
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
				var Request = new RestRequest("api/v1/registration", Method.POST);

				Request.AddHeader("Content-Type", "application/json");
				Request.RequestFormat = DataFormat.Json;

				Request.AddObject(new RequestObject(
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
							// Server is telling us that our registration went fine.
							// Next step: Send SignIn request
							break;
						}
					case System.Net.HttpStatusCode.Forbidden:
						{
							// Server is telling us that registration was unsuccessful.
							// Next step: Wait for user to confirm error and fix possible mistake
							// TODO: Show error message with reason for failure, like one of these:
							//  - Service currently disabled
							//  - Invalid email or password requirements (NOTE: Client should filter this ahead of time... shouldn't really happen)
							//  - Email already registered
							//  - Banned from service
							break;
						}

					default:
					case System.Net.HttpStatusCode.BadRequest:
						{
							// Somehow there was an error in how the request was formed, this shouldn't happen normally.
							// Next step: Retry (up to 5 times, after that throw messagebox with network error warning)
							// TODO: Throw an error message here.
							break;
						}
				}
			}
		}


		// ==========================
		//  Interface #3: v1: SignIn
		// ==========================

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
				/// <summary>hash: Client unique hash</summary>
				[JsonProperty("hash")]
				public string Hash { get; set; }

				/// <summary>email: Email address of user to register</summary>
				[JsonProperty("email")]
				public string Email { get; set; }

				/// <summary>password: Password of user to register</summary>
				[JsonProperty("password")]
				public string Password { get; set; }

				/// <summary>localmodified: Date and time of last modification to the local database</summary>
				[JsonProperty("localmodified")]
				public DateTime LocalModified { get; set; }

				public RequestObject(
					string Hash,
					string Email,
					string Password,
					DateTime LocalModified)
				{
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
				/// <summary>session: Session hash ID generated by server to identify this login instance between the server and the client</summary>
				[JsonProperty("session")]
				public string Session { get; set; }

				/// <summary>name: Real name of user (this may have been changed by the user on the website)</summary>
				[JsonProperty("name")]
				public string Name { get; set; }

				public ResponseObject(
					string Session,
					string Name)
				{
					this.Session = Session;
					this.Name = Name;
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
				var Request = new RestRequest("api/v1/signin", Method.POST);

				Request.AddHeader("Content-Type", "application/json");
				Request.RequestFormat = DataFormat.Json;

				Request.AddObject(new RequestObject(
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
							// Server is telling us the sign in went fine, no need to sync data.
							// Next step: Set internal flag saying our session is active
							break;
						}
					case System.Net.HttpStatusCode.Continue:
						{
							// Server is telling us the sign in went fine, and we should sync our data.
							// Next step: Send Sync request
							break;
						}
					case System.Net.HttpStatusCode.Forbidden:
						{
							// Server is telling us that our sign in has failed.
							// Next step: Wait for user to confirm error and fix possible mistake
							// NOTE: Check "could not sync" flag to see if the SignIn request was sent because of a failure to sync, display error accordingly
							// TODO: Show error message with reason for failure, like one of these:
							//  - Service currently disabled
							//  - Email not registered
							//  - Invalid password
							//  - Banned from service
							break;
						}

					default:
					case System.Net.HttpStatusCode.BadRequest:
						{
							// Somehow there was an error in how the request was formed, this shouldn't happen normally.
							// Next step: Retry (up to 5 times, after that throw messagebox with network error warning)
							// TODO: Throw an error message here.
							break;
						}
				}
			}
		}


		// =============================
		//  Interface #4: v1: KeepAlive
		// =============================

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
				/// <summary>session: Session hash ID retrieved earlier from the server</summary>
				[JsonProperty("session")]
				public string Session { get; set; }

				/// <summary>localmodified: Date and time of last modification to the local database</summary>
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
				var Request = new RestRequest("api/v1/keepalive", Method.POST);

				Request.AddHeader("Content-Type", "application/json");
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
							// Server is telling us that our session is good and doesn't need to sync.
							// Next step: Nothing, all is good in the world :)
							break;
						}
					case System.Net.HttpStatusCode.Continue:
						{
							// Server is telling us that our session is good and that we should sync databases.
							// Next step: Send Sync request
							break;
						}
					case System.Net.HttpStatusCode.Forbidden:
						{
							// Server is telling us that our session is no longer active.
							// Next step: Send SignIn request
							break;
						}

					default:
					case System.Net.HttpStatusCode.BadRequest:
						{
							// Somehow there was an error in how the request was formed, this shouldn't happen normally.
							// Next step: Retry (up to 5 times, after that throw messagebox with network error warning)
							// TODO: Throw an error message here.
							break;
						}
				}
			}
		}


		// ========================
		//  Interface #5: v1: Sync
		// ========================

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
				/// <summary>session: Session hash ID retrieved earlier from the server</summary>
				[JsonProperty("session")]
				public string Session { get; set; }

				/// <summary>timestamp: Date and time of when this sync was sent from the client</summary>
				[JsonProperty("timestamp")]
				public DateTime Timestamp { get; set; }

				/// <summary>localdatabase: Entire local database of tasks</summary>
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
				/// <summary>timestamp: Date and time of when this sync was completed and sent back from the server to the client</summary>
				[JsonProperty("timestamp")]
				public DateTime Timestamp { get; set; }

				/// <summary>remotedatabase: Updated and merged database from server</summary>
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
				var Request = new RestRequest("api/v1/sync", Method.POST);

				Request.AddHeader("Content-Type", "application/json");
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
							// Server is telling us that the synchronization went fine.
							// Next step: Copy data from the response into the local database
							break;
						}
					case System.Net.HttpStatusCode.Forbidden:
						{
							// Server is telling us that our session is not valid and thus the sync did not happen.
							// Next step: Create "could not sync" flag, send SignIn request
							break;
						}

					default:
					case System.Net.HttpStatusCode.BadRequest:
						{
							// Somehow there was an error in how the request was formed, this shouldn't happen normally.
							// Next step: Retry (up to 5 times, after that throw messagebox with network error warning)
							// TODO: Throw an error message here.
							break;
						}
				}
			}
		}
	}
}
