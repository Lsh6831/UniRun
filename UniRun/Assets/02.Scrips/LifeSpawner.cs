using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSpawner : MonoBehaviour
{

    public GameObject life;
    public int count = 1;

    public float lifeSMin = 5.0f;
    public float lifeSMax = 10.0f;
    private float lifeBS;

    public float yMin = -3.5f;
    public float yMax = 2.25f;
    private float xPos = 20f;

    private GameObject[] lifePool;
    private int lifeIndex = 0;

    private Vector2 poolPosttion = new Vector2(0, -25);
    private float lastLifeTime;

    // Start is called before the first frame update
    void Start()
    {
        lifePool = new GameObject[count];

        for(int i =0;i<count;i++)
        {
            for (int e = 0; e < count; e++)
            {
                // platformPrefab을 원본으로 새 발판을
                // poolPosition 위치에 복제 생성
                // 생성된 발판을 platforms 배열에 할당
                //platforms[i] = Instantiate(platformPrefab, poolPosttion, Quaternion.identity);
                lifePool[e] = Instantiate(life, poolPosttion, Quaternion.identity);
                //      i=위치                         ,위치        ,백터3 업(라이트) 와 같음
            }
            // 마지막 배치 시점 초기화
            lastLifeTime = 0f;
            // 다음번 배치까지의 시간 간격을 초기화
            lifeBS = 0f;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        // 게임 오버 상태에서는 동작하지 않아야 한다
        if (GameManager.instance.isGameover) return;
        // 마지막 배치 시점에서 timeBetSpawn 이상 시간이 흘렀다면
        if (Time.time >= lastLifeTime + lifeBS)
        {
            // 기록된 마지막 배치 시점을 현재 시점으로 갱신
            lastLifeTime = Time.time;

            // 다음 배치까지의 시간 간격을 timeBetSpawnMin,
            // timeBetSpawnMax 사이에서 랜덤 가져오기
            lifeBS = Random.Range(lifeSMin, lifeSMax);

            // 배치할 위치의 높이를 yMin과 yMax 사시에서 랜덤 가져오기
            float yPos = Random.Range(yMin, yMax);

            // 사용할 현재 순번의 발판 게임 오브젝트를 비활성화하고
            // 바로 즉시 다시 활성화 이 때, 발판의 Platform 컴포넌트의
            // OnEnable 메서드가 실행됨
            lifePool[lifeIndex].SetActive(false);
            lifePool[lifeIndex].SetActive(true);

            // 현재 순번의 발판을 화면 오른쪽에 재배치
            lifePool[lifeIndex].transform.position = new Vector2(xPos, yPos);
            // 순번 넘기기
            lifeIndex++;

            // 마지막 순번에 도달했다면...
            if (lifeIndex >= count)
            {
                lifeIndex = 0;
            }
        }
        }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

        }
    }
}
