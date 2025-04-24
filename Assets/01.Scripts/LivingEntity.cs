using System;
using UnityEngine;

public class LivingEntity : MonoBehaviour,IDamageable
    //����ü�� ������ ���� ������Ʈ���� ���� ���븦 ����
    //ü��,����� �޾Ƶ��̱�,������,��� �̺�Ʈ�� ����
{
    public float startingHealth = 100f;//���� ü��
    public float health { get; protected set; }//���� ü��
    public bool dead { get; protected set; }//��� ����
    public event Action onDeath;//��� �� �ߵ��� �̺�Ʈ

   protected virtual void OnEnable()//����ü�� Ȱ��ȭ�� �� ���¸� ����
    {

        dead = false;//������� ���� ���·� ����
        health = startingHealth;//ü���� ���� ü������ �ʱ�ȭ

    }

    public virtual void OnDamage(float damage,Vector3 hitpoint, Vector3 hitNormal)//�������� �Դ� ���
    {
        health -= damage;//������ ��ŭ ü�� ����
        if(health<=0&&!dead)//ü���� 0����&&���� ���� �ʾҴٸ� ���ó��
        {
            Die();

        }

    }
    public virtual void RestoreHealth(float newHealth)
    {
        if (dead)
        {
            // �̹� ����� ��� ü���� ȸ���� �� ����
            return;
        }

        float nowHealth = newHealth;

        if (nowHealth > startingHealth - health)
        {
            nowHealth = startingHealth - health;
        }

        // ü�� �߰�
        health += nowHealth;
    }

    public virtual void Die()//���ó��
    {
        if(onDeath!=null)//onDeath �̺�Ʈ�� ��ϵ� �޼��尡 �ִٸ� ����
        {

            onDeath();
        }
        dead = true;//��� ���¸� ������ ����
    }
}
