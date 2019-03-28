using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crane : MonoBehaviour
{
    [SerializeField] GameCtrl m_theCenter;
    [SerializeField] Transform m_trCraneHead;
    float m_fHeadDistance;
    [SerializeField] Transform m_trCamera;
    //IK Related
    [SerializeField] Animator m_animator;
    [SerializeField] Transform m_trLeftTrig;
    [SerializeField] Transform m_trRightTrig;

    // Start is called before the first frame update
    void Start()
    {
        m_theCenter.GameEvent.AddListener(OnStatusChanged);
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void OnAnimatorIK(int layerIndex)
    {
        if (m_theCenter.GameEvent.Current == GameEvent.GameStatus.PLAY)
        {
            m_animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
            m_animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
            m_animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
            m_animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);

            m_animator.SetIKPosition(AvatarIKGoal.LeftHand, m_trLeftTrig.position);
            m_animator.SetIKRotation(AvatarIKGoal.LeftHand, m_trLeftTrig.rotation);
            m_animator.SetIKPosition(AvatarIKGoal.RightHand, m_trRightTrig.position);
            m_animator.SetIKRotation(AvatarIKGoal.RightHand, m_trRightTrig.rotation);
        }
    }

    void OnStatusChanged(GameEvent.GameStatus incoming)
    {
        switch (incoming)
        {
            case GameEvent.GameStatus.CALIBRATING:
                //1. Swap transform if taken wrong one.
                if(m_trCamera.localPosition.x - m_trLeftTrig.parent.localPosition.x < 0 && m_trCamera.localPosition.x - m_trRightTrig.parent.localPosition.x > 0)
                {
                    //Yan: Swap!!
                    GameObject go = m_trLeftTrig.gameObject;
                    m_trLeftTrig = m_trRightTrig.gameObject.transform;
                    m_trRightTrig = go.transform;
                }
                else
                {
                    Debug.Log("LeftX:" + m_trLeftTrig.parent.localPosition.x + " RightX:" + m_trRightTrig.parent.localPosition.x);
                }
                //2. Calibrate Avatar.
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
