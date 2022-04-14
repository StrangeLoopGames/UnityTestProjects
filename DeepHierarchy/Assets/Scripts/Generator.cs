using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    private enum Mode
    {
        Root,
        Nested,
        NestedWithBase,
    }

    [SerializeField]
    private Mode mode = Mode.Root;

    [SerializeField] 
    private GameObject prefabToSpawn;

    [Header("Root")]
    [SerializeField]
    private int rootAmount = 1000;
    
    [Header("Nested")]
    [SerializeField]
    private int nestedRootAmount = 100;
    [SerializeField]
    private int nestedAmount = 10;

    [Header("Nested with base")]
    [SerializeField]
    private int baseAmount = 5;
    [SerializeField]
    private int nestedWithBaseRootAmount = 100;
    [SerializeField]
    private int nestedWithBaseAmount = 10;

    private void Start()
    {
        switch (mode)
        {
            case Mode.Root:
                GenerateRootObjects();
                break;
            case Mode.Nested:
                GenerateNestedObjects();
                break;
            case Mode.NestedWithBase:
                GenerateNestedWithBaseObjects();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void GenerateRootObjects()
    {
        Debug.Log($"Total objects created: {rootAmount}");
        
        for(int i = 0; i < rootAmount; i++)
        {
            GameObject newObj = Instantiate(prefabToSpawn);
            newObj.name = $"{prefabToSpawn.name}{i:000}";
        }
    }

    private void GenerateNestedObjects()
    {
        int totalObjects = nestedRootAmount * nestedAmount;
        Debug.Log($"Total objects created: {totalObjects}");
        
        for(int i = 0; i < nestedRootAmount; i++)
        {
            GameObject rootObject = Instantiate(prefabToSpawn);
            rootObject.name = $"{prefabToSpawn.name}{i:000}";
            
            //Create the nested objects
            GameObject previousObject = rootObject;
            for(int j = 0; j < nestedAmount; j++)
            {
                GameObject nestedObject = Instantiate(prefabToSpawn, previousObject.transform);
                previousObject = nestedObject;
            }
        }
    }

    private void GenerateNestedWithBaseObjects()
    {
        int totalObjects = (nestedRootAmount * nestedAmount) + baseAmount;
        Debug.Log($"Total objects created: {totalObjects}");

        //Create the static base objects
        GameObject root = null;
        for(int i = 0; i < baseAmount; i++)
        {
            GameObject newObj = new GameObject($"Base{i:00}");
            if(root != null)
                newObj.transform.parent = root.transform;
            
            root = newObj;
        }
        
        for(int i = 0; i < nestedRootAmount; i++)
        {
            GameObject rootObject = Instantiate(prefabToSpawn, root.transform);
            rootObject.name = $"{prefabToSpawn.name}{i:000}";
            
            //Create the nested objects
            GameObject previousObject = rootObject;
            for(int j = 0; j < nestedAmount; j++)
            {
                GameObject nestedObject = Instantiate(prefabToSpawn, previousObject.transform);
                previousObject = nestedObject;
            }
        }
    }
}