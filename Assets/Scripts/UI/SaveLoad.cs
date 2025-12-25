using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoad : MonoBehaviour
{
	[SerializeField] private Transform player;
	[SerializeField] private GameObject clo1, clo2;
	[SerializeField] private Cloth clo;
	[SerializeField] private BellTaker bt;
	[SerializeField] private Menu men;
	void Awake(){
		bt = player.GetComponent<BellTaker>();
	}
	public void Save(Dropdown drop){
		// int slot = drop.getToggleId();
		int slot = drop.value;
		PlayerStorage sv = new PlayerStorage(player.position, bt.bellcounter, bt.i);
		HackerSave saver = new HackerSave($"./SaveIA_{slot}.json");
		saver.writeFile(sv);
	}

	public void Load(Dropdown drop){
		// int slot = drop.getToggleId();
		int slot = drop.value;
		HackerSave loader = new HackerSave($"./SaveIA_{slot}.json");
		PlayerStorage data = loader.readFile();
		player.position = data.position;
		bt.bellcounter = data.bellcounter;
		bt.i = data.i;
		StartCoroutine("Protector");
	}

	IEnumerator Protector(){
		clo.enabled = false;
		clo1.SetActive(false);
		clo2.SetActive(true);
		while(men.flag) {yield return new WaitForSeconds(0.1f);}
		yield return new WaitForSeconds(0.01f);
		clo1.SetActive(true);
		clo2.SetActive(false);
		clo.enabled = true;
	}
}
