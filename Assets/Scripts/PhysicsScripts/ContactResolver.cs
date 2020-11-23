using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ContactResolver
{
    public static bool ContactTest(GameObject first, GameObject second)
    {
        if (Vector3.Distance(first.transform.position, second.transform.position) < 1.0f)
        {
            return true;
        }

        return false;
    }
}
