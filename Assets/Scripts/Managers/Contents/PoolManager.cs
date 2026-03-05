using UnityEngine;
using System.Collections.Generic;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    [SerializeField]
    private GameObject _itemPrefab; //복사본

    [SerializeField]
    private int _poolSize = 20; //개수

    //Key: 프리팹 이름, Value: 아이템 풀 
    private Dictionary<string, List<GameObject>> _pools = new Dictionary<string, List<GameObject>>();

    void Awake()
    {
        Instance = this;
    }

    public GameObject GetItem(GameObject prefab)
    {
        string Key = prefab.name;

        if(!_pools.ContainsKey(Key))
        {
            _pools.Add(Key, new List<GameObject>());
        }

        foreach (GameObject obj in _pools[Key])
        {
            //비활성화 오브젝트
            if(!obj.activeSelf)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        //없으면 생성 후 할당
        GameObject newObj = Instantiate(prefab);
        newObj.name = Key;
        _pools[Key].Add(newObj);
        return newObj;

    }

}
