using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;


public class SiloStorage : MonoBehaviour
{
	private SaveSystem saveSys;
	private string cropsFile;

    // Start is called before the first frame update
    void Start()
    {
        this.saveSys = new SaveSystem();
		this.cropsFile = Application.persistentDataPath + "/crops.json";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private List<Crops> getCrops() {

		// If file does not exists, return a empty list
		if (this.saveSys.fileExists(this.cropsFile) == false) {			
			List<Crops> crops = new List<Crops>();
			return crops;
		}

		// Read data from file
		string data = this.saveSys.readFile(this.cropsFile);
		List<Crops> jsonData = JsonConvert.DeserializeObject<List<Crops>>(data);

		return jsonData;
	}
}
