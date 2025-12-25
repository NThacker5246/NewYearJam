using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField] private NPC[] npc;

	void Awake(){
		StartCoroutine("Ticking");
	}

	IEnumerator Ticking(){
		while(true){
			for(int i = 0; i < npc.Length; ++i){
				yield return new WaitForSeconds(10f);
				if(!npc[i].gameObject.activeInHierarchy){
					npc[i].Restart();
				}
			}
		}
	}
}
