using System;
using Mechanics.MoveSystem;
using Mechanics.SpellSystem;
using UnityEngine;

namespace Mechanics.ControllSystem
{
    public class Player : MonoBehaviour,IControllable
    {
        private Main inputAction;

        [SerializeField] 
        private Fly _mainMoveComponent;

        private bool _isTurnedOnRight;

        private void Awake()
        {
            LoadData();
            inputAction = new Main();
        }

        private void Start()
        {
            UISpellView.CastSpellEvent += Cast;
        }

        private void OnDestroy()
        {
            SaveData();
            UISpellView.CastSpellEvent -= Cast;
        }

        private void OnEnable()
        {
            inputAction.Enable();
        }
        private void OnDisable()
        {
            inputAction.Disable();
        }

        private void FixedUpdate()
        {
            ControllMove(inputAction.Move.Move.ReadValue<Vector2>());
        }

        private void SaveData()
        {
            PlayerPrefs.SetFloat("XPosition",transform.position.x);
            PlayerPrefs.SetFloat("YPosition",transform.position.y);
        }
        private void LoadData()
        {
            if (PlayerPrefs.HasKey("XPosition") && PlayerPrefs.HasKey("YPosition"))
                transform.position =
                    new Vector3(PlayerPrefs.GetFloat("XPosition"), PlayerPrefs.GetFloat("YPosition"), 0);
        }
        public void ControllMove(Vector2 direction)
        {
            _mainMoveComponent.Turn(direction);

            GetComponent<SpriteRenderer>().flipX = direction.x > 0;
        }

        private void Cast(int index)
        {
            GetComponent<SpellCasterComponent>().CastSpell(index);
        }
    }
}