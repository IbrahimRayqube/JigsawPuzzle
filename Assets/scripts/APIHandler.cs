using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using System;
using UnityEngine.UI;

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

public class APIHandler : Singleton<APIHandler>
{
    public Response response;
    public string baseURL;
    public string loginEndPoint;
    public string getEndPoint;
    public string UserEndPoint;
    public Sprite check;
    public AllUsers allUsers;
    public Response[] temp;
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
    }

    public List<Response> getAllUsers()
    {
        List<Response> allUsers;

        return allUsers;
    }

    public void sendLogin()
    {
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
            Credentials credentials = new Credentials();
            credentials.email = "unity@rayqube.com";
            credentials.password = "unity@123";
            string body = JsonUtility.ToJson(credentials);
            Debug.Log("Sending API");
            RestClient.Request(new RequestHelper
            {
                Uri = baseURL + getEndPoint,
                Method = "GET",
                Body = credentials,
                Headers = new Dictionary<string, string> {
                    { "Accept", "application/json" },
                { "Authorization", "Bearer " +response.access_token }
            },
            }).Then(res => {
                Debug.Log(res.Text);
                //response = JsonUtility.FromJson<Response>(res.Text);
                allUsers = JsonUtility.FromJson<AllUsers>(res.Text);
            }).Catch(err => {
                Debug.Log(err.Message);
            });
        }).Catch(err => {
            Debug.Log(err.Message);
        });
    }
}
