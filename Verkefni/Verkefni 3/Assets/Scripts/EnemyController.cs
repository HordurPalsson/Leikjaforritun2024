using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public GameManager GameManager;
    public float detectionRange = 10f;
    public Transform player;
    public Player playerScript;
    private NavMeshAgent agent;
    public float health;
    public float pushForce = 10f;
    public float enemyDamage = 10f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        health = 100;
    }

    void Update()
    {
        if (player != null && Vector3.Distance(transform.position, player.position) <= detectionRange)
        {
            agent.SetDestination(player.position);

            // Lætur enemie horfa á leikmanninn
            Vector3 direction = player.position - transform.position;
            direction.y = 0;
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            }
        }

        if (health <= 0)
        {
            GameManager.UpdateScore(10);
            Debug.Log("Enemy killed!");
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRb = collision.collider.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                playerScript.TakeDamage(enemyDamage);
                GameManager.UpdateScore(-10);
            
                Vector3 pushDirection = collision.collider.transform.position - transform.position;
                pushDirection.y = 0f; 
                pushDirection.Normalize();

                // Ýtir leikmanninum
                playerRb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
            }
            
        }

        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(50);
        }
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Enemy took damage!");
        health -= damage;
    }
}
