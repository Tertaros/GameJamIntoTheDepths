using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PlayerViewController : MonoBehaviour
{
    private Camera m_charactorCamera;

    [Header("Parameters")]
    public float m_rotateSpeed;

    private Vector3 m_refCameraPos;
    private float m_rotationUpDown = 0.0f;
    private float m_rotationLeftRight = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_charactorCamera = GetComponentInChildren<Camera>();
        if (!m_charactorCamera)
        {
            Debug.LogError("Charactor camera is not assigned! ");
        }
        m_refCameraPos = m_charactorCamera.transform.position - transform.position;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentMousePos = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector2 mouseRotation = currentMousePos * Time.deltaTime * m_rotateSpeed;

        m_rotationUpDown -= mouseRotation.y;
        m_rotationUpDown = Mathf.Clamp(m_rotationUpDown, -89.9f, 89.9f);
        m_rotationLeftRight += mouseRotation.x;
        m_charactorCamera.transform.localRotation = Quaternion.Euler(m_rotationUpDown, 0, 0);
        m_charactorCamera.transform.position = transform.position + m_refCameraPos;

        transform.rotation = Quaternion.Euler(0, m_rotationLeftRight, 0);
    }
}
