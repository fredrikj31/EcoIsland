using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EcoIsland
{

	public class ResetGame : MonoBehaviour
	{
		public string[] deleteFiles;
		private SaveSystem saveSys;

		// Start is called before the first frame update
		void Start()
		{
			this.saveSys = new SaveSystem();
		}

		// Update is called once per frame
		void Update()
		{

		}

		public void resetGame()
		{
			// Firstly delete all those files :)
			foreach (string item in deleteFiles)
			{
				string path = Application.persistentDataPath + "/" + item;

				this.saveSys.deleteFile(path);
			}

			// Then reload the scene
			StartCoroutine(this.reloadScene());

			// Then create the files with it's default content in it.
		}

		private IEnumerator reloadScene() {
			SceneManager.LoadScene("Main");
			yield return new WaitForSeconds(0.5f);
			SceneManager.LoadScene("Main");
		}
	}
}
