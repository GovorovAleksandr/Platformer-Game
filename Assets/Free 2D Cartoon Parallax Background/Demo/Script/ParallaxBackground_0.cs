using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground_0 : MonoBehaviour
{
    public bool Camera_Move;
    public float Camera_MoveSpeed = 1.5f;

    [Header("Layer Setting")]

    public float[] Layer_Speed = new float[7];
    public GameObject[] Layer_Objects = new GameObject[7];

    private Transform _camera;
    private float[] startPos = new float[7];
    private float boundSizeX;
    private float sizeX;

    private void Start()
    {
        _camera = Camera.main.transform;
        sizeX = Layer_Objects[0].transform.localScale.x;
        boundSizeX = Layer_Objects[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x;

        for (int i = 0; i < 5;i++)
        {
            startPos[i] = _camera.position.x;
        }
    }

    private void Update()
    {
        if (Camera_Move)
        {
            _camera.position += Vector3.right * Time.deltaTime * Camera_MoveSpeed;
        }
    }
}
