using UnityEngine;
using UnityEngine.SceneManagement;
public class PacmanController : MonoBehaviour
{

    float turnSmoothVelocity;
    public float moveSpeed = 5f;
    public static PacmanController instance; 
    public Transform startingPosition;
    [SerializeField]
    private Vector2 movementInput;
     public Transform Mazebound;
     private void Awake()
    {
        // Singleton pattern to ensure there's only one instance of PacManController
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

       void Update()
    {
        // Manually set movementInput for testing purposes
        SetManualMovementInput();

        MovePacman();
    }

    void SetManualMovementInput()
    {
        // Set the desired input values manually using arrow keys
        float horizontal = Input.GetKey(KeyCode.RightArrow) ? 1f : Input.GetKey(KeyCode.LeftArrow) ? -1f : 0f;
        float vertical = Input.GetKey(KeyCode.UpArrow) ? 1f : Input.GetKey(KeyCode.DownArrow) ? -1f : 0f;

        movementInput = new Vector2(horizontal, vertical).normalized;
    }

    void MovePacman()
    {
        Vector3 moveDirection = new Vector3(movementInput.x, 0f, movementInput.y).normalized;

        if (moveDirection.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveVector = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            transform.Translate(moveVector * moveSpeed * Time.deltaTime, Space.World);
             Vector3 clampedPosition = new Vector3
        (
            Mathf.Clamp(transform.position.x, Mazebound.position.x - Mazebound.localScale.x / 2f, Mazebound.position.x + Mazebound.localScale.x / 2f),
            transform.position.y,
            Mathf.Clamp(transform.position.z, Mazebound.position.z - Mazebound.localScale.z / 2f, Mazebound.position.z + Mazebound.localScale.z / 2f)
        );
        transform.position=clampedPosition;
        }
    }
    public float turnSmoothTime = 0.1f;
     public void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Dot"))
        {
            // Pac-Man has collided with a dot
            // The DotController script will handle scoring and dot destruction
        }
        if (other.CompareTag("Cherry"))
        {
            CherryController cherryController = other.GetComponent<CherryController>();
            if (cherryController != null)
            {
                cherryController.EatCherry();
                ScoreManager.instance.AddLife(1);
                GhostController01.instance.MakeGhostsVulnerable();
                
            }
        }
        else if (other.CompareTag("Ghost"))
        {
            GhostController01 ghostController = other.GetComponent<GhostController01>();
            if (ghostController != null)
            {
                if (ghostController.IsVulnerable)
                {
                    // Pac-Man eats vulnerable ghost
                    Debug.Log("Pacman eats Vulnerable ghost");
                    GhostController01.instance.Disappear();
                    ScoreManager.instance.AddScore(200);
                }
                else
                {
                    // Pac-Man collides with non-vulnerable ghost
                    Debug.Log("Pacman eats non-vulnerable ghost");
                    ScoreManager.instance.LoseLife();
                }
            }
        }
    }

    
     private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ghost"))
        {
            GhostController01 ghostController = collision.gameObject.GetComponent<GhostController01>();
            if (ghostController != null)
            {
                if (ghostController.IsVulnerable)
                {
                    // Pac-Man eats vulnerable ghost
                    GhostController01.instance.Disappear();
                    ScoreManager.instance.AddScore(200);
                }
            }
        }
    }

     public void ResetPosition()
    {
        // Reset Pac-Man's position to the starting point
        transform.position = startingPosition.position;
    }
       

}



