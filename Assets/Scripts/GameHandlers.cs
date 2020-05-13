using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameHandlers : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //PlayerData playerdata = new PlayerData();
        //playerdata.position = new Vector3(5, 0);
        //playerdata.health = 80;

        //string json = JsonUtility.ToJson(playerdata);
        //Debug.Log(json);

        //File.WriteAllText(Application.dataPath + "/saveFile.json", json);


        string json = File.ReadAllText(Application.dataPath + "/saveFile.json");
        PlayerData loadPlayerData = JsonUtility.FromJson<PlayerData>(json);
        Debug.Log("poistion: " + loadPlayerData.position);
        Debug.Log("health: " + loadPlayerData.health);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private class PlayerData
    {
        public Vector3 position;
        public int health;
    }
}
