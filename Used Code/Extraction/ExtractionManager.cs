using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtractionManager : MonoBehaviour
{
    [SerializeField] List<Transform> mychildren;
    [SerializeField] GameObject extractionPointPrefab;
    private void Start() {
        int chosenPoint = Random.Range(0,(mychildren.Count - 1) * 100);
        if(chosenPoint <= 75){chosenPoint = 0;}
        else if(chosenPoint <= 125){chosenPoint = 1;}
        else if(chosenPoint <= 200){chosenPoint = 2;}

        Instantiate(extractionPointPrefab,mychildren[chosenPoint].position,Quaternion.identity);
        
    }
}
