using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_TxtMPTime;
    [SerializeField] private TextMeshProUGUI m_TxtMPScore;

    public void UpdateTimmer(int time)
    {
        m_TxtMPTime.text = time.ToString();
    }
}
