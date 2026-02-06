using UnityEngine;

public class PlayerSensor : MonoBehaviour
{
    [SerializeField]
    PlayerController _player;

    //머리에 물체가 닿였는지 감지만 하는 역할.
    private void OnTriggerEnter(Collider other)
    {
        if (_player == null)
        {
            Debug.LogError("PlayerSensor: PlayerController가 연결되지 않았습니다.");
            return;
        }
            
        //머리에 닿이면 PlayerController로 넘겨짐
        if(other.CompareTag("Target") || other.CompareTag("Avoid"))
        {
            _player.HandleCollection(other.gameObject);
        }
    }
}
