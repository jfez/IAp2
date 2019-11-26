using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    static readonly float Field_Of_View_Min = 80f;
    static readonly float Field_Of_View_Max = 20f;

    static readonly float Velocity_Zoom = 60f;

    static readonly float Drag_Speed = 10f;

    public float minX, minY, maxX, maxY;

    public Vector3 dragOrigin;

    public Camera myCamera;


    private void Awake()
    {
        myCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        float zoom = Input.GetAxisRaw("Mouse ScrollWheel");
        if (zoom < 0f && myCamera.fieldOfView < Field_Of_View_Min)
            myCamera.fieldOfView += Velocity_Zoom * Time.deltaTime;
        else if (zoom > 0f && myCamera.fieldOfView > Field_Of_View_Max)
            myCamera.fieldOfView -= Velocity_Zoom * Time.deltaTime;

        //Arrastrar cámara
        if (Input.GetMouseButtonDown(1))
        {
            dragOrigin = Input.mousePosition;
            Debug.Log("Arrastrar");
            return;
        }
        else if (!Input.GetMouseButton(1))
            return;

        DragCamera();
    }

    void DragCamera()
    {
        Vector3 pos = myCamera.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(pos.x * Drag_Speed * Time.deltaTime, 0f, pos.y * Drag_Speed * Time.deltaTime);

        float sumX = transform.position.x + move.x;
        float sumY = transform.position.z + move.z;

        if (sumX > minX && sumX < maxX && sumY > minY && sumY < maxY)
            transform.Translate(move, Space.World);

    }
}