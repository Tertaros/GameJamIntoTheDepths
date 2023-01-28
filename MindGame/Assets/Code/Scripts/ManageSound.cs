using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(List<AudioSource>))]
[RequireComponent(typeof(List<AudioSource>))]
public class ManageSound : MonoBehaviour
{
    public List<AudioSource> m_music;
    public List<AudioSource> m_sfx;

    public float m_musicVolume = 1f;
    public float m_sfxVolume = 1f;

    void Update()
    {
        foreach (AudioSource m in m_music)
            m.volume = m.maxDistance * m_musicVolume;
        foreach (AudioSource m in m_sfx)
            m.volume = m.maxDistance * m_sfxVolume;
    }
}
