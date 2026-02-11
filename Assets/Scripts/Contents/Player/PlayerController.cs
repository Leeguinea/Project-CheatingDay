using System.Collections;
using TMPro;
using UnityEngine;

/*
 * [용도]
 * 키보드로 게임 내 플레이어 캐릭터의 행동(이동, 충돌 처리 등)을 제어
 * [역할]
 * 1. InputManager에서 전달받은 입력 신호를 바탕으로 캐릭터를 "실제 이동"시킴.
 * 2. 음식과의 충돌을 판정함.
 * 3. 점수 관리 및 UI 업데이트 요청.[나중에 분리] 
 * [참조]
 * CharacterController: 플레이어의 물리적 이동을 담당 
 * PenaltySystem: 시간 경과에 따른 점수 감점 로직 담당
 */

public class PlayerController : MonoBehaviour
{
    [SerializeField] 
    float _speed = 10.0f;

    [SerializeField] 
    int _score = 0;

    [SerializeField] 
    float _rotationSpeed = 10.0f;

    [SerializeField]
    TextMeshProUGUI _scoreText;

    Vector3 _gravityVelocity = Vector3.zero; // 누적될 중력 속도

    //참조
    CharacterController _controller;
    PenaltySystem _penalty;

    void Start()
    {
        Application.targetFrameRate = 60;

        //참조
        _controller = GetComponent<CharacterController>(); //키보드 입출력
        _penalty = GetComponent<PenaltySystem>();

        // 중복 등록 방지
        if (Managers.Input != null)
        {
            Managers.Input.KeyAction -= OnKeyboard;
            Managers.Input.KeyAction += OnKeyboard;
        }

        UpdateScoreUI();
    }

    void OnKeyboard()
    {
        Vector3 dir = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) dir += Vector3.forward;
        if (Input.GetKey(KeyCode.S)) dir += Vector3.back;
        if (Input.GetKey(KeyCode.A)) dir += Vector3.left;
        if (Input.GetKey(KeyCode.D)) dir += Vector3.right;

        Vector3 moveDir = Vector3.zero;

        if (dir.magnitude > 0.0001f)
        {
            dir = dir.normalized;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), _rotationSpeed * Time.deltaTime);
            moveDir = dir * _speed;
        }

        // 중력 로직 개선
        if (_controller.isGrounded && _gravityVelocity.y < 0)
        {
            _gravityVelocity.y = -2f;
        }
        else
        {
            _gravityVelocity.y += -9.81f * Time.deltaTime;
        }

        _controller.Move((moveDir + _gravityVelocity) * Time.deltaTime);
    }

    //[나중에 UIMangaer로 분리]
    void UpdateScoreUI()
    {
        if (_scoreText != null)
            _scoreText.text = $"Score: {_score}";
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        HandleCollection(hit.gameObject);
    }

    //[Item 자체 혹은 Collector로 이동]
    public void HandleCollection(GameObject go)
    {
        if (go == null) return;

        if (go.CompareTag("Target") || go.CompareTag("Avoid"))
        {
            //어떤 아이템을 먹으면 패널티 타이머를 리셋시킴
            if (_penalty != null)
            {
                _penalty.ResetPenaltyTimer();
            }

            int scoreGain = go.CompareTag("Target") ? 10 : -5;
            ChangeScore(scoreGain);
            Destroy(go);
        }
    }

    public void ChangeScore(int amount)
    {
        _score += amount;
        UpdateScoreUI ();

        //점수가 마이너스면 게임 오버 처리
        if(_score < 0) 
        {
            GameManager gm = FindFirstObjectByType<GameManager>();
            if (gm != null) 
            {
                gm.EndGame(false);
            }
            else
            {
                Debug.LogError("씬에 GameManager가 없습니다.");
            }
        }
    }
    
}