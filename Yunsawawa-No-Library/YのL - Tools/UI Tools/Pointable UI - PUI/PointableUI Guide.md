<h1 align="center"> YのL Tools - Pointable UI (PUI) </h1>

<h4 align="center"> A better solution for Unity's Button with more features and advantages. <br><br>

## Description
```
If you are making an inventory system for your game and you use Button to show if the item is selected or invoke methods that
be called when you clicked on the Button, then you have a very big problem, the item slot (Button) will be deselected when you
click on a random place. Or when you make a button to delete an item, you want that when you press the button by accident, and you
want to hover it out of the button then release it to cancel the deleting action, but the Button that Unity gives you is suck? Here
is the solution:

A better version of Unity's Button:
- Have more individual modes for different purposes. Such as
    + Standard Button: A normal button works the same as Unity's but have more event invoked.
    + Ignore Deselect: Solution for unwantedly deselect the button with just clicking on UI objects with specific layer.
    + Hover To Select: Don't need to click on the button, just hover it to make the button selected.
    + Only Click Button: Button only used for clicking, not be selected.
- Familiar usage due to some properties are the same with Unity's button.
```

## Usage Guide

<h3><i> PUI - Graphic </i></h3>

```
Change, switch PUI graphic with familiar properties.
```

<b><i> Properties: </i></b>

<details>
 <summary><b> Transition: Color Tint </b></summary>
 <i> Note: This transition mode requires Image component </i>
 <br></br>
  <ul>
    <li> Target Graphic: Image component of object. </li>
    <li> Normal Color: Color of Target Graphic when PUI is idle. </li>
    <li> Highlighted Color: Color of Target Graphic when pointer enter PUI. </li>
    <li> Pressed Color: Color of Target Graphic when PUI is pressed. </li>
    <li> Selected Color: Color of Target Graphic when PUI is selected. </li>
    <li> Disabled Color: Color of Target Graphic when PUI is not Interactable. </li>
  </ul>
 <p align="center">
   <img width="100%" alt="TransitionColorTint" src="https://github.com/Yunasawa/Yunasawa-No-Library/assets/113672166/d140a057-7bf8-411c-8e90-ab1edbad45bb">
 </p>
</details>

<details>
 <summary><b> Transition: Sprite Swap </b></summary>
 <i> Note: This transition mode requires Image component </i>
 <br></br>
  <ul>
    <li> Target Graphic: Image component of object. </li>
    <li> Normal Sprite: Sprite of Target Graphic when PUI is idle. </li>
    <li> Highlighted Sprite: Sprite of Target Graphic when pointer enter PUI. </li>
    <li> Pressed Sprite: Sprite of Target Graphic when PUI is pressed. </li>
    <li> Selected Sprite: Sprite of Target Graphic when PUI is selected. </li>
    <li> Disabled Sprite: Sprite of Target Graphic when PUI is not Interactable. </li>
  </ul>
 <p align="center">
   <img width="100%" alt="TransitionSpriteSwap" src="https://github.com/Yunasawa/Yunasawa-No-Library/assets/113672166/fe2ad6c5-e18b-4ae6-ae30-8725d076ec64">
 </p>
</details>

<details>
 <summary><b> Transition: Animation </b></summary>
  <ul>
    <li> Normal Trigger: Animation played when PUI is idle. </li>
    <li> Highlighted Trigger: Animation played when pointer enter PUI. </li>
    <li> Pressed Trigger: Animation played when PUI is pressed. </li>
    <li> Selected Trigger: Animation played when PUI is selected. </li>
    <li> Disabled Trigger: Animation played when PUI is not Interactable. </li>
  </ul>
 <p align="center">
   <img width="100%" alt="TransitionColorTint" src="https://github.com/Yunasawa/Yunasawa-No-Library/assets/113672166/65a52cd5-7b43-40e6-af39-5b0c2b17d7e2">
 </p>
</details>

<h3><i> PUI - Event </i></h3>

```
Invoke event assigned to PUI.
```

<b><i> Properties: </i></b>

<details>
 <summary><b> Event Invoked </b></summary>
  <ul>
    <li> OnSelect: Invoked when PUI is selected. </li>
    <li> OnDeselect: Invoked when PUI is deselected. </li>
    <li> OnPointerClick: Invoked when PUI is clicked then released, but not works when pointer is out of PUI. </li>
    <li> OnPointerDown: Invoked when PUI is pressed. </li>
    <li> OnPointerUp: Invoked when PUI is released, still works when pointer is out of PUI. </li>
    <li> OnEnter: Invoked when pointer enter PUI. </li>
    <li> OnExit: Invoked when pointer exit PUI; </li>
  </ul>
 <p align="center">
   <img width="100%" alt="TransitionColorTint" src="https://github.com/Yunasawa/Yunasawa-No-Library/assets/113672166/0484cc5c-0f3b-401a-ad9a-a094259a3a96">
 </p>
</details>


<h3><i> PUI - Mode </i></h3>

```
Specific mode of PUI for different purposes.
```

<details>
 <summary><b> Standard Button </b></summary>
  <ul>
    <li> Usage: Works like a normal Unity's Button. </li>
  </ul>
</details>

<details>
 <summary><b> Ignore Deselect </b></summary>
  <ul>
    <li> Usage: Ignore deselect PUI when click on UI with ignore layer. </li>
  </ul>
  <p align="center">
   <img width=600" alt="TransitionColorTint" src="https://github.com/Yunasawa/Yunasawa-No-Library/assets/113672166/12783369-e76c-4224-86fd-d3da7912a4e7">
  </p>
</details>

<details>
 <summary><b> Hover To Select </b></summary>
  <ul>
    <li> Usage: Select PUI just by hover over it. </li>
  </ul>
</details>

<details>
 <summary><b> Only Click Button </b></summary>
  <ul>
    <li> Usage: Just for clicking purpose, not select after that. </li>
  </ul>
</details>
