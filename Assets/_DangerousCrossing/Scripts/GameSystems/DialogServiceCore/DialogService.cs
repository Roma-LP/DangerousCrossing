using System;
using System.Collections.Generic;
using _DangerousCrossing.Scripts.Interfaces;
using _DangerousCrossing.Scripts.Utilities;
using UnityEngine;

namespace _DangerousCrossing.Scripts.GameSystems.DialogServiceCore
{
    public class DialogService : MonoBehaviour, IDialogService
    {
        [SerializeField] private RectTransform _dialogsContainer;
        
        private readonly List<Type> _activeDialogTypes = new List<Type>();
        private readonly List<DialogView> _activeDialogs = new List<DialogView>();
        
        public event Action<Type> DialogShown;
        public event Action<Type> DialogHide;
        
        private DialogView InstantiateDialog<TArgs>(Type dialogType, TArgs args) where TArgs : DialogArgs
        {
            DialogView dialogPrefab = Resources.Load<DialogView>(GameConstants.DIALOGS_RESOURCE_FOLDER + dialogType.Name);

            if (dialogPrefab != null) 
            {
                DialogView instance = Instantiate(dialogPrefab).GetComponent<DialogView>();
                (instance as IReceiveArgs<TArgs>).SetArgs(args);
                
                return instance;
            }
            
            throw new Exception($"You try to instantiate dialog that has no instance or contains name not equal to its type ({dialogType.Name})");
        }
        
        private void ShowDialog(DialogView dialogView, Action completeCallback = null)
        {
            dialogView
                .AddShownHandler(OnDialogShown)
                .AddHiddenHandler(OnDialogHidden);

            dialogView.SetParent(_dialogsContainer);
            dialogView.Show();
            
            completeCallback?.Invoke();
        }
        
        private void OnDialogShown(DialogView dialogView)
        {
            _activeDialogs.Add(dialogView);
            DialogShown?.Invoke(dialogView.GetType());
        }

        private void OnDialogHidden(DialogView dialogView)
        {
            _activeDialogs.Remove(dialogView);
            _activeDialogTypes.Remove(dialogView.GetType());

            DialogHide?.Invoke(dialogView.GetType());
            
            dialogView
                .RemoveShownHandler(OnDialogShown)
                .RemoveHiddenHandler(OnDialogHidden);

            this.UniversalWait(GameConstants.WAIT_TIME_BEFORE_CLEAR_MEMORY, ClearMemory);
        }
        
        private bool Contains(Type dialogType) => _activeDialogTypes.Contains(dialogType);
        private void ClearMemory() => Resources.UnloadUnusedAssets();
        
        public void CallDialog(Type dialogType, Action completeCallback = null, bool onlyOneDialogCanCall = true)
        {
            if (onlyOneDialogCanCall && Contains(dialogType))
                return;
            
            _activeDialogTypes.Add(dialogType);
            
            ShowDialog(InstantiateDialog<DialogArgs>(dialogType, null), completeCallback);
        }

        public void CallDialog<TArgs>(Type dialogType, TArgs args, Action completeCallback = null, bool onlyOneDialog = false) where TArgs : DialogArgs
        {
            if (onlyOneDialog && Contains(dialogType))
                return;

            _activeDialogTypes.Add(dialogType);

            ShowDialog(InstantiateDialog<TArgs>(dialogType, args), completeCallback);
        }

        public IDialogService CloseAllDialogs()
        {
            while (_activeDialogs.Count > 0) 
                _activeDialogs[0].Hide();

            return this;
        }

        public IDialogService CloseDialog(DialogView dialogView)
        {
            dialogView.Hide();

            return this;
        }
    }
}