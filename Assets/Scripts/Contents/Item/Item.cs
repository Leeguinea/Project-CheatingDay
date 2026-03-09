using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour
{
    [SerializeField]
    private AudioClip _targetSound;

    [SerializeField]
    private AudioClip _AvoidSound;

    public enum ItemType { Target, Avoid }
    public ItemType type;
    
    private bool _isGrounded = false;


    //땅과 Item 충돌했을 때 비활성화
    private void OnCollisionEnter(Collision collision)
    {
        if(!_isGrounded && collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
            StartCoroutine(DeactivateAfterDelay());
        }
    }

    IEnumerator DeactivateAfterDelay()
    {
        yield return new WaitForSeconds(1.0f);

        gameObject.SetActive(false);
        _isGrounded = false;
    }


    //플레이어가 Item을 먹었을 때 효과음 재생
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("트리거 감지됨: " + other.gameObject.name);
        if (other.CompareTag("Player"))
        {
            AudioClip clipToPlay = (type == ItemType.Target) ? _targetSound : _AvoidSound;
            Debug.Log("플레이어 충돌 감지!");

            if(SoundManager.Instance != null && clipToPlay != null)
            {
                SoundManager.Instance.PlaySFX(clipToPlay);
            }
            _isGrounded = false;
            gameObject.SetActive(false);

            
        }
    }

    private void OnEnable()
    {
        _isGrounded = false;
    }

}
