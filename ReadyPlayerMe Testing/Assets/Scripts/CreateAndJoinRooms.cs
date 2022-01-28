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

    public void CreateOrJoinRoom(string name)
    {
      //  Debug.Log("Esta es el nombre que se recibe: " + name);
       // Debug.Log("Este es el largo de los cuartos: " + DataHolder.serverData.Resp.rooms.Length);

        _customProperties["Nickname"] = DataHolder.serverData.Resp.id_user;
        PhotonNetwork.LocalPlayer.CustomProperties = _customProperties;
        PhotonNetwork.LocalPlayer.NickName = DataHolder.serverData.Resp.id_user;
        PhotonNetwork.JoinOrCreateRoom(name, new RoomOptions(), TypedLobby.Default);//Se debe cambiar por la informacion del boton
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
        for (int i = 0; i < DataHolder.serverData.Resp.rooms.Length; i++)
        {
            //Debug.Log("Este es el largo de DataHolder: " + DataHolder.serverData.Resp.rooms.Length);
            GameObject newButton = Instantiate(button);
            newButton.transform.SetParent(ParentCanvas.transform, false);
            Debug.Log("Este es el nombre del cuarto" + DataHolder.serverData.Resp.rooms[i].nombre_room);
            newButton.GetComponent<Button>().onClick.AddListener(()=> CreateOrJoinRoom(DataHolder.serverData.Resp.rooms[i].nombre_room));

            GameObject newText = Instantiate(text);
            newText.transform.SetParent(newButton.transform, false);
            newText.GetComponent<Text>().text = DataHolder.serverData.Resp.rooms[i].nombre_room;
        }
    }
}
