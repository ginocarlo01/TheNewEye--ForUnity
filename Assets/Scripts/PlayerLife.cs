using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private SFX deathSFX;

    public static Action playerDeath;
    public static Action<SFX> playerDeathSFX;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Die()
    {
        playerDeath?.Invoke();
        playerDeathSFX?.Invoke(deathSFX);

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
