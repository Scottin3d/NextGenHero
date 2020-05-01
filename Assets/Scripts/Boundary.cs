using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour {
  public Camera MainCamera;
  /* I could have not used this, however it is a helluva lot easier to just use a gameobject
     instead of doing vector math.  Using a gameobject to detect edges eliminated the need to
     account for the size of the sprite.  As long as the sprite is moving in a forward direction
     it will detect the edge of the camera properly.
  */
  public GameObject Head;
  public float ReflectAmound = 90;

  private Vector2 ScreenBounds;
  private float SW;
  public float X;
  private float SH;
  public float Y;

  
  // Start is called before the first frame update
  void Start() {

    SH = MainCamera.orthographicSize;
    SW = SH * MainCamera.aspect;

    ScreenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));

  }

  // Update is called once per frame
  void LateUpdate() {
    Vector3 ViewPosition = transform.position;
    ViewPosition.x = Mathf.Clamp(ViewPosition.x, ScreenBounds.x * -1, ScreenBounds.x);
    ViewPosition.y = Mathf.Clamp(ViewPosition.y, ScreenBounds.y * -1, ScreenBounds.y);

    X = Head.transform.position.x;
    Y = Head.transform.position.y;

    if (X <= -SW|| X >= SW|| Y <= -SH|| Y >= SH) {
      Debug.Log("Reflect");
      transform.localRotation *= Quaternion.Euler(0, 0, ReflectAmound);
    }

    transform.position = ViewPosition;

  }
}
