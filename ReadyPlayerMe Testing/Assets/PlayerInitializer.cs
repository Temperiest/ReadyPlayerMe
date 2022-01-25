using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerInitializer : MonoBehaviour
{
    public float playerHeight;
    public Vector3 center;
    public float radius;

    private bool hasSyncAnims;
    // Start is called before the first frame update
    void Start()
    {
        if(GetComponent<PhotonView>().IsMine)
        {
            CharacterController controller = gameObject.AddComponent<CharacterController>();
            controller.height = playerHeight;
            controller.center = center;
            controller.radius = radius;
            var TPC = gameObject.AddComponent<StarterAssets.ThirdPersonController>();
            TPC.GroundLayers |= (1 << LayerMask.NameToLayer("Default"));
        }

        var cinemachineVirtualCameraObject = GameObject.Find("PlayerFollowCamera");
        cinemachineVirtualCameraObject.GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = GameObject.Find("CameraRoot").transform;

    }

    private void Update()
    {
        if (!hasSyncAnims)
        {
            if (GetComponentInChildren<PhotonAnimatorView>() != null && GetComponentInChildren<PhotonAnimatorView>().GetSynchronizedParameters().Count != 0)
            {
                foreach (var p in GetComponentInChildren<PhotonAnimatorView>().GetSynchronizedParameters())
                {
                    p.SynchronizeType = PhotonAnimatorView.SynchronizeType.Discrete;
                }
                hasSyncAnims = true;
            }
        }
    }
}
