using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private AudioClip _targetSound;

    [SerializeField]
    private AudioClip _AvoidSound;

    public enum ItemType { Target, Avoid }
    public ItemType type;

    //땅과 Item 충돌했을 때 비활성화
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            gameObject.SetActive(false);
        }
    }

    //플레이어가 Item을 먹었을 때 효과음 재생
    private void OnTriggerEnter(Collider other)
    {
        AudioClip clipToPlay = (type == ItemType.Target) ? _targetSound : _AvoidSound;

        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어 충돌 감지!"); 
            SoundManager.Instance.PlaySFX(clipToPlay);
        }

        gameObject.SetActive(false);
    }
}
