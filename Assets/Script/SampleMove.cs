using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AreaMoveScript)),
DisallowMultipleComponent]
public class SampleMove : MonoBehaviour
{
    [SerializeField]
    private float MoveSize = 2.0f;
    [SerializeField]
    private AreaMoveScript area;

    [SerializeField]
    private float MoveSpeed = 1.0f;

    //とりあえずsin波でいい感じに往復しとく
    void Update() {
        var move = AddSin();
        transform.position += new Vector3(move, 0, 0);
        area.SetCenterToThis();
        GameObject[] hits = area.getHitCastAll();
        foreach (var hit in hits) {
            hit.transform.position += new Vector3(move, 0, 0);
        }

    }


    private float oldSin = 0;
    private float AddSin() {
        float sin = Mathf.Sin(2 * Mathf.PI * MoveSpeed * Time.time);
        float data = sin - oldSin;
        oldSin = sin;
        return data * MoveSize;
    }
}
