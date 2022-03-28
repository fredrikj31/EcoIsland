using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class SaveSettings : MonoBehaviour
{
	private SaveSystem saveSys;
	private string settingFilePath;

	public Toggle musicToggler;
	public Slider musicSlider;
	public Toggle soundEffectsToggler;
	public Slider soundEffectsSlider;

	public GameObject musicController;
	public GameObject soundEffectsController;


    // Start is called before the first frame update
    void Start()
    {
        this.saveSys = new SaveSystem();
		this.settingFilePath = Application.persistentDataPath + "/settings.json";

		// Sets the default values
		this.setSettingValues();
    }

    public void saveSettings() {
		bool musicEnabled = this.musicToggler.isOn;
		float musicVolumen = this.musicSlider.value;
		bool effectsEnabled = this.soundEffectsToggler.isOn;
		float effectsVolumen = this.soundEffectsSlider.value;

		SaveSetting saveSetting = new SaveSetting();
		saveSetting.musicEnabled = musicEnabled;
		saveSetting.musicVolumen = musicVolumen;
		saveSetting.effectsEnabled = effectsEnabled;
		saveSetting.effectsVolumen = effectsVolumen;

		string jsonResult = JsonConvert.SerializeObject(saveSetting);

		// When finished saving all the tiles, then save to file
		if (this.saveSys.fileExists(this.settingFilePath)) {
			this.saveSys.overwriteFileContent(this.settingFilePath, jsonResult);
		} else {
			this.saveSys.createFile(this.settingFilePath);
			this.saveSys.overwriteFileContent(this.settingFilePath, jsonResult);
		}

		// Sets the new values
		this.musicController.GetComponent<MusicController>().updateValues(saveSetting);
		this.soundEffectsController.GetComponent<EffectController>().updateValues(saveSetting);
	}

	public SaveSetting loadSettings() {
		if (this.saveSys.fileExists(this.settingFilePath)) {
			string data = this.saveSys.readFile(this.settingFilePath);

			SaveSetting result = JsonConvert.DeserializeObject<SaveSetting>(data);
			return result;
		} else {
			SaveSetting saveSetting = new SaveSetting();
			saveSetting.musicEnabled = true;
			saveSetting.musicVolumen = 1.0f;
			saveSetting.effectsEnabled = true;
			saveSetting.effectsVolumen = 1.0f;

			string jsonResult = JsonConvert.SerializeObject(saveSetting);

			// When finished saving all the tiles, then save to file
			if (this.saveSys.fileExists(this.settingFilePath)) {
				this.saveSys.overwriteFileContent(this.settingFilePath, jsonResult);
			} else {
				this.saveSys.createFile(this.settingFilePath);
				this.saveSys.overwriteFileContent(this.settingFilePath, jsonResult);
			}

			return saveSetting;
		}
	}

	public void setSettingValues() {
		SaveSetting result = this.loadSettings();

		this.musicToggler.isOn = result.musicEnabled;
		this.musicSlider.value = result.musicVolumen;
		this.soundEffectsToggler.isOn = result.effectsEnabled;
		this.soundEffectsSlider.value = result.effectsVolumen;
	}
}
