using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] items;  // 생성할 아이템  1. 탄약  2. HP회복 아이템
    public Transform playerTr;  // 플레이어 트랜스폼
    public float maxDist = 5f;  // 플레이어 위치에서 아이템이 배치될 최대 반경
    public float timeBetSpawnMax = 7f;  // 최대 시간 간격
    public float timeBetSpawnMin = 2f;  // 최소 시간 간격
    private float timeBetSpawn;  // 생성 간격
    private float lastSpwanTime;  // 마지막 생성 시점

    void Start()
    {
        timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);
        lastSpwanTime = 0f;
        playerTr = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (Time.time >= lastSpwanTime + timeBetSpawn && playerTr != null) // playerTr=!null 유효성 검사
        {
            lastSpwanTime = Time.time;
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);
            Spawn();
        }
    }

    void Spawn()
    {
        Vector3 spawnPos = GetRandomPointOnNavMesh(playerTr.position, maxDist);
        spawnPos += Vector3.up * 0.5f;  // 바닥에서 위치를 0.5 만큼 올림
        // 아이템 중 하나를 무작위로 골라서 랜덤위치에 생성
        GameObject selectecItem = items[Random.Range(0, items.Length)];
        GameObject item = Instantiate(selectecItem, spawnPos, Quaternion.identity);
        // 생성된 아이템을 5초 후에 소멸
        Destroy(item, 5.0f);
    }

    // 네비메시위에 랜덤한 위치를 반환하는 메서드
    // center를 중심으로 거리 반경 안에서 랜덤한 위치를 찾음
    private Vector3 GetRandomPointOnNavMesh(Vector3 center, float distance)
    {//                     반지름이 1인 구(원) 안에서 랜덤한 점을 반환하는 프로퍼티
        Vector3 randomPos = Random.insideUnitSphere * distance + center;
        // 네비메쉬 샘플링의 결과 정보를 저장하는 변수
        NavMeshHit hit;
        // maxdistance 반경 안에서 randomPos에 가장 가까운 네비메쉬 위의 한점을 찾는다.
        NavMesh.SamplePosition(randomPos, out hit, distance, NavMesh.AllAreas);
        return hit.position;
    }
}
