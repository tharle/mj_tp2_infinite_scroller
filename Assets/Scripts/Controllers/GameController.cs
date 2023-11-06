using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Session m_Session = Session.SPRING;

    public Session getCurrentSession()
    {
        return m_Session;
    }
}
