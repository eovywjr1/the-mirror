using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenCoordinateCorrector
{
    private static int pixelPerUnit = 64; //pixelPerUnit ют╥б
    private float unitPerPixel = 0;
    public ScreenCoordinateCorrector()
    {
        unitPerPixel = 1.0f / pixelPerUnit;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float convertToPixel(float unit)
    {
        return unit * pixelPerUnit;
    }
    public float convertToUnit(float pixel)
    {
        return pixel * unitPerPixel;
    }

}
