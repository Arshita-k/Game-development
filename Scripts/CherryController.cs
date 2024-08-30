using UnityEngine;

public class CherryController : MonoBehaviour
{
    public int ghostVulnerabilityDuration = 10;
    public int points = 200; 
    public void EatCherry()
    {
        // Make all ghosts vulnerable
        GhostController01[] ghosts = FindObjectsOfType<GhostController01>();
        foreach (GhostController01 ghost in ghosts)
        {
            ghost.MakeVulnerable(ghostVulnerabilityDuration);
            
        }
        ScoreManager.instance.AddScore(points); 
        // Disappear the cherry
        gameObject.SetActive(false); // or Destroy(gameObject);
    }
}
