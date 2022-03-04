using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//PlayerConteroller 는 플레이어 캐릭터로서 Player
public class PlayerController : MonoBehaviour
{
    // 플레이어가 사망 시 재생할 오디오 클립
    public AudioClip deathClip;
    // 점프 힘
    public float jumpForce = 700f;

    // 누적 점프 횟수
    private int jumpCount = 0;
    // 플레이어가 바닥에 닿았는지 확인
    private bool isGrounded = false;
    // 플레이어가 죽었나 살았나 = 사망 상태
    private bool isDead = false;
    // 사용할 리지드바디 컴포넌트
    private Rigidbody2D playerRigidbody;
    // 사용할 오디오 소스 컴포넌트
    private AudioSource playerAudio;
    // 사용할 애니메이터 컴포넌트
    private Animator animator;
    void Start()
    {
        // 각 전역변수의 초기화
        // 게임 오브젝트로부터 사용할 컴포넌트들을 가져와 변수에 할당
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAudio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        //                이것만 실행시 플레이어가 리지드바디 2D를 찾아서 가져온다

    }

    // Update is called once per frame
    void Update()
    {
        // 사용자의 입력을 감지하고 점프하는 처리
        //1. 현재 상황에 알맞은 애니메이션을 재생.
        //2. 마우스의 왼쪽 버튼의 클릭을 감지하고 점프.
        //3. 마우스의 왼쪽 버튼을 오래 누르면 높이 점프하도록.
        //4. 최대 점프 횟수에 도달하면 점프를 못하도록 막기.

        //사망 시 더이상 처리를 진행하지 않고 종료하는 처리
        if (isDead) return;

        // 마우스 왼쪽 버튼을 눌렀으면 & 최대점프 횟수 2에 도달하지 않았다면.
        if (Input.GetMouseButtonDown(0) && jumpCount < 2)
        {
            // 점프 횟수를 증가
            jumpCount++;
            // 점프 직전의 속도를 순각적으로 (0,0)으로 변경
            // = 점프 직전까지의 힘 또는 속도가 상쇄되거나 합쳐져서 점프 높이가 비일관적으로 되는 현상을 막기 위해
            playerRigidbody.velocity = Vector2.zero;

            //리지드바디를 이용하여 위쪽으로 힘주기
            playerRigidbody.AddForce(new Vector2(0, jumpForce));

            // 오디오 소스 재생
            playerAudio.Play();
        }
        //velocity는 속도
        else if (Input.GetMouseButtonUp(0) && playerRigidbody.velocity.y > 0)
        {
            // 마우스 왼쪽 버튼에서 손을 떼는 순간과 속도의 y값이 양수라면(위로 상승중 이라면)
            // 이때 현재속도를 절반으로 변경
            playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;
        }

        // 애니메이터의 Grounded 파라미터를 isGrounded 값으로 갱신
        animator.SetBool("Grounded", isGrounded);
        animator.GetBool("Grounded");
    }
            void Die()
        {
            // 사망 처리
            // 애니메이터의 Die 트리거 파라미터를 셋
            animator.SetTrigger("Die");

            //오디오 소스에 할당된 오디오 클립을 deatClib으로 병경
            playerAudio.clip = deathClip;
            //교채 했으니 재생
            playerAudio.Play();

            // 속도를 제로(0,0)로 변경
            playerRigidbody.velocity = Vector2.zero;
            //사망상태를 True로 변경
            isDead = true;

        }
        //oncol 만 입력하면 나오는것중 Enter2D
        private void OnCollisionEnter2D(Collision2D collision)
        {
            //플레이어가 바닥에 닿자마자 감지하는 처리
            //어떤 콜라이더와 닿았으며,충돌포면이 위쪽을 보고있는지 확인
            if(collision.contacts[0].normal.y >0.7f)
            //0.7 하는 이유는 1.0보다 못한 부분(모서리의 라운드 부분 같은곳) 까지 감지 하기 위해
            
            {
            //contacts 는 충돌지점들의 정보를 담는 contactPoint 타입의 데이터를 contacts라는
            //배열 변수에 
            //normal : 충돌 지점에서 충돌 표면의 방향(노말벡터) 를 알려주는 변수 입니다.

            // isGrounded를 true로 변경하고, 누적 점프 횟수를 0으로 리셋
            isGrounded = true;
            jumpCount = 0;
            }
    }
        //oncol 만 입력하면 나오는것중 Exit2D
        private void OnCollisionExit2D(Collision2D collision)
        {
        //바닥에서 벗어나자 마자 처리

        //어떤 콜라이더에서 떼어진 경우 isGrounded를 false 변경
        isGrounded = false; 

        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // 트리거 콜라이더를 가진 장애물과의 충돌을 감지

            // 충돌한 상대방의 태그가 Dead 이면서 아직 사망하지 않았나요?
            if(collision.tag=="Dead" && !isDead)
            {
                Die();
            }
        }
}
    