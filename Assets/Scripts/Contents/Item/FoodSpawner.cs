using System.Collections;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] _foodPrefabs; //프리팹을 담을 배열

    [SerializeField]
    float _spawnInterval = 0.9f; //생성주기

    [SerializeField]
    float _mapRangeX = 30f;
    float _mapRangeZ = 30f;

    private Transform _playerTransform; //플레이어 정보를 담아둠.

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
            _playerTransform = player.transform;
        else
            Debug.LogWarning("FoodSpawner: 'Player'태그를 찾을 수 없어 랜덤 모드로만 작동");

        StartCoroutine(SpawnRoutine()); 
    }

    IEnumerator SpawnRoutine()
    {
        while(true)
        {
            SpawnFood();
            yield return new WaitForSeconds(_spawnInterval);
        }
    }
    
    void SpawnFood()
    {
        if (_foodPrefabs == null || _foodPrefabs.Length == 0)
            return;

        float finalX, finalZ;
        float chance = Random.Range(0f, 100f); //가능성

        //70% 확률로 플레이어 주변 5m이내에 스폰
        if (_playerTransform != null || chance < 40f)
        {
            finalX = Random.Range(_playerTransform.position.x - 5f, _playerTransform.position.x + 5f);
            finalZ = Random.Range(_playerTransform.position.z - 5f, _playerTransform.position.z + 5f);
        }
        //30% 확률로 맵 전체에 랜덤하게 스폰
        else
        {
            finalX = Random.Range(-_mapRangeX, _mapRangeX);
            finalZ = Random.Range(-_mapRangeZ, _mapRangeZ);
        }
            
        Vector3 spawnPos = new Vector3(finalX, 17f, finalZ);
        int randomIndex = Random.Range(0, _foodPrefabs.Length);
        Instantiate(_foodPrefabs[randomIndex], spawnPos, Quaternion.identity); //Quaternion.identity: 회전값 없음
        
    }

}
