using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    [SerializeField] private float minFallSpeed = 5f;                        // Minimum Fall Speed
    [SerializeField] private float maxFallSpeed = 6f;                        // Maximum Fall Speed

    private float fallSpeed;                                                 // Default Fall Speed

    private void OnEnable()
    {
        fallSpeed = Random.Range(minFallSpeed, maxFallSpeed);
    }

    // Update is called once per frame
    private void Update()
    {
        // Make the Object Fall According to Fall Speed
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);
    }
}
