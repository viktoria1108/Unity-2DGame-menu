using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Level Segment", fileName = "New Level Segment")]
public class LevelSegmentSO : ScriptableObject
{
    public const int maxLevelType = 5;
    [Range(1, maxLevelType)]
    public int levelType; 
    [Tooltip("All the objecets must be in one parent")]
    public Segment segmentPrefab;
    [Space(10)]
    [Tooltip("Use only if you want to do a spesific thing after applying the segment")]
    public UnityEvent OnSegmentApply;
}
