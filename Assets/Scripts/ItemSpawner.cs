using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Runtime.CompilerServices;

public class ItemSpawner : MonoBehaviour
{
    public Item itemToSpawn;
    public float speed = 0.01f;
    public List<Item> items = new List<Item>();
    public Transform parent;
    public List<Transform> points;

    private void Start()
    {
        InvokeRepeating(nameof(BeginSpawn), 1, 1f);
    }

    private void BeginSpawn()
    {
        var spawnedItem = Instantiate(itemToSpawn, parent);
        spawnedItem.itemPosition = GetRandomLocation();
        items.Add(spawnedItem);
    }

    private Vector3 GetRandomLocation()
    {
        
        // var xRand = Random.Range(minHorizontal, maxHorizontal);
        // var yRand = Random.Range(minVertical, maxVertical);
        int point = Random.Range(0, points.Count);
        return new Vector3(points[point].position.x, points[point].position.y, 0);
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
        item.itemPosition.z += speed;
    }


}
