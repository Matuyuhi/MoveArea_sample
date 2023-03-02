using UnityEngine;

[RequireComponent(typeof(Camera)),//camera必須
DisallowMultipleComponent]
public class PlayerCamera : MonoBehaviour {

  [SerializeField, HeaderAttribute("PlayerObject")]
  private GameObject targetObj;
  [SerializeField, Range(1,20), HeaderAttribute("Camera Move Distanse")]
  private float moveUp = 1.0f;
  [SerializeField, Range(1,20)]
  private float moveSide = 1.0f;

  [ContextMenu("Init")]
  private void Init(){
  }

  private Vector3 targetPos;
  private bool isActive = true;
 
  void Start () {
    targetPos = targetObj.transform.position;
  }
  
  void Update() {

    transform.position += targetObj.transform.position - targetPos;
    targetPos = targetObj.transform.position;


    if (isActive) {

      float mouseInputX = Input.GetAxis("Mouse X");
      float mouseInputY = Input.GetAxis("Mouse Y");

      transform.RotateAround(targetPos, Vector3.up, mouseInputX * Time.deltaTime * 200f * moveUp);

      transform.RotateAround(targetPos, transform.right, mouseInputY * Time.deltaTime * 200f * moveSide);
    }
  } 
}// PlayerCamera Class