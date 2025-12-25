using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Magic: MonoBehaviour
{
	[SerializeField] private int isHOT, health;
	[SerializeField] private float cooldown;
	[SerializeField] private Snaryad toObject;
	[SerializeField] private Camera cam;
	[SerializeField] private float[] cool;
	[SerializeField] private GameObject atselect, respawn;
	[SerializeField] private bool flag, casted;
	[SerializeField] private Animator anim;

	void Update(){
		if(casted) {
			anim.SetBool("Cast", false);
			casted = false;
		}
		if(health <= 0) {
			respawn.SetActive(true);
			Time.timeScale = 0.1f;
		}
		if(Input.GetMouseButtonDown(0) && cooldown >= cool[isHOT] && isHOT == 1){
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if(Physics.Raycast(ray, out hit, Mathf.Infinity)){
				toObject.transform.position = hit.point;
				toObject.Blow(1);
			}
			anim.SetBool("Cast", true);
			casted = true;

		} else if(Input.GetMouseButtonDown(0) && cooldown >= cool[isHOT]){
			toObject.transform.position = transform.position;
			toObject.Blow(isHOT);
			anim.SetBool("Cast", true);
			casted = true;
		} else {
			cooldown += Time.deltaTime;
		}

		for(int i = 0; i < 3; ++i){
			if(Input.GetKeyDown(KeyCode.Alpha1 + i)){
				isHOT = i;
			}
		}
	}

	public void Damage(){
		health -= (int) Mathf.Round(Random.Range(0, 5));
	}

	public void Restart(){
		SceneManager.LoadScene(1);
		Time.timeScale = 1;
	}
}