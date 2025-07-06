using UnityEngine;

public class Platform : MonoBehaviour
{

    public GameObject[] obstacles; // 장애물 오브젝트들

    public bool stepped = false;

    private void OnEnable()   // onEnable은 컴포넌트가 활성화될 때 마다 매번 다시 실행
    {
        stepped = false;

        // 장애물 수만큼
        for (int i = 0; i < obstacles.Length; i++){

            // 현재 순번의 장애물을 1/3 확률로 생성
            if (Random.Range(0, 3) == 0)
            {
                obstacles[i].SetActive(true);
            }
            else
            {
                obstacles[i].SetActive(false);
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
