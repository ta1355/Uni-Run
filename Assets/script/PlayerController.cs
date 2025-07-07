using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public AudioClip deathClip;
    public float jumpForce = 550f;

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

        rigid.gravityScale = 3f;
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
        animator.SetTrigger("Die");

        playerAudio.clip = deathClip;

        playerAudio.Play();

        // 사망시 속도를 0으로 변경
        rigid.linearVelocity = Vector2.zero;

        isDead = true;

        GameManager.instance.OnPlayerDead();

    }

    // 트리거 콜라이터를 가진 장애물과의 출돌을 감지
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Dead" && !isDead)
        {
            // 충돌한 상대방의 태그가 Dead이며 아직 사망하지 않는다면 Die함수 실행시키기
            Debug.Log("Dead zone triggered!");
            Die();
        }
    }

    // 바닥에 닿았음을 감지하는 처리
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 어떤 콜라이더와 닿았으며, 충돌 표면이 위쪽을 보고 있으면
        if (collision.contacts[0].normal.y > 0.7f)
        {

            // isGrounded를 true로 변경, 누적 점프 횟수 0으로 초기화
            isGrounded = true;
            jumpCount = 0;
        }
    }

    // 바닥에서 벗어났음을 감지하는 처리
    void OnCollisionExit2D(Collision2D collision)
    {
        // 어떤 콜라이더에서 떼어진 경우 isGrounded를 false로 변경
        isGrounded = false;
    }
}
