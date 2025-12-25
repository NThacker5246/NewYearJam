using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BellTaker : MonoBehaviour
{
	[SerializeField] public int bellcounter, i;
	[SerializeField] private bool inColl;
	[SerializeField] private Transform church;
	[SerializeField] private Text txt;
	[SerializeField] private End start;
 	void Update(){
		if(Input.GetKeyDown(KeyCode.E) && inColl){
			church.GetChild(i).gameObject.SetActive(true);
			--bellcounter;
			++i;
			if(i == 7){
				start.Init();
			}
			txt.text = $"{bellcounter}";
		}
	}

	void OnTriggerEnter(Collider other){
		if(other.tag == "Church"){
			inColl = true;
		} else if(other.tag == "Bell"){
			other.gameObject.SetActive(false);
			++bellcounter;
			txt.text = $"{bellcounter}";
		}
	}

	void OnTriggerExit(Collider other){
		if(other.tag == "Church"){
			inColl = false;
		}
	}

}
