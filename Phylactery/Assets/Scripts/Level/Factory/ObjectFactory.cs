using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFactory
{
    private static ObjectFactory _instance;
    public static ObjectFactory Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ObjectFactory();
            }

            return _instance;
        }
    }

    private ObjectFactory()
    {

    }
}
