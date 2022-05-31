using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using System;
using UnityEngine.UI;
using System.IO;

[Serializable]
public class AllUsers
{
    public Response[] allUsers;
}

[Serializable]
public class Credentials
{
    public string email;
    public string password;
    public string name;
    public string phone;
    public int score;
    public string played_at;

    public Credentials()
    {
        email = "sada@gmail.com";
        password = "asda";
        name = "asd";
        phone = "asd";
        score = 0;
        played_at = "2022-12-20 12:25:20";
    }

    public void init()
    {
        email = "sada@gmail.com";
        password = "asda";
        name = "asd";
        phone = "asd";
        score = 0;
        played_at = "2022-12-20 12:25:20";
    }

}

[Serializable]
public class Response
{
    public string access_token;
    public string token_type;
    public string expires_at;
    public int id;
    public string name;
    public string phone;
    public int score;
    public string email;
    public int rank;
}

//public static class JsonHelper
//{
//    public static T[] FromJson<T>(string json)
//    {
//        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
//        return wrapper.Items;
//    }

//    public static string ToJson<T>(T[] array)
//    {
//        Wrapper<T> wrapper = new Wrapper<T>();
//        wrapper.Items = array;
//        return JsonUtility.ToJson(wrapper);
//    }

//    public static string ToJson<T>(T[] array, bool prettyPrint)
//    {
//        Wrapper<T> wrapper = new Wrapper<T>();
//        wrapper.Items = array;
//        return JsonUtility.ToJson(wrapper, prettyPrint);
//    }

//    [Serializable]
//    private class Wrapper<T>
//    {
//        public T[] Items;
//    }
//}

public class APIHandler : Singleton<APIHandler>
{
    public Response response;
    public string baseURL;
    public string loginEndPoint;
    public string getEndPoint;
    public string UserEndPoint;
    public Sprite check;
    public AllUsers allUsers;
    public Response[] root;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        sendLogin();
        //getAllUsers();
    }

    public List<Response> getAllUsers()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            SceneHandler.Instance.warningMsg.text = "Unable to access servers! Please check your network connection, and restart your application.";
            SceneHandler.Instance.warningPopup.SetActive(true);
        }
        RestClient.Request(new RequestHelper
        {
            Uri = baseURL + getEndPoint,
            Method = "GET",
            Headers = new Dictionary<string, string> {
                { "Authorization", "Bearer " +response.access_token }
            }
        }).Then(res => {
            root = JsonHelper.ArrayFromJson<Response>(res.Text);
            return root;
        }).Catch(res => {
            Debug.Log("Error: " + res);
        });
        return null;
    }

    public void sendLogin()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            SceneHandler.Instance.warningMsg.text = "Unable to access servers! Please check your network connection, and restart your application.";
            SceneHandler.Instance.warningPopup.SetActive(true);
        }
        Credentials credentials = new Credentials();
        credentials.email = "unity@rayqube.com";
        credentials.password = "unity@123";
        string body = JsonUtility.ToJson(credentials);
        Debug.Log("Sending API");
        RestClient.Request(new RequestHelper
        {
            Uri = baseURL + loginEndPoint,
            Method = "POST",
            Body = credentials,
            Headers = new Dictionary<string, string> {
                { "Accept", "application/json" }
            },
        }).Then(res => {
            Debug.Log(res.Text);
            response = JsonUtility.FromJson<Response>(res.Text);
        }).Catch(err => {
            Debug.Log(err.Message);
        });
    }

    public void sendUserStats(Credentials playerData) {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            SceneHandler.Instance.warningMsg.text = "Unable to access servers! Please check your network connection, and restart your application.";
            SceneHandler.Instance.warningPopup.SetActive(true);
        }
        Debug.Log("Sinding user stats");
        RestClient.Request(new RequestHelper
        {
            Uri = baseURL + UserEndPoint,
            Method = "POST",
            Body = playerData,
            Headers = new Dictionary<string, string> {
                { "Authorization", "Bearer " +response.access_token }
            }
        }).Then(res => {
            Debug.Log(res.Text);
            //root = JsonHelper.ArrayFromJson<Response>(res.Text);
            //return root;
        }).Catch(res => {
            Debug.Log("Error: " + res);
        });
    }
}
