using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explorer : MonoBehaviour {

    public Material mat;
    public Vector2 pos;
    public float scale;
    public float angle;

    private Vector2 smoothPos;
    private float smoothScale;
    private float smoothAngle;
    
    private void UpdateShader()
    {
        smoothPos = Vector2.Lerp(smoothPos, pos, 0.5f * Time.deltaTime);
        smoothScale = Mathf.Lerp(smoothScale, scale, 0.5f * Time.deltaTime);
        smoothAngle = Mathf.Lerp(smoothAngle, angle, 0.5f * Time.deltaTime);

        float aspect = Screen.width / (float)Screen.height;

        float scaleX = smoothScale;
        float scaleY = smoothScale;

        if (aspect > 1f) {
            scaleY /= aspect;
        }
        else {
            scaleX *= aspect;
        }

        mat.SetVector("_Area", new Vector4(smoothPos.x, smoothPos.y, scaleX, scaleY));
        mat.SetFloat("_Angle", smoothAngle);
    }

    private void HandleInputs()
    {
        if (Input.GetKey(KeyCode.E)) {
            scale *= 0.99f;
        }
        else if (Input.GetKey(KeyCode.Q)) {
            scale *= 1.01f;
        }

        Vector2 dir = new Vector2(0.01f * scale, 0);
        dir = new Vector2(dir.x * Mathf.Cos(angle), dir.x * Mathf.Sin(angle));

        if (Input.GetKey(KeyCode.A)) {
            pos -= dir;
        }
        else if (Input.GetKey(KeyCode.D)) {
            pos += dir;
        }

        dir = new Vector2(-dir.y, dir.x);

        if (Input.GetKey(KeyCode.S)) {
            pos -= dir;
        }
        else if (Input.GetKey(KeyCode.W)) {
            pos += dir;
        }

        if (Input.GetKey(KeyCode.Z)) {
            angle = Mathf.Clamp(angle + 0.01f, -3.1415f, 3.1415f);
        }
        else if (Input.GetKey(KeyCode.X)) {
            angle = Mathf.Clamp(angle - 0.01f, -3.1415f, 3.1415f);
        }
    }

    public void Update()
    {
        HandleInputs();
        UpdateShader();
    }
}
