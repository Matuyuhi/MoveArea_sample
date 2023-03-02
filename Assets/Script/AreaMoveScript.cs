using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]//同一objectの複数付与を避ける
public class AreaMoveScript: MonoBehaviour
{
    [SerializeField]
    private Vector3 FieldCenter = Vector3.zero;
    [SerializeField,HeaderAttribute("判定範囲")]
    private Vector3 FieldSize = Vector3.zero;

    [ContextMenu("SetCenterToThis")]
    public void SetCenterToThis() {
        FieldCenter = transform.position;
    }

    public Vector3 getFieldCenter {
        get {
            return FieldCenter;
        }
    }
    private void Update() {
        foreach (var hit in getHitCastAll()) {
            Debug.Log(hit.name);
            
        }
        
    }

    public GameObject[] getHitCastAll() {
        // get on hit object for all
        var hits = Physics.BoxCastAll(
            FieldCenter, //        center
            FieldSize / 2, //      boxの半径
            Vector3.forward, //    castの向き
            transform.rotation, // 回転はobjに合わせる
            1.0f,
            LayerMask.GetMask("Default"));
        
        //Raycast は要らないからobjectだけ取り出す
        List<GameObject> hitObjects = new List<GameObject>();
        foreach (var hit in hits) {
            hitObjects.Add(hit.collider.gameObject);
        }

        //範囲内のobjをList->Arrayに変換して返す
        return hitObjects.ToArray();

        //以下　hitデータのデバック用
        // Debug.Log($"検出されたコライダーの数: {hits.Length}");

        // foreach (var hit in hits) {
        //         //指定tagのみ表示
        //     if (hit.collider.tag == "Player") {
        //         Debug.Log($"検出されたオブジェクト {hit.collider.name}");
        //     }
        // }
    }


    //option 
    // Editorでcast範囲描画
    private void OnDrawGizmos()
    {
        // ビュー上でループする範囲が見えるようにしておこう。
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube( FieldCenter, FieldSize );
    }
    
}


