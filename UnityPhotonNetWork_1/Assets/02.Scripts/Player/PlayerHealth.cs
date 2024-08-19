using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : LivingEntity
{
    public Slider healthSlider;        // ü���� ǥ���� �����̴�
    public AudioClip hitClip;         // �ǰ� �Ҹ�
    public AudioClip itemPickupClip;  // ������ �ݴ� �Ҹ�
    public AudioClip deahtClip;
    [SerializeField] private AudioSource playerAudioPlayer;  // �÷��̾� �Ҹ� �����
    [SerializeField] private Animator playerAnimator;  // �÷��̾� �ִϸ�����
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerShooter playerShooter;

    private readonly int hashDie = Animator.StringToHash("DieTrigger");

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAudioPlayer = GetComponent<AudioSource>();
        playerShooter = GetComponent<PlayerShooter>();
    }

    protected override void OnEnable()  // �������̵� �ٸ��� �� ����
    {
        base.OnEnable();  // �θ� Ŭ������ �̺�Ʈ �Լ�
        healthSlider.gameObject.SetActive(true);
        healthSlider.maxValue = startingHealth;
        healthSlider.value = health;
        playerMovement.enabled = true;
        playerShooter.enabled = true;
    }

    // ü�� ȸ��
    public override void RestoreHealth(float newHealth)
    {
        base.RestoreHealth(newHealth);
        healthSlider.value = newHealth;
        // ������Ʈ�� ü��
    }

    // ������ ó��
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        if (!dead)
        {
            playerAudioPlayer.PlayOneShot(hitClip);
            // ������� ���� ��쿡�� ȿ���� ���
        }
        // LivingEnity�� OnDamage �����ؼ� ������ ����
        base.OnDamage(damage, hitPoint, hitDirection);
        healthSlider.value = health;
    }

    public override void Die()
    {
        base.Die();
        healthSlider.gameObject.SetActive(false);
        playerAudioPlayer.PlayOneShot(deahtClip, 1.0f);
        playerAnimator.SetTrigger(hashDie);
        playerMovement.enabled = false;
        playerShooter.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // �����۰� �浹�� ��� �ش� �������� ����ϴ� ó��
        if (!dead)
        {   // �浹�� �������� ���� IItem ���۳�Ʈ�� ������ �´�.
            IItem item = other.GetComponent<IItem>();
            if (item != null)
            {
                item.Use(gameObject);
                // ������ ���� �Ҹ� ���
                playerAudioPlayer.PlayOneShot(itemPickupClip, 1.0f);
            }
        }
    }
}
