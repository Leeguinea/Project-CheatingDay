using TMPro.EditorUtilities;
using UnityEngine;

/*
 * [용도]
 * 게임 내 플레이어 캐릭터의 행동(이동, 충돌 처리 등)을 제어
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
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
            transform.position += new Vector3(0.0f, 0.0f, 1.0f) * Time.deltaTime * _speed;
        if (Input.GetKeyDown(KeyCode.S))
            transform.position -= new Vector3(0.0f, 0.0f, 1.0f) * Time.deltaTime * _speed;
        if (Input.GetKeyDown(KeyCode.A))
            transform.position -= new Vector3(1.0f, 0.0f, 0.0f) * Time.deltaTime * _speed;
        if (Input.GetKeyDown(KeyCode.D))
            transform.position += new Vector3(1.0f, 0.0f, 0.0f) * Time.deltaTime * _speed;

    }
}
