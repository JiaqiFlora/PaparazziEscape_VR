using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureChange : MonoBehaviour
{
    public CarChangingController carChangingController;

    private Renderer renderer;
    private Material material;

    private Vector2 offset;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        material = renderer.material;

        offset = material.mainTextureOffset;
    }

    // Update is called once per frame
    void Update()
    {


        if (carChangingController.speed == 20)
        {
            offset.x += 0.1f * Time.deltaTime;
            offset.y += 0.1f * Time.deltaTime;

            material.mainTextureOffset = offset;
        }
        else if (carChangingController.speed == 60)
        {
            offset.x += 0.5f * Time.deltaTime;
            offset.y += 0.1f * Time.deltaTime;

            material.mainTextureOffset = offset;
        }
    }
}
