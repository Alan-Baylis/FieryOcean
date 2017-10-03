using UnityEngine;
using System.Collections;
using Forge3D;

public class ThrowSimulation : MonoBehaviour
{
    public Transform Target;
    public float firingAngle = 45.0f;
    public float gravity = 9.8f;

    [HideInInspector]
    public float gravityInGameCoord { get; set; }
    [HideInInspector]
    public float projectile_Velocity_exp2 { get; set; }
    public float bullet_horizont_speed = 1500f;
    public static float maxDistance { get; set; }
    public Transform cannonHead;
    public static float mashtab = 100f;
    public Transform Projectile;
    public F3DTurret turretController;
    public GameObject bulletPrefab;

    public Transform currentCannonGuidance;
    public Transform swivel;

    private float simulate_coefficient;

    //public float GetGravity()
    //{
    //    return gravityInGameCoord;
    //}

    void Awake()
    {
        simulate_coefficient = 100f;
        mashtab = 100f;
        swivel = transform;
        gravityInGameCoord = (gravity / mashtab)* simulate_coefficient;
        PoolManager.WarmPool(bulletPrefab, 3);
        bullet_horizont_speed = 3000f;
        projectile_Velocity_exp2 = Mathf.Pow(bullet_horizont_speed / mashtab, 2);   // speed in game coordinates
        maxDistance = (projectile_Velocity_exp2 * Mathf.Sin(2 * 45 * Mathf.Deg2Rad)) / gravityInGameCoord;
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
        bullet.position = swivel.position + new Vector3(0, 0.0f, 0);

        // Calculate distance to target
        float target_Distance = Vector3.Distance(bullet.position, Target.position);

        // Calculate speed
        //float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        float sin2Alpha = target_Distance * gravityInGameCoord / projectile_Velocity_exp2;
        float angleOfSinInDegrees = Mathf.Asin(sin2Alpha) * Mathf.Rad2Deg;

        // Extract the X  Y componenent of the velocity
        float Vx = Mathf.Sqrt(projectile_Velocity_exp2) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity_exp2) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        Debug.Log("angel: " + firingAngle.ToString() + "Vx: " + Vx.ToString());

        // Calculate flight time.
        float flightDuration = target_Distance/ Vx;
        // Rotate projectile to face the target.
        bullet.rotation = Quaternion.LookRotation(swivel.forward);

        float elapse_time = 0;

        while (true)
        {
            elapse_time += Time.deltaTime;
            bullet.Translate(0, (Vy - (gravityInGameCoord * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);
            //Debug.Log("bullet position :" + Projectile.position.ToString()+ "time: "+ Time.deltaTime);

            if (bullet.position.y <= 0)
                break;

            yield return null;
        }
    }
}
