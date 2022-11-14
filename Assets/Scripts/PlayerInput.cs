using UnityEngine;
using Zenject;

public class PlayerInput : ITickable
{
    readonly Camera _mainCamera;

    public PlayerInput(Camera mainCamera)
    {
        _mainCamera = mainCamera;
    }

    public void Tick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(_mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.transform && hit.collider.transform.GetComponent<Enemy>())
                hit.collider.transform.GetComponent<Enemy>().Die();
        }
    }
}