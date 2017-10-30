using UnityEngine;
using System.Collections;
using Forge3D;

public class ThrowSimulation : MonoBehaviour
{
    public float firingAngle = 45.0f;
    public float gravity = 9f;

    [HideInInspector]
    public float gravityInGameCoord { get; set; }
    [HideInInspector]
    public float projectile_Velocity_exp2 { get; set; }
    public float bullet_horizont_speed = 1500f;
    public F3DTurret turretController;
    public GameObject bulletPrefab;
    public Transform fireAnchor;
    public Transform swivel;

    private float simulate_coefficient;

    //public float GetGravity()
    //{
    //    return gravityInGameCoord;
    //}

    void Awake()
    {
        swivel = transform;
        PoolManager.WarmPool(bulletPrefab, 3);
    }

    void Start()
    {   }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (turretController != null)
            {
                firingAngle = -turretController.curElevationAngle;
                var bullet = PoolManager.SpawnObject(bulletPrefab, swivel.position, Quaternion.identity).GetComponent<Bullet>();
                StartCoroutine(SimulateProjectile(bullet.transform));
            }
        }
    }

    IEnumerator SimulateProjectile(Transform bullet)
    {
        // Short delay added before Projectile is thrown
        //yield return new WaitForSeconds(1.5f);

        // Move projectile to the position of throwing object + add some offset if needed.
        bullet.position = fireAnchor.position + new Vector3(0, 0.0f, 0);

        // Calculate distance to target
        float target_Distance = Vector3.Distance(bullet.position, turretController.GetTarget());

        // Extract the X  Y componenent of the velocity
        float Vx = turretController.GetSpeed() * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = turretController.GetSpeed() * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        //Debug.Log("angel: " + firingAngle.ToString() + "Vx: " + Vx.ToString());

        // Calculate flight time.
        float flightDuration = target_Distance / Vx;
        // Rotate projectile to face the target.
        bullet.rotation = Quaternion.LookRotation(swivel.forward);

        float elapse_time = 0;

        while (true)
        {
            elapse_time += Time.deltaTime;
            bullet.Translate(0, (Vy - (turretController.GetGravity() * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);
            //Debug.Log("bullet position :" + Projectile.position.ToString()+ "time: "+ Time.deltaTime);

            if (bullet.position.y <= 0)
                break;

            yield return null;
        }
    }
}
