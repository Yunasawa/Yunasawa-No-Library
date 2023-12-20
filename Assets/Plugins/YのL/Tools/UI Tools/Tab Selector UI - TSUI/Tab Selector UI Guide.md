<h1 align="center"> YのL Tools - <img width="30" height="30" src="https://github.com/Yunasawa/Yunasawa-No-Library/assets/113672166/956baea7-c881-4f6c-8b10-8e9eca2f66b4" alt=""> Tab Selector UI (PUI) </h1>

<h4 align="center"> Manage several UI tabs in an easier way. <br><br>

<kbd><a align="right" href="https://github.com/Yunasawa/Yunasawa-No-Library/blob/main/Readme.md"><font size="10"> Return to Library </font></a></kbd>

## Description
```
Your making an UI function with several tabs, there're many buttons link to the pages; but you have to make a custom manager
script to manage the buttons and tabs to decide which to show up (enable), which to hide (disable). With this, you can easily
do the work with just some few steps.
```
<div id="back">
<h2> Table Of Contents </h2>
</div>
  
<a href="#tab-button"><img width="20" height="20" src="https://github.com/Yunasawa/Yunasawa-No-Library/assets/113672166/956baea7-c881-4f6c-8b10-8e9eca2f66b4" alt=""> Tab Button </a><br>
<a href="#tab-action"><img width="20" height="20" src="https://github.com/Yunasawa/Yunasawa-No-Library/assets/113672166/c9dbb0f6-cca8-45bd-9234-553256f7f393" alt=""> Tab Action </a><br>
<a href="#tab-page"><img width="20" height="20" src="https://github.com/Yunasawa/Yunasawa-No-Library/assets/113672166/fde966dd-f4d7-45ab-a63a-dd6eeaa7b286" alt=""> Tab Page </a><br>
<a href="#tab-manager"><img width="20" height="20" src="https://github.com/Yunasawa/Yunasawa-No-Library/assets/113672166/21d2e23c-0c3a-4985-a6b2-4ba23946388e" alt=""> Tab Manager </a><br>

## Usage Guide

<div id="tab-button">
<h3> <img width="20" height="20" src="https://github.com/Yunasawa/Yunasawa-No-Library/assets/113672166/956baea7-c881-4f6c-8b10-8e9eca2f66b4" alt=""> Tab Button &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <a href="back">Back</a></h3>
</div>


```
This is the component added to buttons to help showing up linked Tab Page when selected.
```

<b><i> Note: </i></b> 
  - If you add this into an object, if it doesn't have Button component, then the component will be auto-added. 

<b><i> Properties: </i></b>
  - Label: Label of Button, use to force select the button using Tab Manager.
  - Tab Manager: The manager that manages the tab buttons.
  - Tab State: State of current Tab Button to show if it is selected or not.
  <p align="center"><img width="456" height="101" src="https://github.com/Yunasawa/Yunasawa-No-Library/assets/113672166/e5810475-1bb3-4898-a00a-58b5d457edda" alt=""></p>

---

<div id="tab-action">
<h3> <img width="20" height="20" src="https://github.com/Yunasawa/Yunasawa-No-Library/assets/113672166/c9dbb0f6-cca8-45bd-9234-553256f7f393" alt=""> Tab Action &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <a href="back">Back</a></h3>
</div>

```
This is the component added to buttons to handle Tab Button's events and transitions.
```

<b><i> Note: </i></b> 
  - I fyou want to use this component, it must be added to Tab Buttons.

