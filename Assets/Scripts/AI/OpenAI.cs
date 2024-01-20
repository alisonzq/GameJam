using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class OpenAI : MonoBehaviour
{
    class RequestMicrophoneData
    {
        public string text_message;
        public string vendor;
    }

    class RequestInitializeVendor
    {
        public string vendor;
    }

    class ResponseVendor
    {
        public string text_message;
    }

    public class ErrorResponse
    {
        public string error;
    }

    string Base = "https://jamtest-eb817c59cddf.herokuapp.com";

    public static bool validResponse = false;
    public static string text = "";

    public void Start()
    {
        string url = Base + "/";

        RequestInitializeVendor data = new RequestInitializeVendor();
        data.vendor = "vendor1";

        string jsonData = JsonUtility.ToJson(data);
        byte[] byteData = System.Text.Encoding.UTF8.GetBytes(jsonData);

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(byteData);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        StartCoroutine(receiveStory(request));

    }

    IEnumerator receiveStory(UnityWebRequest request)
    {

        using (request)
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                CatchError(request);
            }
            else
            {
                string response = request.downloadHandler.text;
                ResponseVendor responseVendor = JsonUtility.FromJson<ResponseVendor>(response);

                text = responseVendor.text_message;
                Debug.Log(text);
                validResponse = true;
            }
        }
    }

    public void GenerateResponse(string prompt)
    {
        if (!string.IsNullOrEmpty(prompt))
        {
            SoundAsync(prompt);
        }
    }

    void SoundAsync(string prompt)
    {
        string url = Base + "/receive_input/";
        RequestMicrophoneData data = new RequestMicrophoneData();
        data.text_message = prompt;
        data.vendor = "vendor1";

        string jsonData = JsonUtility.ToJson(data);
        byte[] byteData = System.Text.Encoding.UTF8.GetBytes(jsonData);

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(byteData);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        StartCoroutine(receiveResponse(request));

    }

    IEnumerator receiveResponse(UnityWebRequest request)
    {
        using (request)
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                CatchError(request);
                validResponse = false;
                
            }
            else
            {
                string response = request.downloadHandler.text;
                ResponseVendor responseData = JsonUtility.FromJson<ResponseVendor>(response);

                text = responseData.text_message;
                Debug.Log(text);
                validResponse = true;
            }
        }
    }

    void CatchError(UnityWebRequest request)
    {
        if (request.responseCode == 400)
        {
            string response = request.downloadHandler.text;
            ErrorResponse errorResponse = JsonUtility.FromJson<ErrorResponse>(response);
            Debug.LogError("Server Error: " + errorResponse.error);
        }
        else
        {
            Debug.Log("Error: " + request.error);
        }
    }



}
