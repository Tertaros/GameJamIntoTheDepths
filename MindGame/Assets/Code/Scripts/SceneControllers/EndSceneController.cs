using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class EndSceneController : MonoBehaviour
{
    [Header("Parameters")]
    public RectTransform m_canvas;
    public RectTransform m_panelEndScene;
    public RectTransform m_panelCredit;
    public RectTransform m_panelBG;

    public float m_timeBeforeRolling = 3f;
    public float m_rollingSpeed = 50.0f;
    public float m_timeAfterCreditInPlace = 3f;

    private float m_curTimeBeforeRolling = 0.0f;
    private bool m_hasFinished = false;
    private float m_curTimeAfterCreditInPlace = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (m_canvas == null)
        {
            Debug.LogError("Panel is missing! ");
        }
        if (m_panelEndScene == null)
        {
            Debug.LogError("Panel is missing! ");
        }
        if (m_panelCredit == null)
        {
            Debug.LogError("Panel is missing! ");
        }
        if (m_panelBG == null)
        {
            Debug.LogError("Panel is missing! ");
        }

        m_curTimeBeforeRolling = 0.0f;
        m_hasFinished = false;
        m_curTimeAfterCreditInPlace = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        bool toRoll = false;

        if (m_curTimeBeforeRolling < m_timeBeforeRolling)
        {
            m_curTimeBeforeRolling += Time.deltaTime;
        }
        else if (m_hasFinished)
        {
            if (m_curTimeAfterCreditInPlace < m_timeAfterCreditInPlace)
            {
                m_curTimeAfterCreditInPlace += Time.deltaTime;
            }
            else
            {
                OnEnd();
            }
        }
        else
        {
            toRoll = true;
        }

        if (toRoll)
        {
            bool hasFinished = true;

            if (!CheckEndScenePanelIsAbove())
            {
                Vector3 pos = m_panelEndScene.localPosition;
                pos.y = pos.y + m_rollingSpeed * Time.deltaTime;
                m_panelEndScene.localPosition = pos;

                hasFinished = false;
            }

            if (!CheckCreditPanelIsCentered())
            {
                Vector3 pos = m_panelCredit.localPosition;
                pos.y = pos.y + m_rollingSpeed * Time.deltaTime;
                m_panelCredit.localPosition = pos;

                hasFinished = false;
            }
            else
            {
                Vector3 pos = m_panelCredit.localPosition;
                pos.y = 0f;
                m_panelCredit.localPosition = pos;
            }

            m_hasFinished = hasFinished;
        }
    }

    private bool CheckEndScenePanelIsAbove()
    {
        if (m_panelEndScene.localPosition.y - m_panelEndScene.rect.height / 2.0f > m_canvas.rect.height / 2.0f)
        {
            return true;
        }
        return false;
    }

    private bool CheckCreditPanelIsCentered()
    {
        if (m_panelCredit.localPosition.y >= 0f)
        {
            return true;
        }
        return false;
    }

    private void OnEnd()
    {
        GameplayManager.Instance.LoadMainMenu();
    }
}
