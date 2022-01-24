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

    //Esta funcion se llama al inicio de la escena
    /*
     * Cuando entro en la escena donde se encuentran todos los demas jugadores genero el avatar de cada uno de ellos
     */
    void Start()
    {
        GameObject obj = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
        obj.name = "Avatar_" + DataHolder.serverData.Resp.id_user;

        foreach (Player p in PhotonNetwork.PlayerListOthers)
        {
            if (p.CustomProperties["URL"] != null)
            {
                AvatarLoader al = new AvatarLoader();
                al.LoadAvatar((string)p.CustomProperties["URL"], AvatarImportedCallback, AvatarLoadedCallback);
            }
        }
    }


    /*
     * Esta funcion se llama cuando un jugador cualquiera entra en la sala,
     * si el jugador que entra soy yo, se instancia un contenedor con los scripts de photon.
     */
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (newPlayer.NickName == PhotonNetwork.LocalPlayer.NickName)
        {
            GameObject obj = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
            obj.name = "Avatar_" + newPlayer.NickName;
        }       
    }

    /*
     * Esta funcion se llama cuando algun jugador cualquiera modifica sus propiedades
     * Al llamarse, genera el avatar correspondiente al jugador que cambio sus propiedades
     * Falta agregar un IF para que solo se instancie el modelo solo si la propiedad modificada es la URL
     */
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        AvatarLoader loader = new AvatarLoader();
        loader.LoadAvatar((string)changedProps["URL"], AvatarImportedCallback, AvatarLoadedCallback);
    }

    /*
     * Actualmente sin uso, esa funcion se llama cuando el modelo termina de ser descargado y generado
     */
    private void AvatarImportedCallback(GameObject avatar)
    {
        Debug.Log("Avatar Imported!: " + avatar.name);
    }

    /*
     * Esta funcion se llama cuando el objeto termina de ser instanciado en la escena
     * Esta funcion se encarga de guardar el avatar generado dentro del contenedor que contiene los componentes de photon
     */
    private void AvatarLoadedCallback(GameObject avatar, AvatarMetaData metaData)
    {
        GameObject obj = GameObject.Find(avatar.name);
        avatar.AddComponent<PhotonAnimatorView>();
        PhotonAnimatorView view = avatar.GetComponent<PhotonAnimatorView>();
        foreach( var p in view.GetSynchronizedParameters())
        {
            p.SynchronizeType = PhotonAnimatorView.SynchronizeType.Discrete;
        }
        avatar.transform.position = obj.transform.position;
        avatar.transform.SetParent(obj.transform);
    }
}
