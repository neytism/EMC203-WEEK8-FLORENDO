using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemSpawner : MonoBehaviour
{
    public List<Item> itemsPrefab;
    public List<Item> items = new List<Item>();
    public Transform parent;
    public List<Transform> points;
    public float repeatRate = 1f;
    private int _lane;

    private void Start()
    {
        InvokeRepeating(nameof(BeginSpawn), 1, 2f);
    }

    private void BeginSpawn()
    {
        _lane = Random.Range(0, points.Count);
        int temp = _lane;
        Spawn(_lane);
        _lane = Random.Range(0, points.Count);
        while (temp == _lane)
        {
            _lane = Random.Range(0, points.Count);
        }
        Spawn(_lane);
    }
    
    private void Spawn( int lane)
    {
        if (Player.IsDead) return;
        
        GameObject obj = ObjectPool.Instance.PoolObject(itemsPrefab[Random.Range(0, itemsPrefab.Count)].gameObject, Vector3.zero);
        //var spawnedItem = Instantiate(itemToSpawn);
        obj.SetActive(true);
        var spawnedItem = obj.GetComponent<Item>();
        spawnedItem.ResetItem();
        spawnedItem.lanePosition = lane;
        spawnedItem.itemPosition = GetRandomLocation(lane);
        
        if (!items.Contains(spawnedItem))
        {
            items.Add(spawnedItem);
        }
    }

    

    private Vector3 GetRandomLocation(int lane)
    {
        
        // var xRand = Random.Range(minHorizontal, maxHorizontal);
        // var yRand = Random.Range(minVertical, maxVertical);
        
        return new Vector3(points[lane].position.x, points[lane].position.y, 0);
    }


    private void Update()
    {
        foreach (var item in items)
        {
            ItemMover(item);
        }

        //transform.position = GetRandomLocation();
        List<Transform> children = new List<Transform>();
        foreach(Transform child in transform)
        {
            children.Add(child);
        }       
        children.OrderBy(x => x.position.z);
        
        for(int i = 0 ; i < children.Count; i++)
        {
            children[i].SetSiblingIndex(i);
        }
    }

    private void ItemMover(Item item)
    {
        item.speed += item.acceleration;
        item.itemPosition.z += item.speed; 
    }

}
