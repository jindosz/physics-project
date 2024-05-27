using UnityEngine;

public class FollowPlayerX : MonoBehaviour
{
    public Transform player; // 오브젝트의 Transform을 여기에 할당합니다.
    private float offsetY = 5; // 카메라와 오브젝트 사이의 Y축 거리. 필요에 따라 조정 가능합니다.
    private float offsetZ = -1; // 일반적으로 카메라는 오브젝트보다 뒤에 있어야 하므로 음수값이 적합합니다.

    float difference;

    void Update()
    {
        difference = player.position.x - transform.position.x;

        if (Mathf.Abs(difference) > 50f)
        {
            // 카메라의 위치를 오브젝트의 X축 위치에 맞추고, Y축과 Z축 위치는 고정시킵니다.
            transform.position = new Vector3(player.position.x, offsetY, offsetZ);
            difference = 0f;
        }

    }
}
