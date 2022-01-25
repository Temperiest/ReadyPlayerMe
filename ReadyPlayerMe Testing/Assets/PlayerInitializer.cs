using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using Cinemachine;
public class PlayerInitializer : MonoBehaviourPunCallbacks
{
    public float playerHeight;
    public Vector3 center;
    public float radius;
    private Animator anim;
    private bool hasSyncAnims;
    // Start is called before the first frame update


    void Start()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
              ExitGames.Client.Photon.Hashtable _customProperties = PhotonNetwork.LocalPlayer.CustomProperties;
            _customProperties["URL"] = DataHolder.serverData.Resp.url_avatar + "+" + DataHolder.serverData.Resp.id_user;
            PhotonNetwork.LocalPlayer.SetCustomProperties(_customProperties);

            CharacterController controller = gameObject.AddComponent<CharacterController>();
            controller.height = playerHeight;
            controller.center = center;
            controller.radius = radius;
            var TPC = gameObject.AddComponent<StarterAssets.ThirdPersonController>();
            TPC.GroundLayers |= (1 << LayerMask.NameToLayer("Default"));
        }

        var cinemachineVirtualCameraObject = GameObject.Find("PlayerFollowCamera");
        cinemachineVirtualCameraObject.GetComponent<CinemachineVirtualCamera>().Follow = GameObject.Find("CameraRoot").transform;
        //var c = cinemachineVirtualCameraObject.GetComponent<CinemachineVirtualCamera>();
        //c.GetCinemachineComponent<Cinemachine3rdPersonFollow>().CameraDistance = 0;
    }

    private void Update()
    {       

        if (!hasSyncAnims)
        {
            var animView = GetComponentInChildren<PhotonAnimatorView>();

            if (animView != null && animView.GetSynchronizedParameters().Count != 0)
            {
                anim = GetComponentInChildren<Animator>();
                GetComponent<PhotonView>().FindObservables(true);
                foreach (var p in animView.GetSynchronizedLayers())
                {
                    animView.SetLayerSynchronized(p.LayerIndex, PhotonAnimatorView.SynchronizeType.Discrete);
                }
                foreach (var p in animView.GetSynchronizedParameters())
                {
                    animView.SetParameterSynchronized(p.Name, p.Type, PhotonAnimatorView.SynchronizeType.Discrete);
                }

                hasSyncAnims = true;
            }
        }
    }
}
