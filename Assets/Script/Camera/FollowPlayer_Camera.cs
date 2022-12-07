using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer_Camera : MonoBehaviour
{
    Transform Player;
    public Vector3 offset;

    private void Awake()
    {
        Player = GameObject.Find("Player").transform;
    }
    // Update is called once per frame
    void Update()
    {
        this.transform.position = Player.position + offset;
    }
    public IEnumerator Shake(float duration, float magnitude)
    {
       // Debug.Log("CameraShake!!!!");
        Vector3 originalPos = this.transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(this.transform.position.x + x, this.transform.position.y + y, this.transform.position.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        this.transform.localPosition = this.transform.position;
    }
}
