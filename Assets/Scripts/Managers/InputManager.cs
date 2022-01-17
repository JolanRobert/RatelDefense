using UnityEngine;

public class InputManager : MonoBehaviour
{
    void Update()
    {
        if (GameManager.instance.state == GameManager.GameState.PLAY)
        {
            
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Shoot.instance.Fire();
            }
            else if(Input.GetKey(KeyCode.Mouse1)) //&& PlayerManager.instance.spellCooldown==0)
            {
                Explosion.instance.Line();
            }
            else if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                Explosion.instance.Pop();
                Explosion.instance.isMove = true;
                
            }

            if (Input.GetKey(KeyCode.Z))
            {
                PlayerMovement.instance.Move(Input.GetAxis("Vertical"),Input.GetAxis("Horizontal"));
            }
            if (Input.GetKey(KeyCode.S))
            {
                PlayerMovement.instance.Move(Input.GetAxis("Vertical"),Input.GetAxis("Horizontal"));
            }
            if (Input.GetKey(KeyCode.Q))
            {
                PlayerMovement.instance.Move(Input.GetAxis("Vertical"),Input.GetAxis("Horizontal"));
            }
            if (Input.GetKey(KeyCode.D))
            {
                PlayerMovement.instance.Move(Input.GetAxis("Vertical"),Input.GetAxis("Horizontal"));
            }

            if (Input.GetAxis("Mouse X")!=0 || Input.GetAxis("Mouse Y")!=0)
            {
                FPS_Camera.instance.Rotate(Input.GetAxis("Mouse X"),Input.GetAxis("Mouse Y"));
            }
        }
    }
}
