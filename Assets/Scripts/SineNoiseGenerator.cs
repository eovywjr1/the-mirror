using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SineNoiseGenerator : MonoBehaviour
{
    [SerializeField]
    float amplitude;
    [SerializeField]
    float noiseAmplitude;
    [SerializeField]
    float time;
    [SerializeField]
    float offset;
    Light2D light;
    float output;
    float t = 0;//지금까지 지난 시간
    float noise = 0; //랜덤
    float lastUpdatedTime = 0; //최근 noise 값 갱신 시간
    bool lightOn = false; //라이터 꺼짐/켜짐
    bool toggleable = true; //중복 입력 방지
    private void Awake()
    {
        
        
    }

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light2D>();  
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            toggleLight();
        }
        if (t / time - lastUpdatedTime >= Mathf.PI * 2)
        {
            noise = Random.Range(-noiseAmplitude, noiseAmplitude);
            lastUpdatedTime = t / time;
        }
        output = Mathf.Sin(t / time) * noise + offset;
        light.intensity = output * System.Convert.ToInt16(lightOn);
        t += 1;
    }
    void toggleLight()
    {
        if (toggleable)
        {
            lightOn = !lightOn;
            StartCoroutine(preventDoubleInputCoroutine());
        }

    }
    IEnumerator preventDoubleInputCoroutine()
    {
        toggleable = false;
        yield return new WaitForSeconds(0.1f);
        toggleable = true;

    }
}
