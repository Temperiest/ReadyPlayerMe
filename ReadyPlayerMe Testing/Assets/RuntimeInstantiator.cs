using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wolf3D.ReadyPlayerMe.AvatarSDK;

public class RuntimeInstantiator : MonoBehaviour
{
    public List<string> models;

    public void LoadModel()
    {
        int index = Random.Range(0, models.Count);

        AvatarLoader loader = new AvatarLoader();
        loader.LoadAvatar(models[index]);
    }
}
