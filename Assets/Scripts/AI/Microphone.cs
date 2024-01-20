using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Whisper;
using Whisper.Utils;

public class Microphone : MonoBehaviour
{
    public OpenAI openAI;

    public WhisperManager whisper;
    public MicrophoneRecord microphoneRecord;

    public Button button;
    public TextMeshProUGUI buttonText;
    public TextMeshProUGUI tmptext;
    string outputText;

    private void Awake()
    {
        microphoneRecord.OnRecordStop += OnRecordStop;
        button.onClick.AddListener(OnButtonPressed);
    }

    private void OnButtonPressed()
    {
        if (!microphoneRecord.IsRecording)
        {
            microphoneRecord.StartRecord();
            buttonText.text = "Stop";
        }
        else
        {
            microphoneRecord.StopRecord();
            buttonText.text = "Record";
        }
    }


    private async void OnRecordStop(AudioChunk recordedAudio)
    {
        buttonText.text = "Record";

        var res = await whisper.GetTextAsync(recordedAudio.Data, recordedAudio.Frequency, recordedAudio.Channels);
        if (res == null || outputText == "")
            return;

        var text = res.Result;

        outputText = text;
        tmptext.text = text;
        openAI.GenerateResponse(outputText);
    }

}
