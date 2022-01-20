using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wolf3D.ReadyPlayerMe.AvatarSDK;
using Photon.Pun;
using UnityEngine.Networking;
using System.Text;
using Newtonsoft.Json;

public class RuntimeInstantiator : MonoBehaviour
{
    public List<string> models;

    public void LoadModel()
    {
        UserClass user = new UserClass("rodrigo@holo.cl", "123456");

        StartCoroutine(CallLogin("http://api.datafab.cl/minverso/v1/auth", GetJsonString(user))); //El usuario se crea en esta parte por motivos de prueba

        int index = UnityEngine.Random.Range(0, models.Count);

        ExitGames.Client.Photon.Hashtable _customProperties = PhotonNetwork.LocalPlayer.CustomProperties;
        _customProperties["URL"] = models[index] + "+" + (string)_customProperties["Nickname"];
        PhotonNetwork.LocalPlayer.SetCustomProperties(_customProperties);
    }

    //esto deberia ser una clase aparte que guarde la informacion del jugador al hacer login

    public string GetJsonString(object a)
    {
        return JsonConvert.SerializeObject(a);
    }

    public IEnumerator CallLogin(string url, string logindataJsonString)
    {
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(logindataJsonString);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();        
        if (request.error != null)
        {
            Debug.Log("Erro: " + request.error);
        }
        else
        {
            Debug.Log("Status Code: " + request.responseCode);           
            byte[] downloadedBytes = request.downloadHandler.data;
            Debug.Log(downloadedBytes.Length);
            Debug.Log("down: "+Encoding.UTF8.GetString(request.downloadHandler.data));
        }

    }

}
