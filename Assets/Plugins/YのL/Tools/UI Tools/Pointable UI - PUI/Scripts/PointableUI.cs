using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YNL.Attribute;
using YNL.Extension.Method;
using YNL.Extension.Constant;

namespace YNL.Tools.UI
{
    [AddComponentMenu("YNL/Tools/UI/Pointable UI")]
    public class PointableUI : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {
        #region ▶ Properties
        public bool Interactable = true;

        #region ▶ PUIMode Properties
        private bool _isSelected;
        [BoxGroup("PUI Mode", showLabel: false)] public PUIMode Mode;
        [Space()]
        [ShowIf("Mode", Value = PUIMode.IgnoreDeselect), BoxGroup("PUI Mode")] public string IgnoreDeselectName = "IgnoreDeselect";
        [ShowIf("Mode", Value = PUIMode.IgnoreDeselect), BoxGroup("PUI Mode")] public LayerMask IgnoreDeselectLayer;
        [ShowIf("Mode", Value = PUIMode.IgnoreDeselect), BoxGroup("PUI Mode")] public bool DetectCoveredIgnoreUI;

        [ShowIf("Mode", Value = PUIMode.HoverToSelect), BoxGroup("PUI Mode")] public bool DeselectOnExit;
        #endregion

        #region ▶ PUI Graphics
        [Title("PUI Graphics")]
        public EUITransition Transition;
        [Space(10)]
        [HideIfEnum("Transition", (int)EUITransition.None)]
        [HideIfEnum("Transition", (int)EUITransition.Animation)]
        public Image TargetGraphic;
        [Space()]
        private Color DefaultColor;
        [ShowIf("Transition", Value = EUITransition.Color)] public Color NormalColor = new(1, 1, 1, 1);
        [ShowIf("Transition", Value = EUITransition.Color)] public Color HighlightedColor = new(1, 1, 1, 1);
        [ShowIf("Transition", Value = EUITransition.Color)] public Color PressedColor = new(0.65f, 0.65f, 0.65f, 1);
        [ShowIf("Transition", Value = EUITransition.Color)] public Color SelectedColor = new(1, 1, 1, 1);
        [ShowIf("Transition", Value = EUITransition.Color)] public Color DisabledColor = new(0.75f, 0.75f, 0.75f, 0.5f);
        [ShowIf("Transition", Value = EUITransition.Color)] public float FadeDuration = 0.1f;
        [Space()]
        [ShowIf("Transition", Value = EUITransition.SpriteSwap)] public Sprite NormalSprite;
        [ShowIf("Transition", Value = EUITransition.SpriteSwap)] public Sprite HighlightedSprite;
        [ShowIf("Transition", Value = EUITransition.SpriteSwap)] public Sprite PressedSprite;
        [ShowIf("Transition", Value = EUITransition.SpriteSwap)] public Sprite SelectedSprite;
        [ShowIf("Transition", Value = EUITransition.SpriteSwap)] public Sprite DisabledSprite;
        [Space(10)]
        [ShowIf("Transition", Value = EUITransition.Animation)] public Animator _Animator;
        [Space(10)]
        [ShowIf("Transition", Value = EUITransition.Animation)] public string NormalTrigger = "Normal";
        [ShowIf("Transition", Value = EUITransition.Animation)] public string HighlightedTrigger = "Highlighted";
        [ShowIf("Transition", Value = EUITransition.Animation)] public string PressedTrigger = "Pressed";
        [ShowIf("Transition", Value = EUITransition.Animation)] public string SelectedTrigger = "Selected";
        [ShowIf("Transition", Value = EUITransition.Animation)] public string DisabledTrigger = "Disabled";
        #endregion

        #region ▶ PUI Event
        [FoldoutGroup("PUI Event")]
        [Header("Invoked when PUI is selected")]
        [FoldoutGroup("PUI Event/On Select | Deselect")] public UnityEvent Select;
        [Header("Invoked when PUI is deselected")]
        [FoldoutGroup("PUI Event/On Select | Deselect")] public UnityEvent Deselect;
        [Header("Invoked when PUI is clicked, but not be invoked when pointer is out of PUI")]
        [FoldoutGroup("PUI Event/On Pointer Click")] public UnityEvent LeftClick;
        [FoldoutGroup("PUI Event/On Pointer Click")] public UnityEvent RightClick;
        [FoldoutGroup("PUI Event/On Pointer Click")] public UnityEvent MiddleClick;
        [Header("Invoked when PUI is pressed")]
        [FoldoutGroup("PUI Event/On Pointer Down")] public UnityEvent LeftDown;
        [FoldoutGroup("PUI Event/On Pointer Down")] public UnityEvent RightDown;
        [FoldoutGroup("PUI Event/On Pointer Down")] public UnityEvent MiddleDown;
        [Header("Invoked when PUI is released, still be invoked even when pointer is out of PUI")]
        [FoldoutGroup("PUI Event/On Pointer Up")] public UnityEvent LeftUp;
        [FoldoutGroup("PUI Event/On Pointer Up")] public UnityEvent RightUp;
        [FoldoutGroup("PUI Event/On Pointer Up")] public UnityEvent MiddleUp;
        [Header("Invoked when pointer enter a PUI")]
        [FoldoutGroup("PUI Event/On Enter | Exit")] public UnityEvent Enter;
        [Header("Invoked when pointer exit a PUI")]
        [FoldoutGroup("PUI Event/On Enter | Exit")] public UnityEvent Exit;
        #endregion
        #endregion

