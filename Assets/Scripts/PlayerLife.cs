using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private AudioSource deathSFX;

    public static Action playerDeath;

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
        playerDeath?.Invoke();

        // Play the death sound effect.
        deathSFX.Play();

        // Trigger the death animation.
        animator.SetTrigger("death");

        
    }

    private void RestartLevel()
    {
        // Save game data using JsonReadWriteSystem.
        JsonReadWriteSystem.INSTANCE.Save();

        // Reload the current scene.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
