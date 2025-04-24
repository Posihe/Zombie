using System;
using UnityEngine;

public class LivingEntity : MonoBehaviour,IDamageable
    //생명체로 동작할 게임 오브젝트들을 위한 뼈대를 제공
    //체력,대미지 받아들이기,사망기능,사망 이벤트를 제공
{
    public float startingHealth = 100f;//시작 체력
    public float health { get; protected set; }//현재 체력
    public bool dead { get; protected set; }//사망 상태
    public event Action onDeath;//사망 시 발동할 이벤트

   protected virtual void OnEnable()//생명체가 활성화될 떄 상태를 리셋
    {

        dead = false;//사망하지 않은 상태로 시작
        health = startingHealth;//체력을 시작 체력으로 초기화

    }

    public virtual void OnDamage(float damage,Vector3 hitpoint, Vector3 hitNormal)//데미지를 입는 기능
    {
        health -= damage;//데미지 만큼 체력 감소
        if(health<=0&&!dead)//체력이 0이한&&아직 죽지 않았다면 사망처리
        {
            Die();

        }

    }
    public virtual void RestoreHealth(float newHealth)
    {
        if (dead)
        {
            // 이미 사망한 경우 체력을 회복할 수 없음
            return;
        }

        float nowHealth = newHealth;

        if (nowHealth > startingHealth - health)
        {
            nowHealth = startingHealth - health;
        }

        // 체력 추가
        health += nowHealth;
    }

    public virtual void Die()//사망처리
    {
        if(onDeath!=null)//onDeath 이벤트에 등록된 메서드가 있다면 실행
        {

            onDeath();
        }
        dead = true;//사망 상태를 참으로 변경
    }
}
