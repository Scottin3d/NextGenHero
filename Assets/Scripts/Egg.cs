using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is the main behavior for the egg projectile
/// spawned by the player.
/// </summary>
public class Egg : MonoBehaviour {
  public UIAPI uiapi;
  // projectile speed
  public float Speed = 40f;

  private Vector2 ScreenBounds;
  private float SH;
  private float SW;

  // components 
  Rigidbody2D RB;

  

  private void Start() {
    //uiapi = GameObject.Find("Canvas").GetComponent<UIAPI>();

    SH = Camera.main.orthographicSize;
    SW = SH * Camera.main.aspect;

    ScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

    RB = GetComponent<Rigidbody2D>();
    RB.velocity = transform.up * Speed;
    uiapi.IncEgg();

  }

  void LateUpdate() {
    Vector3 ViewPosition = transform.position;
    ViewPosition.x = Mathf.Clamp(ViewPosition.x, ScreenBounds.x * -1, ScreenBounds.x);
    ViewPosition.y = Mathf.Clamp(ViewPosition.y, ScreenBounds.y * -1, ScreenBounds.y);

    float X = transform.transform.position.x;
    float Y = transform.transform.position.y;

    if (X <= -SW || X >= SW || Y <= -SH || Y >= SH) {
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
