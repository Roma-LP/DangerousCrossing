using _DangerousCrossing.Scripts.Interfaces;
using _DangerousCrossing.Scripts.Utilities;

namespace _DangerousCrossing.Scripts.ChestLockCore
{
    public class ChestLockProps : ObjectFloatRotateAnimation,  IInteractable
    {
        private IDialogService _dialogService;
        private IDataStorage _dataStorage;

        public void Init(IDialogService dialogService, IDataStorage dataStorage)
        {
            _dialogService = dialogService;
            _dataStorage = dataStorage;
        }
        
        public void Interact()
        {
            StopAnimation();
            _dialogService.CallDialog<ChestLockDialogArgs>(typeof(ChestLockDialog), new ChestLockDialogArgs()
            {
                IDataStorage = _dataStorage
            });
        }
    }
}