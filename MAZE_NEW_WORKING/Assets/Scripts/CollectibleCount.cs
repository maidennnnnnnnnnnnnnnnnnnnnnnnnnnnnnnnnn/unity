using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectibleCount : MonoBehaviour
{
    public GameObject exit;
    public TextMeshProUGUI win;
    TMPro.TMP_Text text;
    int count = 0;

    void Awake()
    {
        text = GetComponent<TMPro.TMP_Text>();
        UpdateExitState();
    }

    void OnEnable() => Collectible.OnCollected += OnCollectibleCollected;
    void OnDisable() => Collectible.OnCollected -= OnCollectibleCollected;

    void OnCollectibleCollected()
    {
        count++;
        text.text = count.ToString() + "/8";
        UpdateExitState();
    }

    void UpdateExitState()
    {
        if (count >= 8)
        {
            text.text = "All pages picked up. Escape now!";
            exit.SetActive(true);
            Debug.Log("exit opened");
        }
    }
}

