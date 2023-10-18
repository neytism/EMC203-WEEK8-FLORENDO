using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float speed = 0.01f; 
    public float acceleration = 0.001f;
    public Vector3 itemPosition;
    private CameraComponent cameraComponent;
    private void Awake()
    {
        cameraComponent = FindObjectOfType<CameraComponent>();
    }

    private void Update()
    {
        var perspective = cameraComponent.focalLength / (cameraComponent.focalLength + itemPosition.z);
    
        perspective = Mathf.Pow(1 - perspective, 3f);
    
        var position = cameraComponent.vanishingPoint.position;
    
        Vector3 offset = itemPosition - position;
    
        transform.localScale = Vector3.one * perspective;
        
        float adjustedZ = position.z - transform.localScale.x;
    
        transform.position = new Vector3(position.x + offset.x * perspective, position.y + offset.y * perspective, adjustedZ);
    }
    
    
    private void OnEnable()
    {
       StartCoroutine(BulletLife());
       }

    IEnumerator BulletLife()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
        

    }

    public void ResetItem()
    {
        speed = 0f;
        itemPosition = Vector3.zero;
        transform.localScale = Vector3.zero;
    }


}
