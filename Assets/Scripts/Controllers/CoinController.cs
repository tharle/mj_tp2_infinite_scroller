using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    [SerializeField] int m_Value = 15;
    GameController m_Controller;
    
    private void Start()
    {
        m_Controller = FindAnyObjectByType<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(GameParameters.TagName.PLAYER))
        {
            m_Controller.AddScorePoints(m_Value, true);
            
            Destroy(gameObject);
        }
    }
}
