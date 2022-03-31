using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EcoIsland
{
	public class MusicController : MonoBehaviour
	{
		public GameObject SaveController;
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
			}
			else
			{
				this.audioPlayer.volume = 0f;
			}
		}

		public void updateValues(Setting input)
		{
			AudioSource musicController = this.GetComponent<AudioSource>();
			if (input.musicEnabled == true)
			{
				musicController.volume = input.musicVolumen;
			}
			else
			{
				musicController.volume = 0;
			}
		}
	}
}
