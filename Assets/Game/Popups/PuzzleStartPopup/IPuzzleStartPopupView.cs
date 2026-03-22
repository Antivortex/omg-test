using System;
using EngineCore.UI;
using UnityEngine;

namespace Game.Popups.PuzzleStartPopup
{
    public interface IPuzzleStartPopupView : IPopupView
    {
        event Action<int> OnThumbnailSelected;
        event Action OnStartFreeClicked;
        event Action OnStartForCoinsClicked;
        event Action OnWatchAdClicked;
        event Action OnCloseClicked;

        void SetThumbnails(Sprite[] sprites);
        void SetPreviewImage(Sprite sprite);
        void SetSelectedThumbnail(int index);
    }
}
