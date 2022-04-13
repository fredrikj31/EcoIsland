using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace EcoIsland
{

	public class SaveSystem
	{
		public bool fileExists(string filePath)
		{
			if (File.Exists(filePath))
			{
				return true;
			}
			else
			{
				//Debug.Log("File does not exist");
				return false;
			}
		}

		public bool overwriteFileContent(string filePath, string json)
		{
			if (File.Exists(filePath))
			{
				StreamWriter writer = new StreamWriter(filePath);

				writer.Write(json);

				writer.Close();

				return true;
			}
			else
			{
				Debug.Log("File does not exist");
				return false;
			}
		}

		public string readFile(string filePath)
		{
			if (File.Exists(filePath))
			{
				StreamReader reader = new StreamReader(filePath);

				string readResult = reader.ReadToEnd();

				reader.Close();

				return readResult;
			}
			else
			{
				Debug.Log("File does not exist");
				return null;
			}
		}

		public bool createFile(string filePath)
		{
			FileStream stream = new FileStream(filePath, FileMode.Create);

			stream.Close();

			return true;
		}

		public void deleteFile(string filePath) {
			File.Delete(filePath);
		}
	}
}
