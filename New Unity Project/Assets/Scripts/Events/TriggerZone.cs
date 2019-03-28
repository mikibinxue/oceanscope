using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    [SerializeField] Transform m_trLeft;
    [SerializeField] Transform m_trRight;
    [SerializeField] Transform m_trCamera;
    [SerializeField] Transform m_trRoot;
    [SerializeField] GameCtrl m_theCenter;
    [SerializeField] WingsEvent.WingStatus m_itsStatus;
    [SerializeField] bool m_isLeft;
    Color m_color;
    Color m_yellow = new Color(1,1,0,0.5f);
    int m_nTimeCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        m_color = GetComponent<Renderer>().material.color;
        m_theCenter.GameEvent.AddListener(OnStatusChanged);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_itsStatus != WingsEvent.WingStatus.FORWARD && m_itsStatus != WingsEvent.WingStatus.BACK)
        {
            if (other.name == m_trLeft.name || other.name == m_trRight.name)
            {
                GetComponent<Renderer>().material.color = Color.yellow;
                m_theCenter.WingsEvent.Invoke(m_isLeft, m_itsStatus);
            }
        //    else
        //        Debug.Log(other.name);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.name == m_trLeft.name || other.name == m_trRight.name)
        {
            if (m_itsStatus == WingsEvent.WingStatus.FORWARD)
            {
                if (m_isLeft)
                    m_theCenter.m_nLeftForward = 1;
                else
                    m_theCenter.m_nRightForward = 1;
            }
            else if(m_itsStatus == WingsEvent.WingStatus.BACK)
            {
                if (m_isLeft)
                    m_theCenter.m_nLeftForward = -1;
                else
                    m_theCenter.m_nRightForward = -1;
            }
        }
    //    else
    //        Debug.Log(other.name);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform == m_trLeft || other.gameObject.transform == m_trRight)
        {
            if (m_itsStatus == WingsEvent.WingStatus.FORWARD)
            {
                if (m_isLeft)
                    m_theCenter.m_nLeftForward = 0;
                else
                    m_theCenter.m_nRightForward = 0;
            }
            else if (m_itsStatus == WingsEvent.WingStatus.BACK)
            {
                if (m_isLeft)
                    m_theCenter.m_nLeftForward = 0;
                else
                    m_theCenter.m_nRightForward = 0;
            }
            GetComponent<Renderer>().material.color = m_color;
        }
    }

    void OnStatusChanged(GameEvent.GameStatus incoming)
    {
        switch (incoming)
        {
            case GameEvent.GameStatus.CALIBRATING:
                Vector3 vecLocal = transform.localPosition;
                Quaternion quatLocal = transform.localRotation;
                transform.parent = m_trCamera;
                transform.localPosition = vecLocal;
                transform.localRotation = quatLocal;
                transform.parent = m_trRoot;
                break;
            default:
                break;
        }
    }
}
