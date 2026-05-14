using System.Collections.Generic;
using UnityEngine;

public class StudyPuzzle : Puzzle
{
    [SerializeField] List<GameObject> objectsToActivateWhenSolved = new();

    protected override void Awake()
    {
        base.Awake();
        if (objectsToActivateWhenSolved.Count <= 0 )
            return;
        foreach (var item in objectsToActivateWhenSolved)
            item.SetActive(true);
    }
    protected override void OnSolved()
    {
        base.OnSolved();
        if (objectsToActivateWhenSolved.Count <= 0)
            return;
        foreach (var item in objectsToActivateWhenSolved)
            item.SetActive(true);
    }
    protected override void OnFailed()
    {
        base.OnFailed();
    }
}
