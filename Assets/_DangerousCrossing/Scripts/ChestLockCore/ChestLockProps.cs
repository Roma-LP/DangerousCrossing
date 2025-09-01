using _DangerousCrossing.Scripts.Interfaces;
using _DangerousCrossing.Scripts.Utilities;

namespace _DangerousCrossing.Scripts.ChestLockCore
{
    public class ChestLockProps : ObjectFloatRotateAnimation,  IInteractable
    {
        private IDialogService _dialogService;

        public void Init(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }
        
        public void Interact()
        {
            StopAnimation();
            _dialogService.CallDialog(typeof(ChestLockDialog));
        }
    }
}