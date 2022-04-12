using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EcoIsland
{
	public class EffectController : MonoBehaviour
	{
		public GameObject SaveController;
		public Text effectsText;
		public Slider effectsSlider;
		private AudioSource audioPlayer;
		private Setting settings;

		// Start is called before the first frame update
		void Start()
		{
			this.audioPlayer = this.GetComponent<AudioSource>();

			SaveSettings saveSettings = SaveController.GetComponent<SaveSettings>();
			this.settings = saveSettings.loadSettings();

			if (this.settings.effectsEnabled == true)
			{
				this.audioPlayer.volume = this.settings.effectsVolumen;
				this.updateText(this.settings.effectsVolumen);
			}
			else
			{
				this.audioPlayer.volume = 0f;
				this.updateText(this.settings.effectsVolumen);
			}
		}

		public void updateValues(Setting input)
		{
			AudioSource effectController = this.GetComponent<AudioSource>();
			if (input.effectsEnabled == true)
			{
				effectController.volume = input.effectsVolumen;
				this.updateText(input.effectsVolumen);
			}
			else
			{
				effectController.volume = 0;
				this.updateText(input.effectsVolumen);
			}
		}

		public void updateText(float input) {
			this.effectsText.text = "Volumen: " + Mathf.Round(input * 100) + "%";
		}

		public void updatedSlider() {
			this.updateText(effectsSlider.value);
		}
	}
}
