//HackerSave Lib

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class HackerSave
{
	private string name;

	public HackerSave(string fileName){
		this.name = fileName;
	}

	public bool writeFile(PlayerStorage obj){
		// Create a file to write to.
		using (StreamWriter sw = new StreamWriter(name))
		{
			sw.WriteLine(JsonUtility.ToJson(obj));
			return true;
		}
		//return false;
	}

	public PlayerStorage readFile(){
		using (StreamReader sr = new StreamReader(name))
		{
			string s = sr.ReadLine();
			PlayerStorage data = JsonUtility.FromJson<PlayerStorage>(s);
			return data;
		}
	}
}

[Serializable]
public struct PlayerStorage
{
	public Vector3 position;
	public int bellcounter, i;

	public PlayerStorage(Vector3 position, int bellcounter, int i) {
		this.position = position;
		this.i = i;
		this.bellcounter = bellcounter;
	}
}