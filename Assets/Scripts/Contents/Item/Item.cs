using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType { Target, Avoid }
    public ItemType Type;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            gameObject.SetActive(false);
        }
    }

}
