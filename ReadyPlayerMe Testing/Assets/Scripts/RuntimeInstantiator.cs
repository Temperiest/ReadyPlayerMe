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
    public void LoadModel()
    {
        ExitGames.Client.Photon.Hashtable _customProperties = PhotonNetwork.LocalPlayer.CustomProperties;
        _customProperties["URL"] = DataHolder.serverData.Resp.url_avatar + "+" + DataHolder.serverData.Resp.id_user;
        PhotonNetwork.LocalPlayer.SetCustomProperties(_customProperties);
    }
}
