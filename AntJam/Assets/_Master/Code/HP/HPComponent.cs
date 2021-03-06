﻿using UnityEngine;
using System.Collections;

public class HPComponent : MonoBehaviour
{
    [SerializeField]
	protected HP m_hp;

    public HP hp { get { return m_hp; } }
    public bool isAlive { get { return m_hp.currentHp > 0; } }
	public bool isHurt { get; set; }

    protected virtual void Update()
    {
        if (hp.currentHp <= 0)
        {
            Debug.Log("Dead: " + name);
            enabled = false;
			gameObject.SetActive (false);
        }
    }
}
