using Core.DI;
using EngineCore.Resources;
using EngineCore.UI;
using UnityEngine;

namespace Game.Popups.PuzzleStartPopup
{
    public class PuzzleStartPopupPresenter : PopupPresenter<IPuzzleStartPopupView>
    {
        private readonly PuzzleStartPopupModel _model;
        private readonly IResourceManager _resourceManager;

        public PuzzleStartPopupPresenter(PuzzleStartPopupModel model, IContext context)
        {
            _model = model;
            _resourceManager = context.Resolve<ResourceManager>();
        }

        protected override void OnInitialize()
        {
            var sprites = new Sprite[_model.ImageNames.Length];
            for (int i = 0; i < _model.ImageNames.Length; i++)
            {
                sprites[i] = _resourceManager.LoadSprite(_model.ImageNames[i]);
            }

            View.SetThumbnails(sprites);
            View.SetPreviewImage(sprites[_model.SelectedIndex]);
            View.SetSelectedThumbnail(_model.SelectedIndex);

            SubscribeEvents();
        }

        protected override void OnAfterHide()
        {
            UnsubscribeEvents();
        }

        private void SubscribeEvents()
        {
            View.OnThumbnailSelected += OnThumbnailSelected;
            View.OnStartFreeClicked += OnStartFreeClicked;
            View.OnStartForCoinsClicked += OnStartForCoinsClicked;
            View.OnWatchAdClicked += OnWatchAdClicked;
            View.OnCloseClicked += OnCloseClicked;
        }

        private void UnsubscribeEvents()
        {
            View.OnThumbnailSelected -= OnThumbnailSelected;
            View.OnStartFreeClicked -= OnStartFreeClicked;
            View.OnStartForCoinsClicked -= OnStartForCoinsClicked;
            View.OnWatchAdClicked -= OnWatchAdClicked;
            View.OnCloseClicked -= OnCloseClicked;
        }

        private void OnThumbnailSelected(int index)
        {
            _model.SelectedIndex = index;
            var sprite = _resourceManager.LoadSprite(_model.SelectedImageName);
            View.SetPreviewImage(sprite);
            View.SetSelectedThumbnail(index);
        }

        private void OnStartFreeClicked()
        {
            Debug.Log($"Start Free clicked. Selected puzzle: {_model.SelectedImageName}");
        }

        private void OnStartForCoinsClicked()
        {
            Debug.Log($"Start for Coins clicked. Selected puzzle: {_model.SelectedImageName}");
        }

        private void OnWatchAdClicked()
        {
            Debug.Log($"Watch Ad clicked. Selected puzzle: {_model.SelectedImageName}");
        }

        private void OnCloseClicked()
        {
            Debug.Log("Close clicked.");
            Hide();
        }
    }
}
