using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadMarker : MonoBehaviour
{
    [SerializeField] GameCtrl m_theCenter;
    [SerializeField] Transform m_trCraneHead;
    [SerializeField] Transform m_trCamera;

    // Start is called before the first frame update
    void Start()
    {
        m_theCenter.GameEvent.AddListener(OnStatusChanged);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnStatusChanged(GameEvent.GameStatus incoming)
    {
        switch (incoming)
        {
            case GameEvent.GameStatus.CALIBRATING:

                Vector3 vecLocal = transform.localPosition;
                Quaternion quatLocal = transform.localRotation;
                //transform.parent = m_trHeadMarker;
                //m_trHeadMarker.parent = m_trCamera;
                //m_trHeadMarker.localPosition = Vector3.zero;
                //m_trHeadMarker.localRotation = Quaternion.identity;
                //transform.localPosition = vecLocal;
                //transform.localRotation = quatLocal;
                //transform.parent = m_theCenter.transform;
                /////////////////////////////////////////////////
                transform.parent = m_trCamera;
                transform.localPosition = vecLocal;
                transform.localRotation = quatLocal;
                transform.parent = m_theCenter.transform;
                break;
            default:
                break;
        }
    }

}
