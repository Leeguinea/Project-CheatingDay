using System.Collections;
using TMPro.EditorUtilities;
using UnityEngine;

/*
 * [용도]
 * 키보드로 게임 내 플레이어 캐릭터의 행동(이동, 충돌 처리 등)을 제어
 * [역할]
 * 1. InputManager에서 전달받은 입력 신호를 바탕으로 캐릭터를 "실제 이동"시킴.
 * 2. 음식과의 충돌을 판정함.
 * 3. 캐릭터의 상태(이동, 데미지 등)를 시각적으로 표현.
 */

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 10.0f;

    CharacterController _controller;

    bool _isLogSent = false;

    void Start()
    {
        // 프레임을 60으로 고정합니다. (에디터 UI 먹통 방지)
        Application.targetFrameRate = 60;

        _controller = GetComponent<CharacterController>();

        //중복 등록 방지
        Managers.Input.KeyAction -= OnKeyboard;
        Managers.Input.KeyAction += OnKeyboard;

        StartCoroutine(CoCheckFoods());
    }

    void Update()
    {
    }

    IEnumerator CoCheckFoods()
    {
        while (true)
        {
            CheckFallinFoods();
            yield return new WaitForSeconds(0.1f);
        }
    }

    //InputManager가 매프레임마다 함수를 대신 호출해줌.
    void OnKeyboard()
    {
        //초기엔 (0, 0, 0)
        Vector3 dir = Vector3.zero;

        if(Input.GetKey(KeyCode.W)) dir += Vector3.forward;
        if(Input.GetKey(KeyCode.S)) dir += Vector3.back;
        if(Input.GetKey(KeyCode.A)) dir += Vector3.left;
        if(Input.GetKey(KeyCode.D)) dir += Vector3.right;

        Vector3 gravity = new Vector3(0, -9.81f, 0);
        Vector3 moveDir = Vector3.zero;


        //키를 누를 때
        if(dir.magnitude > 0.0001f)
        {
            dir = dir.normalized; // 대각선 속도 보정
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 0.2f); // 회전

            moveDir = dir * _speed; //이동 방향 계산 
        }

        _controller.Move((moveDir + gravity) * Time.deltaTime); //늘 중력을 받게 함.

    }

    //[캐릭터 콜라이더]
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.name == "Food")
        {
            Debug.Log($"충돌한 대상은 {hit.gameObject.name}이다.");
        }        
    }

    //[트리거-아이템]
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Food")
        {
            Debug.Log($"{other.gameObject.name} 음식을 먹었다.");
            Destroy(other.gameObject);
        }
    }

    void CheckFallinFoods()
    {
        // 1. 레이저 발사 위치와 방향 설정
        Vector3 rayStart = transform.position + Vector3.up * 1.5f;
        Vector3 look = Vector3.up;
        int mask = 1 << 8; // Food 레이어

        
        RaycastHit hit;
        if (Physics.Raycast(rayStart, look, out hit, 10f, mask))
        {
            // 3. 중복 로그 방지 장치
            if (!_isLogSent)
            {
                if (hit.collider.CompareTag("BadFood"))
                {
                    Debug.Log("Detected: BadFood");
                }
                else if (hit.collider.CompareTag("GoodFood"))
                {
                    Debug.Log("Detected: GoodFood");
                }

                _isLogSent = true;
            }
        }
        else
        {
            // 4. 감지된 게 없으면 로그를 다시 보낼 수 있게 리셋
            _isLogSent = false;
        }
    }



}
