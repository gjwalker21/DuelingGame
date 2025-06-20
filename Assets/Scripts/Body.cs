using UnityEngine;
using UnityEngine.InputSystem.Android.LowLevel;

public class Body : MonoBehaviour
{
    private int characterHealth = 100;
    private GameController gameController;
    void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Bullet"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                Collider2D hitPart = contact.collider;
                Debug.Log("Bullet hit part: " + hitPart.name);
            }
        }
    }
}
