using System;
using _DangerousCrossing.Scripts.GameSystems.DialogServiceCore;

namespace _DangerousCrossing.Scripts.Interfaces
{
    public interface IDialogService
    {
        public event Action<Type> DialogShown;
        public event Action<Type> DialogHide;
        public void CallDialog(Type dialogType, Action completeCallback = null, bool onlyOneDialogCanCall = false);
        public void CallDialog<TArgs>(Type dialogType, TArgs args, Action completeCallback = null, bool onlyOneDialog = false) where TArgs : DialogArgs;
        public IDialogService CloseAllDialogs();
        public IDialogService CloseDialog(DialogView dialogView);
    }
}