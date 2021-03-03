using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject cinemachineVirtualCamera;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private PolygonCollider2D cameraCollider;
    [SerializeField] private PolygonCollider2D roomCollider;
    [SerializeField] private bool lockLeft;
    [SerializeField] private bool lockRight;
    [SerializeField] private bool lockUp;
    [SerializeField] private bool lockDown;
    [SerializeField] private Vector2 roomSize;
    private float cameraHeight;
    private float cameraWidth;

    public void Start()
    {
        cameraHeight = 2f * Camera.main.orthographicSize;
        cameraWidth = cameraHeight * Camera.main.aspect;

        FixCameraCollider();
        FixRoomCollider();
    }
    public void Update()
    {
        if (CheckIfPlayerEnterRoom() )
        {
            cinemachineVirtualCamera.SetActive(true);
            if (cinemachineVirtualCamera.GetComponent<CinemachineVirtualCamera>().Follow == null)
            {
                cinemachineVirtualCamera.GetComponent<CinemachineVirtualCamera>().Follow = GameObject.Find("Player(Clone)").transform;
            }
        }
        else
        {
            cinemachineVirtualCamera.SetActive(false);
        }
    }
    public void  FixCameraCollider()
    {
        Vector2 point1 = new Vector2(-(roomSize.x + (lockLeft ? 0 : 1) * cameraWidth) / 2, (roomSize.y + (lockUp ? 0 : 1) * cameraHeight) / 2);
        Vector2 point2 = new Vector2((roomSize.x + (lockRight ? 0 : 1) * cameraWidth) / 2, (roomSize.y + (lockUp ? 0 : 1) * cameraHeight) / 2);
        Vector2 point3 = new Vector2((roomSize.x + (lockRight ? 0 : 1) * cameraWidth) / 2, -(roomSize.y + (lockDown ? 0 : 1) * cameraHeight) / 2);
        Vector2 point4 = new Vector2(-(roomSize.x + (lockLeft ? 0 : 1) * cameraWidth) / 2, -(roomSize.y + (lockDown ? 0 : 1) * cameraHeight) / 2);
        Vector2[] elements = { point1, point2, point3, point4 };
        cameraCollider.SetPath(0, elements);
    }
    public void FixRoomCollider()
    {
        Vector2 point1 = new Vector2(-roomSize.x / 2, roomSize.y / 2);
        Vector2 point2 = new Vector2(roomSize.x / 2, roomSize.y / 2);
        Vector2 point3 = new Vector2(roomSize.x / 2, -roomSize.y / 2);
        Vector2 point4 = new Vector2(-roomSize.x / 2, -roomSize.y / 2);
        Vector2[] elements = { point1, point2, point3, point4 };
        roomCollider.SetPath(0, elements);
    }
    public bool CheckIfPlayerEnterRoom()
    {
        return Physics2D.OverlapBox(transform.position, roomSize, 0, whatIsPlayer);
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, (Vector3)roomSize);
    }
}
