<h1 align="center"> YのL Tools - <img width="30" height="30" src="https://github.com/Yunasawa/Yunasawa-No-Library/assets/113672166/956baea7-c881-4f6c-8b10-8e9eca2f66b4" alt=""> Tab Selector UI (PUI) </h1>

<h4 align="center"> Manage several UI tabs in an easier way. <br><br>

<kbd><a align="right" href="https://github.com/Yunasawa/Yunasawa-No-Library/blob/main/Readme.md"><font size="10"> Return to Library </font></a></kbd>

## Description
```
Your making an UI function with several tabs, there're many buttons link to the pages; but you have to make a custom manager
script to manage the buttons and tabs to decide which to show up (enable), which to hide (disable). With this, you can easily
do the work with just some few steps.
```

## Usage Guide

<h3> <img width="20" height="20" src="https://github.com/Yunasawa/Yunasawa-No-Library/assets/113672166/956baea7-c881-4f6c-8b10-8e9eca2f66b4" alt=""> Tab Button </h3>

```
This is the component added to buttons to help showing up linked Tab Page when selected.
```

<b><i> Note: </i></b> 
  - If you add this into a button (object contains Button component), then Button component will be removed.
  - This have Pointable UI's functions so you can edit or invoke actions just like PUI can do.
  - Parent object must have Tab Manager component to run properly.

<b><i> Properties: </i></b>
  - Tab State: State of current Tab Button to show if it is selected or not.
  <p align="center"><img width="410" height="22" src="https://github.com/Yunasawa/Yunasawa-No-Library/assets/113672166/49e25844-7e1e-4521-b709-fb10dae600e3" alt=""></p>

<b><i> Update: </i></b>
  - Fix the compatibility of TSUI and PUI (PUI in IgnoreDeselect mode but TSUI can still have selection transition). 

<br></br>

<h3> ITabSelectable (Interface)</h3>

```
An interface implemented by a Monobehaviour class, added along with Tab Button and handles Tab Button's transitions.
```



---

<h3> <img width="20" height="20" src="https://github.com/Yunasawa/Yunasawa-No-Library/assets/113672166/fde966dd-f4d7-45ab-a63a-dd6eeaa7b286" alt=""> Tab Page </h3>

```
This is the component added to pages showed or hided when linked Tab Button is selected.
```

<b><i> Note: </i></b> 
  - Make sure to refer the pages to "Tab Selection Pair" field in Tab Manager with the key is correct Tab Button.

<b><i> Properties: </i></b>
  - Tab State: State of current Tab Page to show if it is selected or not.
  <p align="center"><img width="410" height="22" src="https://github.com/Yunasawa/Yunasawa-No-Library/assets/113672166/49e25844-7e1e-4521-b709-fb10dae600e3" alt=""></p>

---

<h3> <img width="20" height="20" src="https://github.com/Yunasawa/Yunasawa-No-Library/assets/113672166/21d2e23c-0c3a-4985-a6b2-4ba23946388e" alt=""> Tab Manager </h3>

```
This is the component added to parent of Tab Buttons to to manage buttons and pages.
```

<b><i> Usage: </i></b> 
  - After adding all Tab Button components, inside Tab Manager, click on <kbd>Get All TSUI Buttons</kbd> to quickly get all Tab Buttons.
  <p align="center"><img width="477" height="111" src="https://github.com/Yunasawa/Yunasawa-No-Library/assets/113672166/ed04fdaf-cda8-4708-98ac-4afda034417a" alt=""></p>

  - Switch [Tab Selector Type] into <kbd>Switch Tab</kbd>
  <p align="center"><img width="474" height="29" src="https://github.com/Yunasawa/Yunasawa-No-Library/assets/113672166/a9ff6e41-27fd-4358-9a8c-4f90ab870aae" alt=""></p>

  - Click on <kbd>Get All TSUI Buttons To Key</kbd> to add all buttons to dictionary as keys, then make sure to refer all the pages with currect button key.
  <p align="center"><img width="474" height="184" src="https://github.com/Yunasawa/Yunasawa-No-Library/assets/113672166/e20fdae4-458d-4d9b-868d-109a170aaad7" alt=""></p>
