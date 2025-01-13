using UnityEngine;

public class CameraControler : MonoBehaviour
{
    [SerializeField] private float _speed;
    private float _currentPosX;
    //Vai nos dizerem que direção ir
    private Vector3 _velocity = Vector3.zero;

    private void Update()
    {
        //Aqui queremos posicionar a câmera confomre jogamos
        //transform.position é a posição da câmera
        //SmoothDamp muda um vetor conforme o tempo, dessa maneira ele "suaviza" a transição de movimento
        //Mto bom pra controlar a câmera e deixar o movimento mais natural
        transform.position = Vector3.SmoothDamp(transform.position,
        new Vector3(_currentPosX, transform.position.y, transform.position.z),
        ref _velocity, _speed);
    }

    public void MoveToNewRoom(Transform _newRoom)
    {
        //_currentPosX = _newRoom.position.x;
        transform.position = new Vector3(_newRoom.position.x, _newRoom.position.y, transform.position.z);
    }
}
