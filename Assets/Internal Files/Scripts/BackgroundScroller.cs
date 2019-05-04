using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField, Range(-10, 10)] private float scrollSpeed = 0.1f;

    private Material backgroundMaterial = null;
    private Vector2 offset;

    private void Start()
    {
        backgroundMaterial = GetComponent<Renderer>().material;
        offset = new Vector2(0, scrollSpeed);
    }

    private void Update()
    {
        backgroundMaterial.mainTextureOffset += offset * Time.deltaTime;
    }
}