using UnityEngine;

public class Aiming : MonoBehaviour
{
    private Rigidbody2D shoulderBody;
    private Transform bulletSpawn;
    public float aimSpeed = 1f;
    public float recoilTime = 0.5f;
    private float lastY;
    public float nextAimTime = 0f;
    public float recoilStrength = 0.5f;
    void Update()
    {
        if (Time.time >= nextAimTime && HoldingFinger)
        {
            shoulderBody.gravityScale = 0f;
            Aim();
        }
        else
        {
            shoulderBody.gravityScale = 1f;
            UpdateLastY();
        }
    }
    void Awake()
    {
        shoulderBody = transform.GetComponent<Rigidbody2D>();
        bulletSpawn = transform.GetChild(0).GetChild(0).GetChild(0);
    }
    private bool HoldingFinger
    {
        get
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                {
                    return true;
                }
            }
            return false;
        }
    }
    private void UpdateLastY()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.position.x < Screen.width / 2)
            {
                lastY = touch.position.y;
            }
        }
    }
    public void Recoil()
    {
        nextAimTime = Time.time + recoilTime;
        Vector2 recoil = new(0, transform.up.y * recoilStrength);
        shoulderBody.AddForceAtPosition(recoil, bulletSpawn.position, ForceMode2D.Impulse);
    }
    private void Aim()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.position.x < Screen.width / 2)
            {
                if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Stationary)
                {
                    lastY = touch.position.y;
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    float deltaY = touch.position.y - lastY;
                    lastY = touch.position.y;

                    float rotationAmount = deltaY * aimSpeed;
                    shoulderBody.MoveRotation(shoulderBody.rotation + rotationAmount);
                }
            }
        }
    }
}
