using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSeek : MonoBehaviour
{
    [SerializeField] private float minX;
    [SerializeField] private float maxX;

    void LateUpdate()
    {
        if (Root.LevelManager.IsGameStarted())
        {
            var player = Root.PlayerController;

            if (player.GetPlayerPosition().z > maxX)
            {
                transform.Translate(Vector3.forward * 0.2f);
                maxX = player.GetPlayerPosition().z;
                minX = maxX - 20;
            }

            if (player.GetPlayerPosition().z < minX)
            {
                transform.Translate(Vector3.forward * -0.2f);
                minX = player.GetPlayerPosition().z;
                maxX = minX + 20;
            }
        }

    }
}
