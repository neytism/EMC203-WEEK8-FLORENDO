using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StarsSpawner : MonoBehaviour
{
    public Item itemToSpawn;
    public List<Item> items = new List<Item>();
    public Transform parent;
    [SerializeField] private float _spawnRadius = 5f;

    private void Start()
    {
        InvokeRepeating(nameof(BeginSpawn), 1, Random.Range(0.1f, 0.3f));
        InvokeRepeating(nameof(BeginSpawn), 1, Random.Range(0.1f, 0.3f));
        InvokeRepeating(nameof(BeginSpawn), 1, Random.Range(0.1f, 0.3f));
    }

    private void BeginSpawn()
    {
        GameObject obj = ObjectPool.Instance.PoolObject(itemToSpawn.gameObject, Vector3.zero);
        //var spawnedItem = Instantiate(itemToSpawn);
        obj.SetActive(true);
        var spawnedItem = obj.GetComponent<Item>();
        spawnedItem.ResetItem();
        spawnedItem.itemPosition = GetRandomLocation();
        

        if (!items.Contains(spawnedItem))
        {
            items.Add(spawnedItem);
        }
        
    }

    private Vector3 GetRandomLocation()
    {
        Vector2 spawnPos = transform.position;
        spawnPos = Random.insideUnitCircle.normalized * _spawnRadius;
        
        return new Vector3(spawnPos.x, spawnPos.y, 0);
    }

    private void Update()
    {
        foreach (var item in items)
        {
            ItemMover(item);
        }


        transform.position = GetRandomLocation();
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
        item.speed += item.acceleration; // Increase speed by acceleration
        item.itemPosition.z += item.speed; // Move item by its speed
    }


}