using System.Collections;
using TMPro;
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

    [SerializeField]
    int _score = 0;


    [SerializeField]
    TextMeshProUGUI _scoreText;  //텍스트를 담는 바구니.


    CharacterController _controller;

    void Start()
    {
        // 프레임을 60으로 고정합니다. //나중에 삭제 요청 
        Application.targetFrameRate = 60;

        _controller = GetComponent<CharacterController>();

        //중복 등록 방지
        Managers.Input.KeyAction -= OnKeyboard;
        Managers.Input.KeyAction += OnKeyboard;

        UpdateScoreUI();
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

    //UI에 점수 표시 갱신
    void UpdateScoreUI()
    {
        if (_scoreText != null)
            _scoreText.text = $"Score: {_score}";
    }

    //몸체(즉, 머리 제외) 충돌-> 혹시나 머리 이외의 곳에 물체가 닿일 것을 고려함. 
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        HandleCollection(hit.gameObject);
    }

    public void HandleCollection(GameObject go)
    {
        if (go == null)
            return;
        if(go.CompareTag("Target"))
        {
            _score += 10;
            UpdateScoreUI();
            Destroy(go);
            Debug.Log("Target");
        }
        else if(go.CompareTag("Avoid"))
        {
            _score = Mathf.Max(0, _score - 5);
            UpdateScoreUI();
            Destroy(go);
            Debug.Log("Avoid");
        }
    }



}
