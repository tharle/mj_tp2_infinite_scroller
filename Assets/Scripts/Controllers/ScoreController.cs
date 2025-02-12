using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private int m_ScoreBase = 50;

    private GameController m_Controller;

    private void Start()
    {
        m_Controller = FindAnyObjectByType<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(GameParameters.TagName.PLAYER)) {
            m_Controller.AddScorePoints(m_ScoreBase, false);
        }
        
    }
}
