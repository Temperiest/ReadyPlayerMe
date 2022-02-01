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
    private Dictionary<string, RoomInfo> cachedRoomList = new Dictionary<string, RoomInfo>();

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
        if(PhotonNetwork.CurrentRoom.GetPlayer(PhotonNetwork.CurrentRoom.masterClientId).UserId == PhotonNetwork.LocalPlayer.UserId)
        {
            PhotonNetwork.CurrentRoom.MaxPlayers = 1;//(byte)DataHolder.serverData.Resp.GetRoom(PhotonNetwork.CurrentRoom.Name).max_user;
            Debug.Log("El numero maximo de usuarios es: " + PhotonNetwork.CurrentRoom.MaxPlayers);
        }
        PhotonNetwork.LoadLevel("Game");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        if (returnCode == ErrorCode.GameFull)
        {
            Debug.Log("Rooms is full, try later");
        }
        else
        {
            Debug.Log("Unexpected error joined room, try again");
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        UpdateCachedRoomList(roomList);
    }

    private void UpdateCachedRoomList(List<RoomInfo> roomList)
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            RoomInfo info = roomList[i];
            if (info.RemovedFromList)
            {
                cachedRoomList.Remove(info.Name);
            }
            else
            {
                cachedRoomList[info.Name] = info;
            }
        }
        UpdateButtonCacheList();
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
        UpdateButtonCacheList();

    }
    public void DeleteButtons()
    {
        foreach(Transform child in ParentCanvas.transform)
        {
            if (child.tag != "indelible")
                Destroy(child.gameObject);
        }
    }

    public void UpdateButtonCacheList()
    {
        foreach(Transform t in ParentCanvas.transform)
        {
            if(t.gameObject.GetComponent<Button>() != null && !t.CompareTag("indelible"))
            {
                Debug.Log("entre al primer if");
                if (cachedRoomList.ContainsKey(t.GetComponentInChildren<Text>().text))
                {
                    Debug.Log("Entre al segundo if");
                    t.GetComponent<Button>().interactable = cachedRoomList[t.GetComponentInChildren<Text>().text].PlayerCount < cachedRoomList[t.GetComponentInChildren<Text>().text].MaxPlayers;
                    Debug.Log("Este es mi valor: " + t.GetComponent<Button>().interactable);
                }
            }
        }
    }
}