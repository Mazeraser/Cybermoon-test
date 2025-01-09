using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics.SpellSystem
{
    public class SpellCasterComponent : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] _castableSpells;

        private int _lastIndex = -1;
        private bool _canSpell = true;
        
        public void CastSpell(int spellIndex)
        {
            if (_canSpell)
            {
                _canSpell = false;
                _lastIndex = spellIndex;
                ISpell spell = Instantiate(_castableSpells[spellIndex],transform.position, transform.rotation)
                                .GetComponent<ISpell>();
                spell.Cast();
                StartCoroutine(RecoverCasting(spell.ReloadTime));
            }
        }

        private void SaveData()
        {
            PlayerPrefs.SetInt("CanSpell",_canSpell ? 1 : 0);
            PlayerPrefs.SetInt("LastIndex",_lastIndex);
        }

        private void LoadData()
        {
            if (PlayerPrefs.HasKey("CanSpell")) _canSpell = PlayerPrefs.GetInt("CanSpell") == 1;
            if (PlayerPrefs.HasKey("LastIndex")) _lastIndex = PlayerPrefs.GetInt("LastIndex");
        }

        private void Awake()
        {
            LoadData();
            if (!_canSpell && _lastIndex>=0)
                StartCoroutine(RecoverCasting(_castableSpells[_lastIndex].GetComponent<ISpell>().ReloadTime));
        }

        private void OnDestroy(){
            SaveData();
        }

        private IEnumerator RecoverCasting(float time)
        {
            Debug.Log($"Casting is recovering in {time} seconds");
            yield return new WaitForSeconds(time);
            Debug.Log("Casting is recovered");
            _lastIndex = -1;
            _canSpell = true;
        }
    }
}
    
