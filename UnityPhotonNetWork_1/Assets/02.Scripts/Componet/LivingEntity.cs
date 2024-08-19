using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour,IDamageable
{
    public float startingHealth = 100f;          // 시작 체력
    public float health { get; protected set; }  // 현재 체력
    public bool dead { get; protected set; }     // 사망 상태
    public event Action onDeath;                 // 사망시 발동 할 이벤트

    // 생명체가 활성화 될때 상태를 리셋시킴
    protected virtual void OnEnable()
    {// virtual : 물려받을 가상 메서드
        dead = false;  // 사망하지 않은 상태로
        health = startingHealth;  // 체력을 시작 체력으로 초기화
    }

    public virtual void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        health -= damage;
        if (health <= 0 && !dead)
        {
            Die();
        }
    }

    // 체력을 회복하는 기능
    public virtual void RestoreHealth(float newHealth)
    {
        if (dead)
        {// 이미 죽었다면 체력을 회복 할 수 없다.
            return;
        }
    }

    public virtual void Die()
    {
        if (onDeath != null)
        {
            onDeath();
        }
        dead = true;
    }
}
// LivingEntity 클래스는 IDamageable을 상속하므로 OnDamage()
// 메서드를 반드시 구현 해야한다.
