using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amazon.Polly;
using Amazon.Runtime;
using Amazon;
using Amazon.Polly.Model;
using System.IO;
using System;
using UnityEngine.Networking;
using System.Threading.Tasks;

public class TextToSpeech : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private async void Start()
    {
        var credentials = new BasicAWSCredentials("AKIAZQ3DUQMNH3UPH3LK","d0pv12vsOSGD2t4Z9tzpZk0Ks4KmPVo4mYvw1Nrs");
        var client = new AmazonPollyClient(credentials, RegionEndpoint.USEast1);

        var request = new SynthesizeSpeechRequest(){
            Text = "get get run over, over!",
            Engine = Engine.Standard,
            VoiceId = VoiceId.Joanna,
            OutputFormat = OutputFormat.Mp3
        };

        var response = await client.SynthesizeSpeechAsync(request, destroyCancellationToken);

        WriteIntoFile(response.AudioStream);

        using (var www = UnityWebRequestMultimedia.GetAudioClip($"{Application.persistentDataPath}/audio.mp3", AudioType.MPEG))
        {
            var op = www.SendWebRequest();
            while (!op.isDone) await Task.Yield();
            var clip = DownloadHandlerAudioClip.GetContent(www);
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    private void WriteIntoFile(Stream stream){
        using (var fileStream = new FileStream($"{Application.persistentDataPath}/audio.mp3", FileMode.Create)){
            byte[] buffer = new byte[8*1024];
            int bytesRead;

            while ((bytesRead = stream.Read(buffer, 0, buffer.Length))>0){
                fileStream.Write(buffer, 0, bytesRead);
            }
        }
    }

}
