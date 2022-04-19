using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace EcoIsland
{
	public class BarnStorage : MonoBehaviour
	{
		private GameObject barnMenu;
		private SaveSystem saveSys;
		private string itemsFile;

		// Start is called before the first frame update
		void Start()
		{
			this.saveSys = new SaveSystem();

			this.barnMenu = GameObject.FindGameObjectWithTag("BarnMenu").transform.GetChild(0).gameObject;
			this.itemsFile = Application.persistentDataPath + "/items.json";

			// Check if file exists, if not, then create it, with one wheat in the file
			if (this.saveSys.fileExists(this.itemsFile) == false)
			{
				// Creates the file.
				this.saveSys.createFile(this.itemsFile);
				// Creates a temp list with all values
				List<Item> tempList = new List<Item>();
				// Creating the starter amount of wheat.
				foreach (var itemType in Enum.GetValues(typeof(ItemTypes)))
				{
					Item item = new Item();
					item.itemName = itemType.ToString();
					item.itemAmount = 0;
					tempList.Add(item);
				}
				// Saving the crop to the file
				this.saveItems(tempList);
			}
		}

		void OnMouseDown()
		{
			this.barnMenu.SetActive(true);
		}

		private void saveItems(List<Item> input)
		{
			// Save the new crops to a file
			string jsonString = JsonConvert.SerializeObject(input);
			// Save it to a file
			this.saveSys.overwriteFileContent(this.itemsFile, jsonString);
		}

		public List<Item> getItems()
		{
			// If file does not exists, return a empty list
			if (this.saveSys.fileExists(this.itemsFile) == false)
			{
				List<Item> items = new List<Item>();
				return items;
			}

			// Read data from file
			string data = this.saveSys.readFile(this.itemsFile);
			List<Item> jsonData = JsonConvert.DeserializeObject<List<Item>>(data);

			return jsonData;
		}

		public Item getCrop(string name)
		{
			// Get all CropItem
			List<Item> allItems = this.getItems();

			if (allItems == null)
			{
				return null;
			}

			// Loop to find crop
			foreach (Item item in allItems)
			{
				if (item.itemName == name)
				{
					return item;
				}
				else
				{
					continue;
				}
			}
			return null;
		}

		public void addItem(ItemTypes type)
		{
			// Get all CropItem in silo
			List<Item> allItems = this.getItems();

			// Remove the already existing item from the silo
			foreach (Item selectedItem in allItems)
			{
				if (selectedItem.itemName == type.ToString())
				{
					selectedItem.itemAmount += 1;
					this.saveItems(allItems);
					return;
				}
			}

			// Check if there are not any crop item with name in the silo
			// Create new instance of new crop
			Item item = new Item();
			item.itemName = type.ToString();
			item.itemAmount = 1;

			// Add the crop to the system
			allItems.Add(item);
			this.saveItems(allItems);
			// Updates the silo menu UI
			this.GetComponent<BarnMenu>().updateUI();
			return;
		}

		public void removeItem(ItemTypes type)
		{
			// Get all CropItem in silo
			List<Item> allItems = this.getItems();

			// Remove the already existing item from the silo
			foreach (Item item in allItems)
			{
				if (item.itemName == type.ToString())
				{
					if (item.itemAmount == 0)
					{
						return;
					}

					item.itemAmount -= 1;
					this.saveItems(allItems);
					// Updates the silo menu UI
					//this.GetComponent<BarnMenu>().updateUI();
					return;
				}
			}

			// Updates the silo menu UI
			this.GetComponent<BarnMenu>().updateUI();
			return;
		}

		public bool hasAmount(ItemTypes type, int amount)
		{
			// Get all CropItem in silo
			List<Item> allItems = this.getItems();

			// Remove the already existing item from the silo
			foreach (Item item in allItems)
			{
				if (item.itemName == type.ToString())
				{
					if (item.itemAmount >= amount)
					{
						return true;
					}
					else
					{
						return false;
					}
				}
			}

			return false;
		}
	}
}
