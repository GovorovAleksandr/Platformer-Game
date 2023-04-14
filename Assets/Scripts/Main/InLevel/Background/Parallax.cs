using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private List<ParallaxObject> _objects;

    [SerializeField] private Transform Target;

    public Transform[] layers;
    public float[] speeds;

    private Vector3 lastCameraPosition;

    private void Start()
    {
        lastCameraPosition = Target.transform.position;
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < layers.Length; i++)
        {
            float deltaX = Target.transform.position.x - lastCameraPosition.x;
            Vector3 layerPosition = _objects[i].Transform.position;
            layerPosition.x += deltaX * speeds[i];
            _objects[i].Transform.position = layerPosition;
        }

        lastCameraPosition = Target.transform.position;
    }

}