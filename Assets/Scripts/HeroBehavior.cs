using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// This script controls the player 'hero' movement
/// </summary>
public class HeroBehavior : MonoBehaviour {
  // components
  Rigidbody2D RB;
  [SerializeField]
  UIAPI uiapi;

  // speed modifierto preserve base speed
  [SerializeField]
  float HeroSpeedMultiplier = 1f;
  float mHeroSpeed = 20f;
  const float kHeroRotateSpeed = 100f;
  bool heroFreeze;

  // control type
  bool useMouse;
  Vector3 mousePosition;
  const float mouseMoveSpeed = 1f;
  
  void Start() {
    uiapi = GameObject.Find("Canvas").GetComponent<UIAPI>();
    RB = GetComponent<Rigidbody2D>();
  }

  /// <summary>
  /// because I am controlling the movement with the rigidbody,
  /// if needs to be done in the fixedupdate method.  However,
  /// input can be collected in update.
  /// </summary>
  private void FixedUpdate() {
    if (!useMouse) {
      UpdateMotion();
    }
    UpdateRotation();
  }

  private void Update() {
    // wasd/ arrow
    if (!useMouse) {
      // speed up
      if (Input.GetKey(KeyCode.UpArrow)) {
        if (HeroSpeedMultiplier == 0) {
          HeroSpeedMultiplier = 1;
        }
        HeroSpeedMultiplier += 1 * Time.smoothDeltaTime;
      }
      // slow down
      if (Input.GetKey(KeyCode.DownArrow)) {
        if (HeroSpeedMultiplier > 1) {
          HeroSpeedMultiplier -= 1 * Time.smoothDeltaTime;
        }
      }
    }

    // mouse control
    if (useMouse) {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = Vector2.Lerp(transform.position, mousePosition, mouseMoveSpeed);
    }

    // stop motion
    if (Input.GetKeyDown(KeyCode.P)) {
      if (!heroFreeze) {
        HeroSpeedMultiplier = 0;
      } else {
        HeroSpeedMultiplier = 1f;
      }
      heroFreeze = !heroFreeze;
    }

    // change control
    if (Input.GetKeyDown(KeyCode.M)) {
      if (useMouse) {
        useMouse = false;
        uiapi.SetHeroMovement("WASD");
      } else {
        useMouse = true;
        uiapi.SetHeroMovement("Mouse");
      }
    }
  }

  // player motion updates
  private void UpdateMotion() {
    RB.MovePosition(transform.position + (transform.up * mHeroSpeed * HeroSpeedMultiplier * Time.smoothDeltaTime));
  }

  private void UpdateRotation() {
    RB.MoveRotation(RB.rotation + Input.GetAxis("Horizontal") * -1 * (kHeroRotateSpeed * Time.smoothDeltaTime));
  }

  public void Reflect(Vector3 normal) {
    transform.up = Vector2.Reflect(transform.up, normal);
  }

  

}
