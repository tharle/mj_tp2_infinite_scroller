using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextScore : MonoBehaviour
{

    [SerializeField] private float m_Timer = 0.5f;
    private float m_ElpseTime = 0;

    private void Update()
    {
        DestroyInTime();
    }

    private void DestroyInTime() 
    {
        m_ElpseTime += Time.deltaTime;
        
        if (m_ElpseTime > m_Timer) GetComponent<TextMeshPro>().text = "";
    }

    public void playText(string text)
    {
        GetComponent<TextMeshPro>().text = text;
        m_ElpseTime = 0;
    }
}
