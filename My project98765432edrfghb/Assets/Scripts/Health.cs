using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private const float MAX_HEALTH = 100f;
    public float health = MAX_HEALTH;
    private Image healthBar;

    void Start()
    {
        healthBar = GetComponent<Image>();
    }
    void Update()
    {
        healthBar.fillAmount = health / MAX_HEALTH;
    }
}
