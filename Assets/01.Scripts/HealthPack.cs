using UnityEngine;

public class HealthPack : MonoBehaviour, IItem
{
    public float health = 50;

    public void Use(GameObject target)
    {
        LivingEntity life = target.GetComponent<LivingEntity>();
        if (life != null)
        {

            // target의 체력을 회복하는 처리
            life.RestoreHealth(health);
            Debug.Log(target.name + "의 체력을 회복했다 : " + health);

        }
        Destroy(gameObject);
    }
}
