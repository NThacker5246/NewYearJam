using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
	[SerializeField] private Animator anim;
	[SerializeField] private Transform player;
	[SerializeField] private bool initiate;
	public void Init(){
		transform.GetChild(0).gameObject.SetActive(true);
		anim.SetTrigger("End");
		StartCoroutine("TpPlayer");
	}

	void Update(){
		if(initiate){

			player.position = Vector3.MoveTowards(player.position, transform.GetChild(0).position, Time.deltaTime*5);
			if(Vector3.Distance(player.position, transform.GetChild(0).position) <= 1f){
				SceneManager.LoadScene(2);
			}
		}
	}

	IEnumerator TpPlayer(){
		yield return new WaitForSeconds(1f);
		initiate = true;
		player.GetComponent<Rigidbody>().useGravity = false;
	}
}
