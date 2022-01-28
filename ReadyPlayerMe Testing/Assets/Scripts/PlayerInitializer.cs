using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using Cinemachine;
public class PlayerInitializer : MonoBehaviourPunCallbacks, IPunObservable
{
    public float playerHeight;
    public Vector3 center;
    public float radius;
    public Animator anim;
    public bool hasSyncAnims = false;
    public bool hasModel = false;
    // Start is called before the first frame update

    int _animIDSpeed = Animator.StringToHash("Speed");
	int _animIDGrounded = Animator.StringToHash("Grounded");
	int _animIDJump = Animator.StringToHash("Jump");
	int _animIDFreeFall = Animator.StringToHash("FreeFall");
	int _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");

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
    }

    private void Update()
    {       
        if (!hasSyncAnims && hasModel)
        {
            anim = GetComponentInChildren<Animator>();
            hasSyncAnims = true;
        }
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (hasModel)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(anim.GetFloat(_animIDSpeed));
                stream.SendNext(anim.GetBool(_animIDGrounded));
                stream.SendNext(anim.GetBool(_animIDJump));
                stream.SendNext(anim.GetBool(_animIDFreeFall));
                stream.SendNext(anim.GetFloat(_animIDMotionSpeed));
                Debug.Log("enviando velocidad " + anim.GetFloat(_animIDSpeed));
            }
            else
            {
                anim.SetFloat(_animIDSpeed, (float)stream.ReceiveNext());
                anim.SetBool(_animIDGrounded, (bool)stream.ReceiveNext());
                anim.SetBool(_animIDJump, (bool)stream.ReceiveNext());
                anim.SetBool(_animIDFreeFall, (bool)stream.ReceiveNext());
                anim.SetFloat(_animIDMotionSpeed, (float)stream.ReceiveNext());
                Debug.Log("recibiendo Speed " + anim.GetFloat(_animIDSpeed));
            }
        }
    }
}