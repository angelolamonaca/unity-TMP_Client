using System.Collections.Generic;
using UnityEngine;
using NativeWebSocket;

public class Connection : MonoBehaviour
{
    WebSocket _websocket;

    // Start is called before the first frame update
    async void Start()
    {
        var headers = new Dictionary<string, string> {{"authorization", "Bearer al123"}};

        _websocket = new WebSocket("ws://localhost:8080/world/characters", headers);
        
        _websocket.OnOpen += () => { Debug.Log("Connection open!"); };

        _websocket.OnError += (e) => { Debug.Log("Error! " + e); };

        _websocket.OnClose += (e) => { Debug.Log("Connection closed!"); };

        _websocket.OnMessage += (bytes) =>
        {
            Debug.Log("OnMessage!");
            Debug.Log(bytes);

            // getting the message as a string
            // var message = System.Text.Encoding.UTF8.GetString(bytes);
            // Debug.Log("OnMessage! " + message);
        };

        // Keep sending messages at every 0.3s
        // InvokeRepeating("SendWebSocketMessage", 0.0f, 0.3f);

        // waiting for messages
        await _websocket.Connect();
    }

    void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        _websocket.DispatchMessageQueue();
#endif
    }

    async void SendWebSocketMessage()
    {
        if (_websocket.State != WebSocketState.Open) return;
        // Sending bytes
        await _websocket.Send(new byte[] {10, 20, 30});

        // Sending plain text
        await _websocket.SendText("plain text message");
    }

    private async void OnApplicationQuit()
    {
        await _websocket.Close();
    }
}