using System.Collections;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] _foodPrefabs; //프리팹을 담을 배열

    [SerializeField]
    float _spawnInterval = 2.0f; //생성주기

    private Transform _playerTransform; //플레이어 정보를 담아둠.

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
            _playerTransform = player.transform;

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
        if (_playerTransform != null) 
        {
            float playerX = _playerTransform.position.x;
            float playerZ = _playerTransform.position.z;

            float randomX = Random.Range(playerX - 5f, playerX + 5f);

            Vector3 spawnPos = new Vector3(randomX, 10f, playerZ);

            int randomIndex = Random.Range(0, _foodPrefabs.Length);
            Instantiate(_foodPrefabs[randomIndex], spawnPos, Quaternion.identity); //Quaternion.identity: 회전값 없음
        }
        
    }

}
