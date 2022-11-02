using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//using Image = UnityEngine.UI.Image;
namespace TTBattle.UI
{
    [RequireComponent(typeof(PolygonCollider2D))]
    public class MapCell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        [SerializeField] public List<MapCell> NextCell;
        [SerializeField] public int id;
        [SerializeField] private float _warriorInfluence;
        [SerializeField] private float _assasinInfluence;
        [SerializeField] private float _mageInfluence;
        [SerializeField] public Color _activeChoiseColor;
        [SerializeField] public Color _selectedCellColor;
        [SerializeField] public Color _usualColor;
        private Color _lastColor;
        private Image _image;
        private MapScript _map;
        [NonSerialized] public float[] uintsInfluence = new float [3];
        public bool _isAccasible;
        public bool _isSelected;
        
        private void Awake()
        {
            _image = GetComponent<Image>();
            _lastColor = _usualColor;
            _image.color = _lastColor;
            GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
            SetUnitsInfluence();
            _map = GetComponentInParent<MapScript>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_isAccasible && _isSelected)
            {
                _image.color = _selectedCellColor;
                foreach (MapCell mapCell in _map._mapCell.NextCell) 
                { 
                    mapCell._isAccasible = false;
                }
                    // доп функця для атаки
            }
            else if (_isAccasible && !_isSelected)
            {
                _lastColor = _selectedCellColor;
                _map.NewMapCell = this;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_isAccasible && !_isSelected)
            {
                _image.color = _activeChoiseColor;
            }
            else
            {
                if (_isAccasible && _isSelected)
                {
                    _image.color = _activeChoiseColor;
                    _activeChoiseColor.a = 0.4f;
                }
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_isAccasible && !_isSelected)
            {
                _image.color = _lastColor;
            }

            if (_isAccasible && _isSelected)
            {
                _activeChoiseColor.a = 1f;
                _image.color = _lastColor;
            }
        }

        private void SetUnitsInfluence()
        {
            uintsInfluence[0] = (100 + _warriorInfluence) / 100;
            uintsInfluence[1] = (100 + _assasinInfluence) / 100;
            uintsInfluence[2] = (100 + _mageInfluence) / 100;
        }

        public void CellIsLeaved()
        {
            _lastColor = _usualColor;
            _image.color = _lastColor;
            _isSelected = false;
            _isAccasible = false;
            foreach (MapCell mapCell in NextCell)
            {
                mapCell._isAccasible = false;
            }
        }
        
        public void CellIsSelected()
        {
            _isSelected = true;
            _isAccasible = true;
            SetCellCollorAsPlayers(_map._playerSelector);
            foreach (MapCell mapCell in NextCell)
            {
                mapCell._isAccasible = true;
            }
        }

        public void SetCellCollorAsPlayers(ArmyPanel player)
        {
            _lastColor = player._playerMapCellColor;
            _lastColor.a = 0.8f;
            _image.color = _lastColor;
        }
    }
}