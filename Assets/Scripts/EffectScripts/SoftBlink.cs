using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoftBlink : CameraScreenEffect
{
    /*
     [parameters]
    0 : 어두워지거나 밝아지는데 걸리는 시간
    1 : 깜깜한 상태로 지속되는 시간
     */
    Image canvasImage;

    bool isBlinking = false;
    
    public SoftBlink(int[] param, GameObject effectCanvasPrefab) : base(param, effectCanvasPrefab) { }
    public override void Activate()
    {
        if(!isBlinking)
            StartCoroutine(Blink());
    }

    public override void Deactive()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        canvasImage = canvasPrefab.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Fade(int direction)//1 : 페이드 인, -1 : 페이드 아웃
    {
        float a = -0.5f * direction + 0.5f;
        while (direction == 1 && a < 1 || direction == -1 && a > 0)
        {
            
            float dt = Time.deltaTime;
            yield return new WaitForSeconds(dt);
            a += dt / (parameters[0] / 1000f) * direction;
            canvasImage.color = new Color(canvasImage.color.r, canvasImage.color.g, canvasImage.color.b, a);
        }
    }
    IEnumerator Blink()
    {
        isBlinking = true;
        StartCoroutine(Fade(1));
        yield return new WaitForSeconds(parameters[1] / 1000f + 2 * parameters[0] / 1000);
        StartCoroutine(Fade(-1));
        isBlinking = false;

    }
}
