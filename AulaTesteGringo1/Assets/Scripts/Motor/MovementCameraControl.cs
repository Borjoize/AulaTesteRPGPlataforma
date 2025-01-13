using System;
using UnityEngine;

public class MovementCameraControl : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _aheadDistance;
    [SerializeField] private float _cameraSpeed;
    private float _lookAhead;

    private void Update()
    {
        //Isso aqui faz a câmera seguir o jogador na mesma velocidade que ele
        //Se só isso tá ok pra tu, então deixa como está mas tira o +_lookAhead, pq n serve pra nada
        //Com o _lookAhead a câmera vai ficar um pouco na frente do jogador, o que facilita
        //caso seja relevante ver o que existe na frente. Especialmente importante pra jogos rápidos
        transform.position = new Vector3(_player.position.x + _lookAhead, transform.position.y, transform.position.z);

        //O Mathf.Lerp serve para gradualmente mudar um valor pro outro
        //Assim a câmera vai gradualmente de um lado pro outro. Se você tira ele, ela "teleporta" o que confunde
        //O localScale vai ser 1 ou -1 dependendo da direção do jogador
        //Porque estamos multiplicando o _aheadDistance pode ficar negativo quando o jogador olhar pra trás
        //E o "Lerp" vai fazer com que essa virada seja uma transição tranquila
        _lookAhead = Mathf.Lerp(_lookAhead, (_aheadDistance * _player.localScale.x), Time.deltaTime * _cameraSpeed);
    }
}
