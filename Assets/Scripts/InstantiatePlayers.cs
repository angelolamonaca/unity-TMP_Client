using System;
using System.Collections;
using System.Text;
using Models;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class InstantiatePlayers : MonoBehaviour
{
    private static GameObject _player;

    private void Start()
    {
        _player = GameObject.Find("PlayerArmature");
        StartCoroutine(Upload());
    }

    private static IEnumerator Upload()
    {
        var position = _player.transform.position;
        var character = new Character();
        var newPosition = new Position();
        newPosition.x = position.x;
        newPosition.y = position.y;
        newPosition.z = position.z;
        character.position = newPosition;
        character.id = 1;
        
        var jsonString = JsonUtility.ToJson(character);
        var formData = Encoding.UTF8.GetBytes(jsonString);
        
        using var www = UnityWebRequest.Post("http://localhost:8080/character/add", "");
        www.uploadHandler = new UploadHandlerRaw(formData); //body
        www.SetRequestHeader("Content-Type", "application/json;charset=utf-8");
        www.SetRequestHeader ("Content-Type", "application/json");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Upload complete!");
        }
    }
}