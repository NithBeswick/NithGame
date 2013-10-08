using UnityEngine;
using System.Collections;
using System.IO;

//*********************************************
//	SaveLoadManger handles saving and loading
//	of variables in the "OurSaveFile" class
//	(lower down in this file)
//*********************************************
public class SaveLoadManager {
	//this line turns this class into a singleton, by using SaveLoadManger.insance.
	//you can access its public methods and variables as if you referenced it from somewhere
	public static SaveLoadManager instance = new SaveLoadManager();
	public OurSavefile saveFile;
	
	public bool LoadRealSavefile() {
		//first check if the directory exists, if it doesnt, create it
		if(!Directory.Exists(Application.dataPath + "/Resources")) {
			Directory.CreateDirectory(Application.dataPath + "/Resources");
		}
		
		string path = Application.dataPath + "/Resources/mysave.txt";		
		
		saveFile = new OurSavefile();
		//if the file exists, load it as a string, and parse it in our save file
		if(File.Exists(path)) {
			StreamReader sr = new StreamReader(path);
			string s = sr.ReadToEnd();
			sr.Close();
			
			saveFile.load(s);
			return true;
		} else {
			Debug.Log("No save file");
			return false;
		}
	}
	
	public void SaveRealSavefile() {
		//first check if the directory exists, if it doesnt, create it
		if(!Directory.Exists(Application.dataPath + "/Resources")) {
			Directory.CreateDirectory(Application.dataPath + "/Resources");
		}
		
		string path = Application.dataPath + "/Resources/mysave.txt";		
		
		//if the file exists, delete it, its easier making a new one
		if(File.Exists(path)) {
			File.Delete(path);
		}
		
		//get the savefile as a string, and write it
		StreamWriter tw = new StreamWriter(path);
		tw.WriteLine(saveFile.save());
		tw.Close();
	}
	
	//the following methods are for saving and loading using PlayerPrefs
	public void SavePref(string key, float val) {
		PlayerPrefs.SetFloat(key, val);
	}
	
	public void SavePref(string key, int val) {
		PlayerPrefs.SetInt(key, val);
	}
	
	public void SavePref(string key, string val) {
		PlayerPrefs.SetString(key, val);
	}
	
	public float LoadPrefFloat(string key) {
		if(PlayerPrefs.HasKey(key))
			return PlayerPrefs.GetFloat(key);
		else
			return 0;
	}
	
	public float LoadPrefFloat(string key, float defaultValue) {
		if(PlayerPrefs.HasKey(key))
			return PlayerPrefs.GetFloat(key);
		else
			return defaultValue;
	}
}


//*********************************************
//	OurSaveFile contains all the variables we want to save
//	it also has a save and load method to convert it to
//	and from a string.
//*********************************************
//System.Serializable makes the class accessable from the
//Unity inspector
[System.Serializable]
public class OurSavefile {
	public Vector3 playerPosition;
	public Vector2 guiPosition;
	
	public string save() {
		string s = playerPosition.x + ":" + playerPosition.z;
		s += '\n';
		s +=  guiPosition.x + ":" + guiPosition.y;
		return s;
	}
	
	public void load(string text) {
		string[] lines = text.Split('\n');
		
		string[] split = lines[0].Split(':');
		playerPosition = new Vector3(float.Parse(split[0]), 0, float.Parse(split[1]));
		
		string[] split2 = lines[1].Split(':');
		guiPosition = new Vector2(float.Parse(split2[0]), float.Parse(split2[1]));
	}
}