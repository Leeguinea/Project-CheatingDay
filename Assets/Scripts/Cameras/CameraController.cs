using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    GameObject _player;

    [SerializeField]
    Vector3 _delta = new Vector3 (0.0f, 6.0f, -0.5f);

    void LateUpdate()
    {
        if (_player == null)
            return;

        //카메라 위치
        transform.position = _player.transform.position + _delta;
        //카메라 방향 (우선 플레이어 정면으로 임시 설정) -> 나중에는 키보드 입력에 따라 바뀔지도?
        transform.LookAt(_player.transform);
    }

}
