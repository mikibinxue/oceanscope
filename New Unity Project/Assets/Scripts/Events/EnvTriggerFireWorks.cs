using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvTriggerFireWorks : MonoBehaviour
{
    [SerializeField] ParticleSystem m_fireWorks;
    [SerializeField] Transform m_theAvatar;

    // Start is called before the first frame update
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
            if (!m_fireWorks.isPlaying)
                m_fireWorks.Play();
        }
    }
}
