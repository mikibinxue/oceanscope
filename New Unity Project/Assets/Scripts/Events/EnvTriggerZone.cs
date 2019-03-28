using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvTriggerZone : MonoBehaviour
{
    [SerializeField] Transform m_theAvatar;
    [SerializeField] Transform m_theDirection;
    [SerializeField] LandingSpotController m_theController;
    [SerializeField] FlockController m_theFlock;
    bool isIn = false;
    [SerializeField] int nCountDownSeconds = 6; //5Sec. 
    int m_nCountDown;
    //Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform == m_theAvatar)
        {
            m_nCountDown = nCountDownSeconds;
            m_theController.ScareAll();
            if(!isIn)
            {
                isIn = true;
                StartCoroutine(FlyTowards());
            }            
        }
    }
    IEnumerator FlyTowards()
    {
        while (m_nCountDown > 0)
        {
            m_theFlock.SetFlockDirection(m_theDirection.position);
            if (!isIn)
                m_nCountDown--;
            yield return new WaitForSeconds(0.5f);
        }
        m_theController.LandAll();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.transform == m_theAvatar)
        {
            //
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform == m_theAvatar)
        {
            isIn = false;
        }
    }
}
