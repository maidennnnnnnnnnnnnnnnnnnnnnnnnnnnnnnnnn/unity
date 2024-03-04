using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public TextMeshProUGUI health;
    public GameObject deathScreen;
    public GameObject deathScreenAudio;
    private void Start()
    {
        currentHealth = maxHealth;

    }

    private void FixedUpdate()
    {
        if (health != null)
        {
            health.text = "Health: " + currentHealth.ToString();
        }
        if (currentHealth <= 0)
        {
            deathScreen.SetActive(true);
            StartCoroutine("Waiter");
        }
        else
        {
            deathScreen.SetActive(false);
        }
    }

    private void DecreaseHealthOverTime()
    {
        currentHealth -= 50;
        Debug.Log("Player health: " + currentHealth);
    }
    IEnumerator Waiter()
    {
        yield return new WaitForSecondsRealtime(6);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        yield return null;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            InvokeRepeating("DecreaseHealthOverTime", 1f, 1f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            CancelInvoke("DecreaseHealthOverTime");
        }
    }
}

