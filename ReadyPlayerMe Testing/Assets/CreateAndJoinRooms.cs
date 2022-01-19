using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public InputField nameField;
    public InputField createInput;
    public InputField joinInput;
    ExitGames.Client.Photon.Hashtable _customProperties = new ExitGames.Client.Photon.Hashtable();

    public void JoinRoom()
    {
        _customProperties["Nickname"] = nameField.text; //este es el nickname del USUARIO dentro de la sala
        PhotonNetwork.LocalPlayer.CustomProperties = _customProperties;
        PhotonNetwork.LocalPlayer.NickName = nameField.text;
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public void CreateRoom()
    {
        _customProperties["Nickname"] = nameField.text; //este es el nickname del USUARIO dentro de la sala
        PhotonNetwork.LocalPlayer.CustomProperties = _customProperties;
        PhotonNetwork.LocalPlayer.NickName = nameField.text;
        PhotonNetwork.CreateRoom(createInput.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }
}
