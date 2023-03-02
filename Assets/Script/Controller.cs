using UnityEngine;

[RequireComponent(typeof(Rigidbody)), //rigidbody必須
DisallowMultipleComponent]
public class Controller : MonoBehaviour {
  [SerializeField, HeaderAttribute("PlayerState")]
  private float moveSpeed = 3f;

  private float inputHorizontal;
  private float inputVertical;
  private Rigidbody rb;


  
  void Start() {
    rb = GetComponent<Rigidbody>();
    Cursor.visible = false;
    Screen.lockCursor = true;
  }
  
  void Update() {
    inputHorizontal = Input.GetAxisRaw("Horizontal");
    inputVertical = Input.GetAxisRaw("Vertical");
    Jump();
  }
  
  void FixedUpdate() {

    Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

    Vector3 moveForward = cameraForward * inputVertical + Camera.main.transform.right * inputHorizontal;

    rb.velocity = moveForward * moveSpeed + new Vector3(0, rb.velocity.y, 0);

    if (moveForward != Vector3.zero) {
      transform.rotation = Quaternion.LookRotation(moveForward);
    }
  }

  private bool jumpNow = false;
  [SerializeField]
  private float jumpPower;
  private void OnCollisionEnter(Collision other) {
    if (jumpNow == true) {
      if (other.gameObject.CompareTag("ground")) {
        jumpNow = false;
      }
    }
  }
  //最低限 ジャンプ用
  void Jump() {
    if (jumpNow == true) return;
    if (Input.GetKeyDown(KeyCode.Space)) {
      rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
      jumpNow = true;
    }
  }
}