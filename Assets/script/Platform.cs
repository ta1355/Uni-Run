using UnityEngine;

public class Platform : MonoBehaviour
{

    public GameObject[] obstacles; // 장애물 오브젝트들

    public bool stepped = false;

    private void OnEnable()   // onEnable은 컴포넌트가 활성화될 때 마다 매번 다시 실행
    {
        stepped = false;
        
        int safeIndex = Random.Range(0, obstacles.Length); // 장애물 중 하나를 안전한 위치로 지정

         // 장애물 수만큼
        for (int i = 0; i < obstacles.Length; i++)
        {
            if (i == safeIndex)
            {
                // 선택된 안전 인덱스는 반드시 꺼둠
                obstacles[i].SetActive(false);
            }
            else
            {
                // 나머지 위치는 30% 확률로 장애물 생성
                obstacles[i].SetActive(Random.value < 0.3f);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌대상이 player고 이전에 player가 밟지 않았다면
        if (collision.collider.tag == "Player" && !stepped)
        {
            stepped = true;

            GameManager.instance.AddScore(1);
        }
    }
}
