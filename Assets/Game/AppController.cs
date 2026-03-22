using Core.DI;
using EngineCore.Resources;
using EngineCore.UI;
using Game.Popups;
using Game.Popups.PuzzleStartPopup;
using UnityEngine;

namespace Game
{
    public class AppController : MonoBehaviour
    {
        [SerializeField] private PuzzleStartPopupView _puzzleStartPopupView;

        private SimpleContext _context;

        private void Start()
        {
            _context = new SimpleContext();

            var resourceManager = new ResourceManager("PuzzleImages/");
            var popupSystem = new PopupSystem();

            _context.AddServiceToRegister<IResourceManager>(resourceManager);
            _context.AddServiceToRegister<PopupSystem>(popupSystem);
            _context.AddServiceToRegister<PopupsFactory>(new PopupsFactory());
            _context.AddServiceToRegister<IPuzzleStartPopupView>(_puzzleStartPopupView);
            _context.Register();

            ShowStartPopup();
        }

        
        private void ShowStartPopup()
        {
            //can potentially be simplified into a single call Show<PuzzleStartPopupPresenter> in a high level class
            var factory = _context.Resolve<PopupsFactory>();
            var popupSystem = _context.Resolve<PopupSystem>();
            var presenter = factory.CreateStartPopup();
            popupSystem.ShowPopup<PuzzleStartPopupPresenter, IPuzzleStartPopupView>(presenter, _puzzleStartPopupView);
        }

        private void OnDestroy()
        {
            _context?.Release();
        }
    }
}
