using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using System;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    ExitGames.Client.Photon.Hashtable _customProperties = new ExitGames.Client.Photon.Hashtable();

    public GameObject button;
    public GameObject text;
    public Canvas ParentCanvas;

    public void CreateOrJoinRoom(string room_name)
    {
        _customProperties["Nickname"] = DataHolder.serverData.Resp.id_user;
        PhotonNetwork.LocalPlayer.CustomProperties = _customProperties;
        PhotonNetwork.LocalPlayer.NickName = DataHolder.serverData.Resp.id_user;
        PhotonNetwork.JoinOrCreateRoom(room_name, new RoomOptions(), TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        PhotonNetwork.LoadLevel("Game");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        Debug.Log("Unexpected error joined room, try again");
    }

    public void CreateButton()
    {
        Debug.Log(DataHolder.serverData.Resp.rooms.Length);

        foreach(var room in DataHolder.serverData.Resp.rooms)
        {
            GameObject newButton = Instantiate(button);
            newButton.transform.SetParent(ParentCanvas.transform, false);

            newButton.GetComponent<Button>().onClick.AddListener(() => CreateOrJoinRoom(room.nombre_room));
            newButton.name = room.nombre_room + "Button";

            GameObject newText = Instantiate(text);
            newText.transform.SetParent(newButton.transform, false);
            newText.GetComponent<Text>().text = room.nombre_room;
        }

    }
}
