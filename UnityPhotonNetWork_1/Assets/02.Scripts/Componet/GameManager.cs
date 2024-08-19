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
    private int score = 0; // 현재 게임 점수

    public bool isGameOver{ get; private set; }

    void Awake()
    {
        // 씬에 싱글턴 오브젝트가 된 다른 GameManager 오브젝트가 있다면
        if (instance != null)
        {
            Destroy(gameObject);  // 자신을 파괴
        }
    }

    private void Start()
    {// FindAnyObjectByType은 Find 보다 찾는 속도가 느리다.
        FindAnyObjectByType<PlayerHealth>().onDeath += EndGame;
        // 플레이어 캐릭터의 사망 이벤트 발생시 게임오버
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