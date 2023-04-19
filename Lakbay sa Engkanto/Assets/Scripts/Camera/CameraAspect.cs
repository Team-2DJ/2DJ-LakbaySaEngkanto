using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAspect : MonoBehaviour
{
    private new Camera camera;

    [SerializeField] private float screenHeight, screenWidth;

    void Start()
    {
        camera = GetComponent<Camera>();
        float srcHeight = screenHeight;
        float srcWidth = screenWidth;

        float deviceScreenAspect = srcWidth / srcHeight;

        camera.aspect = deviceScreenAspect;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
