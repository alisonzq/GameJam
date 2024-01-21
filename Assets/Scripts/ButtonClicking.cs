using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Security.Cryptography.X509Certificates;

public class ButtonClicking : MonoBehaviour
{
    public Button yourButton;
    // If the sceneToLoad is overwritten, when the button is pressed it
    // will load the corresponding scene, default of -1 will not load anything
    public int sceneToLoad = -1;

    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Debug.Log("You have clicked the button!");
        if (sceneToLoad != -1) {
        LevelLoader.LoadScene(sceneToLoad);
        }
    }
}
