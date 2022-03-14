using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 발판으로서 필요한 동작을 담은 스크립트
public class Platform : MonoBehaviour
{
    // 장애물 오브젝트들을 담는 배열
    public GameObject[] obstacles;
    // 플레이어 캐릭터가 밟았는지
    private bool stepped = false;

    // 새로운 유니티 이벤트 메서드를 확인
    private void OnEnable()
    {
        // Awake()나 Start() 같은 유니티 이벤트 메서드 입니다.
        // Start() 메서드처럼 컴포넌트가 활성화 될떄 자동으로
        // 한 번 실행되는 메서드 입니다. 그런데,
        // 처음 한 번만 실행되는 Start() 메서드와 달리
        // OnEnable() 메서드는 컴포넌트가 활성화될 때 마다
        // 매번 다시 실행되는 메서드라서, 컴포넌트를 끄고 다시
        // 켜는 방식으로 재실행할수 있는 메서드 입니다.

        //발판을 리셋하는 처리

        // 밞힘 상태를 리셋
        stepped = false;

        // 장애물의 수만큼 루프
        for(int i = 0; i < obstacles.Length; i++)
        {
            // 현재 순번의 장애물을 1/3의 확률로 활성화
            if (Random.Range(0, 3) == 0)
            {
                obstacles[i].SetActive(true);
            }
            else
            {
                obstacles[i].SetActive(false);
            }
            //위와 같은 식
            //obstacles[i].SetActive(Random.Range(0, 3) == 0 ? true : false);
        }
        
    }

    // 플레이어 캐릭터가 지신을 밟았을때 점수가 올라간다
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 상대방의 태그가 Player 이고
        // 이전에 플레이어 캐릭터가 밟지 않았다면
        if (collision.collider.tag == "Player" && !stepped)
        {
            //점수를 추가하고 밞힘 상태를 참으로 변경
            stepped = true;

            GameManager.instance.AddScore(1);
        }
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
