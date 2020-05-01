using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointTrigger : MonoBehaviour
{
  public float respawnTime = 1f;
  SpriteRenderer spriteRenderer;
  private Vector3 spawnPosition;
  private float opacity = 1.0f;
  Color sprite;
  // Start is called before the first frame update
  void Start() {
    spriteRenderer = GetComponent<SpriteRenderer>();
    sprite = spriteRenderer.color;
    spawnPosition = transform.position;
  }



  private void OnTriggerEnter2D(Collider2D collision) {
    if (collision.gameObject.tag == "Egg") {
      opacity -= 0.25f;

      // if opacity == 0, destroy and respawm
      if (opacity <= 0.0f) {
        // respawn
        StartCoroutine(Respawn());

      } else {
        sprite.a = opacity;
        spriteRenderer.color = sprite;
      }

      
    }
  }

  IEnumerator Respawn() {
    spriteRenderer.enabled = false;

    yield return new WaitForSeconds(respawnTime);

    float newXpos = Random.Range(spawnPosition.x - 15f, spawnPosition.x + 15f);
    float newYpos = Random.Range(spawnPosition.y - 15f, spawnPosition.y + 15f);
    Vector3 position = new Vector3(newXpos, newYpos);
    transform.position = position;
    opacity = 1f;
    sprite.a = opacity;
    spriteRenderer.color = sprite;
    spriteRenderer.enabled = true;
  }
}
