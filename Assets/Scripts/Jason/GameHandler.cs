using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    void Start()
    {
        /*PlayerData player = new PlayerData();
        player.position = new Vector3(15, 0);
        player.health = 80;

        string jason = JsonUtility.ToJson(player);
        Debug.Log(jason);

        File.WriteAllText(Application.dataPath + "/ysr.json", jason);
        */

        string jon = File.ReadAllText(Application.dataPath + "/ysr.json");

        PlayerData loadedData = JsonUtility.FromJson<PlayerData>(jon);
        Debug.Log("position:" + loadedData.position);
        Debug.Log("health:" + loadedData.health);
    }
    
    private class PlayerData
    {
        public Vector3 position;
        public int health;
    }
}
