using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is the main behavior for the egg projectile
/// spawned by the player.
/// </summary>
public class Egg : MonoBehaviour {
  // components 
  Rigidbody2D RB;
  [SerializeField]
  UIAPI uiapi;

  public float Speed = 40f;

  private Vector2 ScreenBounds;
  private float screenHeight;
  private float screenWidth;

  private void Start() {
    uiapi = GameObject.Find("Canvas").GetComponent<UIAPI>();
    RB = GetComponent<Rigidbody2D>();
    RB.velocity = transform.up * Speed;

    screenHeight = Camera.main.orthographicSize;
    screenWidth = screenHeight * Camera.main.aspect;

    ScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

    uiapi.IncEgg();
  }

  void LateUpdate() {
    Vector3 ViewPosition = transform.position;
    ViewPosition.x = Mathf.Clamp(ViewPosition.x, ScreenBounds.x * -1, ScreenBounds.x);
    ViewPosition.y = Mathf.Clamp(ViewPosition.y, ScreenBounds.y * -1, ScreenBounds.y);

    float X = transform.transform.position.x;
    float Y = transform.transform.position.y;

    if (X <= -screenWidth || X >= screenWidth || Y <= -screenHeight || Y >= screenHeight) {
      uiapi.DecEgg();
      Destroy(gameObject);
    }

  }

  // if the egg project collides with the 'enemy', trigger particle effectrs
  private void OnTriggerEnter2D(Collider2D collision) {
    if (collision.gameObject.tag == "Enemy") {
      Debug.Log("Hit enemy");
      uiapi.DecEgg();
      Destroy(gameObject);
    }
    if (collision.gameObject.tag == "Waypoint") {
      Debug.Log("Hit Waypoint");
      uiapi.DecEgg();
      Destroy(gameObject);
    }
  }
}
