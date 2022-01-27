using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Este archivo se utiliza para guardar la informacion del jugador y dar formato a los archijos JSON que entran y salen por el servidor
 */
public class DataHolder : MonoBehaviour
{
    public static UserServerData serverData;
}

public class User
{
    [JsonProperty("user")]
    public UserLoginData Userlogin { get; set; }

    public User(string email, string pass)
    {
        Userlogin = new UserLoginData(email, pass);
    }
}

public partial class UserLoginData
{
    public string email;
    public string passapp;

    public UserLoginData(string mail, string pass)
    {
        email = mail;
        passapp = pass;
    }
}

[System.Serializable]
public class UserServerData
{
    [JsonProperty("resp")]
    public ServerResp Resp;
}

[System.Serializable]
public class ServerResp
{
    public string status;
    public string token;
    public string id_user;
    public string nombre;
    public string apellido1;
    public string apellido2;
    public string email;
    public string url_avatar;
    public string room_Name = "sala 1";
}
