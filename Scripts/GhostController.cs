using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using System.Collections;
public class GhostController01 : MonoBehaviour
{
    public bool isVulnerable = false;
    public static GhostController01 instance;
    private Material originalMaterial;
    public Material vulnerableMaterial;
    private float vulnerabilityTimer = 0f;
    public float speed = 3f;
    public float changeDirectionInterval = 2f; 
    private float timer = 0f;
    public Transform Mazebound;
    public void MakeVulnerable(int duration)
    {
        isVulnerable = true;
        vulnerabilityTimer = duration;
        originalMaterial = GetComponent<Renderer>().material;
        GetComponent<Renderer>().material = vulnerableMaterial;

        // Add logic to handle ghost vulnerability
    }

    public bool IsVulnerable
    {
        get { return isVulnerable; }
    }
    public void Disappear()
    {
    // Example: Play a disappearing animation or particle effect
    GetComponent<Animator>().SetTrigger("Disappear");

    // Example: Disable ghost GameObject after a delay for animation or effect
    StartCoroutine(DisableAfterDelay(1.0f));
    }

    IEnumerator DisableAfterDelay(float delay)
    {

        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
    
    private void Update()
    {
          if (isVulnerable)
        {
            vulnerabilityTimer -= Time.deltaTime;

            if (vulnerabilityTimer <= 0)
            {
                isVulnerable = false;
                GetComponent<Renderer>().material = originalMaterial;

                // Add logic for when vulnerability ends
            }
        }
        timer += Time.deltaTime;

        if (timer >= changeDirectionInterval)
        {
            ChangeDirection();
            timer = 0f;
        }

        Move1();
    }
        public void MakeGhostsVulnerable()
    {
        // Find all ghosts in the scene and make them vulnerable
        GhostController01[] ghosts = FindObjectsOfType<GhostController01>();
        foreach (GhostController01 ghost in ghosts)
        {
            ghost.MakeVulnerable(10);
        }
    }

    private void ChangeDirection()
    {
        // Generate a random direction
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;

        // Rotate the ghost to face the random direction
        transform.forward = randomDirection;
    }

    private void Move1()
    {
        // Move the ghost forward in its current direction
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Vector3 clampedPosition = new Vector3
        (
            Mathf.Clamp(transform.position.x, Mazebound.position.x - Mazebound.localScale.x / 2f, Mazebound.position.x + Mazebound.localScale.x / 2f),
            transform.position.y,
            Mathf.Clamp(transform.position.z, Mazebound.position.z - Mazebound.localScale.z / 2f, Mazebound.position.z + Mazebound.localScale.z / 2f)
        );
        transform.position=clampedPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PacmanController pacMan = other.GetComponent<PacmanController>();
            if (pacMan != null)
            {
                // Handle collision with Pac-Man (e.g., reduce life or game over).
                ScoreManager.instance.LoseLife();
            }
        }
    
    }
}





 
