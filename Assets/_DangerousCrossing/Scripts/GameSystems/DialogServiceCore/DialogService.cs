using System;
using System.Collections.Generic;
using System.Linq;
using _DangerousCrossing.Scripts.Interfaces;
using _DangerousCrossing.Scripts.Utilities;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _DangerousCrossing.Scripts.GameSystems.DialogServiceCore
{
    public class DialogService : MonoBehaviour, IDialogService
    {
        [SerializeField] private RectTransform _dialogsContainer;

        private readonly List<Type> _activeDialogTypes = new List<Type>();
        private readonly List<DialogView> _activeDialogs = new List<DialogView>();
        private readonly Dictionary<Type, DialogView> _activeDialogsDict = new Dictionary<Type, DialogView>();

        public event Action<DialogView> OnDialogShown;
        public event Action<DialogView> OnDialogHide;

        private async UniTask<DialogView> InstantiateDialog<TArgs>(Type dialogType, TArgs args) where TArgs : DialogArgs
        {
            string resourcePath = GameConstants.DIALOGS_RESOURCE_FOLDER + dialogType.Name;
            ResourceRequest request = Resources.LoadAsync<DialogView>(resourcePath);

            await request;

            DialogView dialogPrefab = request.asset as DialogView;

            if (dialogPrefab != null)
            {
                DialogView instance = Instantiate(dialogPrefab);
                ((IReceiveArgs<TArgs>)instance).SetArgs(args);

                return instance;
            }

            throw new Exception(
                $"You try to instantiate dialog that has no instance or contains name not equal to its type ({dialogType.Name})");
        }

        private void ShowDialog(DialogView dialogView, Action completeCallback = null)
        {
            dialogView
                .AddShownHandler(OnDialogShownHandler)
                .AddHiddenHandler(OnDialogHiddenHandler);

            dialogView.SetParent(_dialogsContainer);
            dialogView.Show();

            completeCallback?.Invoke();
        }

        private void OnDialogShownHandler(DialogView dialogView)
        {
            _activeDialogs.Add(dialogView);
            //OnDialogShown?.Invoke(dialogView.GetType());
            OnDialogShown?.Invoke(dialogView);
        }

        private void OnDialogHiddenHandler(DialogView dialogView)
        {
            _activeDialogs.Remove(dialogView);
            _activeDialogTypes.Remove(dialogView.GetType());

            OnDialogHide?.Invoke(dialogView);

            dialogView
                .RemoveShownHandler(OnDialogShownHandler)
                .RemoveHiddenHandler(OnDialogHiddenHandler);

            this.StartCoroutineUniversalWait(GameConstants.WAIT_TIME_BEFORE_CLEAR_MEMORY, ClearMemory);
        }

        private bool Contains(Type dialogType) => _activeDialogTypes.Contains(dialogType);
        private void ClearMemory() => Resources.UnloadUnusedAssets();

        public async void CallDialog(Type dialogType, Action completeCallback = null, bool onlyOneDialogCanCall = true)
        {
            if (onlyOneDialogCanCall && Contains(dialogType))
                return;

            _activeDialogTypes.Add(dialogType);

            ShowDialog(await InstantiateDialog<DialogArgs>(dialogType, null), completeCallback);
        }

        public async void CallDialog<TArgs>(Type dialogType, TArgs args, Action completeCallback = null,
            bool onlyOneDialog = true) where TArgs : DialogArgs
        {
            if (onlyOneDialog && Contains(dialogType))
                return;

            _activeDialogTypes.Add(dialogType);

            ShowDialog(await InstantiateDialog<TArgs>(dialogType, args), completeCallback);
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

        public bool TryGetDialog<T>(out T dialog) where T : DialogView
        {
            DialogView dialogView = _activeDialogs.FirstOrDefault(dlg => dlg.GetType() == typeof(T));

            if (dialogView == default)
            {
                dialog = null;
                return false;
            }

            dialog = (T)dialogView;
            return true;
        }
    }
}