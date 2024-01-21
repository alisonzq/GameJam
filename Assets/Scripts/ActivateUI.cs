using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateUI : MonoBehaviour
{

    public GameObject canvas;

    public void ActivateUIOnClick()
    {
        if (canvas != null)
        {
            canvas.SetActive(true);
        }
    }

    public void DeactivateUIOnClick()
    {
        if (canvas != null)
        {
            canvas.SetActive(false);
        }
    }
}
