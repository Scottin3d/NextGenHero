using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflect : MonoBehaviour {
  [SerializeField]
  HeroBehavior heroBehavior;

  private void Start() {
    heroBehavior = transform.parent.GetComponent<HeroBehavior>();
  }

  
  private void OnTriggerEnter2D(Collider2D collision) {
    if (collision.gameObject.tag == "Edge") {
      heroBehavior.Reflect();
    }
  }
}
