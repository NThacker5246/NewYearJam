using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC: MonoBehaviour 
{
	[SerializeField] private int health, strength;
	[SerializeField] private UnityEngine.AI.NavMeshAgent agent;
	[SerializeField] private Transform player;
	[SerializeField] private bool inColl;
	[SerializeField] private Animator anim;

	void Awake(){
		StartCoroutine("loop");
	}

	IEnumerator loop(){
		while(true){
			if(health <= 0) {gameObject.SetActive(false); break;}

			anim.SetInteger("State", 1);
			agent.destination = player.position + new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
			yield return new WaitForSeconds(2f);
			print("Go");
			anim.SetInteger("State", 2);
			TryAttack();
			yield return new WaitForSeconds(0.1f);
			anim.SetInteger("State", 0);
		}
	}

	void TryAttack(){
		if(inColl){
			player.GetComponent<Magic>().Damage();
		}
	}

	void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			inColl = true;
		}
	}
	
	void OnTriggerExit(Collider other){
		if(other.tag == "Player"){
			inColl = false;
		}
	}

	public void Damage(float k){
		health -= (int) Mathf.Floor(Random.Range(0, 2) * k);
	}

	public void Teleport(){
		gameObject.SetActive(false);
	}

	public void Restart(){
		transform.position = player.position + new Vector3(Random.Range(-20, 20), 50, Random.Range(-20, 20));
		health = 100;
		inColl = false;
		gameObject.SetActive(true);
		Awake();
	}
}