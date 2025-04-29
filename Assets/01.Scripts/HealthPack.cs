using UnityEngine;

public class HealthPack : MonoBehaviour, IItem
{
    public float health = 50;

    public void Use(GameObject target)
    {
        LivingEntity life = target.GetComponent<LivingEntity>();
        if (life != null)
        {

            // target�� ü���� ȸ���ϴ� ó��
            life.RestoreHealth(health);
            Debug.Log(target.name + "�� ü���� ȸ���ߴ� : " + health);

        }
        Destroy(gameObject);
    }
}
