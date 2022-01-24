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
    /*
     * Esta funcion añade la propiedad URL al usuario en photon,
     * llamando asi a la funcion onPlayerPropertiesUpdate para que el y los demas jugadores descarguen el modelo
     * Esta funcion se encuentra aqui por motivos de prueba,
     * Esta funcion deberia llamarse cuando el jugador entre en la sala
     */
    public void LoadModel()
    {
        ExitGames.Client.Photon.Hashtable _customProperties = PhotonNetwork.LocalPlayer.CustomProperties;
        _customProperties["URL"] = DataHolder.serverData.Resp.url_avatar + "+" + DataHolder.serverData.Resp.id_user;
        PhotonNetwork.LocalPlayer.SetCustomProperties(_customProperties);
    }
}
