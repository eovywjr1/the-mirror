using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLitShaderEmulator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject lightObject;
    SineNoiseGenerator noiseGenerator;
    SpriteRenderer spriteRenderer;
    float r, g, b;
    void Start()
    {
        noiseGenerator = lightObject.GetComponent<SineNoiseGenerator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        float r = spriteRenderer.color.r;
        float g = spriteRenderer.color.g;
        float b = spriteRenderer.color.b;
    }

    // Update is called once per frame
    void Update()
    {
        
        float intensity = noiseGenerator.getOutput();
        spriteRenderer.color = new Color(CalculateBrightnessColor(r, intensity), CalculateBrightnessColor(g, intensity), CalculateBrightnessColor(b, intensity));
        Debug.Log(spriteRenderer.color.ToString());
    }
    float CalculateBrightnessColor(float f, float intensity) //명암 계산하는 함수
    {
        return Mathf.Clamp( 1.0f *intensity, 0, 1);
        
    }
}
