using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public InputField createInput;
    public InputField joinInput;

    ExitGames.Client.Photon.Hashtable _customProperties = new ExitGames.Client.Photon.Hashtable();

    public void JoinRoom()
    {
        _customProperties["Nickname"] = DataHolder.serverData.Resp.id_user;
        PhotonNetwork.LocalPlayer.CustomProperties = _customProperties;
        PhotonNetwork.LocalPlayer.NickName = DataHolder.serverData.Resp.id_user;
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public void CreateRoom()
    {
        _customProperties["Nickname"] = DataHolder.serverData.Resp.id_user;
        PhotonNetwork.LocalPlayer.CustomProperties = _customProperties;
        PhotonNetwork.LocalPlayer.NickName = DataHolder.serverData.Resp.id_user;
        PhotonNetwork.CreateRoom(createInput.text);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        PhotonNetwork.LoadLevel("Game");
    }
}