<b><i> Properties: </i></b>
  - Tab Event: Switch to <kbd>Invoke</kbd> to use Unity Events.
  - Tab Transition: Switch to <kbd>Color And Transform</kbd> to quickly handle buttons transition, ot <kbd>Animation</kbd> to play its animations.
  <p align="center"><img width="460" height="118" src="https://github.com/Yunasawa/Yunasawa-No-Library/assets/113672166/170d5821-4fd1-4860-bb55-63acc4460b1d" alt=""></p>

  - OnSelect: Events invoked when TSUI is selected.
  - OnDeselect: Events invoked when TSUI is deselected.
  <p align="center"><img width="464" height="315" src="https://github.com/Yunasawa/Yunasawa-No-Library/assets/113672166/763dae3f-a025-45b5-80c2-6e2e9b90e17e" alt=""></p>

  - Image Transition: List of coloring transitions of images assigned.
  - Tmp Transition: List of coloring transition of TextMeshPros assigned.
  - Transform Transition: List of transform transition of RectTransform assigned. (If you use Transform Transition, you have to create 2 new individual RectTransform with position, rotation and scale that you want for 2 transition slots.)
  <p align="center"><img width="699" height="475" src="https://github.com/Yunasawa/Yunasawa-No-Library/assets/113672166/ae037802-a70b-4b78-a547-b5e038ebd30d" alt=""></p>

<br></br>

<h3> ITabActionable (Interface)</h3>

```
If you don't want to use Tab Acion, then implement this interface to a Monobehaviour class, add along with Tab Button to handle
your own Tab Action.
```

To use this interface, you have to implement it into a new class, then declare 2 public method: Select() and Deselect()
See this example for ITabActionable:

```csharp
public class ITabButtonTransition : MonoBehaviour, ITabActionable
{
    [SerializeField] private bool _enable;
    [Space(10)]
    [SerializeField] private Image _thisImage;
    [SerializeField] private Color _unSelectColor;
    [SerializeField] private Color _selectColor;

    private void OnValidate()
    {
        if (_thisImage == null) _thisImage = GetComponent<Image>();
    }

    private void Awake()
    {
        if (_thisImage == null) _thisImage = GetComponent<Image>();
    }

    public void Select()
    {
        if (!_enable) return;

        _thisImage?.TweenColor(_thisImage.color, _selectColor, 0.25f);
    }

    public void Deselect()
    {
        if (!_enable) return;

        _thisImage?.TweenColor(_thisImage.color, _unSelectColor, 0.25f);
    }
}
```

---

<div id="tab-page">
<h3> <img width="20" height="20" src="https://github.com/Yunasawa/Yunasawa-No-Library/assets/113672166/fde966dd-f4d7-45ab-a63a-dd6eeaa7b286" alt=""> Tab Page &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <a href="back">Back</a></h3>
</div>

```
This is the component added to pages showed or hided when linked Tab Button is selected.
```

<b><i> Note: </i></b> 
  - Make sure to refer the pages to "Tab Selection Pair" field in Tab Manager with the key is correct Tab Button.

<b><i> Properties: </i></b>
  - Tab State: State of current Tab Page to show if it is selected or not.
  <p align="center"><img width="410" height="22" src="https://github.com/Yunasawa/Yunasawa-No-Library/assets/113672166/49e25844-7e1e-4521-b709-fb10dae600e3" alt=""></p>

---

<div id="tab-manager">
<h3> <img width="20" height="20" src="https://github.com/Yunasawa/Yunasawa-No-Library/assets/113672166/21d2e23c-0c3a-4985-a6b2-4ba23946388e" alt=""> Tab Manager &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <a href="back">Back</a></h3>
</div>

```
This is the component added to parent of Tab Buttons to to manage buttons and pages.
```

<b><i> Usage: </i></b> 
  - Switch [Tab Selector Type] into <kbd>Switch Tab</kbd>
  - After adding all Tab Button components, if the Tab Manager is the parent object of Tab Buttons, youo can click on <kbd>Get All TSUI Buttons</kbd> to quickly get all Tab Buttons.
  - Assign all the page fields with Tab Pages.
  <p align="center"><img width="472" height="235" src="https://github.com/Yunasawa/Yunasawa-No-Library/assets/113672166/283ef36b-d328-4fed-a064-74b38702ead0" alt=""></p>
