using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomBoundary : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        var ghost = col.gameObject.GetComponent<Enemy>();
        if (ghost != null)
            ghost.Remove();
    }
}
