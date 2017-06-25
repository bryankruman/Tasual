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
				case Setting.Protocols.JSON: Save_JSON(ref Array, Settings.TextFile); break;
			}
		}

		public static void Load(ref List<Task> Array, Setting Settings)
		{
			switch (Settings.Protocol)
			{
				default:
				case Setting.Protocols.JSON: Load_JSON(ref Array, Settings.TextFile); break;
			}
		}

		private static void Save_JSON(ref List<Task> Array, string PathToFile)
		{
			try
			{
				if (!PathToFile.Contains("\\"))
				{
					PathToFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, PathToFile);
				}
				using (FileStream OutputFile = File.Open(PathToFile, FileMode.Create))
				using (StreamWriter OutputStream = new StreamWriter(OutputFile))
				using (JsonWriter OutputJson = new JsonTextWriter(OutputStream))
				{
					OutputJson.Formatting = Formatting.Indented;
					JsonSerializer Serializer = new JsonSerializer();
					Serializer.Serialize(OutputJson, Array);
				}

			}
			catch (Exception Args)
			{
				Console.WriteLine("ArrayHandler.Save_JSON(): {0}\nTrace: {1}", Args.Message, Args.StackTrace);
			}
		}

		private static void Load_JSON(ref List<Task> Array, string PathToFile)
		{
			try
			{
				if (!PathToFile.Contains("\\"))
				{
					PathToFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, PathToFile);
				}
				using (StreamReader InputFile = File.OpenText(PathToFile))
				using (JsonReader InputJson = new JsonTextReader(InputFile))
				{
					Array.Clear();
					JsonSerializer Serializer = new JsonSerializer();
					Array = (List<Task>)Serializer.Deserialize(InputJson, typeof(List<Task>));
				}
			}
			catch (Exception Args)
			{
				Console.WriteLine("ArrayHandler.Load_JSON(): {0}\nTrace: {1}", Args.Message, Args.StackTrace);
			}
		}
	}
}
