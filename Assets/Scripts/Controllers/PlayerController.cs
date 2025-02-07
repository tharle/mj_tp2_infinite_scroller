using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private const float SPEED_INCR_PER_LEVEL = 0.1f;
    [SerializeField] private float m_Speed = 0.35f;
    [SerializeField] private float m_JumpForce = 10f;
    private bool m_alive = true;

    [SerializeField] private BoxCollider2D m_GroundCheck;
    [SerializeField] private Animator m_Animator;
    private Rigidbody2D m_Body;

    GameController m_Controller;
    bool m_IsGround = true;
    bool m_IsDoubleJump = true;

    [SerializeField] AudioSource m_AudioDeath;
    [SerializeField] AudioSource m_AudioJump;



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

        if (IsJumpKeyPressed())
        {

            ResetVelocityJump();
            m_Body.AddForce(m_JumpForce * Vector3.up, ForceMode2D.Impulse);
            m_AudioJump.Play(); 

            if (m_IsGround)
            {
                m_IsDoubleJump = true;
                m_IsGround = false;
            }
            else if(m_IsDoubleJump)
            {
                m_IsDoubleJump = false;
#if DEBUG
                m_IsDoubleJump = CheatManager.Instance.InInfinitJump();
#endif

                m_Animator.SetBool(GameParameters.AnimationPlayer.BOOL_DOUBLE_JUMP, true);

            }


        }

        m_Animator.SetFloat(GameParameters.AnimationPlayer.FLOAT_VELOCITY_Y, m_Body.linearVelocity.y);
    }

    private bool IsJumpKeyPressed()
    {
        if(Input.GetKeyDown(GameParameters.InputName.KEY_JUMP) ) return true;

        if(Input.touchCount > 0) return Input.GetTouch(0).phase == TouchPhase.Began;
        
        return false;
    }

    private void ResetVelocityJump()
    {
        Vector3 velocity = m_Body.linearVelocity;
        velocity.y = 0;
        m_Body.linearVelocity = velocity;
    }

    private void Move()
    {

        float speed = m_Speed + (m_Controller.GetLevel() * SPEED_INCR_PER_LEVEL);
        Vector3 displacement  = Vector3.right * speed * Time.deltaTime ;
        transform.Translate(displacement);
    }

    public void LossLife()
    {
#if DEBUG
        if (CheatManager.Instance.InGodMode()) return;
#endif
        m_Animator.SetTrigger(GameParameters.AnimationPlayer.TRIGGER_DIE);
        m_alive = false;
        StartCoroutine(DoDie());
        m_AudioDeath.Play();
    }

    IEnumerator DoDie()
    {
        yield return new WaitForSeconds(1f);
        m_Controller.LossLife();
    }
}
