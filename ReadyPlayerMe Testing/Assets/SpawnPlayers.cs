using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Wolf3D.ReadyPlayerMe.AvatarSDK;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class SpawnPlayers : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
        obj.name = "Avatar_" + PhotonNetwork.LocalPlayer.NickName;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Player Entered");
        GameObject obj = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
        obj.name = "Avatar_" + newPlayer.NickName;     
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        AvatarLoader loader = new AvatarLoader();
        loader.LoadAvatar((string)changedProps["URL"], AvatarImportedCallback, AvatarLoadedCallback);
    }

    private void AvatarImportedCallback(GameObject avatar)
    {
        Debug.Log("Avatar Imported!: " + avatar.name);
    }

    private void AvatarLoadedCallback(GameObject avatar, AvatarMetaData metaData)
    {
        GameObject obj = GameObject.Find(avatar.name);
        avatar.transform.SetParent(obj.transform);
    }
}
