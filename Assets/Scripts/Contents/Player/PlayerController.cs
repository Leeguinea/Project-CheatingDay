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
    [SerializeField] float _speed = 10.0f;
    [SerializeField] int _score = 0;
    [SerializeField] float _rotationSpeed = 10.0f; 

    Vector3 _gravityVelocity = Vector3.zero; // 누적될 중력 속도
    [SerializeField] TextMeshProUGUI _scoreText;

    CharacterController _controller;

    void Start()
    {
        Application.targetFrameRate = 60;
        _controller = GetComponent<CharacterController>();

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

    void UpdateScoreUI()
    {
        if (_scoreText != null)
            _scoreText.text = $"Score: {_score}";
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        HandleCollection(hit.gameObject);
    }

    public void HandleCollection(GameObject go)
    {
        if (go == null) return;

        if (go.CompareTag("Target"))
        {
            _score += 10;
            UpdateScoreUI();
            Destroy(go);
        }
        else if (go.CompareTag("Avoid"))
        {
            _score -= 5;
            UpdateScoreUI();
            Destroy(go);
        }

        if (_score < 0)
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