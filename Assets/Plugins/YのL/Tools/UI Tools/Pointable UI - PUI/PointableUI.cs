using Sirenix.OdinInspector;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YNL.Attribute;

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
        #endregion

        #region ▶ PUI Graphics
        [BoxGroup("PUI Graphic")] public PUITransition Transition;
        [Space(10)]
        [HideIfEnum("Transition", (int)PUITransition.None), BoxGroup("PUI Graphic")]
        [HideIfEnum("Transition", (int)PUITransition.Animation), BoxGroup("PUI Graphic")]
        public Image TargetGraphic;
        [Space()]
        private Color DefaultColor;
        [ShowIf("Transition", Value = PUITransition.ColorTint), BoxGroup("PUI Graphic")] public Color NormalColor = new(1, 1, 1, 1);
        [ShowIf("Transition", Value = PUITransition.ColorTint), BoxGroup("PUI Graphic")] public Color HighlightedColor = new(1, 1, 1, 1);
        [ShowIf("Transition", Value = PUITransition.ColorTint), BoxGroup("PUI Graphic")] public Color PressedColor = new(0.65f, 0.65f, 0.65f, 1);
        [ShowIf("Transition", Value = PUITransition.ColorTint), BoxGroup("PUI Graphic")] public Color SelectedColor = new(1, 1, 1, 1);
        [ShowIf("Transition", Value = PUITransition.ColorTint), BoxGroup("PUI Graphic")] public Color DisabledColor = new(0.75f, 0.75f, 0.75f, 0.5f);
        [ShowIf("Transition", Value = PUITransition.ColorTint), BoxGroup("PUI Graphic")] public float FadeDuration = 0.1f;
        [Space()]
        [ShowIf("Transition", Value = PUITransition.SpriteSwap), BoxGroup("PUI Graphic")] public Sprite NormalSprite;
        [ShowIf("Transition", Value = PUITransition.SpriteSwap), BoxGroup("PUI Graphic")] public Sprite HighlightedSprite;
        [ShowIf("Transition", Value = PUITransition.SpriteSwap), BoxGroup("PUI Graphic")] public Sprite PressedSprite;
        [ShowIf("Transition", Value = PUITransition.SpriteSwap), BoxGroup("PUI Graphic")] public Sprite SelectedSprite;
        [ShowIf("Transition", Value = PUITransition.SpriteSwap), BoxGroup("PUI Graphic")] public Sprite DisabledSprite;
        [Space(10)]
        [ShowIf("Transition", Value = PUITransition.Animation), BoxGroup("PUI Graphic")] public Animator _Animator;
        [Space(10)]
        [ShowIf("Transition", Value = PUITransition.Animation), BoxGroup("PUI Graphic")] public string NormalTrigger = "Normal";
        [ShowIf("Transition", Value = PUITransition.Animation), BoxGroup("PUI Graphic")] public string HighlightedTrigger = "Highlighted";
        [ShowIf("Transition", Value = PUITransition.Animation), BoxGroup("PUI Graphic")] public string PressedTrigger = "Pressed";
        [ShowIf("Transition", Value = PUITransition.Animation), BoxGroup("PUI Graphic")] public string SelectedTrigger = "Selected";
        [ShowIf("Transition", Value = PUITransition.Animation), BoxGroup("PUI Graphic")] public string DisabledTrigger = "Disabled";
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
            if (Transition == PUITransition.ColorTint || Transition == PUITransition.SpriteSwap)
            {
                if (TargetGraphic == null)
                {
                    TargetGraphic = GetComponent<Image>();
                    if (TargetGraphic == null) Debug.Log($"<color=#FFE045><b>⚠ Warning: </b></color> Require <b><color=#00FF87>Image</color></b> component if PUI is in <i><b>Color Tint</b></i> or <i><b>Sprite Swap</b></i> transition mode.");
                }
                else
                {
                    DefaultColor = TargetGraphic.color;

                    if (Transition == PUITransition.ColorTint)
                    {
                        if (NormalColor != Color.white) TargetGraphic.color = NormalColor;
                    }
                    if (Transition == PUITransition.SpriteSwap)
                    {
                        if (NormalSprite != null) TargetGraphic.sprite = NormalSprite;
                    }
                }
            }
            if (Transition == PUITransition.Animation)
            {
                if (_Animator == null)
                {
                    _Animator = GetComponent<Animator>();
                    if (_Animator == null) Debug.Log($"<color=#FFE045><b>⚠ Warning: </b></color> Require <b><color=#00FF87>Animator</color></b> component if PUI is in <i><b>Animation</b></i> transition mode.");
                }
            }
        }

        private void OnEnable()
        {
            if (Transition == PUITransition.ColorTint || Transition == PUITransition.SpriteSwap)
            {
                if (TargetGraphic == null) TargetGraphic = this.GetComponent<Image>();
            }
            if (Transition == PUITransition.ColorTint) if (TargetGraphic != null) TargetGraphic.color = NormalColor;
            if (Transition == PUITransition.SpriteSwap) if (TargetGraphic != null) TargetGraphic.sprite = NormalSprite;
            if (Transition == PUITransition.Animation) if (_Animator != null) _Animator.Play(NormalTrigger);

            IgnoreDeselectLayer = LayerMask.NameToLayer(IgnoreDeselectName);
        }
        #endregion

        #region ▶ PUI Handler Methods
        public void OnSelect(BaseEventData eventData)
        {
            PUIInteractableHandler("OnSelect");

            if (Transition == PUITransition.ColorTint) TargetGraphic.color = SelectedColor;
            if (Transition == PUITransition.SpriteSwap) TargetGraphic.sprite = SelectedSprite;
            if (Transition == PUITransition.Animation) if (_Animator != null) _Animator.Play(SelectedTrigger);

            if (eventData.selectedObject != this.gameObject) eventData.selectedObject = this.gameObject;

            if (!_isSelected) PUIEventHandler("OnSelect", null);

            _isSelected = true;
        }

        public void OnDeselect(BaseEventData eventData)
        {
            PUIInteractableHandler("OnDeselect");

            if (RaycastUI.GetUIElement(IgnoreDeselectLayer) != null && Mode == PUIMode.IgnoreDeselect)
            {
                StartCoroutine(ReselectDelayed(this.gameObject));

                if (Transition == PUITransition.ColorTint) TargetGraphic.color = SelectedColor;
                if (Transition == PUITransition.SpriteSwap) TargetGraphic.sprite = SelectedSprite;
                if (Transition == PUITransition.Animation) if (_Animator != null) _Animator.Play(SelectedTrigger);
                return;
            }

            if (Transition == PUITransition.ColorTint)
            {
                if (NormalColor != Color.white) TargetGraphic.color = NormalColor;
                else TargetGraphic.color = DefaultColor;
            }
            if (Transition == PUITransition.SpriteSwap) TargetGraphic.sprite = NormalSprite;
            if (Transition == PUITransition.Animation) if (_Animator != null) _Animator.Play(NormalTrigger);

            _isSelected = false;

            PUIEventHandler("OnDeselect", null);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            PUIInteractableHandler("OnClick");

            if (Mode == PUIMode.HoverToSelect) return;

            if (Mode == PUIMode.OnlyClickButton)
            {
                if (Transition == PUITransition.ColorTint)
                {
                    if (NormalColor != Color.white) TargetGraphic.color = NormalColor;
                    else TargetGraphic.color = DefaultColor;
                }
                if (Transition == PUITransition.SpriteSwap) TargetGraphic.sprite = NormalSprite;
                if (Transition == PUITransition.Animation) if (_Animator != null) _Animator.Play(NormalTrigger);
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
            if (Transition == PUITransition.ColorTint) TargetGraphic.color = PressedColor;
            if (Transition == PUITransition.SpriteSwap) TargetGraphic.sprite = PressedSprite;
            if (Transition == PUITransition.Animation) if (_Animator != null) _Animator.Play(PressedTrigger);

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

            if (Transition == PUITransition.ColorTint) TargetGraphic.color = HighlightedColor;
            if (Transition == PUITransition.SpriteSwap) TargetGraphic.sprite = HighlightedSprite;
            if (Transition == PUITransition.Animation) if (_Animator != null) _Animator.Play(HighlightedTrigger);

            PUIEventHandler("OnEnter", eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            PUIInteractableHandler("OnExit");

            if (Mode == PUIMode.HoverToSelect) StartCoroutine(ReselectDelayed(null));

            if (Transition == PUITransition.ColorTint) if (TargetGraphic.color != SelectedColor)
                {
                    if (NormalColor != Color.white) TargetGraphic.color = NormalColor;
                    else TargetGraphic.color = DefaultColor;
                }
            if (Transition == PUITransition.SpriteSwap) if (TargetGraphic.sprite != SelectedSprite) TargetGraphic.sprite = NormalSprite;
            if (Transition == PUITransition.Animation) if (_Animator != null) _Animator.Play(NormalTrigger);

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

    public enum PUITransition
    {
        None, ColorTint, SpriteSwap, Animation
    }

    public enum PUIMode
    {
        StandardButton, // Just like Unity's original button
        IgnoreDeselect, // Ignore deselecting when clicking on UI with specific layer/tag
        HoverToSelect, // Select when hovering pointer
        OnlyClickButton, // Just for clicking purpose, not select after clicking
    }
}