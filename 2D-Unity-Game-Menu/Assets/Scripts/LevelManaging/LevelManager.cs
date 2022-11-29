using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    public float startPoint, endPoint;
    public static LevelManager instance;
    public float levelSpeed;
    public bool HasLoaded{get; private set;} = false;
    
    private int levelType = 1;
    public int[] TypeLengths{get; private set;} 
    
    private void Awake() {
        if(instance != null){
            Destroy(gameObject);
            return;
        }else instance = this;
    }

    private void Start() {
        TypeLengths = new int[LevelSegmentSO.maxLevelType];
        for(int i = 0; i < TypeLengths.Length; i++){
            var temp = SegmentApplier.Instance._segments.Where(x => x.levelType == i + 1);
            if(temp.Count() == 0) continue;
            SegmentApplier.Instance.segments.Add(temp);
            TypeLengths[i] = temp.Count();
        }
        HasLoaded = true;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(new Vector3(startPoint, 0f, 0f), 0.15f);
        Gizmos.DrawSphere(new Vector3(endPoint, 0f, 0f), 0.15f);
    }
}