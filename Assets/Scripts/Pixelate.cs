using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pixelate : MonoBehaviour
{
    [ExecuteInEditMode]
    [SerializeField]
    int pixelX;
    [SerializeField]
    int pixelY;
    Camera camera;
    Canvas canvas;
    RawImage image;
    ScreenCoordinateCorrector corrector;

    // Start is called before the first frame update
    private void Awake()
    {
        corrector = new ScreenCoordinateCorrector();
        camera = transform.GetChild(0).gameObject.GetComponent<Camera>();
        canvas = transform.GetChild(1).gameObject.GetComponent<Canvas>();
        image = canvas.transform.GetChild(0).gameObject.GetComponent<RawImage>();

    }
    void Start()
    {
        camera.orthographicSize = corrector.convertToUnit(pixelY) / 2.0f;
        canvas.GetComponent<RectTransform>().sizeDelta = new Vector2(pixelX, pixelY);
        image.GetComponent<RectTransform>().sizeDelta = new Vector2(pixelX, pixelY);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        source.filterMode = FilterMode.Point;
        RenderTexture resultTexture = RenderTexture.GetTemporary(source.width / pixels, source.height / pixels, 0, source.format);
        resultTexture.filterMode = FilterMode.Point;
        Graphics.Blit(source, resultTexture);
        Graphics.Blit(resultTexture, destination);
        RenderTexture.ReleaseTemporary(resultTexture);
    }
    */
}
