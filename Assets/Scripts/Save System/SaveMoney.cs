using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveMoney : MonoBehaviour
{

	private SaveSystem saveSys;
	private string moneyPath;
    // Start is called before the first frame update
    void Start()
    {
        this.saveSys = new SaveSystem();
		this.moneyPath = Application.persistentDataPath + "/money.json";
    }

    public void addMoney(int amount) {

	}

	public void removeMoney(int amount) {

	}
}
