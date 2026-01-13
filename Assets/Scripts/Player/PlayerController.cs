using TMPro.EditorUtilities;
using UnityEngine;

/*
 * [용도]
 * 키보드로 게임 내 플레이어 캐릭터의 행동(이동, 충돌 처리 등)을 제어
 * [역할]
 * 1. InputManager에서 전달받은 입력 신호를 바탕으로 캐릭터를 실제 이동시킴.
 * 2. 음식과의 충돌을 판정함.
 * 3. 캐릭터의 상태(이동, 데미지 등)를 시각적으로 표현.
 */

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 10.0f;

    void Start()
    {
        //중복 등록 방지
        Managers.Input.KeyAction -= OnKeyboard;
        Managers.Input.KeyAction += OnKeyboard;
    }


    //InputManager가 매프렘마다 함수를 대신 호출해줌.
    void OnKeyboard()
    {
        //초기엔 (0, 0, 0)
        Vector3 dir = Vector3.zero;

        if(Input.GetKey(KeyCode.W)) dir += Vector3.forward;
        if(Input.GetKey(KeyCode.S)) dir += Vector3.back;
        if(Input.GetKey(KeyCode.A)) dir += Vector3.left;
        if(Input.GetKey(KeyCode.D)) dir += Vector3.right;

        if(dir.magnitude > 0.0001f)
        {
            // 대각선 속도 보정
            dir = dir.normalized;

            // 회전
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 0.2f);

            // 이동
            transform.position += dir * Time.deltaTime * _speed;
        }

    }
}
