using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float m_Speed = 10;
    [SerializeField] private float m_JumpForce = 10;
    private Rigidbody2D m_Body;

    void Start()
    {
        m_Body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
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
}
