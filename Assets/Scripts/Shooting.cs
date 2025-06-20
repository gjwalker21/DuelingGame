using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;
    private Aiming aim;
    public float bulletSpeed = 10f;
    public uint ammoCount = 6;
    public float shootCooldown = 1f;
    private float nextShootTime = 0f;

    private bool TappedFinger
    {
        get
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.position.x > Screen.width / 2)
                {
                    if (
                    touch.phase == TouchPhase.Began)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }

    private bool canShoot
    {
        get
        {
            if (Time.time >= nextShootTime && ammoCount > 0)
            {
                return true;
            }
            return false;
        }
    }


    private void Awake()
    {
        aim = transform.parent.parent.parent.GetComponent<Aiming>();
    }

    void Update()
    {
        if (canShoot && TappedFinger)
        {
            nextShootTime = Time.time + shootCooldown;
            Shoot();
        }
    }

    /// <summary>
    /// Shooting logic
    /// </summary>
    private void Shoot()
    {
        // Instantiate bullet
        GameObject bulletInstance = Instantiate(bullet, transform.position, Quaternion.identity);
        Rigidbody2D newBulletBody = bulletInstance.GetComponent<Rigidbody2D>();

        // Fire bullet
        Vector2 direction = -transform.right.normalized;
        newBulletBody.AddForce(direction * bulletSpeed, ForceMode2D.Impulse);

        // Recoil
        aim.Recoil();

        // Decrement ammo amount
        ammoCount--;
    }
}