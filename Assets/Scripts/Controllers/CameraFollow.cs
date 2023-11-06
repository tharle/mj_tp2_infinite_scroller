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

    void Update()
    {
        Vector3 positionToFollow = m_ToFollow.transform.position + m_Offset;
        positionToFollow.y = transform.position.y; // block translate in y
        positionToFollow.z = transform.position.z; // block translate in z
        transform.position = positionToFollow;
    }
}
