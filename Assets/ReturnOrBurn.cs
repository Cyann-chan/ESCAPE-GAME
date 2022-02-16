using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnOrBurn : MonoBehaviour
{
    [SerializeField] Transform m_fireTransform;
    [SerializeField] float m_distanceTreshold = 0.8f; 
    [SerializeField] float m_timeBeforeAction = 2;
    private Vector3 m_initialPosition;
    private Quaternion m_initialRotation;


    // Start is called before the first frame update
    void Start()
    {
        m_initialPosition = transform.position;
        m_initialRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Dropped()
    {
        StartCoroutine(DroppedCoroutine());
    }

    IEnumerator DroppedCoroutine()
    {
        yield return new WaitForSeconds(m_timeBeforeAction);
        if(Vector3.Distance(m_fireTransform.position, transform.position) < m_distanceTreshold)
        {
            //Burn
            gameObject.SetActive(false);
        } else
        {
            //return
            transform.SetPositionAndRotation(m_initialPosition, m_initialRotation);
        }
    }
}
