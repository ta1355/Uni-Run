using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{

    public GameObject platformPrefab; // 발판 프리팹

    public int count = 3; // 생성할 발판 수

    public float timeBetSpawnMin = 1.25f; // 다음 배치까지의 시간 간격 최솟값

    public float timeBetSpawnMax = 2.25f; // 다음 배치까지의 시간 간격 최댓값

    private float timeBetSpawn; // 다음 배치까지의 시간 간격

    public float yMin = -3.5f; // 배치할 위치의 최소 y값

    public float yMax = 1.5f; // 배치할 위치의 최대 y값

    private float xPos = 15f; // 배치할 위치의 x값

    private GameObject[] platforms;  // 미리 생성한 발판들
    private int currentIndex = 0; // 현재 배치할 발판의 인덱스

    // 초반에 생성한 발판의 화면 밖에 숨겨둘 위치
    private Vector2 poolPosition = new Vector2(0, -25);
    private float lastSpawnTime; // 마지막 배치 시점

    void Start()
    {
        platforms = new GameObject[count];

        for (int i = 0; i < count; i++)
        {
            // platformPrefab을 원본으로 새 바판을 poolPosition 위치에 복제 생성
            // 생성된 발판을 platform 배열에 할당
            platforms[i] = Instantiate(platformPrefab, poolPosition, Quaternion.identity); // Quaternion.identity는 회전값을 0으로 설정
        }
        // 마지막 배치 시점 초기화
        lastSpawnTime = 0f;
        // 다음번 배치까지의 시간 간격을 0으로 초기화
        timeBetSpawn = 0f;
    }

    void Update()
    {
        if (GameManager.instance.isGameover)
        {
            return;
        }

        // 마지막 배치 시점에서 timeBetSpawn 이상 시간이 흘렀다면
        if (Time.time >= lastSpawnTime + timeBetSpawn)
        {
            lastSpawnTime = Time.time; // 마지막 배치 시점 갱신

            // 다음 배치까지의 시간 간격을 랜덤으로 설정
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);

            // 위치의 높이를 랜덤으로 설정
            float yPos = Random.Range(yMin, yMax);

            // 사용할 현재 순번의 발판 게임 오브젝트를 비활성화하고 즉시 다시 활성화
            // 이떄 발판의 platform 컴포넌트의 onenable 메서드가 실행
            platforms[currentIndex].SetActive(false);
            platforms[currentIndex].SetActive(true);

            // 현재 순번의 발판을 화면 오른쪽에 재배치
            platforms[currentIndex].transform.position = new Vector2(xPos, yPos);

            currentIndex++;

            if (currentIndex >= count)
            {
                currentIndex = 0; // 순번이 발판수 넘어가면 초기화
            }
        }

        
    }
}
