using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EcoIsland
{
	public class EffectPlayer : MonoBehaviour
	{
		public List<AudioClip> audioClips;

		private Dictionary<string, AudioClip> effects;
		private AudioSource player;

		void Start()
		{
			this.effects = new Dictionary<string, AudioClip>();
			this.player = this.GetComponent<AudioSource>();

			foreach (AudioClip myClip in audioClips)
			{
				this.effects.Add(myClip.name, myClip);
			}
		}

		public void playEffect(string effectName)
		{
			this.player.clip = effects[effectName];
			this.player.Play();
		}
	}
}
