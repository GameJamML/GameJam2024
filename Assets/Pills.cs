using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pills : MonoBehaviour
{
    public static Action OnPillsPickedUp;
    bool pillsPickedUp = false;
    bool childSick = false;
    [SerializeField] private GameObject m_Pills;

    private void OnEnable()
    {
        ChildNeeds.OnChildSick += SetChildSick;
    }

    private void OnDisable()
    {
        ChildNeeds.OnChildSick -= SetChildSick;
    }

    private void Update()
    {
        if(childSick && !pillsPickedUp)
        {
            m_Pills.SetActive(true);
        }
        else
        {
            m_Pills.SetActive(false);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnPillsPickedUp?.Invoke();
            pillsPickedUp = true;
            m_Pills.SetActive(false);
        }
    }

    private void SetChildSick(bool value)
    {
        childSick = value;

        if(value == false)
        {
            pillsPickedUp = false;
        }
    }

}
