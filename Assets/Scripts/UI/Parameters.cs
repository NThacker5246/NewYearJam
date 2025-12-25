using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Parameters : MonoBehaviour
{
	[SerializeField] private Text pos, fps, tmr;
	[SerializeField] private float fpfiltered, tmra;
	[SerializeField] private Transform player;

	void Update(){
		float fa = 1f / Time.deltaTime;
		fpfiltered += (fa - fpfiltered) * 0.05f;

		tmra += Time.deltaTime;
		int tm = (int) tmra;
		tmr.text = $"TMR: {(int) (tm/3600)}/{(int) (tm/60) % 60}/{tm % 60}";
		pos.text = $"POS: {player.position.ToString()}";
		fps.text = $"FPS: {(int)fpfiltered}";
	} 
}
