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

        foreach(var room in DataHolder.serverData.Resp.rooms)
        {
            GameObject newButton = Instantiate(button);
            newButton.GetComponent<Button>().onClick.AddListener(() => CreateOrJoinRoom(room.nombre_room));
            newButton.name = room.nombre_room + "Button";

            newButton.transform.SetParent(ParentCanvas.transform, false);
            newButton.transform.SetAsFirstSibling();

            newButton.GetComponentInChildren<Text>().text = room.nombre_room;
        }

    }

    public void DeleteButtons()
    {
        foreach(Transform child in ParentCanvas.transform)
        {
            if (child.tag != "indelible")
                Destroy(child.gameObject);
        }
    }
}
