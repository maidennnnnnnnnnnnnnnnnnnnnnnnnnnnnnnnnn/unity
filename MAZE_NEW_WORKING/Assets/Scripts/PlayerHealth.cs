using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public TextMeshProUGUI Health;
    public GameObject deathScreen;
    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void FixedUpdate()
    {
        if (Health != null)
        {
            Health.text = "Health: " + currentHealth.ToString();
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
        //Debug.Log(currentHealth);
    }

    private void DecreaseHealthOverTime()
    {
        currentHealth -= 4;
        Debug.Log("Player health: " + currentHealth);
    }
    IEnumerator Waiter()
    {
        yield return new WaitForSecondsRealtime(4);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        yield return null;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            InvokeRepeating("DecreaseHealthOverTime", 0f, 0.05f);
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
