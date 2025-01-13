using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform _previousRoom;
    [SerializeField] private Transform _nextRoom;
    //Usamos o Serialized pra poder editar no Unity Editor pra cada "cópia" do quarto que fizermos
    //Sem precisar voltar no código toda hora

    [SerializeField] private CameraControler _cam;

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        //Aqui ele verifica se o que colidiu tem a Tag Player, que nós definimos no Editor
        if(_collision.CompareTag("Player"))
        {
            Debug.Log("Player collided with the door");
            Debug.Log($"Player position: {_collision.transform.position.x}, Door position: {transform.position.x + _collision.bounds.extents.x}");

            //Aqui checa a direção que o Player tá vindo, pra saber pra qual quarto ele tá indo
            //if(_collision.transform.position.x > transform.position.x)
            if (_collision.transform.position.x < transform.position.x + _collision.bounds.extents.x)
            {
                Debug.Log("Moving to the next room");
                _cam.MoveToNewRoom(_nextRoom);
            }
            else if (_collision.transform.position.x > transform.position.x - _collision.bounds.extents.x)
            {
                Debug.Log("Moving to the previous room");
                _cam.MoveToNewRoom(_previousRoom);
            }
        }
    }
}
