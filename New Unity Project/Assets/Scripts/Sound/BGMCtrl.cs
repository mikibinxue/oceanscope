using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMCtrl : MonoBehaviour
{
    [SerializeField] GameCtrl m_theCenter;
    [SerializeField] AudioClip[] m_bgmList;
    [SerializeField] AudioSource m_as;
    bool m_hasStarted = false;
    int m_nCurrentIndex = 1;
    // Start is called before the first frame update
    void Start()
    {
        m_theCenter.GameEvent.AddListener(OnStatusChanged);
    }

    // Update is called once per frame
    void Update()
    {
        if(m_hasStarted)
        {
            if (!m_as.isPlaying)
            {
                m_nCurrentIndex = (m_nCurrentIndex + 1) % m_bgmList.Length;
                m_as.clip = m_bgmList[m_nCurrentIndex];
                m_as.Play();
            }
        }
    }

    void OnStatusChanged(GameEvent.GameStatus incoming)
    {
        switch (incoming)
        {
            case GameEvent.GameStatus.CALIBRATING:
                m_hasStarted = true;
                break;
            default:
                break;
        }
    }

}
