using System;
using UnityEngine;

namespace _DangerousCrossing.Scripts.GameSystems.DialogServiceCore
{
    public abstract class Dialog : MonoBehaviour
    {
        protected DialogArgs _dialogArgs;
        protected bool _visible { get; set; }
        
        protected event Action<DialogView> _onDialogShown;
        protected event Action<DialogView> _onDialogHidden;
        
        protected void NotifyListeners()
        {
            if (_visible)
            {
                if (_onDialogShown != null) 
                    _onDialogShown?.Invoke((DialogView)this);
            }
            else
            {
                if (_onDialogHidden != null) 
                    _onDialogHidden?.Invoke((DialogView)this);
            }
        }
        
        public Dialog AddShownHandler(Action<DialogView> dialogShownHandler)
        {
            _onDialogShown += dialogShownHandler;
            return this;
        }
        
        public Dialog RemoveShownHandler(Action<DialogView> dialogShownHandler)
        {
            _onDialogShown -= dialogShownHandler;
            return this;
        }
       
        public Dialog AddHiddenHandler(Action<DialogView> dialogHiddenHandler)
        {
            _onDialogHidden += dialogHiddenHandler;
            return this;
        }
        
        public Dialog RemoveHiddenHandler(Action<DialogView> dialogHiddenHandler)
        {
            _onDialogHidden -= dialogHiddenHandler;
            return this;
        }
    }
}