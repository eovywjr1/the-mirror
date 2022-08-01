using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorCamera : MonoBehaviour
{
    Camera mirrorCamera;
    void Start()
    {
        mirrorCamera = GetComponent<Camera>();
    }

    public void OnPreCull()
    {
        mirrorCamera.ResetProjectionMatrix();
        mirrorCamera.projectionMatrix *= Matrix4x4.Scale(new Vector3(-1, 1, 1));
    }

    private void OnPreRender()
    {
        GL.invertCulling = true;
    }

    private void OnPostRender()
    {
        GL.invertCulling = false;
    }
}
