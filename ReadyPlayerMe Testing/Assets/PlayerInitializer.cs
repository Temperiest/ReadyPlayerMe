using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerInitializer : MonoBehaviour
{
    public float playerHeight;
    public Vector3 center;
    public float radius;
    // Start is called before the first frame update
    void Start()
    {
        if(GetComponent<PhotonView>().IsMine)
        {
            CharacterController controller = gameObject.AddComponent<CharacterController>();
            controller.height = playerHeight;
            controller.center = center;
            controller.radius = radius;
            gameObject.AddComponent<StarterAssets.ThirdPersonController>();
        }
    }
}
