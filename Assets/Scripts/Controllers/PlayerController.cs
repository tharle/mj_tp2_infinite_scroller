using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float m_Speed = 10;
    [SerializeField] private float m_JumpForce = 10;
    private bool m_alive = true;

    [SerializeField] private BoxCollider2D m_GroundCheck;
    [SerializeField] private Animator m_Animator;
    private Rigidbody2D m_Body;

    GameController m_Controller;
    bool m_IsGround = true;
    bool m_IsDoubleJump = true;



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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(GameParameters.TagName.GROUND))
        {
            m_IsGround = true;
            m_Animator.SetBool(GameParameters.AnimationPlayer.BOOL_DOUBLE_JUMP, false);
        }
    }

    private void Jump()
    {
        if (!m_IsGround && !m_IsDoubleJump) return;

        if (Input.GetKeyDown(GameParameters.InputName.KEY_JUMP))
        {

            ResetVelocityJump();
            m_Body.AddForce(m_JumpForce * Vector3.up, ForceMode2D.Impulse);

            if(m_IsGround)
            {
                m_IsDoubleJump = true;
                m_IsGround = false;
            }
            else if(m_IsDoubleJump)
            {
                m_IsDoubleJump = false;
                m_Animator.SetBool(GameParameters.AnimationPlayer.BOOL_DOUBLE_JUMP, true);
            }


        }

        m_Animator.SetFloat(GameParameters.AnimationPlayer.FLOAT_VELOCITY_Y, m_Body.velocity.y);
    }

    private void ResetVelocityJump()
    {
        Vector3 velocity = m_Body.velocity;
        velocity.y = 0;
        m_Body.velocity = velocity;
    }

    private void Move()
    {

        float speed = m_Speed * m_Controller.GetLevel() / 2;
        Vector3 displacement  = Vector3.right * speed * Time.deltaTime ;
        transform.Translate(displacement);
    }

    public void LossLife()
    {
        m_Animator.SetTrigger(GameParameters.AnimationPlayer.TRIGGER_DIE);
        // TODO Jouer music game over
        m_alive = false;
        StartCoroutine(DoDie());
    }

    IEnumerator DoDie()
    {
        yield return new WaitForSeconds(1f);
        m_Controller.LossLife();
    }
}
