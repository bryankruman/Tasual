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
	class ArrayHandler
	{
		private static DateTime ProgramLastWriteTime { get; set; }
		private static DateTime ProgramLastReadTime { get; set; }
		//private static string ProgramLastWriteFile { get; set; }

		public static void ReAssignGroup(List<Task> Array, string OldTaskGroup, string NewTaskGroup)
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

		public static void Save(ref List<Task> Array, Setting Settings)
		{
			switch (Settings.Protocol)
			{
				default:
				case Setting.Protocols.JSON: Save_JSON(ref Array, Settings.StorageFolder); break;
			}
		}

		public static void Load(ref List<Task> Array, Setting Settings)
		{
			switch (Settings.Protocol)
			{
				default:
				case Setting.Protocols.JSON: Load_JSON(ref Array, Settings.StorageFolder); break;
			}
		}

		public static bool StorageNewerThanProgram(string PathToFile)
		{
			if (!File.Exists(PathToFile)) { return false; }
			//if (ProgramLastWriteFile != PathToFile) { return false; }

			DateTime FileLastWriteTime = File.GetLastWriteTime(PathToFile);

			bool WrittenAfterProgram = (FileLastWriteTime > ProgramLastWriteTime);
			bool WrittenAfterRead = (FileLastWriteTime > ProgramLastReadTime);

			Console.WriteLine(
				"File written after program: {0}, file written after read: {1}, needs update: {2}",
				WrittenAfterProgram.ToString(),
				WrittenAfterRead.ToString(),
				(WrittenAfterProgram && WrittenAfterRead).ToString()
			);

			if (WrittenAfterProgram && WrittenAfterRead)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		private static void Save_JSON(ref List<Task> Array, string FolderPath)
		{
			try
			{
				string PathToFile = Path.Combine(FolderPath, "tasks.db");

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
					Serializer.Serialize(OutputJson, Array);
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

		private static void Load_JSON(ref List<Task> Array, string FolderPath)
		{
			try
			{
				string PathToFile = Path.Combine(FolderPath, "tasks.db");
				using (StreamReader InputFile = File.OpenText(PathToFile))
				using (JsonReader InputJson = new JsonTextReader(InputFile))
				{
					Array.Clear();
					JsonSerializer Serializer = new JsonSerializer();
					Array = (List<Task>)Serializer.Deserialize(InputJson, typeof(List<Task>));
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
