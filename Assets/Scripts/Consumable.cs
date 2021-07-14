using UnityEngine;

public class Consumable : MonoBehaviour
{
    public ConsumableTypeEnum type = ConsumableTypeEnum.HEAL;
    public float value;
    public bool despawn;
    public float timeToDespawn;

    // Update is called once per frame
    void Update()
    {
        if (despawn)
        {
            UpdateDespawnTime();
            if (timeToDespawn < 0f)
            {
                Destroy(gameObject);
            }
        }
    }

    private void UpdateDespawnTime()
    {
        timeToDespawn -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (TagEnum.PLAYER.Equals(other.transform.gameObject.tag))
        {
            Status status = other.transform.gameObject.GetComponent<Status>();
            ApplyEffectBasedOnType(type, status);
            Destroy(gameObject);
        }
    }

    private void ApplyEffectBasedOnType(ConsumableTypeEnum type, Status status)
    {
        switch (type)
        {
            case ConsumableTypeEnum.HEAL:
                status.AddHealth(value);
                break;
            case ConsumableTypeEnum.ENERGY:
                status.AddEnergy(value);
                break;
            case ConsumableTypeEnum.ATTACK_UP:
                status.AddAttack(value);
                break;
            case ConsumableTypeEnum.DEFENSE_UP:
                status.AddDefense(value);
                break;
            default:
                Debug.LogError("Any effect was chosen for this Consumable");
                break;
        }
    }
}
