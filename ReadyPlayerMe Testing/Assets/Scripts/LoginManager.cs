using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{    
    public Button loginButton;
    public Toggle keepSesisonToggle;

    public InputField emailField;
    public InputField passwordField;

    public CreateAndJoinRooms roomCreator;

    [Header("Canvas")]
    public GameObject loginCanvas;
    public GameObject roomCanvas;

    private void Start()
    {
        if (PlayerPrefs.HasKey("KeepSession"))
        {
            loginCanvas.SetActive(false);
            Login(PlayerPrefs.GetString("Email"), PlayerPrefs.GetString("Password"));
        }
        else
        {
            loginCanvas.SetActive(true);
        }
    }
    public void Login()
    {
        User user = new User(emailField.text, passwordField.text);
        StartCoroutine(CallLogin("http://api.datafab.cl/minverso/v1/auth", JsonConverter.Converter.Serialize(user)));
    }
    public void Login(string email, string pass)
    {
        User user = new User(email, pass);
        StartCoroutine(CallLogin("http://api.datafab.cl/minverso/v1/auth", JsonConverter.Converter.Serialize(user)));
    }

    public IEnumerator CallLogin(string url, string logindataJsonString)
    {
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(logindataJsonString);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.error != null)
        {
            Debug.Log("Erro: " + request.error);
        }
        else
        {
            DataHolder.serverData = JsonConvert.DeserializeObject<UserServerData>(request.downloadHandler.text);
            if(DataHolder.serverData.Resp.status == "OK")
            {
                if(keepSesisonToggle.isOn || PlayerPrefs.HasKey("KeepSession"))
                {
                    PlayerPrefs.SetInt("KeepSession", keepSesisonToggle ? 1 : 0);
                    PlayerPrefs.SetString("Email", emailField.text);
                    PlayerPrefs.SetString("Password", passwordField.text);
                }
                else
                {                         
                    PlayerPrefs.DeleteAll();
                }
                ChangeCanvas(false, true);
                roomCreator.CreateButton();               
            }
            else
            {
                Debug.Log("Incorrect User Data");
            }
        }
    }
    public void CheckButton() => loginButton.interactable = emailField.text.Length > 0 && passwordField.text.Length > 0;
    public void ChangeCanvas(bool login, bool room)
    {
        loginCanvas.SetActive(login);
        roomCanvas.SetActive(room);
    }

    public void ReLogin()
    {
        ChangeCanvas(true, false);
        PlayerPrefs.DeleteAll();
        roomCreator.DeleteButtons();
    }
    
}
