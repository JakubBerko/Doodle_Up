using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class ScreenSize : MonoBehaviour
{
    public Camera camera;
    BoxCollider2D cameraCollider;

    public UnityEvent<Collider2D> ExitTriggerFired;

    [SerializeField]
    private float moveOffset = 0.2f;
    // Start is called before the first frame update
    private void Awake()
    {
        this.camera.transform.localScale = Vector3.one;
        cameraCollider = GetComponent<BoxCollider2D>();
        cameraCollider.isTrigger = true;
    }

    private void Start()
    {
        transform.position = Vector3.zero;
        UpdateBorderSize();
    }

    private void UpdateBorderSize()
    {
        float y = camera.orthographicSize * 2;
        Vector2 boxColliderSize = new Vector2(y * camera.aspect, y);
        cameraCollider.size = boxColliderSize;
    }
}
