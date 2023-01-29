using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerController : MonoBehaviour
{
    [Header("Parameters")]
    public Vector3 m_openDirectionLocal = new Vector3(0,0,-1);
    public float m_openDistance = 0.5f;
    public float m_speed = 1;
    public AudioSource m_moveSound;
    public Vector3 OrgPosition
    {
        get => m_orgPosition;
    }
    public Vector3 TargPosition {
        get { return m_orgPosition + m_openDistance * m_openDirectionLocal; }
    }

    private Vector3 m_orgPosition;

    private bool m_opened = false;
    private bool m_moving = false;

    // Start is called before the first frame update
    void Start()
    {
        m_openDirectionLocal.Normalize();
        m_openDirectionLocal = transform.TransformDirection(m_openDirectionLocal);
        m_orgPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_moving)
        {
            if(!m_opened)
            {
                if(MoveTowards(m_orgPosition, TargPosition))
                {
                    m_moving = false;
                    m_opened = true;
                }
            }
            else
            {
                if (MoveTowards(TargPosition, m_orgPosition))
                {
                    m_moving = false;
                    m_opened = false;
                }
            }
        }
    }

    public void Interact()
    {
        m_moving = true;
        m_moveSound.Play();
    }

    bool MoveTowards(Vector3 posOrg, Vector3 posTarg)
    {
        bool reachedPosition = false;

        Vector3 dirMove = (posTarg - posOrg).normalized;
        Vector3 dirToTarg = (posTarg - gameObject.transform.position).normalized;

        if(Vector3.Dot(dirToTarg, dirMove)>0)
        {
            gameObject.transform.position = gameObject.transform.position + dirToTarg * m_speed * Time.deltaTime;
            Vector3 newDirToTarg = (posTarg - gameObject.transform.position).normalized;
            if (Vector3.Dot(dirToTarg, dirMove) <= 0)
            {
                gameObject.transform.position = posTarg;
                reachedPosition = true;
            }
        }
        else
        {
            gameObject.transform.position = posTarg;
            reachedPosition = true;
        }

        return reachedPosition;
    }
}
