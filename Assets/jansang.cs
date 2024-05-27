using System.Collections.Generic;
using UnityEngine;

public class PersistentAfterimage : MonoBehaviour
{
    public GameObject afterimagePrefab; // 잔상 프리팹
    private List<GameObject> afterimages = new List<GameObject>(); // 생성된 잔상들을 저장할 리스트

    public float afterimageDelay = 0.1f; // 잔상 생성 간격
    public int maxAfterimages = 500;
    private float timer; // 타이머

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= afterimageDelay)
        {
            CreateAfterimage();
            timer = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            ClearAfterimages();
        
        if (afterimages.Count > maxAfterimages)
        {
            RemoveOldestAfterimage();
        }
    }

    void CreateAfterimage()
    {
        GameObject afterimage = Instantiate(afterimagePrefab, transform.position, transform.rotation);
        SpriteRenderer originalSprite = GetComponent<SpriteRenderer>();
        SpriteRenderer afterimageSprite = afterimage.GetComponent<SpriteRenderer>();

        afterimageSprite.sprite = originalSprite.sprite;
        afterimageSprite.color = new Color(1f, 1f, 1f, 0.5f); // 잔상의 초기 투명도 설정

        afterimages.Add(afterimage); // 생성된 잔상을 리스트에 추가
    }

    void RemoveOldestAfterimage()
    {
        if (afterimages.Count == 0) return;

        // 가장 오래된 잔상 제거
        Destroy(afterimages[0]);
        afterimages.RemoveAt(0);
    }

    public void ClearAfterimages()
    {
        foreach (GameObject afterimage in afterimages)
        {
            Destroy(afterimage); // 모든 잔상 제거
        }
        afterimages.Clear(); // 리스트 비우기
    }
}
