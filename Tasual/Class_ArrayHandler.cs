// ===========================================
//  Copyright (c) 2017 Bryan Kruman
//
//  See LICENSE.rtf file in the project root
//  for full license information.
// ===========================================

using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Tasual
{
	/// <summary>
	/// Dedicated class to handle task arrays and database saving/loading.
	/// </summary>
	public class ArrayHandler
	{
		/// <summary>
		/// Array container class which holds both the array information and list array itself.
		/// </summary>
		public class ArrayContainer
		{
			[JsonProperty("info")]
			public ArrayInfo Info { get; set; }

			[JsonProperty("array")]
			public List<Task> Array { get; set; }
		}

		/// <summary>
		/// Array information class which stores basic database information.
		/// </summary>
		public class ArrayInfo
		{
			[JsonProperty("interface")]
			public int InterfaceVersion { get; set; } = 1;

			[JsonProperty("hash")]
			public string Hash { get; set; } = "foobar";

			[JsonProperty("created")]
			public TimeInfo Created { get; set; }

			[JsonProperty("modified")]
			public TimeInfo Modified { get; set; }

			[JsonProperty("collapsed")]
			public List<String> Collapsed { get; set; } = null;
		}

		/// <summary>Last time we wrote to the locally stored file.</summary>
		private static DateTime ProgramLastWriteTime { get; set; }
		/// <summary>Last time we read from the locally stored file.</summary>
		private static DateTime ProgramLastReadTime { get; set; }

		//public static ArrayContainer Arif { get; set; }

		public static ArrayInfo Info = new ArrayInfo();

		/// <summary>
		/// Scan through task array and rename existing groups to a new group.
		/// </summary>
		/// <param name="Array">Task array to sort through.</param>
		/// <param name="OldTaskGroup">Old task group to be removed/renamed.</param>
		/// <param name="NewTaskGroup">New task group with which to replace the old group.</param>
		public static void ReAssignGroup(ref List<Task> Array, string OldTaskGroup, string NewTaskGroup)
		{
			foreach (Task Task in Array)
			{
				if (Task == null) { break; }
				if (Task.Group == OldTaskGroup)
				{
					Task.Group = NewTaskGroup;
				}
			}
		}

		/// <summary>
		/// Merge two task arrays together into one by comparing their tasks and picking the most recently modified items.
		/// </summary>
		/// <param name="Internal">Primary task list to be merged into. This list also becomes the output of this function.</param>
		/// <param name="Changed">Secondary task list to merge into the primary.</param>
		public static void Compare(ref List<Task> Internal, ref List<Task> Changed)
		{
			List<Task> RemovalList = new List<Task>();
			List<Task> AddedList = new List<Task>();

			foreach (Task ChangedTask in Changed)
			{
				bool FoundSameID = false;

				// Check to see if there is a task in the internal list that matches this one
				foreach (Task InternalTask in Internal)
				{
					if (InternalTask.ID != ChangedTask.ID) { continue; }

					FoundSameID = true;

					if (ChangedTask.Time.Modified > InternalTask.Time.Modified)
					{
						// Changed task is newer
						RemovalList.Add(InternalTask);
						AddedList.Add(ChangedTask);
					}
				}

				// We didn't find any tasks with the same ID, which means Changed has a task which Internal doesn't
				if (!FoundSameID)
				{
					// Add changed task
					AddedList.Add(ChangedTask);
				}
			}

			foreach (Task RemoveTask in RemovalList)
			{
				Internal.Remove(RemoveTask);
			}

			foreach (Task AddTask in AddedList)
			{
				Internal.Add(AddTask);
			}
		}

		/// <summary>
		/// Save task list from the task array into the selected storage file.
		/// </summary>
		/// <param name="Array">Task array from which to save.</param>
		public static void Save(ref List<Task> Array)
		{
			switch (Settings.Config.Protocol)
			{
				default:
				case Settings.Protocols.JSON: Save_JSON(ref Array, Settings.Config.StorageFolder); break;
			}
		}

		/// <summary>
		/// Load task list from the selected storage file into the task array.
		/// </summary>
		/// <param name="Array">Task array to load in to.</param>
		public static void Load(ref List<Task> Array)
		{
			switch (Settings.Config.Protocol)
			{
				default:
				case Settings.Protocols.JSON: Load_JSON(ref Array, Settings.Config.StorageFolder); break;
			}
		}

		/// <summary>
		/// Check whether the file in storage is newer than the loaded task array in internal memory.
		/// </summary>
		/// <param name="PathToFile">Full file path of storage file to check.</param>
		/// <returns>Boolean of whether or not the locally stored file is newer than our internal memory.</returns>
		public static bool StorageNewerThanProgram(string PathToFile)
		{
			if (!File.Exists(PathToFile)) { return false; }
			//if (ProgramLastWriteFile != PathToFile) { return false; }

			DateTime FileLastWriteTime = File.GetLastWriteTime(PathToFile);

			bool WrittenAfterProgram = (FileLastWriteTime > ProgramLastWriteTime);
			bool WrittenAfterRead = (FileLastWriteTime > ProgramLastReadTime);

			/*Console.WriteLine(
				"File written after program: {0}, file written after read: {1}, needs update: {2}",
				WrittenAfterProgram.ToString(),
				WrittenAfterRead.ToString(),
				(WrittenAfterProgram && WrittenAfterRead).ToString()
			);*/

			if (WrittenAfterProgram && WrittenAfterRead)
			{
				Console.WriteLine("Resyncing database at {0}", DateTime.Now.ToString());
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Save task list from the task array into the tasks.db json file.
		/// </summary>
		/// <param name="Array">Task array from which to save tasks.</param>
		/// <param name="FolderPath">Path to the directory where the tasks.db database is stored.</param>
		private static void Save_JSON(ref List<Task> Array, string FolderPath)
		{
			try
			{
				string PathToFile = Path.Combine(FolderPath, "tasks.json");

				if (StorageNewerThanProgram(PathToFile))
				{
					// File in storage is newer than our internal array
					// Lets compare the changed array and the internal array before saving just to be safe
					List<Task> ChangedArray = new List<Task>();

					Load_JSON(ref ChangedArray, FolderPath);
					Compare(ref Array, ref ChangedArray);
				}

				// Create directory just in case it doesn't already exist
				DirectoryInfo DI = Directory.CreateDirectory(FolderPath);

				// Save database
				using (FileStream OutputFile = File.Open(PathToFile, FileMode.Create))
				using (StreamWriter OutputStream = new StreamWriter(OutputFile))
				using (JsonWriter OutputJson = new JsonTextWriter(OutputStream))
				{
					OutputJson.Formatting = Formatting.Indented;
					JsonSerializer Serializer = new JsonSerializer();
					ArrayContainer Container = new ArrayContainer();

					Container.Array = Array;
					Container.Info = Info;

					Serializer.Serialize(OutputJson, Container);
				}

				// Set the last write time 
				ProgramLastWriteTime = File.GetLastWriteTime(PathToFile);
				//ProgramLastWriteFile = PathToFile;
			}
			catch (Exception Args)
			{
				Console.WriteLine("ArrayHandler.Save_JSON(): {0}\nTrace: {1}", Args.Message, Args.StackTrace);
			}
		}

		/// <summary>
		/// Load task list from the tasks.db json file into the task array.
		/// </summary>
		/// <param name="Array">Task array to load tasks into.</param>
		/// <param name="FolderPath">Path to the directory where the tasks.db database is stored.</param>
		private static void Load_JSON(ref List<Task> Array, string FolderPath)
		{
			try
			{
				string PathToFile = Path.Combine(FolderPath, "tasks.json");
				using (StreamReader InputFile = File.OpenText(PathToFile))
				using (JsonReader InputJson = new JsonTextReader(InputFile))
				{
					ArrayContainer Container = new ArrayContainer();
					JsonSerializer Serializer = new JsonSerializer();
					Container = (ArrayContainer)Serializer.Deserialize(InputJson, typeof(ArrayContainer));

					Array.Clear();
					//Info.Minimized.Clear();

					Array = Container.Array;
					Info = Container.Info;
				}
				
				// Set the last read time
				ProgramLastReadTime = DateTime.Now;
			}
			catch (Exception Args)
			{
				Console.WriteLine("ArrayHandler.Load_JSON(): {0}\nTrace: {1}", Args.Message, Args.StackTrace);
			}
		}
	}
}
