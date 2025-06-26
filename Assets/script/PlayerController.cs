using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public AudioClip deathClip;
    public float jumpForce = 700f;

    private int jumpCount = 0;
    private bool isGrounded = false;
    private bool isDead = false;

    private Rigidbody2D rigid;
    private Animator animator;
    private AudioSource playerAudio;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isDead)
        {
            return;
        }

        // 마우스 왼쪽 버튼을 눌렀으며 && 최대 점프 횟수에 도달하지 않았다면(2단 점프 가능함)
        if (Input.GetMouseButtonDown(0) && jumpCount < 2)
        {
            jumpCount++;
            rigid.linearVelocity = Vector2.zero;

            rigid.AddForce(new Vector2(0, jumpForce));

            playerAudio.Play();
        }
        else if (Input.GetMouseButtonUp(0) && rigid.linearVelocity.y > 0)
        {  // 마우스 왼쪽 버튼에서 손을 떼는 순간 && 속도의 y 값이 양수라면(위로 상승중)

            rigid.linearVelocity = rigid.linearVelocity * 0.5f; // 현재 속도 절반으로 
        }

        // Grounded 파라미터를 isGrounded 값으로 갱신
        animator.SetBool("Grounded", isGrounded);


    }

    void Die()
    {

    }

    // 트리거 콜라이터를 가진 장애물과의 출돌을 감지
    void OnTriggerEnter2D(Collider2D collision)
    {

    }

    // 바닥에 닿았음을 감지하는 처리
    void OnCollisionEnter2D(Collision2D collision)
    {

    }

    // 바닥에서 벗어났음을 감지하는 처리
    void OnCollisionExit2D(Collision2D collision)
    {
        
    }
}
