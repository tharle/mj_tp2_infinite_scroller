using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform m_ToFollow;

    Vector3 m_Offset;

    void Start()
    {
        m_Offset = transform.localPosition - m_ToFollow.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = m_ToFollow.transform.position + m_Offset;
    }
}
