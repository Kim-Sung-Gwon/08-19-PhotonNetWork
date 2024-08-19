using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour,IDamageable
{
    public float startingHealth = 100f;          // ���� ü��
    public float health { get; protected set; }  // ���� ü��
    public bool dead { get; protected set; }     // ��� ����
    public event Action onDeath;                 // ����� �ߵ� �� �̺�Ʈ

    // ����ü�� Ȱ��ȭ �ɶ� ���¸� ���½�Ŵ
    protected virtual void OnEnable()
    {// virtual : �������� ���� �޼���
        dead = false;  // ������� ���� ���·�
        health = startingHealth;  // ü���� ���� ü������ �ʱ�ȭ
    }

    public virtual void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        health -= damage;
        if (health <= 0 && !dead)
        {
            Die();
        }
    }

    // ü���� ȸ���ϴ� ���
    public virtual void RestoreHealth(float newHealth)
    {
        if (dead)
        {// �̹� �׾��ٸ� ü���� ȸ�� �� �� ����.
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
// LivingEntity Ŭ������ IDamageable�� ����ϹǷ� OnDamage()
// �޼��带 �ݵ�� ���� �ؾ��Ѵ�.
