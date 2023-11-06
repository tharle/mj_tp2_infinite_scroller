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
        float AxisHorizontal = Input.GetAxis(GameParameters.InputNames.AXIS_HORIZONTAL);
        Vector3 displacement  = new Vector3(AxisHorizontal * m_Speed * Time.deltaTime, 0, 0);
        transform.Translate(displacement);
        //Vector3 velocity = m_Body.velocity;
        //velocity.x = AxisHorizontal * m_Speed;
        //m_Body.velocity = velocity; 
    }
}
