using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Newtonsoft.Json;

namespace EcoIsland
{
	public class SiloStorage : MonoBehaviour
	{
		private GameObject siloMenu;
		private SaveSystem saveSys;
		private string cropFile;

		// Start is called before the first frame update
		void Start()
		{
			this.siloMenu = GameObject.FindGameObjectWithTag("SiloMenu").transform.GetChild(0).gameObject;
			this.saveSys = new SaveSystem();
			this.cropFile = Application.persistentDataPath + "/cropitem.json";

			// Check if file exists, if not, then create it, with one wheat in the file
			if (this.saveSys.fileExists(this.cropFile) == false)
			{
				// Creates the file.
				this.saveSys.createFile(this.cropFile);
				// Creates a temp list with all values
				List<CropItem> tempList = new List<CropItem>();
				// Creating the starter amount of wheat.
				foreach (var cropType in Enum.GetValues(typeof(CropTypes)))
				{
					CropItem crop = new CropItem();
					crop.cropName = cropType.ToString();
					crop.cropAmount = 0;
					tempList.Add(crop);
				}
				// Saving the crop to the file
				this.saveCrops(tempList);
			}
		}

		public void updateUI() {
			// Updates the menu UI
			this.GetComponent<SiloMenu>().updateUI();
		}

		private void saveCrops(List<CropItem> input)
		{
			// Save the new crops to a file
			string jsonString = JsonConvert.SerializeObject(input);
			// Save it to a file
			this.saveSys.overwriteFileContent(this.cropFile, jsonString);
		}

		public List<CropItem> getCropItems()
		{

			// If file does not exists, return a empty list
			if (this.saveSys.fileExists(this.cropFile) == false)
			{
				List<CropItem> CropItem = new List<CropItem>();
				return CropItem;
			}

			// Read data from file
			string data = this.saveSys.readFile(this.cropFile);
			List<CropItem> jsonData = JsonConvert.DeserializeObject<List<CropItem>>(data);

			return jsonData;
		}

		public CropItem getCrop(string name)
		{
			// Get all CropItem
			List<CropItem> allCropItem = this.getCropItems();

			if (allCropItem == null)
			{
				return null;
			}

			// Loop to find crop
			foreach (CropItem crop in allCropItem)
			{
				if (crop.cropName == name)
				{
					return crop;
				}
				else
				{
					continue;
				}
			}
			return null;
		}

		public void addCrop(CropTypes type)
		{
			// Get all CropItem in silo
			List<CropItem> allCropItem = this.getCropItems();

			// Remove the already existing item from the silo
			foreach (CropItem item in allCropItem)
			{
				if (item.cropName == type.ToString())
				{
					item.cropAmount += 1;
					this.saveCrops(allCropItem);
					// Updates the silo menu UI
					this.updateUI();
					return;
				}
			}

			// Check if there are not any crop item with name in the silo
			// Create new instance of new crop
			CropItem crop = new CropItem();
			crop.cropName = type.ToString();
			crop.cropAmount = 1;

			// Add the crop to the system
			allCropItem.Add(crop);
			this.saveCrops(allCropItem);
			// Updates the silo menu UI
			this.updateUI();
			return;
		}

		public void removeCrop(CropTypes type) {
			// Get all CropItem in silo
			List<CropItem> allCropItem = this.getCropItems();

			// Remove the already existing item from the silo
			foreach (CropItem item in allCropItem)
			{
				if (item.cropName == type.ToString())
				{
					if (item.cropAmount == 0) {
						return;
					}

					item.cropAmount -= 1;
					this.saveCrops(allCropItem);
					// Updates the silo menu UI
					this.updateUI();
					return;
				}
			}

			// Updates the silo menu UI
			this.updateUI();
			return;
		}

		public bool hasAmount(CropTypes type, int amount) {
			// Get all CropItem in silo
			List<CropItem> allCropItem = this.getCropItems();

			// Remove the already existing item from the silo
			foreach (CropItem item in allCropItem)
			{
				if (item.cropName == type.ToString())
				{
					if (item.cropAmount >= amount) {
						return true;
					} else {
						return false;
					}
				}
			}

			return false;
		}

	}
}