        #region ▶ Methods
        #region ▶ Editor Methods
        protected virtual void OnValidate()
        {
            Button getButton;
            if (GetComponent<Button>() != null)
            {
                getButton = GetComponent<Button>();
                getButton.DestroyOnValidate();
            }

            if (Transition == EUITransition.Color || Transition == EUITransition.SpriteSwap)
            {
                if (TargetGraphic == null)
                {
                    TargetGraphic = GetComponent<Image>();
                    if (TargetGraphic == null) MDebug.Warning("Require <b><color=#00FF87>Image</color></b> component if PUI is in <i><b>Color Tint</b></i> or <i><b>Sprite Swap</b></i> transition mode.");
                }
                else
                {
                    DefaultColor = TargetGraphic.color;

                    if (Transition == EUITransition.Color)
                    {
                        if (TargetGraphic.color != SelectedColor)
                        {
                            if (NormalColor != Color.white) TargetGraphic.color = NormalColor;
                        }
                    }
                    if (Transition == EUITransition.SpriteSwap)
                    {
                        if (NormalSprite != null) TargetGraphic.sprite = NormalSprite;
                    }
                }
            }
            if (Transition == EUITransition.Animation)
            {
                if (_Animator == null)
                {
                    _Animator = GetComponent<Animator>();
                    if (_Animator == null) MDebug.Warning("Require <b><color=#00FF87>Animator</color></b> component if PUI is in <i><b>Animation</b></i> transition mode.");
                }
            }
        }

        private void OnEnable()
        {
            if (Transition == EUITransition.Color || Transition == EUITransition.SpriteSwap)
            {
                if (TargetGraphic == null) TargetGraphic = this.GetComponent<Image>();
            }
            if (Transition == EUITransition.Color) if (TargetGraphic != null) TargetGraphic.color = NormalColor;
            if (Transition == EUITransition.SpriteSwap) if (TargetGraphic != null) TargetGraphic.sprite = NormalSprite;
            if (Transition == EUITransition.Animation) if (_Animator != null) _Animator.Play(NormalTrigger);

            IgnoreDeselectLayer = LayerMask.NameToLayer(IgnoreDeselectName);
        }
        #endregion

        #region ▶ PUI Handler Methods
        /// <summary>
        /// Used to force selecting the PUI via code, without clicking on it.
        /// </summary>
        public void ForceSelect() => OnSelect(new(EventSystem.current));
        public void DetectIgnoreUI(bool activate) => DetectCoveredIgnoreUI = activate;

        public void OnSelect(BaseEventData eventData)
        {
            PUIInteractableHandler("OnSelect");

            if (Transition == EUITransition.Color) TargetGraphic.color = SelectedColor;
            if (Transition == EUITransition.SpriteSwap) TargetGraphic.sprite = SelectedSprite;
            if (Transition == EUITransition.Animation) if (_Animator != null) _Animator.Play(SelectedTrigger);

            if (eventData.selectedObject != this.gameObject) eventData.selectedObject = this.gameObject;

            if (!_isSelected) PUIEventHandler("OnSelect", null);

            _isSelected = true;
        }

