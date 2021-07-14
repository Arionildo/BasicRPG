using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Status : MonoBehaviour
{
    public float currentHealth;
    public float currentEnergy;
    public float currentAttack;
    public float currentDefense;
    public StateEnum currentState = StateEnum.IDLE;
    public float maxHealth = 100f;
    public float maxEnergy = 20f;
    public float maxAttack = 1f;
    public float maxDefense = 1f;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentEnergy = maxEnergy;
        currentAttack = maxAttack;
        currentDefense = maxDefense;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDeath();
    }

    private void CheckDeath()
    {
        if (!IsAlive())
        {
            currentState = StateEnum.DEAD;

            if (TagEnum.PLAYER.Equals(gameObject.tag))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            } else
            {
                gameObject.GetComponent<Inventory>()?.SpawnLoots();
                Destroy(gameObject, 1.5f);
            }
        }
    }

    public void AddHealth(float value)
    {
        currentHealth += value;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void AddEnergy(float value)
    {
        currentEnergy += value;
        if (currentEnergy > maxEnergy)
        {
            currentEnergy = maxEnergy;
        }
    }

    public bool IsAlive()
    {
        return currentHealth > 0;
    }

    public void AddAttack(float value)
    {
        currentAttack = maxAttack += value;
    }

    public void AddDefense(float value)
    {
        currentDefense = maxDefense += value;
    }
}
