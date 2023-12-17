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

<b><i> Update: </i></b>
  - Fix the compatibility of TSUI and PUI (PUI in IgnoreDeselect mode but TSUI can still have selection transition). 
  
<h3> <img width="20" height="20" src="https://github.com/Yunasawa/Yunasawa-No-Library/assets/113672166/956baea7-c881-4f6c-8b10-8e9eca2f66b4" alt=""> Tab Page </h3>

```
This is the component added to pages showed or hided when linked Tab Button is selected.
```

