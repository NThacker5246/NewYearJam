using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
	[SerializeField] private GameObject menu, settings, save, about;
	[SerializeField] private Animator[] ansy;
	[SerializeField] public bool flag;

	void Awake(){
		flag = false;
		menu.SetActive(false);
		settings.SetActive(false);
		save.SetActive(false);
		about.SetActive(false);
		Time.timeScale = 1f;
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.Escape)){
			flag = !flag;
			menu.SetActive(flag);
			Time.timeScale = flag ? 0.00001f : 1f;
			for(int i = 0; i < ansy.Length; ++i) ansy[i].speed = flag ? 100000f : 1f;

			settings.SetActive(false);
			save.SetActive(false);
			about.SetActive(false);
		}
	}

	public void GoAhead(){
		SceneManager.LoadScene(0);
	}

	public void Close(){
		flag = false;
		Time.timeScale = 1f;
		StartCoroutine("Closer");
	}

	IEnumerator Closer(){
		yield return new WaitForSeconds(0.2f);
		menu.SetActive(false);
	}

	public void GoBack(){
		// flag = false;
		// Cursor.lockState = CursorLockMode.Locked;
		settings.SetActive(false);
		save.SetActive(false);
		about.SetActive(false);
	}
	
	public void OpenWin(int win){
		flag = true;
		switch(win){
			case 0:
				settings.SetActive(true);
				break;
			case 1:
				save.SetActive(true);
				break;
			case 2:
				about.SetActive(true);
				break;
		}
	}
}
