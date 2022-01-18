using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wolf3D.ReadyPlayerMe.AvatarSDK;
using Photon.Pun;

public class RuntimeInstantiator : MonoBehaviour
{
    public List<string> models;

    public void LoadModel()
    {
        int index = UnityEngine.Random.Range(0, models.Count);

        ExitGames.Client.Photon.Hashtable _customProperties = PhotonNetwork.LocalPlayer.CustomProperties;
        _customProperties["URL"] = models[index] +"+" + (string)_customProperties["Nickname"];
        PhotonNetwork.LocalPlayer.SetCustomProperties(_customProperties);

        //AvatarLoader loader = new AvatarLoader();
        //loader.LoadAvatar((string)_customProperties["URL"], AvatarImportedCallback, AvatarLoadedCallback);
    }

    private void AvatarImportedCallback(GameObject avatar)
    {
        // called after GLB file is downloaded and imported
        Debug.Log("Avatar Imported! :" + avatar.name);
    }

    private void AvatarLoadedCallback(GameObject avatar, AvatarMetaData metaData)
    {
        //ANIMATOR SYNC
        //avatar.AddComponent<PhotonView>();
        Debug.Log("Avatar Loaded!: " + avatar.name);
    }

    public void LoadModel(int index)
    {
        if(index > models.Count)
        {
            Debug.Log("Index Out of Bounds");
            return;
        }

        AvatarLoader loader = new AvatarLoader();
        loader.LoadAvatar(models[index], AvatarImportedCallback, AvatarLoadedCallback);
    }
}
