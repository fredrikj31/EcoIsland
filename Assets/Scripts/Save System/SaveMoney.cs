using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class SaveMoney : MonoBehaviour
{

	public Text moneyText;

	private SaveSystem saveSys;
	private string moneyPath;
    // Start is called before the first frame update
    void Start()
    {
        this.saveSys = new SaveSystem();
		this.moneyPath = Application.persistentDataPath + "/money.json";

		// Check if file exists, if not, then create the file
		if (this.saveSys.fileExists(this.moneyPath) == false) {
			// Creates the file
			this.saveSys.createFile(this.moneyPath);

			// Create default values
			Money money = new Money();
			money.amount = 0;

			// Write the content to the file
			string jsonData = JsonConvert.SerializeObject(money);
			this.saveSys.overwriteFileContent(this.moneyPath, jsonData);
		}

		this.setMoney();
    }

	public int getMoney() {
		// Check if file exists, if not return 0
		if (this.saveSys.fileExists(this.moneyPath) == false) {
			return 0;
		}

		// Read data from file, then return the value
		string jsonData = this.saveSys.readFile(this.moneyPath);
		Money money = JsonConvert.DeserializeObject<Money>(jsonData);

		return money.amount;
	}

    public void addMoney(int amount) {
		// Get current amount of money
		int moneyAmount = this.getMoney();

		// Add the amount to the balance
		string jsonData = this.saveSys.readFile(this.moneyPath);
		Money money = JsonConvert.DeserializeObject<Money>(jsonData);
		// Adding the amount
		money.amount += amount;

		// Save the new data to file
		string newJsonData = JsonConvert.SerializeObject(money);
		this.saveSys.overwriteFileContent(this.moneyPath, newJsonData);

		this.setMoney();
	}

	public void removeMoney(int amount) {
		int moneyAmount = this.getMoney();

		if ((moneyAmount - amount) >= 0) {
			string jsonData = this.saveSys.readFile(this.moneyPath);
			Money money = JsonConvert.DeserializeObject<Money>(jsonData);

			money.amount -= amount;

			string newJsonData = JsonConvert.SerializeObject(money);
			this.saveSys.overwriteFileContent(this.moneyPath, newJsonData);
		}

		this.setMoney();
	}

	public void setMoney() {
		int moneyAmount = this.getMoney();

		this.moneyText.text = moneyAmount.ToString();
	}
}
