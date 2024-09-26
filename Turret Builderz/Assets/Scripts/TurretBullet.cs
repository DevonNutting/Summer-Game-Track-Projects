/* Mark Gaskins
 * THIS IS A MULTI-INSTANCE SCRIPT!
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{

    private Transform target;


    [Header("Bullet Attributes [EDIT]")]
    [Tooltip("The speed that the bullet travels.")] [SerializeField] [Range(10, 150)] float speed = 70.0f;
    [Tooltip("The damage that the bullet does.")] [SerializeField] [Range(2, 1000)] public int power = 50;

    [Space(10), Header("Technical [DO NOT EDIT]")]
    [Tooltip("The tag of the walls, or any object that the bullet can not go through.")] [SerializeField] string _solidObjectTag = "Can Stop Bullets";
    [Tooltip("The radius of the object collision checking.")] [SerializeField] float checkingRange = 1.0f;
    [Tooltip("The SFX that will play when the bullet hits.")] [SerializeField] GameObject hitSFX; // Yes, Super lazy way of doing it but whatever



    public void Seek(Transform _target)
    {
        target = _target;
    }

    private void Update()
    {
        if (target == null) { Destroy(gameObject); return; } // Destroy bullet if no target

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);

        // Checks to see when it should delete the bullet after being too close to an object (after colliding with a wall)
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, checkingRange);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag(_solidObjectTag))
            {
                // BULLET JUICE ~~
                GameObject FX = Instantiate(hitSFX);
                FX.transform.position = this.transform.position;
                Destroy(FX, .9f);

                Destroy(this.gameObject);
            }
        }

    }

    private void HitTarget()
    {
        // BULLET JUICE ~~
        GameObject FX = Instantiate(hitSFX);
        FX.transform.position = this.transform.position;
        Destroy(FX, .9f);

        Damage(target);
        // Destroy(target.gameObject, 0.3f);
    }

    void Damage(Transform enemy)
    {

        Enemy e = enemy.GetComponent<Enemy>();


        if (e != null) e.currentHealth -= power;
            

        Destroy(gameObject);
    }

}
