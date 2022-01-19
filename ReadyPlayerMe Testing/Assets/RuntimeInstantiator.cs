using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wolf3D.ReadyPlayerMe.AvatarSDK;
using Photon.Pun;
using UnityEngine.Networking;
using System.Text;

public class RuntimeInstantiator : MonoBehaviour
{
    public List<string> models;

    public void LoadModel()
    {
        StartCoroutine(CallLogin("http://api.datafab.cl/minverso/v1/auth", GetJsonString(new user { email = "rodrigo@holo.cl", passapp = "123456" })));

        int index = UnityEngine.Random.Range(0, models.Count);

        ExitGames.Client.Photon.Hashtable _customProperties = PhotonNetwork.LocalPlayer.CustomProperties;
        _customProperties["URL"] = models[index] +"+" + (string)_customProperties["Nickname"];
        PhotonNetwork.LocalPlayer.SetCustomProperties(_customProperties);
    }

    public struct user
    {
        public string email;
        public string passapp;
    }

    public string GetJsonString(object a)
    {
        return JsonUtility.ToJson(a);
    }

    public IEnumerator CallLogin(string url, string logindataJsonString)
    {
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(logindataJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.error != null)
        {
            Debug.Log("Erro: " + request.error);
        }
        else
        {
            Debug.Log("All OK");
            Debug.Log("Status Code: " + request.responseCode);
            Debug.Log(request.downloadHandler.data);
            Debug.Log(Encoding.UTF8.GetString(request.downloadHandler.data));
        }

    }

}
