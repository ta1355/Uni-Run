using UnityEngine;

public class ScrollingObject : MonoBehaviour
{

    public float speed = 10f;

  
    void Start()
    {
        
    }


    void Update()
    {
        if (!GameManager.instance.isGameover)
        {
            // 초당 speed의 속도로 왼쪽으로 평행이동
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }
}