        public void OnDeselect(BaseEventData eventData)
        {
            PUIInteractableHandler("OnDeselect");

            GameObject ignoreObject = RaycastUI.GetUIElement(IgnoreDeselectLayer, DetectCoveredIgnoreUI);
            if (ignoreObject != null && ignoreObject != this.gameObject && Mode == PUIMode.IgnoreDeselect)
            {
                StartCoroutine(ReselectDelayed(this.gameObject));

                if (Transition == EUITransition.Color) TargetGraphic.color = SelectedColor;
                if (Transition == EUITransition.SpriteSwap) TargetGraphic.sprite = SelectedSprite;
                if (Transition == EUITransition.Animation) if (_Animator != null) _Animator.Play(SelectedTrigger);
                return;
            }

            if (Transition == EUITransition.Color)
            {
                if (NormalColor != Color.white) TargetGraphic.color = NormalColor;
                else TargetGraphic.color = DefaultColor;
            }
            if (Transition == EUITransition.SpriteSwap) TargetGraphic.sprite = NormalSprite;
            if (Transition == EUITransition.Animation) if (_Animator != null) _Animator.Play(NormalTrigger);

            _isSelected = false;

            PUIEventHandler("OnDeselect", null);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            PUIInteractableHandler("OnClick");

            if (Mode == PUIMode.HoverToSelect) return;

            if (Mode == PUIMode.OnlyClickButton)
            {
                if (Transition == EUITransition.Color)
                {
                    if (NormalColor != Color.white) TargetGraphic.color = NormalColor;
                    else TargetGraphic.color = DefaultColor;
                }
                if (Transition == EUITransition.SpriteSwap) TargetGraphic.sprite = NormalSprite;
                if (Transition == EUITransition.Animation) if (_Animator != null) _Animator.Play(NormalTrigger);
                PUIEventHandler("OnClick", eventData);
                return;
            }
            if (eventData.selectedObject != this.gameObject) eventData.selectedObject = this.gameObject;

            PUIEventHandler("OnClick", eventData);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            PUIInteractableHandler("OnDown");

            if (Mode == PUIMode.HoverToSelect) return;

            if (_isSelected) return;
            if (Transition == EUITransition.Color) TargetGraphic.color = PressedColor;
            if (Transition == EUITransition.SpriteSwap) TargetGraphic.sprite = PressedSprite;
            if (Transition == EUITransition.Animation) if (_Animator != null) _Animator.Play(PressedTrigger);

            PUIEventHandler("OnDown", eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            PUIInteractableHandler("OnUp");

            if (Mode == PUIMode.HoverToSelect) return;

            PUIEventHandler("OnUp", eventData);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            PUIInteractableHandler("OnEnter");

            if (Mode == PUIMode.HoverToSelect)
            {
                if (eventData.selectedObject != this.gameObject) eventData.selectedObject = this.gameObject;
            }
            else
            {
                if (Transition == EUITransition.Color)
                {
                    if (TargetGraphic.color != SelectedColor)
                    {
                        TargetGraphic.color = HighlightedColor;
                    }
                }
                if (Transition == EUITransition.SpriteSwap) TargetGraphic.sprite = HighlightedSprite;
                if (Transition == EUITransition.Animation) if (_Animator != null) _Animator.Play(HighlightedTrigger);
            }

            PUIEventHandler("OnEnter", eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            PUIInteractableHandler("OnExit");

            if (Mode == PUIMode.HoverToSelect && DeselectOnExit) StartCoroutine(ReselectDelayed(null));

            if (Transition == EUITransition.Color)
            {
                if (TargetGraphic.color != SelectedColor)
                {
                    if (NormalColor != Color.white) TargetGraphic.color = NormalColor;
                    else TargetGraphic.color = DefaultColor;
                }
            }
            if (Transition == EUITransition.SpriteSwap) if (TargetGraphic.sprite != SelectedSprite) TargetGraphic.sprite = NormalSprite;
            if (Transition == EUITransition.Animation) if (_Animator != null) _Animator.Play(NormalTrigger);

            PUIEventHandler("OnExit", eventData);
        }
        #endregion

        #region ▶ Extension Methods
        private IEnumerator ReselectDelayed(GameObject gameObj)
        {
            yield return new WaitForEndOfFrame();
            EventSystem.current.SetSelectedGameObject(gameObj);
        }

        private void PUIEventHandler(string eventType, PointerEventData eventData)
        {
            switch (eventType)
            {
                case "OnSelect":
                    Select?.Invoke();
                    break;
                case "OnDeselect":
                    Deselect?.Invoke();
                    break;
                case "OnClick":
                    if (eventData.button == PointerEventData.InputButton.Left) LeftClick?.Invoke();
                    else if (eventData.button == PointerEventData.InputButton.Right) RightClick?.Invoke();
                    else if (eventData.button == PointerEventData.InputButton.Middle) MiddleClick?.Invoke();
                    break;
                case "OnDown":
                    if (eventData.button == PointerEventData.InputButton.Left) LeftDown?.Invoke();
                    else if (eventData.button == PointerEventData.InputButton.Right) RightDown?.Invoke();
                    else if (eventData.button == PointerEventData.InputButton.Middle) MiddleDown?.Invoke();
                    break;
                case "OnUp":
                    if (eventData.button == PointerEventData.InputButton.Left) LeftUp?.Invoke();
                    else if (eventData.button == PointerEventData.InputButton.Right) RightUp?.Invoke();
                    else if (eventData.button == PointerEventData.InputButton.Middle) MiddleUp?.Invoke();
                    break;
                case "OnEnter":
                    Enter?.Invoke();
                    break;
                case "OnExit":
                    Exit?.Invoke();
                    break;
            }
        }
        private void PUIInteractableHandler(string eventType)
        {
            if (!Interactable) return;

            switch (eventType)
            {
                case "OnSelect": if (_isSelected) return; break;
                case "OnDeselect": if (!_isSelected) return; break;
                case "OnClick": break;
                case "OnDown": if (_isSelected) return; break;
                case "OnUp": break;
                case "OnEnter": if (_isSelected) return; break;
                case "OnExit": if (_isSelected) return; break;
            }
        }

        #endregion
        #endregion
    }

    public enum PUIMode
    {
        StandardButton, // Just like Unity's original button
        IgnoreDeselect, // Ignore deselecting when clicking on UI with specific layer/tag
        HoverToSelect, // Select when hovering pointer
        OnlyClickButton, // Just for clicking purpose, not select after clicking
    }
}