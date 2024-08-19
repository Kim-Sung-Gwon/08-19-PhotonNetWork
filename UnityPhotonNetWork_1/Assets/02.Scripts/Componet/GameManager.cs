using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindAnyObjectByType<GameManager>();
            return instance;
        }
    }
    private int score = 0; // ���� ���� ����

    public bool isGameOver{ get; private set; }

    void Awake()
    {
        // ���� �̱��� ������Ʈ�� �� �ٸ� GameManager ������Ʈ�� �ִٸ�
        if (instance != null)
        {
            Destroy(gameObject);  // �ڽ��� �ı�
        }
    }

    private void Start()
    {// FindAnyObjectByType�� Find ���� ã�� �ӵ��� ������.
        FindAnyObjectByType<PlayerHealth>().onDeath += EndGame;
        // �÷��̾� ĳ������ ��� �̺�Ʈ �߻��� ���ӿ���
    }

    public void AddScore(int newScore)
    {
        if (!isGameOver)
        {
            score += newScore;
            UIManager.Instance.UpdateScoreText(score);
        }
    }

    public void EndGame()
    {
        isGameOver = true;
        UIManager.Instance.SetAciveGameOverUI(true);
    }
}