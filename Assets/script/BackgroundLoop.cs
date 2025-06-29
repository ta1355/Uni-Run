using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{

    private float width; // 배경 가로길이

    void Awake()
    {
        BoxCollider2D backgroundCollider = GetComponent<BoxCollider2D>();

        width = backgroundCollider.size.x;
    }

    void Update()
    {
        if (transform.position.x <= -width)
        {
            Reposition();
        }
    }

    private void Reposition()
    {
        // 현재 위치에서 오른쪽으로 가로 길이 * 2 만큼 이동
        Vector2 offset = new Vector2(width * 2f, 0);
        transform.position = (Vector2)transform.position + offset;
    }
}
