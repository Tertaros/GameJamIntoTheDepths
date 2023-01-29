using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class IntroSceneController : MonoBehaviour
{
    public VideoPlayer m_videoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        if(!m_videoPlayer)
        {
            Debug.LogError("VideoPlayer is missing!");
        }
        else
        {
            m_videoPlayer.loopPointReached += OnVideoPlayerEnd;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnVideoPlayerEnd(VideoPlayer videoPlayer)
    {
        GameplayManager.Instance.LoadRoom1();
    }
}
