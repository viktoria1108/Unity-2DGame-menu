using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class SegmentApplier : MonoBehaviour
{
    public static SegmentApplier Instance{get; private set;}

    public List<IEnumerable<LevelSegmentSO>> segments = new List<IEnumerable<LevelSegmentSO>>();
    public LevelSegmentSO[] _segments;

    private void Awake() {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }else Instance = this;
    }

    private void OnEnable() {
        WaitLoadAndSpawn();
        enabled = false;
    }

    private IEnumerator WaitLoadAndSpawn()
    {
        if(!LevelManager.instance.HasLoaded) yield return new WaitUntil(() => LevelManager.instance.HasLoaded);
        SpawnSegment();
    }

        private Segment GetNextSegment()
    {
        var typeIndex = 0; // Temporary 0 change this to current level type when implement the score bar system!!
        var nextSegment = segments[typeIndex].ElementAt(Random.Range(0, LevelManager.instance.TypeLengths[typeIndex])).segmentPrefab;
        return nextSegment;
    }
    public void SpawnSegment()
    {
        var segmentToSpawn = GetNextSegment();

        //var pos = new Vector3();

        Instantiate(segmentToSpawn);
    }

}