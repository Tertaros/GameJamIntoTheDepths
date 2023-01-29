using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinController : MonoBehaviour
{
    [Header("Parameters")]
    public Vector3 rotationAxis = new Vector3(0, 0, -1);
    public float rotationSpeed = 90f;
    public AudioSource m_moveSound;
    public GameObject image;
    private bool m_moving = false;
    private Quaternion startingRotation;

    // Start is called before the first frame update
    void Start()
    {
        startingRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_moving)
        {
            transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
            if (Quaternion.Angle(transform.rotation, Quaternion.AngleAxis(rotationSpeed, rotationAxis) * startingRotation) < 0.5f)
            {
                m_moving = false;
                m_moveSound.Play();
                image.GetComponent<DrawerController>().Interact();
            }
        }
    }

    public void Interact()
    {
        m_moving = true;
    }

}
