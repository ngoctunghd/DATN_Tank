using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class PlaySoundOnClick : MonoBehaviour,IPointerDownHandler, IPointerUpHandler
{

    [SerializeField]
    private AudioSource m_audioSource;
    [SerializeField]
    private AudioClip clipClick;

    private string isMusic;

    private void Awake()
    {

        isMusic = PlayerPrefs.GetString("Music");

        if (isMusic == "Off")
        {
            m_audioSource.mute = true;
        }
        else
        {
            m_audioSource.mute = false;
        }
    }

    void Update()
    {
        isMusic = PlayerPrefs.GetString("Music");

        if (isMusic == "Off")
        {
            m_audioSource.mute = true;
        }
        else
        {
            m_audioSource.mute = false;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        m_audioSource.PlayOneShot(clipClick);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }
}
    

