using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//왼쪽 끝으로 이동한 배경을 오른쪽 끝으로 재배치 처리
public class BackgroundLoop : MonoBehaviour
{
    // 배경의 가로 길이를 담아둘 변수 선언
    private float width;

    //Unity Event Method -> Awake
    private void Awake()
    {

        // Awak() 메서드는 Start() 매서드 처럼 초기 1회 자동 실행되는 유니티 이벤트 메서드 입니다.
        // 하지만, Start() 메서드 보다 실행시점이 한 프레임 더 빠릅니다.
        // 참조하세요 : Unity MeThod LifeCycle

        // 가로 길이를 측정 
        // 박스 콜라이더 2D 가져오기 -> BoxCollider2D 컴포넌트의 Size필드의 X감ㅅ을 가로 길이로 사용
        BoxCollider2D backgroundCollider = GetComponent<BoxCollider2D>();
        width = backgroundCollider.size.x;

    }
   
    // Update is called once per frame
    void Update()
    {
     // 현재 위치가 원점에서 왼쪽으로 width 이상 이동 했을 때
     // 위치를 재배치 
     if(transform.position.x<= -width)
        {
            Repostion();
        }
        
    }

    void Repostion() //위치를 재 배치하는 메서드
    {
        // 현재 위치에서 오른쪽으로 가로 길이 * 2 만큼 이동
        Vector2 offset = new Vector2(width * 2f, 0);
        transform.position = (Vector2)transform.position + offset;
        // pisition 은 vector3 이므로 "(Vector2)" 로 형변환을 해준다
        //(?)// width : 20.48*2=40.48 
        //(?)// -20.48 + 40.48 = 20.48;

    }    
}
