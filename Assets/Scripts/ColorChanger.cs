using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColorChanger : MonoBehaviour
{
    public TMP_Text m_TextComponent;
    void Start()
    {

        m_TextComponent.color = new Color32(15, 98, 230, 255);
    }

    void OnMouseEnter()
    {
        m_TextComponent.color = new Color32(222, 41, 22, 255);
    }

    void OnMouseExit()
    {
        m_TextComponent.color = new Color32(15, 98, 230, 255);
    }

}
