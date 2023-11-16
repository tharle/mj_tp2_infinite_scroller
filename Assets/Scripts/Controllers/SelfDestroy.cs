using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{

    private PlayerController m_Player;
    private float m_DistanceMax = 8f;
    private float m_ElpseTime = 0;
    private float m_FrequenceCheck = 2.0f;

    private void Start()
    {
        m_Player = FindAnyObjectByType<PlayerController>();
    }

    private void Update()
    {

        DetectAndDestroy();
    }

    private void DetectAndDestroy() 
    {
        m_ElpseTime += Time.deltaTime;
        
        // optimization: ça evite faire le vérification chaque frame. 
        if (m_ElpseTime > m_FrequenceCheck)
        {
            m_ElpseTime = 0;
            Vector3 playerPosition = m_Player.gameObject.transform.position;
            float distance = Vector3.Distance(playerPosition, gameObject.transform.position);

            if (distance > m_DistanceMax)
            {
                Destroy(gameObject);
            }
        }
    }
}
