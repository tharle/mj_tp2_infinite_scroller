using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float m_Speed = 10;
    [SerializeField] private float m_JumpForce = 10;
    private bool m_alive = true;

    private Rigidbody2D m_Body;

    GameController m_Controller;


    void Start()
    {
        m_Body = GetComponent<Rigidbody2D>();
        m_Controller = FindAnyObjectByType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_alive)
        {
            Move();
            Jump();
        }
    }

    private void Jump()
    {

        if(Input.GetKeyDown(GameParameters.InputNames.KEY_JUMP))
        {
            m_Body.AddForce(m_JumpForce * Vector3.up, ForceMode2D.Impulse);
        }
    }

    private void Move()
    {
        Vector3 displacement  = Vector3.right * m_Speed * Time.deltaTime;
        transform.Translate(displacement);
    }

    internal void Die()
    {
        // TODO add animation die
        m_Controller.OnGameOver();
    }
}
