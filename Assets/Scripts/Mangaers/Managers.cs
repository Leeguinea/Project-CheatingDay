using UnityEngine;

/*
 * [용도]
 * 게임 시스템 관리
 * [역할]
 * 1. 싱글톤 패턴 사용 (Managers.Instance로 접근 가능)
 */
public class Managers : MonoBehaviour
{
    static Managers _instance; //유일성
    static Managers Instance {  get { return _instance; } }

    InputManager _input = new InputManager();
    public static InputManager Input { get { return Instance._input; } }

    void Start()
    {
        
    }

    void Update()
    {
        _input.Update();
    }
}
