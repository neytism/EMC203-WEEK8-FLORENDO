using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Vector3 itemPosition;
    private CameraComponent cameraComponent;
    private void Awake()
    {
        cameraComponent = FindObjectOfType<CameraComponent>();
    }

    private void Update()
    {
        // Calculate the perspective
        var perspective = cameraComponent.focalLength / (cameraComponent.focalLength + itemPosition.z);
    
        // Modify the perspective using a square function to make the scaling increase faster as the object gets bigger
        perspective = Mathf.Pow(1 - perspective, 2);
    
        var position = cameraComponent.vanishingPoint.position;
    
        Vector3 offset = itemPosition - position;
    
        // Use the modified perspective for the scale
        transform.localScale = Vector3.one * perspective;
        
        // Adjust z value based on scale
        float adjustedZ = position.z - transform.localScale.x;
    
        transform.position = new Vector3(position.x + offset.x * perspective, position.y + offset.y * perspective, adjustedZ);
    }


    private Color GetRandomColor()
    {
        var rRand = Random.Range(0f, 1f);
        var gRand = Random.Range(0f, 1f);
        var bRand = Random.Range(0f, 1f);
        return new Color(rRand, gRand, bRand);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                
    }


}
