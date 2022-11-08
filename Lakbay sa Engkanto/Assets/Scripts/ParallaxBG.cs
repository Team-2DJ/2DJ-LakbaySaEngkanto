using UnityEngine;

public class ParallaxBG : MonoBehaviour
{
    private float length, startPosition;
    [SerializeField] new Camera camera;
    [SerializeField] float parallaxEffect;

    void Start()
    {
        startPosition = transform.position.x;
        camera = Camera.main;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float temp = (camera.transform.position.x * (1 - parallaxEffect));
        float distance = (camera.transform.position.x * parallaxEffect);
        transform.position = new Vector3(startPosition + distance, transform.position.y, transform.position.z);

        if (temp > startPosition + length)
            startPosition += length;
        else if (temp < startPosition - length)
            startPosition -= length;
    }
}
