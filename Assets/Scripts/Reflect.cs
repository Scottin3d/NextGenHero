using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflect : MonoBehaviour {
  [SerializeField]
  HeroBehavior heroBehavior;

  private void Start() {
    heroBehavior = transform.parent.GetComponent<HeroBehavior>();
  }

  private void OnCollisionEnter2D(Collision2D collision) {
    if (collision.gameObject.name.Contains("Edge")) {
      heroBehavior.Reflect(collision.GetContact(0).normal);
    }
  }
}
