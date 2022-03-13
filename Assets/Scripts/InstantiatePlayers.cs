using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;
using UnityEngine;
using UnityEngine.Networking;

public class InstantiatePlayers : MonoBehaviour
{
    private static GameObject _player;

    private void Start()
    {
        _player = GameObject.Find("PlayerArmature");
        StartCoroutine(UploadCurrentPlayer());
        StartCoroutine(GetOtherPlayers());
    }

    private static IEnumerator UploadCurrentPlayer()
    {
        var position = _player.transform.position;
        var character = new Character(42, new Position(position.x, position.y, position.z));

        var jsonString = JsonUtility.ToJson(character);
        var formData = Encoding.UTF8.GetBytes(jsonString);

        using var webRequest = UnityWebRequest.Post("http://localhost:8080/character/add", "");
        webRequest.uploadHandler = new UploadHandlerRaw(formData); //body
        webRequest.SetRequestHeader("Content-Type", "application/json;charset=utf-8");
        webRequest.SetRequestHeader("Content-Type", "application/json");
        yield return webRequest.SendWebRequest();

        switch (webRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
                Debug.LogError(": Error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.LogError(": HTTP Error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.Success:
                Debug.Log(": CurrentPlayerUploaded: " + webRequest.downloadHandler.text);
                break;
            case UnityWebRequest.Result.InProgress:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private static IEnumerator GetOtherPlayers()
    {
        using var webRequest = UnityWebRequest.Get("http://localhost:8080/world/get");
        yield return webRequest.SendWebRequest();

        switch (webRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
                Debug.LogError(": Error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.LogError(": HTTP Error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.Success:
                Debug.Log(": OtherPlayersReceived: " + webRequest.downloadHandler.text);
                var world = JsonUtility.FromJson<World>(webRequest.downloadHandler.text);
                SpawnPlayers(world);
                break;
            case UnityWebRequest.Result.InProgress:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private static void SpawnPlayers(World world)
    {
        foreach (var character in world.characterList.Where(character => character.id != 42))
        {
            Debug.Log(character);
            Instantiate(_player, new Vector3(character.position.x, character.position.y, character.position.z), new Quaternion());
        }
    }
}