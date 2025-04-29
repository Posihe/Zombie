using UnityEngine;

public class AmmoPack : MonoBehaviour, IItem
{
    public int ammo = 30;

    public void Use(GameObject target)
    {
        PlayerShooter playerShooter = target.GetComponent<PlayerShooter>();
        if (playerShooter != null && playerShooter.gun != null)
        {
            // target�� ź���� �߰��ϴ� ó��
            playerShooter.gun.ammoRemain += ammo;
            Debug.Log(target.name + "�� ź���� �����ߴ� : " + ammo);
        }
        Destroy(gameObject);

    }
}
