using UnityEngine;

public class DotController : MonoBehaviour
{
    public int points = 10; // Points awarded for eating the dot

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Pac-Man has collided with the dot
            ScoreManager.instance.AddScore(points); // Add points to the score
            Destroy(gameObject); // Destroy the dot GameObject
        }
    }
}
