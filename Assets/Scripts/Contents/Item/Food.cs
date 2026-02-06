using UnityEngine;

public class Food : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log($"{collision.gameObject.name} ¹Ù´Ú¿¡ ´ê¾Æ {gameObject.name}ÀÌ ÆÄ±«µÇ¾ú´Ù.");
            Destroy(gameObject);
        }
    }
}
