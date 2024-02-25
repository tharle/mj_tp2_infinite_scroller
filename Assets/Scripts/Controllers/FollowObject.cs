using UnityEngine;

public class FollowObject : MonoBehaviour
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

        Follow();
    }

    private void Follow()
    {
        Vector3 positionToFollow = m_ToFollow.transform.position + m_Offset;
        positionToFollow.y = transform.position.y; // block translate in y
        positionToFollow.z = transform.position.z; // block translate in z
        transform.position = positionToFollow;
    }
}
