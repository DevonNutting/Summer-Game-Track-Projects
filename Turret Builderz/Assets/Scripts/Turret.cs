using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("General")]
    [Tooltip("The range in which the turret can reach enemies from.")] [SerializeField] [Range(3, 100)] public float range = 15f;
    [Tooltip("The tip of the turret that its bullets will fire from.")] [SerializeField] Transform firePoint;

    [Tooltip("The turn speed of the hinge. Doesn't affect how fast the turret fires.")] [SerializeField] [Range(0, 30)] float turnSpeed = 10f;
    [Tooltip("The tag of the enemy. Ensure that all enemies have this tag name assigned to them.")] [SerializeField] string enemyTag = "Enemy";
    [Tooltip("The part that contains the fire animation.")] [SerializeField] Animation fireAnimation;

    [Header("Bullet Preferences")]

    [Tooltip("The head part of the turret. This will turn to face its target, and should be the parent of the head of the turret.")] [SerializeField] Transform Hinge;
    [Tooltip("The 'TurretBullet' prefab that spawns each time an enemy is in range of the Turret.")] [SerializeField] GameObject BulletPrefab;

    [Tooltip("The speed in which the Turret fires bullets.")] [SerializeField] [Range(0, 15)] float fireRate = 1f;

    [SerializeField] GameObject FxObject;
    [Tooltip("The sound that can be heard when the gun is fired.")] [SerializeField] AudioClip fireSound;
    [Tooltip("The muzzle flash for the gun.")] [SerializeField] GameObject muzzleFlash;

    private float fireCountdown;
    private float mzwaitTime = 1f;
    private float mzTimer = 0.0f;

    public Transform target;
    private Enemy targetEnemy;
    private AudioSource coreFXPlayer;


    private void Start()
    {

        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        coreFXPlayer = FindObjectOfType<GameManager>().CoreFXPlayer;

        mzwaitTime = fireRate;
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject targetedEnemy = null;
        float distanceToPlayer = 0;
        float cacheEnemyHealth = 999999;
        GameObject cacheEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            /// Handle the Turret Prioritization (v1.10.1A) 
        
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                targetedEnemy = enemy;
            }
        }

        
        if (targetedEnemy != null && shortestDistance <= range && targetedEnemy.GetComponent<Enemy>().enabled) // This targets the nearest enemy, but can be modified later to target the enemy with the most power, HP, etc..
        {
            target = targetedEnemy.transform;
            targetEnemy = targetedEnemy.GetComponent<Enemy>();
        }
        else
            target = null;
 
    }

    private void FixedUpdate()
    {
        if (target == null)
        {
            return;
        }
      
        LockOnTarget();


        if (fireCountdown <= 0f)
        {
            Fire();

            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }


    void LockOnTarget() // Lock on the target when it's detected
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(Hinge.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        Hinge.rotation = Quaternion.Euler(0f, rotation.y, 0f);

    }

    private void Fire()
    {
        // Reset the fire animation.
        fireAnimation.Stop();

        GameObject bulletObject = (GameObject)Instantiate(BulletPrefab, firePoint.position, firePoint.rotation);
        TurretBullet bullet = bulletObject.GetComponent<TurretBullet>();

        if (bullet != null) bullet.Seek(target);

        //  muzzleFlash.SetActive(true);
        // mzTimer = 0f;

        // muzzle flash
        GameObject flashObject = Instantiate(muzzleFlash, firePoint.position, firePoint.rotation);

        // Instantiate a sound object in order to give it a custom pitch
        GameObject soundObject = Instantiate(FxObject, this.gameObject.transform);
        AudioSource audioSource = soundObject.GetComponent<AudioSource>();
        // audioSource.pitch = Random.Range(1.1f, 1.3f); // sounds so bad lol
        audioSource.volume = 1;
        audioSource.clip = fireSound;
        audioSource.Play();
        Destroy(soundObject, fireSound.length);
        Destroy(flashObject, fireSound.length);

        // Play the fire animation.
        fireAnimation.Play();
    }

    private void OnDrawGizmosSelected() // Shows the range of the turret's bullets with a red gizmo. (Ensure Gizmos are enabled in the editor)
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
