using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snaryad : MonoBehaviour
{
	[SerializeField] private float r1, r2, r3;
	[SerializeField] private GameObject ps;
	public void Blow(int a){
		switch(a){
			case 0:
				StartCoroutine("Boom", r1);
				break;
			case 1:
				StartCoroutine("Boom", r2);
				break;
			case 2:
				StartCoroutine("Circler");
				break;
		}
	}

	IEnumerator Boom(float r){
		ps.SetActive(true);
		for(int i = 0; i < 30; ++i){
			Collider[] col = Physics.OverlapSphere(transform.position, r);
			for(int j = 0; j < col.Length; ++j){
				NPC np = col[j].GetComponent<NPC>();
				if(np != null)np.Damage(r/(transform.position - col[j].transform.position).magnitude);
			}
			yield return new WaitForSeconds(0.1f);
		}
		ps.SetActive(false);
	}
	
	IEnumerator Circler(){
		ps.SetActive(true);
		for(int i = 0; i < 60; ++i){
			Collider[] col = Physics.OverlapSphere(transform.position, r3);
			for(int j = 0; j < col.Length; ++j){
				col[j].GetComponent<NPC>().Teleport();
			}
			yield return new WaitForSeconds(0.1f);
		}
		ps.SetActive(false);
	}
}
