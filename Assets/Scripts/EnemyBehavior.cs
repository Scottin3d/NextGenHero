using UnityEngine;
using System.Collections;

/// <summary>
/// This script contols the behaviour the 'enemy'
/// mostly movement
/// </summary>
public class EnemyBehavior : MonoBehaviour {
  // enemy speed
	public float eSpeed = 25f;

  // components
  private Rigidbody2D RB;

	// Use this for initialization
	void Start () {
    RB = GetComponent<Rigidbody2D>();
	}

  /// <summary>
  /// because I am controlling the movement with the rigidbody,
  /// if needs to be done in the fixedupdate method.  However,
  /// input can be collected in update.
  /// </summary>
  void FixedUpdate () {
    UpdateMotion();
  }

  // updates the position of the enemy
  private void UpdateMotion() {
    RB.MovePosition(transform.position + (transform.TransformDirection(Vector3.up) * eSpeed * Time.deltaTime));
  }
}
