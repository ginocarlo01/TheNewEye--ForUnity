using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private AudioSource deathSFX;

    private void Start()
    {
        // Get the Animator component for later use.
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player collided with traps.
        if (collision.gameObject.CompareTag("Traps"))
        {
            Die();
        }
    }

    private void Die()
    {
        // Play the death sound effect.
        deathSFX.Play();

        // Trigger the death animation.
        animator.SetTrigger("death");

        // Stop player movement.
        PlayerMovement.instance.StopPlayerMovement();
    }

    private void RestartLevel()
    {
        // Save game data using JsonReadWriteSystem.
        JsonReadWriteSystem.INSTANCE.Save();

        // Reload the current scene.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
