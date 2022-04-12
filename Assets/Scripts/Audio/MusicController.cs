using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EcoIsland
{
	public class MusicController : MonoBehaviour
	{
		public GameObject SaveController;
		public Text musicText;
		public Slider musicSlider;

		private AudioSource audioPlayer;
		private Setting settings;

		// Start is called before the first frame update
		void Start()
		{
			this.audioPlayer = this.GetComponent<AudioSource>();

			SaveSettings saveSettings = SaveController.GetComponent<SaveSettings>();

			this.settings = saveSettings.loadSettings();

			if (this.settings.musicEnabled == true)
			{
				this.audioPlayer.volume = this.settings.musicVolumen;
				this.updateText(this.settings.musicVolumen);
			}
			else
			{
				this.audioPlayer.volume = 0f;
				this.updateText(this.settings.musicVolumen);
			}
		}

		public void updateValues(Setting input)
		{
			AudioSource musicController = this.GetComponent<AudioSource>();
			if (input.musicEnabled == true)
			{
				musicController.volume = input.musicVolumen;
				this.updateText(input.musicVolumen);
			}
			else
			{
				musicController.volume = 0;
				this.updateText(input.musicVolumen);
			}
		}

		public void updateText(float input) {
			this.musicText.text = "Volumen: " + Mathf.Round(input * 100) + "%";
		}

		public void updatedSlider() {
			this.updateText(musicSlider.value);
		}
	}
}
