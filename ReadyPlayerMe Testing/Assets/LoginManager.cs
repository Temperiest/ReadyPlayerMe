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
    public InputField emailField;
    public InputField passwordField;
    public GameObject loginCanvas;
    public GameObject roomCanvas;

    public void Login()
    {
        User user = new User(emailField.text, passwordField.text);
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
            Debug.Log("Status Code: " + request.responseCode);
            DataHolder.serverData = JsonConvert.DeserializeObject<UserServerData>(request.downloadHandler.text);
            if(DataHolder.serverData.Resp.status == "OK")
            {

                ChangePanels();
            }
            else
            {
                Debug.Log("Error receiving information from the server, try again later");
            }
        }
    }

    public void CheckButton() => loginButton.interactable = emailField.text.Length > 0 && passwordField.text.Length > 0;
    
    public void ChangePanels()
    {
        loginCanvas.SetActive(false);
        roomCanvas.SetActive(true);
    }

}
