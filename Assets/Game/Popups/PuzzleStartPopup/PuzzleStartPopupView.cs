using System;
using System.Collections.Generic;
using EngineCore;
using EngineCore.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Popups.PuzzleStartPopup
{
    public class PuzzleStartPopupView : PopupView, IPuzzleStartPopupView
    {
        [SerializeField] private Transform _thumbnailContainer;
        [SerializeField] private Image _previewImage;
        [SerializeField] private Button _startFreeButton;
        [SerializeField] private Button _startForCoinsButton;
        [SerializeField] private Button _watchAdButton;
        [SerializeField] private Button _closeButton;

        [Header("Thumbnail Settings")]
        [SerializeField] private float _thumbnailSize = 80f;
        [SerializeField] private Color _selectedColor = Color.yellow;
        [SerializeField] private Color _normalColor = Color.white;

        public event Action<int> OnThumbnailSelected;
        public event Action OnStartFreeClicked;
        public event Action OnStartForCoinsClicked;
        public event Action OnWatchAdClicked;
        public event Action OnCloseClicked;

        private readonly List<GameObject> _thumbnailObjects = new List<GameObject>();
        private GoPool _thumbnailPool;
        private int _currentSelectedIndex = -1;
        private bool _buttonsInitialized;

        public override void Show()
        {
            base.Show();

            if (!_buttonsInitialized)
            {
                _startFreeButton.onClick.AddListener(() => OnStartFreeClicked?.Invoke());
                _startForCoinsButton.onClick.AddListener(() => OnStartForCoinsClicked?.Invoke());
                _watchAdButton.onClick.AddListener(() => OnWatchAdClicked?.Invoke());
                _closeButton.onClick.AddListener(() => OnCloseClicked?.Invoke());
                _buttonsInitialized = true;
            }
        }

        public void SetThumbnails(Sprite[] sprites)
        {
            if (_thumbnailPool == null)
                _thumbnailPool = new GoPool(_thumbnailContainer);

            //if any objects instantiated, return them to the pool
            foreach (var obj in _thumbnailObjects)
            {
                obj.GetComponent<Button>().onClick.RemoveAllListeners();
                _thumbnailPool.Return(obj);
            }
            _thumbnailObjects.Clear();

            for (int i = 0; i < sprites.Length; i++)
            {
                var thumbnailObj = _thumbnailPool.Get($"Thumbnail_{i}", typeof(RectTransform), typeof(Image), typeof(Button));

                var rectTransform = thumbnailObj.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(_thumbnailSize, _thumbnailSize);

                var image = thumbnailObj.GetComponent<Image>();
                image.sprite = sprites[i];
                image.preserveAspect = true;
                image.color = _normalColor;

                var button = thumbnailObj.GetComponent<Button>();
                button.onClick.RemoveAllListeners();
                int index = i;
                button.onClick.AddListener(() => OnThumbnailSelected?.Invoke(index));

                _thumbnailObjects.Add(thumbnailObj);
            }
        }

        public void SetPreviewImage(Sprite sprite)
        {
            if (_previewImage != null && sprite != null)
            {
                _previewImage.sprite = sprite;
                _previewImage.preserveAspect = true;
            }
        }

        public void SetSelectedThumbnail(int index)
        {
            if (_currentSelectedIndex >= 0 && _currentSelectedIndex < _thumbnailObjects.Count)
            {
                var prevImage = _thumbnailObjects[_currentSelectedIndex].GetComponent<Image>();
                prevImage.color = _normalColor;
            }

            if (index >= 0 && index < _thumbnailObjects.Count)
            {
                var selectedImage = _thumbnailObjects[index].GetComponent<Image>();
                selectedImage.color = _selectedColor;
            }

            _currentSelectedIndex = index;
        }

        private void OnDestroy()
        {
            _startFreeButton.onClick.RemoveAllListeners();
            _startForCoinsButton.onClick.RemoveAllListeners();
            _watchAdButton.onClick.RemoveAllListeners();
            _closeButton.onClick.RemoveAllListeners();
        }
    }
}
