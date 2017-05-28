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
				using (FileStream OutputFile = File.Open(PathToFile, FileMode.Create))
				using (StreamWriter OutputStream = new StreamWriter(OutputFile))
				using (JsonWriter OutputJson = new JsonTextWriter(OutputStream))
				{
					OutputJson.Formatting = Formatting.Indented;
					JsonSerializer Serializer = new JsonSerializer();
					Serializer.Serialize(OutputJson, Array);
				}

			}
			catch (Exception e)
			{
				Console.WriteLine("ArrayHandler.Save_JSON(): {0}\nTrace: {1}", e.Message, e.StackTrace);
			}
		}

		private static void Load_JSON(ref List<Task> Array, string PathToFile)
		{
			try
			{
				using (StreamReader InputFile = File.OpenText(PathToFile))
				using (JsonReader InputJson = new JsonTextReader(InputFile))
				{
					Array.Clear();
					JsonSerializer Serializer = new JsonSerializer();
					Array = (List<Task>)Serializer.Deserialize(InputJson, typeof(List<Task>));
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("ArrayHandler.Load_JSON(): {0}\nTrace: {1}", e.Message, e.StackTrace);
			}
		}
	}
}
