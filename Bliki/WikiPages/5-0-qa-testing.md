<!-- TITLE: 5.0 QA Testing -->
<!-- SUBTITLE: A central collaborative space to coordinate the QA testing of the 5.0 release -->

# Instructions
Please create a new section for each module, as bugs are found in them.  It should look something like this:

## Module Name

* [ ] Example Bug 1
* [x] Example Bug 2

When you find a bug, add it to the list.  When you're done fixing the bug, check the box.  To avoid having multiple people trying to fix the same bug at the same time, put your name at the beginning of it, in parenthesis, before you start working on it, or if you are planning on taking it, like this:

* [ ] (Steve) Example Bug

# Bugs
## Reviewals
### Less Than High Priority
* [ ] Reviewals application main form size/location seems to be saved on a per-machine basis.  Log in as User 1, and set form size and location to left of screen.  Log in as User 2, form opens to that location.  If screen is moved while in as User 2, and log back in as User 1, it is in User 2's location/size.  This should be on a per-user/per-machine basis, like the Edit Incident and Print Preview settings.  (by Allen)

### Needs Tested
* B4447 - Officer Filter is getting multiples of the officer list (Josh) 
* B4449 - No way to remove the `CompletedById` on a task once it's been completed.  You can pick the blank officer name on the edit task window, but it doesn't clear it in the database.  Only does the date.  (by Allen/Doug Jr)
* B4453 - It should not be possible to resize the screen so small that the controls overlap, get cut off, or become inaccessible.  Either they should resize, the view should become scrollable, or the form should have a minimum size.  This applies to all the main screens accessible via the ribbon bar as all the resizable dialogs (by Steve)
* B4453 - When you resize the window to the smallest size, the binoculars "Views" drop-down button next to the date range gets cropped [After testing 8/19 Allen - this works fine on default zoom level (looks like minimum width of form was adjusted), but if the zoom buttons are used in the lower right, the binoculars disappear again] (by Steve)
* B4458 - The `Edit Employee Task` dialog takes too long to load (by Steve)
* B4459 - The `Assign Employee Task` dialog takes too long to load (by Steve)
* B4466 No indications on the `Search Tasks` tab if a task has been dismissed.  (by Allen)
* B4468 End Dates on the `Task Search` form do not seem to be inclusive.  If I select due date through 9/9/2019, tasks that come due on 9/9/2019 do not appear in results.  Need to set the date to 9/10/2019 to see anything due within the day of 9/9/2019.  (by Allen)
* B4448 - Assign tasks for unstarted documents? (Josh)

### Moved to Axosoft
* B4452 - When you click on the officers button, to show the flotsam, the search field at the top should get focus right away so you can just start typing.  Also, you could be able to, after typing in the search box, just hit the down arrow key to select the items [Tested 9/16/19 by Allen.  The officer name search is working and available by default, but the arrow keys are still not able to move down the filtered list to select an officer by name] (by Steve)
* B4454 - Check to make sure that tasks can't be assigned to a user who does not have permissions to view tasks (by Steve/Allen)
* B4455 - Permission errors need more specific messaging. [Testing 8/19 by Allen- add the requested permission or plain text of the action that was attempted ]
* B4456 - `Can Assign Followup` security permission should be removed (inactivated), once the new `Add Employee Task` permission has been added (and auto-granted based on the old `Can Assign Followup` permission) (by Steve)
	* Someone marked this as fixed, but it doesn't seem to be (by Steve)
* B4457 - In the list of security permissions for employee tasks, all the permissions should be plural (e.g. "Add Note To Others Employee Tasks") rather than singular (e.g. "Add Employee Task").  Also, I don't feel strongly about it, but I feel like the "Employee" qualifier isn't necessary in any of the permissions, since they are already all grouped under the "Employee Tasks" application. (by Steve)
* B4444 - When starting an incident from the Unstarted tab, if an error occurs during the initial save, get a message box style error message `Received 'Incident Report '20190426004' already exists." (5000: InternalServerError) in response to a PUT`.  This should be displaying in one of our standard exception handlers.  https://idnetworks.slack.com/files/U1P4M09JN/FM9BNC6BV/image.png  [Tested 9/16/19 - This is now displaying in the standard exception handler.  However, we should adjust this to show less of the "header in a PUT" stuff, and just be a `BusinessOperationFailed` exception stating the minor details, but not nearly as critical as it was and can wait for hot-fixes]  (by Allen)
* B4467 `Start Supplement` right-click menu on Reviewals search screen is only available for Unstarted reports.  Should that option be available for all report types?  (by Allen)
* B4469 Persist colum size, order, sort for grids (Josh)
* B4470 Review screen needs date filter put back, but it should be like the one on `Due Tasks` (Josh)
### Fixed, Review Please
* [x] (Joe) SQL Script 5.0.034 is not compatible with SQL Server 2008 (TIME ZONE)...
* [x] (Joe) The upgrade script doesn't handle DST when converting between local and UTC
* [x] (Josh) It appears that using `enter` to submit the search for while in a text box can sometimes perform the search without first applying the current changes to the text field (Josh)
* [x] (Josh) The `Edit Employee Task` dialog should have an editable field for `Completed By`.  It, and the `Completed Date` should be disabled if the user doesn't have the `Modify Employee Tasks Completion Information`.  It shouldn't allow you to set one without setting the  other, or blank one out without blanking out the other.  A new context menu option called `Mark Task as Completed` should be added, and it should only be visible if the user has the `Mark Own Employee Tasks as Completed` permission and it's their own task. (by Steve)
* [x] The report number hyperlink on the Assign New Employee Task doesn't work (neither to view nor edit, niether by click nor context menu) (by Steve)
* [x] (Josh) Assign New Task context menu option is available on all three of the task screens, even though the `Add Employee Task` permission is not granted to the logged in user (by Steve)
* [x] (Josh) When I login as a user to whom the `Add Employee Task` permission is granted, but the `Assign Employee Tasks to Others` permission is not granted, I'm not allowed to create new tasks assigned to myself (by Steve)
* [x] (Josh) There is no option to review an incident report via the context menu.  Even though it's to the reviewal stage, where when you double click the item, it opens the report in review mode, when you right click on it it still just shows the option to edit the report, and when you do so, the Review button/panel is not available in the editor window.  The Edit option should be replaced by the Review option once it's at that stage. (by Steve)
	* Josh said he fixed this, but on the Fix Reports, Review Reports, and Search Reports screens, it still shows the Edit option in addition to the Review option (by Steve)
* [x] (Josh) Officer groups don't show on the two officers controls on the Search Tasks screen (by Steve)
* [x] (Josh) The `Assign Employee Task` dialog should default to the current user for the `Assign To` field (by Steve)
* [x] When I login as a user to whom the `Add Employee Task` permission is granted, but the `Assign Employee Tasks to Others` permission is not granted, the `Assign Employee Task` dialog shows the user name, instead of the personnel ID, in the `Assign To` combo box (e.g. `(idnsd) DOGGART, STEVEN [idnsd]` instead of `(SD1) DOGGART, STEVEN [idnsd]`) (by Steve)
* [x] (Josh) (TeamPuffin) Do we need the `Delete Employee Task` permission?  I don't see an option to delete a task anywhere in the UI, even though I have that permission. (by Steve)
	* Someone said this was fixed, but it seems to still be added in the create script and there is no upgrade script that removes it (which there should be, since it's already been deployed to all the QA environments) (by Steve)
* [x] (Josh) When you cancel the Reassign Unstarted Report dialog, it still refreshes the Unstarted Reported screen (by Steve)
* [x] (Josh) Reassign Unstarted Report dialog still doesn't show a full size title bar, and still shows the resize mouse cursor when you hover over the edge of the window, even though the window isn't resizable (by Steve)
* [x] (Josh) The Reassign Unstarted Report dialog should default its combo box to the currently assigned officer (by Steve)
* [x] (Josh) The Review Levels column on the Reviewals Search screen shows an empty tooltip when you hover over one which has no applicable review levels.  It should instead say something like "No Review Necessary". (by Steve)
* [x] (Josh) Some selection-based right-click options are available when no records are selected or even loaded.
	* Josh said he fixed this, but it's still a problem on the Unstarted Reports screen (by Steve)
* [x] You should not be able to deselect all flags (by Steve)
* [x] (Josh) The following columns, on any of the screens, don't allow sorting: `Officer`, `Victim`, `Offender`, `Offense`, `Call Type` *(call type is missing data yet - Josh)* (by Steve)
* [x] (Josh) Unsubmitted Reports screen doesn't automatically refresh after a new task is assigned (by Steve)
* [x] (Josh) Context menu options on Due Tasks screen should use title casing, like all the other screens (by Steve)
* [x] (Josh) The tool-tip on the Search Tasks button on the tool bar shouldn't display a number after the text (by Steve)
* [x] (Josh) All through the UI, it's preferrable (where it's not crazy to do so) to hide buttons, menu options, etc. which the user can never use due to their security permissions, rather than leaving them visible but disabled (by Steve)
* [x] (Josh) The `Description` text-box on the `Assign Employee Task` dialog should be multi-line.  Same goes for the one on the `Edit Employee Task` screen.  Even if we don't allow new-lines in the text, it should wrap.  If we do allow new-lines in the text, which I kinda feel we should, we'll need to handle that when displaying it on a single line in the various lists that display tasks. (by Steve) 
* [x] The `Ok` button on the Officer Groups Maintenance dialog should be relabeled "OK" and should be bigger (by Steve)
* [x] (Josh) The "Assign Employee Task" dialog should be relabeled to "Assign New Task", it should have a title bar, and if it's going to be vertically resizable, the description field should expand with the form (by Steve)
* [x] (Josh) The "Edit Employee Task" dialog should be relabeled to "Edit Task", it should have a title bar (by Steve)
* [x] (Josh) The Assign Employee Task dialog should be displayed modally (by Steve)
* [x] (Josh) The Edit Employee Task dialog should be displayed modally (by Steve)
* [x] (Josh) The Edit Task context menu option on the My Tasks screen should have an ellipsis (by Steve)
* [x] (Josh) The `Edit Employee Task` dialog should show the notes field when it first opens, rather than waiting until it finishes loading to show it (by Steve)
* [x] (Josh) There is no system generated note on a task saying when it was created and to whom it was initially assigned (by Steve)
* [x] (Josh) I can't figure out how to consistently reproduce it, but at least one in 10 times, when I click on a bubble in the officers control, it shows the flotsam instead of removing the officer that I clicked on, and similarly, at least one in ten times, when I click on the empty space on the officer control, it does nothing rather than showing the flotsam (by Steve)
* [x] (Josh) Clicking on the scroll-bar on the officers control causes it to display the flotsam (by Steve)
* [x] (Josh) When you dismiss a task, it doesn't update the lists or notification counts on the other task screens/buttons (by Steve)
* [x] When you add a new task for yourself, and you set the due date to today, the notification count on Due Tasks button isn't automatically updated (by Steve)
* [x] The error/exception dialog should open centered over the current window or at least over the main case management screen (by Steve)
* [x] (Josh) The flotsam for the date control should open below or above the control, not over it (by Steve)
* [x] (Josh) When tabbing through the fields, focus disappears between the CFS and Name fields on the reviewal search screen (by Steve)
* [x] The default button on all dialog windows (the one that happens when you press the enter key--usually the OK button) should be visaully obvious (in windows 10, the standard is a thick blue border around the button) (this is normally done by setting the [IsDefault property] *they all have it, that's just not a standard wpf thing* (https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.button.isdefault)) (by Steve)
* [x] (Joe) On the Search Reports screen, if you deselect the Juvenile and Draft flags, but leave None selected, and then search, it includes ones which have those deselected flags; also if I leave None deselected, some results with no flags are included (by Steve + Josh)
### Fixed and Verified
* [x] (Josh) Due Tasks should have an icon that's different from My Tasks.  Maybe it should be like the Search To-Dos one, but with an alarm clock instead of binoculars (by Steve)
* [x] (Josh) If an exception occurs during the `Search` process on the Search Tasks window, the Search button does not re-enable.  (by Allen/Doug Jr)
* [x] (Josh) When double-click on an `Unstarted` record and hit save, the `Unsubmitted` view does not refresh when the create incident form is closed.  User has to click refresh, which takes a long time to complete. [Tested on 9/16, and it's still not refreshing after clicking save and closing the edit form.  Not sure what point it should be doing so.] (by Allen)
* [x] (Joe) Exception occurs if both To and From dates are set in both the Assigned On and Due By sections.  see https://idnetworks.slack.com/files/U1P4M09JN/FNJ69GCR3/untitled  (by Allen/Doug Jr)
* [x] No reports are visible on the `Unsubmitted` tab when logged in as `sreynolds` on the Martinsville setup.  The following reports should appear in that tab.  `20190803003`, `20190804002`, `20190626005`, `20190609001`, `20190721009`.  (by Allen)
* [x] Group Edit window crashes and does weird stuff when you right click on it (Josh)
* [x] (Josh) Due Tasks date range dropdown can be blanked out (Josh)
* [x] (Josh) On Task grids, rename `View` and other report actions to reflect that they act on a report and not the task (Josh)
* [x] On `Search Tasks`, when an officer name is selected in either the `Assigned To` or `Assigned By` field, it is populating both controls with the same name, and therefore it is impossible to search for a task created by `User 1` and assigned to `User 2`.  (by Allen)
* [x] (Josh) Officer filter from database is not getting set in the UI on launch (by Allen)
* [x] Officer filter from database is not being honored at initial launch of Reviewals, but then takes affect when refresh button is clicked. (by Allen)
* [x] On report `20180605002` on the Martinsville setup.  I logged in as JMinter and added a sticky note to a report by CGriffith.  When I refreshed the grid of the reviewals app logged in as cgriffith, the report did not show in the "Fix" tab.  Further investigation on this found that it was because the report was already approved at both levels, but that raises a bigger question as to how/why it showed up in the `Review` tab for the jminter login.  [Tested again 9/18/19 by Allen, and the Review option is not available if report is approved at all applicable levels for the agency/user combination.] (by Allen)
* [x] (Tim) Exception being thrown when saving a report in Review Mode when adding sticky notes.  Stack trace is here:  https://idnetworks.slack.com/files/U1P4M09JN/FNE63SBT7/untitled (by Allen)
* [x] Logged into Martinsville setup as `jminter` and edited reports `20190803003` and `2019804002` (both for `sreynolds`).  Added a sticky note to each from the `Reports-to-review`, and they disappeared from jminter's view, but ~unable to see them in the `Report-to-fix` tab when logged in as `sreynolds`~.  Reopening the report from the Review Search tab (logged in as jminter) shows the sticky notes added as jminter.  (by Allen)
* [x] (Josh) Reviewals lets you log in as a user with no active personnel record (for instance, this happens with the `service` user), but some features, such as the `Review` panel on an incident, don't show up. Should disable those features which require a personnel record, or add some form of warning so the IDN user knows what's going on. (via Tim)
* [x] (Josh) Changes to Tasks do not appear in any screens without refresh (by Tim)
* [x] On `Reports to Review` tab, if Officer name is selected, the grid refreshes as expected (good).  If the officer name is de-selected by clicking the Red `X` on the name, the grid will never refresh back to the default.  (by Allen)
* [x] Top bar Icon is too high and to the left. Need more padding/margin. (via Tim) **(possibly need to replace `RibbonWindow` as root of `IdnApplicationWindow` with another control)**
* [x] Min, Max, and Close buttons are also truncated when highlighting/hovering in maximized mode. (via Tim)
* [x] (Joe) The UI appropriately disables the `Dismiss Task` context menu option when the user doesn't have the `Dismiss Others Employee Task` permission, but the server doesn't check the permission before performing the operation, so if you change the user's permissions after the UI is running, they are still allowed to do it until they restart the application (by Steve)
* [x] (Joe) The `Dismiss` context menu option on an unstarted report should ask if you are sure (from both the `Unstarted Reports` and `Search Reviewals` screens) (by Steve)
* [x] (Josh) Dismissed tasks are shown in reports screens, when the tasks are expanded, but they aren't crossed out in any way.  They should be crossed out, like completed ones are, but preferrably in a distinguishable way, I think (by Steve)
* [x] The following screens don't show horizontal scroll-bars when the columns are too wide to fit within the list control's view: Reviewals Search, My Tasks, Due Tasks, Tasks Search (by Steve)
* [x] (Joe) When searching reviewals by report number, it always shows a bunch of random incidents in addition to the ones that actually match (for instance, when searching for `SDINC` on the QA system, it shows a bunch that start with `15-` and `18-`)[Unable to recreate, may have been fixed through other changes I made - by Joe 8/30/19] (by Steve)
* [x] (Josh) Launching the Officer Group editor the first time at a site (which would have 0 groups) the spinner never stops spinning on the left pane of the dialog.  (by Allen)
* [x] Clicking the `Save` button on the Add new task screen doesn't give an indication that anything is happening, and remains clickable.  I accidentally assigned 3 tasks to myself for the same report because the save took longer than expected.  The save button should be disabled until a response or exception occurs from the server.  (by Allen)
* [x] A system-generated note should be added to a task when a user dismisses his (or another user's) task.  (by Allen)
* [x] Edit Task window can be brought up by users who do not have permissions to do edit tasks.  User jsnipes on QA has several tasks assigned, and it allows the form to come up, but then errors on save.  This should either allow view and have controls disabled, or be disallowed from the grid/context menu.  (by Allen)
* [x] (Josh)Review tab (actually all tabs) should sort by most recent reports first by date.  When I log in after initial upscale, I'm only seeing 200 reports, and all from 1996 on the Martinsville test environment (as user jminter). [Tested on 8/19, and while I'm getting newer reports (2017 through 2019), the grid is still sorted by report # in ascending order, instead of by date descending.] (by Allen)
* [x] (Tim) (This could be incidents, happens both from Reviewals and within the Cfs lookup in IOR).  The Location on CFS `2019112126` does not fill in the common name on the resulting incident report when started from reviewals.  This should be Chick-Fil-A for this call.  (by Allen)
* [x] `Unsubmitted Reports` screen should be re-titled to `My Unsubmitted Reports` (by Steve)
* [x] (Josh) On the Search Reviews section, the `Stage` column text in the grid should be proper sentence formatting (like the checkboxes).  Currently, they state `InReview` which I'm assuming is the enumeration text.  (by Allen)
* [x] (Joe) Edit task window should have a title bar (by Steve)  (This only displays after receiving responses from the server, and other controls are loaded.  Should be there right away (by Allen))	
* [x] (Josh) Group names on the Officer Group maintenance screen should be editable by pressing `F2` on the keyboard.  (by Allen)
* [x] (Josh, Joe) When launching Reviewals as user that does not have permissions to view tasks, receiving permission denied exceptions.  These should be hidden and the task functionality just disabled/hidden as well (by Allen)
* [x] When launching Reviewals as user that does not have permissions to view employee groups, receiving permission denied exceptions.  These should be hidden and the task functionality just disabled/hidden as well (by Allen)  (Tested on 7/31, and this is still an issue)
* [x] (Joe)The report number criterium on the Tasks Search screen should do a starts-with, or contains search (whichever we are doing in reviewals search), rather than an exact-match search (by Steve)
* [x] (Joe) New `Due:` combo box on the Due Tasks button should have a minimum width.  If a value is not selected, the width of the combobox is only approx. 30 pixels wide, then grows in width once an option is selected.  This should be a set width. (by Allen)
* [x] (Josh) Unstarted tab should sort by most recent pending reports first, by date (by Allen)
* [x] (Joe) Default action on My Unsubmitted Reports screen should be to edit, rather than to view (by Steve)
* [X] When you right-click on a report and choose to assign a task for it, it takes a while, just sitting, seemingly doing nothing, before the assign-to dialog pops up.  If its waiting on some async/long-running task, it should pop-up the empty dialog right away, keep itself disabled, and show a spinner while it loads.  It should have a way to cancel it too if it's taking too long. (by Steve)
* [x] (Joe) When the officer's field is focused, the user should be able to press the F4 key or the Alt+Down keys to display the flotsam.  Also, if the button of the officers control is what get's focused, then the space bar should work as well. (by Steve)
* [x] (Joe) Automatically insert employee task notes whenever task is updated. Those notes form a kind of audit trail and should be marked as system notes, either by a null author id or an additional column. (Josh)
* [x] (Joe) When you press tab on the name control, focus should move to the next field, but instead, it gives focus to the globe button and then to the drop down button (by Steve)
* [x] (Joe) Adding a long note to a task `hmm, so, this is a super long note, and what it is going to do?  can i see it wrap around?' does not wrap the blue note section.  Once add is pressed, all that can be seen is `hmm, so, this is a super long note, a'.  The blue notes should word wrap/expand so that the full note can be seen.  See task assigned to Steve Doggard user on Incident `ES060316-001` for examples. (by Allen)
* [x] (Joe) It should not be possible to select a single cell in the results list.  It should only be possible to select a whole row.  When you click on a cell with the mouse, the whole row should be selected and that cell should not be displayed any differently than any other cell in the row.  The arrow keys should only work to move up and down, not left and right.  The Tab key should should send focus to the From date field and Shift+Tab should send focus to the Search button. (by Steve)
* [x] Logged in as CGriffith on Martinsville setup.  Has "Can Approve Level One", but the Reviewals section is missing. (by Allen)
* [x] (Joe) Assign Task user dropdown is not sorted by anything discernable to the end-user.  This should be sorted by officer last name, then first. *Client has abandoned all hope of sorting and is relying on the server - Josh* (by Allen)
* [x] (Josh) Unstarted tab, when user double-clicks on a pending report, there's no way to cancel the pop-up that appears.  Either add support for the escape key or add an "X" button on top right, or a cancel button that sits between the 2 action items. [After testing, it's not readily apparent that the `Esc` key will canel the popup.  I think there should also be an "X" available] (by Allen)
* [x] (Joe) Make ^ system notes look like system notes (Josh)
* [x] (Joe) Edit Task form has no reference/info to the report it is associated to.  Several times today, I had lost track of which entry I was editing or had clicked on from the grid, and then had a hard time finding my last opened item (by Allen)
* [x] (Joe) The reassign-unstarted-report dialog needs a better title (right now it's "ReassignDialog") (by Steve)
* [x] (Joe) The reassign-unstarted-report dialog should not be minimizable, maximizable, nor vertically resizeable.  It also shouldn't be possible to resize it so small that the controls don't fit anymore (by Steve)
* [x] (Joe) On the Search Reviewals and Search Tasks screens, when you press Shift+Tab while on the From Date field, focus goes to the chevron button, and if you do it again, it goes to the zoom controls.  It should not be possible for the chevron button or zoom controls to get focus (by Steve)
* [x] (Josh/Joe) All officer fields need to be showing users instead of personnel (by Steve)
* [x] (Joe) When adding a note to a task, the server returned an error saying `NotAcceptableException: None of the accepted media types are supported by the resource (http://idn-rms-qa/VaWebServices/QaDev/IdnRms/api/employee-task/note/insert/)` (by Steve)
* [x] (Josh) The Reviewals form is not remembering its location and size between sessions.
* [x] (Joe) I logged in as `idnsd`, went to the `Reports I Need to Review` screen, filtered to only my own reports, scrolled down with the mouse wheel, and the application crashed.  It simply disappeared, with no error message or anything.  I repeated the steps and it happened a second time.  (by Steve)
* [x] (Josh) The Reports I Need to Review screen should never show reports written by the logged in user (by Steve)
* [x] (Tim) The `Assign task` option in the context menus should be re-labeled to `Assign new task`, since, right now, it still sounds like you can only assign one task, like the way it used to work when it was called the follow-up (by Steve)
* [x] (Josh) On the My Unsubmitted Reports screen, when you right click on an item in the list, the View item is in bold, but when you double click, it opens the report in edit mode.  The default action should be the one that's in bold. (by Steve)
* [x] Title casing should be used on all context menu options (by Steve)
* [x] In the context menu, on the results lists, on all the screens, the following menu options should end in ellipses (...): `Assign new task`, `Edit`, `Review`, `Edit Task` (by Steve)
* [x] (Josh) Search button on the "Search" tab should be the default option when Enter is pressed (by Allen)
* [x] (Josh) When pressing the tab key to cycle through the controls on the Search Reviewals screen, it gives focus to the parent box containing the stage check boxes (by Steve)
* [x] When adding a note on a task, the lower right-hand mark of who entered the note only displays the date until the task form is closed and re-opened.  The username does not appear. (by Allen)
* [x] (Josh) The user and date that a note was added to a task, the time is left off. (by Allen)
* [x] (Josh) Reassign-unstarted-task context menu is enabled, even though user does not have permission to do so.  User gets permission denied error upon selecting different officer and clicking `OK`.  (by Allen)
* [x] (Joe) In Tasks Search screen, allow searching by the officer who initiated the report (by Tim/Steve)
* [x] (Tim/Joe) The Officer Grouping Dialog (Groups) does not save officers added to groups (by Tim)
* [x] (Tim) When launching reviewals on QA with the `service` login, getting the error `System.Data.SqlClient.SqlException: Invalid object name 'EmployeeTask'.` at `EmployeeTaskDataAccess` line 204. **Added the 5.0.034.sql file as embedded resource to the project.**
* [x] (Tim) Master name control dropdown not dropping down.
* [x] (Tim) Name control flotsam should not be resizable (or at least not from the two sides that are anchored to the control, whichever those happen to be) (by Steve)
* [x] (Tim) The Officers flotsam doesn't display badge numbers and doesn't have a way to filter by badge number (by Steve) 
* [x] (Joe) Clicking anywhere on the Officers control (on review search, to-do search, and Reports-I-Need-To-Review screen) should display the flotsam (by Steve)
* [x] (Joe) Reviewals keeps an hourglass cursor after load for a long time (via Tim)(Sped up searching, still could use some more optimization, but it's down from 5 minutes to 1 minute)
* [x] (Tim) The Officer Grouping Dialog (Groups) loads the lists (sometimes very) slowly, but doesn't show a spinner demonstrating that it is loading. (by Tim)
* [x] (Tim) Search date range does not return records on the last `To` date.
* [x] (Tim) Officer's `Group` box does not disappear when clicking away from it. (via Tim)
* [x] (Tim) Officer's `Group` box sometimes stops appearing after working on the screen for awhile. (via Tim)
* [x] (Tim) Unclear why clicking above the `Officers` search line opens the `group` box, instead of the officer dropdown (via Tim)
* [x] (Tim) Clickable area above `Officers` line seems small, just maybe the right 1/3 of line. (via Tim)
* [x] (Tim) Main icon for exe and main window should be changed to match the one now used on the RMS menu (by Steve)
* [x] (Josh) When the officers flotsam is displayed, simply moving the mouse so that the cursor is no longer over the flotsam causes it to go away (by Steve) **Increased time-out on leave, but this is as designed, haven't solved - Tim**
* [x] (Tim) It's not clear if the date range on the Search Tasks screen is meant to filter by report date, assigned date, or due date (by Steve)
* [x] (Tim) When Page Down key is pressed on name control, it's supposed to popup the master name search screen  (by Steve)
* [x] (Tim) F4 and Alt+Down should work to show the flotsam on all date controls (by Steve) 
* [x] (Tim) No way to scroll horizontally to view data off the grid to the right (by Allen/Steve)
* [x] (Tim) Review tab is visible when user does not have permissions to approve at any level (bsulliv2 on QA) (by Steve/Allen)
* [x] Officer popup is showing duplicate names on the Review, Search, and Due Tasks tabs.  Also showing Personnel and not Users (by Allen)
* [x] (Tim) Date fields on Review and To-Do search screens don't allow space bar for current date (by Steve)
* [x] Officer popup on the Search tab is showing a list of groups, but the popup on the Review tab does not show the same groups, even when logged in as same user. (by Allen)
* [x] (Joe) Tasks are missing from `Reviewal` data in search results. [After testing by Allen (8-19), the `+` sign now shows, but when clicking the plus, it crashed out of the application]  [Testing 8/20 by Allen, Task `20190701004` assigned to jminter is showing as completed (red strikethrough), but it is not marked as such] (Josh)
* [x] (Joe) Getting duplicate rows in the `Unstarted` tab logged in as cgriffith on the Martinsville qa setup.  Samples are `20190720009` and `20190730005` (by Allen)
* [x] (Joe) Double-clicking on an "Unstarted" record does not fill in the incident # on the new report that is brought up.  `2019112126` while logged in as CGriffith is how I reproduced this.  When I looked up and pre-filled the incident using the CFS Lookup inside IOR, it filled in the Incident # field properly. (by Allen)
* [x] (Tim) Edit button (for groups) and refresh button in the officers flotsam should have tool tips (by Steve)
* [x] The bubbles within the officers control should not receive focus when tabbing through the fields, unless there is going to be some action you can do with each item with the keyboard (by Steve)
* [x] (Joe) The `View Employee Groups` permission should be removed (never added) (by Steve)
* [x] (Joe) Change Employee Group permission names to more comply with standard naming (via Josh)
* [x] Unstarted tab is including calls for service records that have an Incident # that starts with `~`, but an incident report was already manually written for the Incident Number.  Example is CFS `2019061939` for Incident `20190426004`.  Officer had created incident by not using the CFS Lookup. (by Allen)
* [x] (Joe) In the Martinsville data, logged in as `jminter`, trying to reassign a report from the unstarted screen elicits a null ref exception from the server because it can't find the cfs record. I'm just assuming this isn't a data problem. (Josh)
* [x] (Joe) I suspect that I broke the other screens which call `DashboardComponent.SearchAsync` and pass `true` for the `limitByRights` parameter.  I think the correct way to fix that is to remove that `LimitByRights` property from `AllReviewalsSearchParams`, entirely, have the server just do what it's told, without any clever auto-population of the search criteria, and force the server to fully construct the criteria based on the user's permissions itself.  Either that or add a Web API method that the client can call to get the list of all the approval level kind IDs at which the user has permission to approve.  Either way, I think that `LimitByRights` doesn't belong in the search criteria. (Steve)
* [x] (Josh) `KeyNotFoundException` in `IdnCoreService.Controllers.Support.SupportController.GetTableRecords`, as reported by Allen.  At the very least, that method should report a more meaningful error when it can't find a repository for the requested table.
* [x] (Josh) On launching IdnReviewals.exe (directly) with `service` credentials, received a `Cannot Create an Instance of an Interface` error from the service.
* [x] (Joe) Tasks with many many notes continue to grow the popup form, and it eventually gets taller than the screen, and can no longer be scrolled/moved.  Form should have a max size, and then the notes section should be scrollable. *this and the one above it just mean that the scrollbars aren't enabling - J* (by Allen)
* [x] (Joe)(Need approval from The Steve or The Allen) If any of the new permissions can reasonably be automatically assigned to users and roles based on their previous, existing permissions, we should do so as a DB upgrade script so that everyone applicable starts 5.0 with permissions to the new features.
* [x] (Joe) On task screens, Officer, CFS #, Victim, Offender, and Offense columns never show any data (by Steve)
* [x] (Joe) Unsubmitted screen never shows anything, even when there are unsubmitted reports for the current officer (by Steve)
* [x] (Joe) When adding a new task, the server returned an error saying `NotAcceptableException: None of the accepted media types are supported by the resource (http://idn-rms-qa/VaWebServices/QaDev/IdnRms/api/employee-task/insert/)` (by Steve)
* [x] (Joe) All folios need to track CFS number. On reviewals, unstartedcfs needs to switch to just cfs and cfs needs to be added to the EmployeeTask dto. See details in [Slack](https://idnetworks.slack.com/archives/C5KPJPGQY/p1562942288167600)
* [x] (Joe) On Review Search screen, if you go to the to field and type a partial date, like "7/18", and then press tab, the focus moves to the Views button, but the cursor still shows in the To field and it doesn't autocorrect the date until you tab again (by Steve)
* [x] (Joe) On the Review Search screen, the Views button, next to the Date Range should have a tool tip and it should not get focus when tabbing through the fields (by Steve)
* [x] (Josh) When tabbing through the fields on the Review Search screen, the focus goes to the button for the Officers field, and then it goes to the text area of the officers field.  If you hit tab again, it goes to the new group name text box, even when that's not otherwise available via the mouse.  It should only stop at the officers field once. (by Steve)
* [x] (Josh) "Report Type" filters are not hiding when there is only one option.
* [x] (Josh) Option to edit groups only shows up from the search reviewals screen (Josh)
* [x] (Joe) These properties have been added to `AllReviewalsSearchParams` and would appreciate some support on the server: `IncludeFlags` `IncludeNoFlags` `Location` `CfsNumber` (֍)
* [X] (Steve) Server returns the following error when "An error occurred when trying to create a controller of type 'UserController'. Make sure that the controller has a parameterless public constructor" selecting "Assign follow-up" option from the context menu on a report (by Steve)
* [x] (Joe) "Assign follow-up" and "Edit follow-up" options context menu option should be re-labeled "Assign task" and "Edit task", to match the tool-bar.  Similarly, the tool-tip of the To-Do Search button and the title of the search screen should be re-labeled "Search Tasks". (by Steve)
* [x] (Josh) On Reports I Need to Review screen, when you select an officer, it not only doesn't show it in the officer's control, but it doesn't actually filter the list of reports either (the list refreshes, but always shows the full list) (by Steve)
* [x] (Joe) When you start a new main incident report from the Unstarted reports screen, it doesn't fill out the incident number, the CFS number, nor anything else (by Steve)
* [x] (Joe) When you start a new incident supplement report from the Unstarted reports screen, and you choose to start a main report, it populates the CFS number properly, but it doesn't populate the incident number or anything else (by Steve)
* [x] (Josh) Selection filters (folio type, stage, flags) should default to included and none selected should include none. You know what I mean. (by Josh)
* [x] (Josh) Flags filter needs an option for filtering reports with no flags (Josh)
* [x] (Josh) If a task is unread, it should not be included in the total read count (the green notification number) on the My Tasks button.  In other words, if you have two tasks, one of them read and one of them unread, there should be a red 1 and a green 1, meaning one unread and one read.  It shouldn't have a red 1 and a green 2, meaning one unread and two total, which is how it works now (by Steve)
* [x] (Josh) The My Tasks icon on the ribbon bar should explain what the two different colored notification counts mean in the tool tip
* [x] (Josh) You can dismiss a task from the My Tasks screen, but you can't do so from the Due Tasks or Search Tasks screens (by Steve)
* [x] (Steve) When you right-click on a report and choose to assign a task for it, the dialog that pops up shows too many items in the Assign To drop-down.  It's repeating each user many times in the list. Lots of duplicates are coming from `/get_user_summaries` (by Steve, Josh)
* [x] (Steve) When you edit an incident report, and then click the save button in the incident editor window, it goes right back into edit mode (by Steve)
* [x] (Joe) On Search Reviewals screen, Victim, Offender, Offense columns never show any data. Reviewal search results do not include author, offender, victim, or offense information. (by Steve + Josh)
* [x] (Josh) When the application starts, the _Due Tasks_ screen loads before the saved filter settings load, causing them to not be applied initially (Josh)
* [x] While the reviewals module is still loading, it shows draft reports with a juvenile flag instead of a draft flag, then, once it's done loading, they all switch to showing the correct flag (by Steve)
* [x] (Tim) Review Levels column shows all levels, even when they aren't applicable for a particular report (by Steve)
* [x] (Tim) It's not clear, on the Search Tasks screen, whether the officers field is meant to filter by who wrote the report or by who the task is assigned to.  It should probably allow both/either.  This is especially problematic because tasks are assigned to users, but reports are written by personnel, so the fields to not share the same look-up list. (by Steve)
* [x] The Officers flotsam isn't wide enough to show its widest item and it's not scrollable (by Steve) **Made wider, but can't recreate scroll issue - Tim**
	* I meant that the contents wasn't scrollable (as in: there are no scroll bars that allow you to scroll around to see all the contents when the window is too small to show it all)
* [x] (Josh) In the Unstarted reports screen, when you select to show secondary reports, it should show who each report is currently assigned to (by Steve)
* [x] (Josh) All of the lists should default to being sorted by some column by default (probably the report number) (by Steve)
* [x] (Josh) On the Search Reviewals screen, the Location and CFS fields have no effect.  However, even if it did, it feels weird searching on by value that isn't displayed in the results list, so we should probably display them somewhere in each result. (by Steve)
* [x] (Josh) When you assign a new task to an incident from the My Reports to Review screen (and probably others too), and the insert fails, for any reason, the UI still acts like it was successful.  The dialog goes away and the task is added as a sub-item under the incident report.  Once you refresh the list, though, it goes away.  Instead, it should stay on the dialog, displaying a busy indicator until it is succesfully complete.  If it fails, it shouldn't close the dialog.  That way they'll have a chance to try again.  Also, it shouldn't add it as a sub-item in the list until it is successful. (by Steve)
* [x] (Josh)When an officer submits a report for review, the Unsubmitted tab is not auto-refreshing when closing the report's edit form (by Allen/Steve)
* [x] (Tim) Refresh button in officers flotsam is disabled (by Steve)
* [x] (Tim) When you edit a task from the Due Tasks screen, the list doesn't automatically refresh to reflect the changes (by Steve)
* [x] (Josh) Fix the security permissions for Employee Tasks.  They should be: `Add Tasks`, `Modify Task Description`, `Modify Task Due Date`, `Modify Task Completion Information`, `Assign Tasks to Others`, `View Own Tasks`, `View Others' Tasks`, `Add Notes to Own Tasks`, `Add Notes to Others' Tasks`, `Mark Own Tasks as Completed`, `Dismiss Own Tasks`, and `Dismiss Others' Tasks` (by Steve)
* [x] (Josh) Highlight disappears when context menu is displayed, so you can't tell which item you right-clicked on (by Steve)
* [x] (Josh) The Start Report dialog (main report vs. supplement) doesn't need to be resizable (by Steve)
* [x] (Josh) The exception dialog used by Reviewals needs a better icon and title.  It should, presumably, beconsistent with the WinForm one (by Steve)
* [x] Row header does not need to be shown on the left and I don't think that rows not need to be vertically resizable either **Rows are fixed-size now, but the header is where the expander gets displayed. If we want to change that, let's talk** (by Steve)
* [x] (Josh) When you click on a task in the Reports I Need to Review screen (_this seems to be on any place the edit task window comes up_), it takes a long time to display the edit task dialog, with no indicaiton that it's doing anything.  It should show the dialog right away and then display a busy indicator while it loads.  The components should be disabled by default at first with the busy indicator, then enabled once the response(s) are received by the server. (by Steve)
* [x] (Josh) All dialogs should have the Cancel button in the bottom right, and then other similar buttons, like OK, immediately to the left of the Cancel button, like standard windows dialogs (e.g. Open/Save file).  In particular, I noticed that the reassign-unstarted-report dialog was wrong (by Steve)
* [x] (Josh) When a lot of officers are selected, the control keeps growing vertically until eventually, there's no room on the screen for anything else and it starts cropping off on the bottom and there's no way to even scroll down.  It should stop growing at some reasonable size and display a scroll bar.  The max size of the officers control should be small enough that, when the form is resized to it's smallest size, everything still fits on the window. (by Steve)
* [x] (Josh) Escape key and enter key should both work to close officers flotsam (by Steve)
* [x] (Josh) It should be possible, using the arrow and space bar keys to select items in the officers flotsam, using the keyboard (by Steve)
* [x] None of the text controls (except the name control) change color when they are focused (by Steve)
* [x] (Josh) The search box at the top of the officers flotsam should have a search icon on the right side of it, just to make it clear that that's what it does (by Steve)
* [x] (Josh) There is no way to clear all officers from the officers control (except by manually clicking each one) (by Steve)
* [x] When tabbing through the fields on the reviewals search screen, the button on the officers control gets focus where you'd expect it to, but then, after you tab past the search button, the focus goes back to the officers control, but this time it's the whole surrounding container control, not just the button (by Steve)
* [x] Uncompleted tasks are showing as completed in the Reports I Need to Review screen *(not seeing this - Josh & Tim)* (I did see this at one point while testing the week of 8-19-19, but no longer occurring from what I can see..) (by Steve)
* [x] (Josh) All dialog windows, like the Reassign Unstarted Report, should always open centered over the main case managment window, rather than remembering where the dialog was when it was last closed (by Steve)
* [X] (Steve) Unstarted Reports screen doesn't show reports assigned to all officer IDs related to the current user (in my case, I created a CFS, assigned it to officer ID 39, which is the currently active ID for `idnsd`, but it would only show reports assigned to officer ID 35, which is an old, ended personnel record for `idnsd`) (by Steve)
* [X] (Steve) The `Reassign to...` option on the Unstarted Reports screen doesn't work when user has multiple personnel records (in my case I was trying to reassign a report to `idnsd`, and it wasn't finding the active personnel record for that user for some reason, and it was just skipping the operation rather than displaying an error) (by Steve)
* [x] (Steve) I wrote a report (`SDINC1`) as `idnsd`, then I added an open issue to it as `idnas`, but when I log back in as `idnsd`, it doesn't show it on the My Reports to Fix screen (by Steve)
* [x] (Josh) The Reports I Need to Review screen should show only reports that are un-approved in at least one of the levels which the current user can approve, and it should exclude any reports that are awaiting revision, unless it's also awaiting the review of a revision (by Steve)
	* I (Steve) fixed it so that the server only returns unapproved reports, in the levels that the current user can approve, but I didn't fix the UI to correctly construct the search parameters so that it excludes ones that awaiting revision but not awaiting the review of a revision.  To do that, the UI needs to send multiple `AllReviewalsSearchCriteriaSet` objects--one with `AwaitingRevision = false` and another with `AwaitingReviewOfRevision = true`.  
* [x] (Josh) When an officer resolves all open items on report from Fix tab, the grid does not auto-refresh when closing the report's edit form (by Allen/Steve)
* [x] (Joe) Reviews search tab, if all checkboxes are selected, and a report number is provided, all unstarted reports are returned.  Should providing report number limit the results?  (by Allen)
* [x] (Joe) Unstarted tab, the location field is mostly blank.  The same logic used in Incident Reports search results grid should be used.  Street Address; City; State; Zip (Common Name, if it exists, in parenthasis). [Tested 8/23/2019 - Need a space between the address and opening `(` of the common name]  (by Allen)
* [x] (Joe) Reviewals search tab tab, the location field is mostly blank.  The same logic used in Incident Reports search results grid should be used.  Street Address; City; State; Zip (Common Name, if it exists, in parenthasis). [Tested 8/23/2019 - Need a space between the address and opening `(` of the common name] (by Allen)
* [x] Right-clicking on a Task and selecting Dismiss seems to do nothing.  Had new task created by jminter and assigned to jminter for report '20190803006'.  As jminter, I dismissed it, and it remains in the `My Tasks` pane.  (by Allen)
* [x] Logged in as jminter on Martinsville setup, the Assign Task form only has logged in user in the Assign To dropdown.  I think this is one of the new permissions, so we may want to grant the permission to assign tasks to others to users who previously had `Assign Follow-up`.  *This is true.  There is an `Assign Employee Tasks To Others` permission.*  [8/29 - updated DbUpgrade scripts properly assigns new `Assign Task to Others` permission to those users who had Review/Approve.AssignFollowup previously] (by Allen)
* [x] (Joe) The CFS criterium on the Search Reports screen doesn't seem to work (by Steve)
* [x] (Joe) When logged into the 5.0 QA environment as `idnsd`, the `Dismiss Task` context menu option doesn't work for any of the tasks displayed on the `My Tasks` screen.  It seems to simply ignore the action.  It doesn't display any kind of busy indicator, display an error, or refresh the screen.  But it does work from the `Due Tasks` screen.(worked when tested on 8/28/19 - Joe) (by Steve)
* [x] (Josh) The `Due Tasks` screen shows completed tasks (by Steve)
* [x] (Josh) Due Tasks should have a way to filter by user, and it should default to the currently logged in user (by Steve)
	* Josh said he fixed this, but it's not quite right yet.  It doesn't default to the currently logged in user.  Also, when selecting just myself on the 5.0 QA environment (`(SD 1) Doggart Steven [idnsd]`), it still shows some other tasks that are assigned to other users (by Steve)
* [x] (Josh) When first opening the case management screen from the RMS menu, I got an `System.InvalidOperationException: Collection was modified; enumeration operation may not execute.` error  at `System.Linq.Enumerable.ToArray[TSource](IEnumerable1 source)` at `IDN.Ui.Mvvm.OptionExtensions.SelectedValues[TItem,TValue](IEnumerable1 these, Func2 selector)` at `IDN.Ui.Mvvm.OptionExtensions.SelectedValues[T](IEnumerable1 these)` at `IDN.Rms.Reviewals.Ui.ViewModels.DashboardReportsToReviewViewModel.SelectedOfficers()` (by Steve)
* [x] (Josh) The context menu options on the Search Reports screen aren't right for unstarted reports.  They should be the same as the options shown on the Unstarted Reports screen. (by Steve)
* [x] (Josh) The context menu on the Search Reports screen shouldn't show the Review option for Unsubmitted reports (by Steve)
* [x] (Josh) Dismiss Task context menu option should ask if you are sure (by Steve)
* [x] (Allen) Double-clicking on an incident from the Reviewals tab `Reports-To-Approve` as jminter, if the report has already been approved at both levels, the `Approve` button is not enabled in the review screen to allow for removal of approval.  (By Allen)
* [x] (Joe) Officer selection control on the `Reports I Need to Review` section does not filter out the results in the grid.(Unable to reproduce Joe & Josh) (by Allen)
* [x] (Joe) Clicking Refresh on the My Tasks tab crashes the application (when logged in as jsnipes on QA) (tested on 8/9, and still occurring.  Services did happen to be down at the time, but should be a trapped exception).  Tested 8/16, and it doesn't crash the application, but get prompted with 3 exception windows that all state `System.NotSupportedException: The server does not support this request.  Either the client or the server may need to be updated. ---> IDN.Communication.Surface.Exceptions.CommunicationException: Received "Not Found" (404: NotFound) in response to a POST message sent to the Web API resource at "http://10.1.1.205/VaMartinsville/IdnRms/api/user-settings/get-single/".    The request message contained the following headers:
Accept: application/json` (by Allen)
* [x] (Joe) Clicking Refresh on the Review tab crashes the application (when logged in as Bsulliv2 on QA) (tested on 8/9, and still occurring.  Services did happen to be down at the time, but should be a trapped exception).  Tested 8/16, get 3 exception windows stating `Server does not support this request`, and one of the exception windows has 24 nested messages. (by Allen)
* [x] (Joe) Logging in as user `RRatcliffe` on Martinsvlle setup, user has 290 assigned tasks, and it takes 1.5 minutes to return.  [Tested 8/29 - still taking 30 seconds] (by Allen)
* [x] (Joe) Reviewals tab, the location field is mostly blank.  The same logic used in Incident Reports search results grid should be used.  Street Address; City; State; Zip (Common Name, if it exists, in parenthasis). [Tested 8/23/2019 - Need a space between the address and opening `(` of the common name]  (by Allen)

## Incidents
### Critical
### High Priority
* [ ] Email attachments
### Less Than High Priority
### Fixed, Review Please
* [x] *(Multi-Image Branch)* Description updates not saving
* [ ] Start CFS report from main Incident menu - **Decided handled by Case Management**
* [x] Import button - save default path?
* [x] Allow Vehicle import from Master Search
### Fixed and Verified
* [x] (Steve) In the review editor window, when you click the approve button on the toolbar of the review panel, it always shows the default 5 level names, rather than showing the names given in the review setup table (by Steve)
* [x] (Tim) Starting a supplemental incident from the Unstarted tab, I attempted to add a narrative page.  When clicking on the `...` to open the Narrative editor, a null reference exception was received. (by Allen)
* [x] Import multiple attachments at once
* [x] Re-order attachments
* [X] (Steve) When you save an incident, it gets an `IndexOutOfRangeException` in the `IncidentFormPaginator.Paginate` method (by Steve)
* [X] (Steve) When you save an incident, it gets an `IndexOutOfRangeException` on the CFS# field in the `DocumentReviewDataAccess.createReview` method (by Steve)
* [X] (Steve) `NullReferenceException` in `ScarsMarksTattoosControl..ctor` when saving an incident report (by Steve)
* [x] (Tim) Launching the incident viewer fails due to an accidental deletion of the `pagesTransformerFactory` constructor setter in `IncidentFormPaginator`
* [x] (Joe)Addresses written to master address don't have street types.  For instance SD32 saved as "1 MILLENIAL" in master address rather than "1 MILLENIAL DR" (by Steve)
* [x] (Josh) If you close the incident edit window while it's still loading, it displays the following error: `System.NullReferenceException: Object reference not set to an instance of an object.  at IDN.Pages.Edit.Ui.EditCoreControl.getPropertyValues()  at  IDN.Pages.Edit.Ui.EditCoreControl.onCodeBehindIsDone(SupplierDoneEventArgs e)  at IDN.Pages.Edit.Ui.EditCoreControl._codeBehind_Done(Object sender, SupplierDoneEventArgs e)` (by Steve)
* [x] (Tim) Selecting an Offense Category instead of a Statute looks like it fails to save (actually fails to load after save)
* [x] (Mike) Media Page, hovering over non-supported filetype (i.e., PDF), the `Open` button fails to show. (via Tim)
* [X] (Steve) If you start a new report, and you select a CFS with an unstarted report # from the list, it populates the report properly, but the page becomes disabled, as if it was not in edit mode, but the edit button is disabled.  If you switch pages, the page stays disabled, but at least the edit button becomes enabled. (by Steve)
* [X] (Steve) After closing the narrative editor window, the incident editor screen refreshes (after a seemingly too long pause) and then goes into a disabled state, even though it's still in edit mode (by Steve)
* [X] (Steve) When selecting an existing master name record for a person, (Keller, Timothy) who has an address with a suffix and unit (1000 W 39TH ST UNIT 3; ASHTABULA, OH 44004), the address populates with no suffix and no unit (e.g. 1000 39TH; ASHTABULA 44004) (by Steve)
* [x] (Tim) DL # and DL state are both saved to master names as a a combined string (e.g. `OH - 555-001`) in the DL # field, with nothing in the DL State field (by Steve)
* [X] (Steve) I entered the first narrative on a new report (SDINC1), and it showed fine in the narrative window.  Then, when I closed the narrative window, it showed fine on the pages of the incident form.  But then, when reopening the narrative editor window, that window no longer showed the narrative, even though it was still on the form.  The same was true even after saving, and re-opening the whole incident. If I entered a new narrative in the empty window, it overwrote the original one and then had the same problem, itself.  I verified that the narrative COM object does send back the XML containing the narratives, properly, and then the incident module passes the XML back to it properly when it displays it the second time, but the editor window then comes up blank, anyway, even though it was given the XML containing the previously entered narrative. (by Steve)
* [x] (Tim) If you start a new report, and you select a CFS with an unstarted report # from the list, it populates the reported date/time with the call time, rather than the current date/time, and it leaves occurred from/to times blank, even though it could estimate those based on the called/cleared times (by Steve)
* [x] (Tim) When an existing master name record is selected for a vehicle owner, the owner telephone number field does not get populated (by Steve)
* [x] (Tim) When an existing master name record is selected for a property owner, the owner telephone number field does not get populated (by Steve)
* [x] (Tim) When an existing master name record from a CFS source is selected, where their height and weight are empty in the CFS record, and they are saved to master names with "0" for those fields, it populates as "0"s in the incident report (by Steve)
* [x] (Steve) Submit for Review button is not visible on tool bar of the incident editor window when it should be (by Steve)
* [x] (Steve) Import panel doesn't show any items (by Steve)
* [x] (Tim) If you start a new report, and you select a CFS with an unstarted report # from the list, it doesn't populate the SSN, apartment #, phone #, employer name, or work # of the victim (by Steve)
* [x] (Tim) If you start a new report, and you select a CFS with an unstarted report # from the list, it populates "0" for the height and weight of persons when those fields are blank in the CFS (by Steve)
* [x] (Tim) If you start a new report, and you select a CFS with an unstarted report # from the list, it doesn't populate the model field in the vehicle section (by Steve)
* [x] (Tim) There is no close button on the validation pane.  There was one in 3.4.  (by Steve)
* [x] The icons on all of the Clery dialog windows, accessible via the Clery tab on the main ribbon bar of the incident module, need to be changed to match the icons on the ribbon bar (by Steve)
* [x] (Tim) Attachments on media page should be clickable when in View mode. (by Tim/Allen)
* [x] (Tim) Incident Reviewal levels don't match the labels or the number of levels found in `Reviewals` (by Tim) - **MOVED TO AXOSOFT TICKET F2365 and related branch**
* [x] (Tim) Only people who have names should be saved to master names (by Steve)
* [x] We should review all the person types that we write out to master names to make sure they are all useful categorizations (for instance, victims should be `VICTIM`, or `VICTIM - INDIVIDUAL`, rather than just `Individual`, and vehicle and property owners should be `OWNER - PROPERTY` and `OWNER - VEHICLE` rather than just `OWNER`, etc.) (by Steve)
* [x] (Tim) If you select an offense category, instead of a statute, when you save the form, the category's description goes away and doesn't come back (by Steve)
* [x] (Josh)Escape key should work to cancel all dialog windows (by Steve)
* [x ] (Josh) *Tasks don't really have a concept of "waiting since." I switched it to default to Assigned dat.* My Tasks grid is still sorting by Report # assending, and not by Waiting Since (by Allen)
* [x] (Tim) The buttons at the bottom of the Missing Clery Data dialog should have tool-tips.  Same goes for the Contributing Incidents dialog that pops up when you click on a count hyperlink on the Clery report.  They should use the same tool-tips as the ones on the IBR Month Validation Dialog  (by Steve)
* [x] (Tim) When an existing master name record is selected, from a CFS source, which has the DL state and DL # combined in the DL# field (e.g. `OH - 555-001`), it populates it all into the DL # field, leaving the DL State field blank, and it gives an error saying that the DL # is invalid (by Steve)
* [x] (Joe) Search Reviewals screen doesn't show unstarted reports, even when that option is selected (by Steve)
* [x] (Josh) On the Search Reviewals screen, the Flags field has a label, but no control where you can see or edit the value (by Steve)
* [x] (Josh) On the Search Reviewals screen, when tabbing between fields, focus disappears between Report Number and Location, as well as between CFS and Name (by Steve)

## Citations
### Critical
### High Priority
### Moved to Axosoft
* B4465 Several citations lost their associated statutes during the 5.0 upscale.  Examples are:  `0000307611`, `0000266996`, `0000307591`, `0000307585` (by Allen)
* B4472 Logging in as user with no badge throws an exception about a bad xml document at position 2,2.  (by Allen)
### Fixed, Review Please
### Fixed and verified

## Attachments
### Critical
### High Priority
### Less Than High Priority
### Moved to Axosoft
* B4475 When you double click on an empty section of the media page, when it's in edit mode, it now does do the same thing as clicking the Add button, but only if the edit point for that section already has focus.  If you double click on a different section than the one that currently has focus, it doesn't do it. (by Steve)
* B4476 Unable to drag/drop media to the end (i.e. - drag picture 1 to be the last item) if the mouse goes below the last image in the `Sort Media` form.  (by Allen)
### Fixed, Review Please
### Fixed and Verified
* [x] (Tim) I attatched `IVAN KRAMSKOY - CHRIST IN THE WILDERNESS - 1872.JPG` to incident `SDINC1`, and it seems to be attached properly because I can save it and it still works, but on the page, it doesn't show a thumbnail of the image and when I try to view the image, rather than opening it in the image viewer, it launches the default application for "JP" files.  If the issue is that the file name is too long to fit in the field, then the system should automatically shorten it while retaining the proper file type or else give an error and force the user to fix it. (by Steve)
* [x] (Tim) The image viewer isn't working with the `Wool.jpg` file attached to `SDINC1`,  even though the picture shows fine on the page (by Steve)
* [x] (Tim) I attemted to attach a PDF file to an incident report, while the PDF was open for viewing in Adobe Acrobat.  It displayed an error saying that it couldn't access the file because it was locked and then hard-exited the application.  (by Steve)
* [x] (Tim) When you change the description of an attachment on the media page, it uses that description as the file name, rather than retaining the original file name, when you save the attachment (by Steve)
* [x] When you double click on an empty section of the media page, it should do the same thing as clicking the Add button (by Steve)
* [x] (Tim) When you open the incident media page, in edit mode, and you click on one of the attachments to set focus to the edit point, the preview image changes to a different scale.  When you tab off the field, the preview image returns to its correct size. (by Steve)
* [x] (Tim) In the built-in image viewer, if the description for the image is more than a few words long, it truncates the text and there's no way to see what the rest of the description is, even if you resize the form (by Steve)
* [x] (Tim) I attached an RDP file (remote desktop) to an incident, saved it, and then tried to delete the attachment.  When I clicked the delete button, it showed an unhandled exception: `NotSupportedException: ImageConverter cannot convert from (null)`  (by Steve)
* [x] (Tim) When you view an image that's attached to an incident, and it opens the built-in image viewer, it shows a very scaled down low quality version of the image rather than showing it in high quality. (by Steve)

## Master Search
### Critical
### High Priority
### Less Than High Priority
### Moved To Axosoft
* B4477 Alias-connect multiple rows (by Doug)
* B4479 (Tim) When resizing the master name search window smaller, and the vertical size is too small to display both the attachments viewer and the search criteria, the search criteria should take precedence (by Steve) 
	* Mike already fixed this, and it's definitely better than it was, but it could still be improved further.  When on the single-field search tab, it starts cutting off the image spinner much sooner than it needs to.  When on the advanced tab, it first starts cutting off the buttons at the bottom of the systems list,rather than shrinking the list itself, but then it stops doing that and starts cutting off the image spinner, even though the fields above the search button still have room to shrink.  In any case, a minimum size should be set on the form so that you can never make the form so small that it starts cutting things off.  All of the controls should always be accessible, even if it means lots of scrolling, at and available window size.
	* Now, when the master name search screen is resized too small vertically, the bottom part of the system selection controls get cropped by the image spinner (by Steve)
### Fixed, Review Please
* [x] (Tim) No way to clear drop-down values.  Pressing delete key should clear selected value. (by Allen)
* [x] (Tim) Recent searches (both Vehicle and Name) cause errors if you run a second recent query search.  (by Allen)
* [x] (Tim) From main menu, perform a Master Name search.  Go back to menu and try to run Property search, and the title bar changes, but the advanced tab remains on name search fields. (Done from the new menu, legacy menu seems ok) (by Allen)
* [x] (Tim) On the State Query tab on the criteria section on the left side, add scrollbars when necessary if the height of the form does not allow all controls.  (Biggest culprit is the recent queries buttons at the bottom) (by Allen)
* [x] (Tim) When Recently Run queries are searched, and the height of the form is too small, the actual result (3rd/bottom) pane is not visible, and no scroolbars are available to get to it.  (by Allen)
* [x] Allow searches to be passed to all Master Search types
* [x] When the DLNumber column in MasterName contains `OH - 555-009`, as is the format used when saving a person in the CFS module, the master name search screen shows the record as OH for the state (which is correct) and "555" for the number (which is incorrect) (by Steve)
* [x] Allow double-click behavior from State Queries
* [x] Click State Query from Advanced opens State Query Tab and fills fields
* [x] Sometimes when opening/closing MS multiple times from menu, grid is missing
* [x] Right-click option to search on a specific value (like filter-by)
* [x] When you select an item with multiple attachments, and you use the navigation controls on the image spinner to view the other attachments (for instance, you're viewing 3 of 3), and then you select another item which has fewer attachments (for instance, 2), it tries to stay on the same index, but the navigation controls get messed up (for instance, it'll say you're viewing 3 of 2, the next button will be enabled, and the previous button will be disabled).  Instead, when change which item you have selected, it should always reset to showing attachment #1 in the spinner. (by Steve)
* [x] The Select All button in the Select Attachments for Export dialog doesn't work when you click on the actual check box within the button (by Steve)
* [x] The window displayed by the View All Attachments context menu option is supposed to display all of the attachments together, in one list, rather than one at a time.  They should all be the same low-quality thumbnails, like the ones shown in the spinner, and don't really need to be resizable, since, to view the full quality attachment, you can double click on the item in the list.  It should work like a standard list, where it has a selected item which is changable via the single-click or arrow keys and the enter key should do the same thing as double clicking. (by Steve)
### Fixed and Verified
* [x] Default double-click on grid row when opening from RMS menu - open report
* [x] (Mike) Searching for partial SSN in Advanced -> SSN (e.g. "1546") brings back all records (by Steve)
* [x] (Mike) When there are over 1000 records (or whatever the set limit was), it's supposed to cut the results short and display a message saying that only partial results are displayed.  It's not currently doing that.  I was able to run one search which returned over 10K results (and took a long time to run because of it) and it didn't display the message saying that the results were truncated (by Steve)
* [x] Searching for partial phone # in Advanced -> Telephone # (e.g. "2688") brings back all records (by Steve)
* [x] (Tim) Search for "-011" in Advanced -> Reference # finds a handful of results where the reference number ends with that suffix (e.g. "20180330-011"), but if search for "200803" it finds no results (by Steve)
* [x] (Tim) In 3.4, when you search for "Smith" and select the "Last Name Sounds Like" option, you get results including "Smyth" and "Smoot", but in 5.0, it doesn't include anything but exactly matching "Smith" names, even though similar records with those other names exist (by Steve)
* [x] (Tim) Columns resize automatically as you are scrolling through the list of the results, which is disorienting (by Steve)
* [x] (Joe)Searching with an end date but no start date (in In Advanced -> From and To) has no effect.  If you just put in a start date, it works.  And if you put in both a start and end date, it works.  But if you just put an end date, records from dates after that end date are still included. (by Steve)
* [x] (Tim) Single-field search for "Richmond, VA" finds "Barton, Allied" but doesn't show an address in the results grid.  When you view the incident report, though, that person does have an address of "123, Richmond VA" (where the street = "123", city = "Richmond", and state = "VA) (by Steve)
* [x] (Joe)When multiple criteria are entered in the single-search field, separated by semicolons, it shows results maching any of the criteria rather than all of the criteria (OR instead of AND) (by Steve)
* [x] (Mike) If you select a row in the results grid and press the context-menu on the keyboad, it displays an odd mostly empty menu.  Once you right click on any item with the mouse, then after that the keyboard key works fine, until you close and reopen the window.  You get the same weird menu when you right-click on the empty grid before doing a search, or if you right-click on the column headers before right clicking on an item. (by Steve) **.NET RMS MENU REWRITE**
* [x] (Tim) Resizing master name search window cannot be resized with the mouse EXCEPT in the bottom right corner (by Steve) - 
* [x] (Mike) If you click on the master name search button on the RMS Menu, while the Core service is not running, it displays a `NullReferenceException` in `IDN.MasterSearch.Ui.MasterSearchFactory.GetBasicAuthorization`.  The error message should be more relevant. 
	**Can't Reproduce. If I shut off the core service, I get a specific service communication error now. - Tim **
	**I am able to reproduce so I will look into this one. - Mike **
* [x] (Tim) When you enter a name into the master name control and then cick the globe button to search master names for matches, it looks like it does a single-field search, but in reality, it does an advanced search by name.  This is confusing because if you then click the search button to re-do the search, you get different results. (by Steve)
* [x] (Mike) The attachment spinner buttons should be disabled when there is only one image or when there are no images.  Similarly, the left buttons should be disabled when you are viewing the first image and the right buttons should be disabled when you are viewing the last image. (by Steve)
* [x] (Tim) When resizing the master name search window larger, while the Advanced tab is selected, the search criteria sections should grow, first to show all the fields at the top, and second, if there's still more room, to show all the systems.  It's annoying when you have the window maximized, and it has plenty of unused vertical space, but all the criteria stay the same size at the top and you still have to scroll through them (by Steve)
	* [x] Mike already fixed this, and it's definitely better than it was, but it could still be improved further.  On the Advanced tab, when you expand it bigger, the criteria fields above the search button keep growing, bigger than they need to be to show all the fields, and the list of systems never expands. 
* [x] (Tim) The master name search window doesn't remember its location, size, maximized-state, and zoom level (by Steve)
* [x] (Tim) Add tool-tips to the Full Search and Primary Search options (by Steve)
* [x] (Tim) Change text of search button on the Advanced tab to say "SEARCH" rather than "IDN SEARCH", like the single-search tab (by Steve)
* [x] (Joe)The search button should either be disabled while searching (but there'd need to be a way to cancel the current search), or it should change to a cancel button until the search is complete (by Steve)
* [x] (Tim) When you double click on the attachment to view it, the viewer window should show the file name and description somewhere (by Steve)
* [x] (Tim) When you choose to view an attachment, it should launch the default application to show the file, if it's a file type that isn't supported by the built-in image viewer window (that's how the VB6 one worked) (by Steve)
* [x] (Tim) Master Address Search and Master Property Search should both have an option to search by reference number, on their Advanced tab, for consistency, if nothing else, since Names and Property both have that option (by Steve)
* [x] (Tim) At least in the 5.0 RMS VA QA environment, the master property search screen does not show any systems in the check list on the Advanced tab, so you can't do an advanced search (even though the single-field search works, and the results have fairly normal looking values in the System column) (by Steve)
* [x] (Tim) The "drill-down" menu option act on the wrong item when the results grid is sorted by anything other than name (by Steve)
* [x] (Tim) Searched for the name "DOYLE", right-clicked on "MR DARREN DAVID DOYLE II", chose to view the civil report (17-1), got error: `IDN.Exceptions.BusinessOperationFailedException: Unsupported COM class call for file type CIV` (by Steve) **Rms Config requires Civil Web Service URL to work (added to RMS-QA-DEV config)**
* [x] When you right click on a row in the results grid and choose to Group by the column, the text for that row turns all white and stays that way even when you select other rows.  If the background color of that row is blue, you can at least still see it, but when the background for the row is also white, you can't read the text unless the row is highlighted.  (by Steve)
* [x] (Tim) Involvement and Address reports have no rows in printout
* [x] (Tim) The `Report of Person Addresses` report used to show the full street line in the `Address` column, including the number, directionals, and suffix.  In the new one, it now only shows the street name. (by Steve)
* [x] (Tim) Single-field search by reference # doesn't work in master vehicle and master address search screens (by Steve)
* [x] (Tim) Master property and master address search screens doesn't appear to show warning message when results are truncated.  Master vehicle may have the same problem, but we don't seem to have enough records in the DB to test it. (by Steve)
* [x] (Tim) When you double click on a name to have it populate the data into the consuming entry screen, and the consumer specified that they want images (as is the case with the Warrants module), it only asks you to select the images to import if it's already completed the downloading of the previews of the images.  If you just double click on a name, right away, without selecting it first, so it doesn't have time to download the previews, it doesn't show the image selection dialog (by Steve)
* [x] (Tim) When you double click on a name to have it populate the data into the consuming entry screen, and the consumer specified that they want images (as is the case with the Warrants module), and the name that you clicked on has images, when it displays the image selection dialog, it shows 0 images, even though the report has multiple attachments.  If we are filtering them, so it only shows applicable images, then we should skip showing the dialog if there are none that match the criteria. (by Steve)
* [x] (Tim) When you double click on a name to have it populate the data into the Warrants module, and the name that you clicked on has images, so that it pops up the image selection dialog, when you click the continue button, with no images selected, the warrants application locks up. (by Steve)
* [x] The Warrants module asks for login when you open the master names search screen (by Steve) **Tim Covert was working on this issue**
* [x] (Tim) A context menu should be shown when you right click on the attachments viewer, with two options: View Attachment and View All Attachments.  The View Attachment option should be bolded, since that's the action that occurs when you double click.  The View All Attachments should show a resizable window which shows previews of all of the attachments in a scrollable view.  From there, they should be able to double click on any of the attachments to open them individually in the sinlge-image viewer window (or to launch the default application if it's not an image).  This was a feature of the old image spinner. (by Steve)
* [x] There form can not be expanded, horizontally, beyond an arbitrary size (by Steve)
* [x] (Tim) Single-field search for "S" in master address search screen takes an unexpectedly long time, making me think that it isn't properly limiting its queries with TOP or we need to add some SQL indexes (by Steve) **Checked code and Top 1000 is set - Tim**
* [x] (Tim) The involvement report should have column headers on the list at the bottom (by Steve)
* [x] (Tim) The SSN and telephone # search criteria fields should allow partial numbers to be entered and searched (by Steve)
* [x] (Tim) When a full number is entered, which can be parsed as a valid entry, into either the SSN or the telephone # search criteria field, but the properly formatted version is different, the value in the textbox should change to reflect the properly formatted value, like it does for DOBs (i.e. the phone # `1231231234` should be auto-corrected to `123-123-1234`) (by Steve)
* [x] (Tim) Pressing the space bar on a date field should only insert the default (current date) if the field is blank.  For instance, if I try to type `nov 6`, it shouldn't overwrite what I type with the current date as soon as I type the space after the `nov`. (by Steve)
* [x] (Tim) The max results limit warning message is confusing.  It currently says `Only the first 1000 records were returned. Modify search to load more`.  What does it mean by "first"?  And saying "to load more" feels backwards.  It's not possible to load _more_ than the maximum; it's only possible to narrow the criteria to load less, more specific results.  It'd be nice if it was consistent with the one in the incidents module, which just says `Displaying only the 1000 most recent results`.   That requires the logic to actually return the most-recent records (sort by date and take the top 1000), though.  But that would be a good thing to do anyway. (by Steve)
* [x] (Tim) The text box on the Search tab (the single-field one) should get the focus by default when the window first opens, so you can just start typing (by Steve)
* [x] (Tim) Entering multiple criteria, separated by semicolons, in the single-field search, doesn't always work right, especially when one of the criteria is an address.  For instance, if I search for `SD2`, it correctly returns all the people from that incident report, and if I search for `ASHTABULA, OH`, it correctly returns only people with an address in that city, but if I search for `SD; ASHTABULA, OH`, it returns only people from the incident report, and it is a shorter list than before, but still, none of them are from Ashtabula. (by Steve)
* [x] (Tim) The image spinner crops the preview images at top and bottom (e.g. the PDF file attached to SDINC1) (by Steve)
* [x] (Tim) If you go to the warrants application and search for names from the `New Warrant` dialog, and you double click on any name that has no associated file attachments, the master name search screen hides the results list, shows a busy indicator, and never closes (by Steve)
* [x] (Tim) Searching by value in master property's advanced tab only works if you include the cents (by Steve)
* [x] (Tim) The `View Attachment` option in the context menu of the image spinner should be in bold, since it's the default action (by Steve)
* [x] (Tim) The image spinner should not show a context menu when the currently selected item has no attachments (by Steve)
* [x] (Tim) State queries via `service` login recorded as Service even if an officer is spoofed (by Tim)
* [x] (Eduard/Allen?) Recent State Queries search not returning (by Tim)
* [x] (Tim) Ctrl-Q functionality (by Steve)
* [x] (Tim) Vehicle State Queries not enabled (by Tim)
* [x] System checkbox for Calls for Service is misspelled (by Allen)
* [x] (Tim) If master name or vehicle search is opened from new Rms menu before apps appear/message switch registration completed, the State Query tabs never become available (by Allen)
* [x] (Tim) When I (Steve) run RMS, the login and menu come up in the slightly blurry DPI-scaled mode, but normal size.  Then, when I click on the master name search button in the toolbar, the menu suddenly gets smaller and crisper, presumably because it turns off the DPI scaling when the WPF screen opens.  The menu stays tiny until I restart RMS.  I'm running in hi-res (3840x2160) with 150% scaling, on Windows 10.  This is fixed by [setting the High DPI compatibility settings] (https://idnetworks.slack.com/archives/G2LTHPPM1/p1546289112349700) on the shortcut, but it would be better if there was a wrapper around the menu so that it started in the right mode.  **.NET RMS MENU REWRITE**
* [x] (Tim) The minimize/maximize/close buttons on the title bar of the master name search window don't highlight properly when hovering over them, when displayed from the button on the RMS Menu toolbar.  They seem to act properly when shown from the Incident Search screen. Only tested in Windows 10. (by Steve) **.NET RMS MENU REWRITE**
* [x] (Tim) The navigation buttons on the image spinner don't all get disabled when you go from selecting an item that has images to selecting one that has none (by Steve)
* [x] (Tim) The Select Images for Export dialog does not have a title (by Steve)
* [x] (Tim) The Select Images for Export dialog does not resize very well (it may not need to be resizeable at all) (by Steve)
* [x] (Tim) The Select Images for Export dialog should not be minimizable, since it's modal (by Steve)
* [x] (Tim) The `Continue` button on the Select Images for Export dialog is too small.  Both it and the check-boxes are oddly placed. (by Steve)
* [x] (Tim) When you click on the `View All Attachments` option in the image spinner's context menu, it does nothing and the search screen goes away.  When you reopen the search screen, you get an unhandled exception error (by Steve)
* [x] (Tim) There are certain columns, like `Date` which you can't click on the header to sort by it.  (Date works now (on 8/17), but Height and Weight are still unable to be sorted. Allen) (by Steve)
* [x] (Tim) When a person is entered into the incident module with a DL state, but not a DL number, it shows in the master names search results with the state in the DL # column instead of in the DL State column (by Steve)
* [x] The image spinner keeps changing which file it's showing, as it loads them, and in the end, it's left showing the last one even though the label below it says it's showing the first one (by Steve) **Can't Recreate - TIM (maybe fixed?)**
* [x] `Search` tab should be renamed to `Simple`

## Support Data API
### Critical
### Less Than High Priority
* [ ] Support API will stop working after connection error with Core Service (by Tim/Josh)
### Moved to Axosoft
* B4478 Requests for table ID `Rms.Reviewals.FolioType` always return full list even when they are already cached locally (by Steve)
### Fixed, Review Please
* [x] (Tim) `IncidentComVisible` not getting a `SupportCommunication` object with a valid `AccessControl` set. Found in Warrants -> Charge Code launch.
* [x] (Tim) `MissingMethodException` with the message "no parameterless constructor defined for this object" when record set class contains an array property or a nested record set property (by Steve)
### Fixed and Verified
* [x] (Weeks) When writing to the cache, it should make a few attempts to open the cache file before giving up in order to account for concurrent access to the cache (possibly from different proccesses).
* [x] (Steve, Josh, Joe) Support loader needs to be able to deal with concurrent accesses of the same record type. The plan for this is to keep a table of `Task`s indexed by record type that each subsequent request would await. 
* [x] (Tim) Local statutes are not availble in IBR.  They appear to be in the `Support_Statute_Local` table, but they do not appear in the statute lookup in IBR.  (by Allen)

## Folio Reports API
### Fixed, Review Please
* [x] The header section of the Warrant folio report is not populating (when generated via the master name drilldown).  I've investigated it as much as I can, and, near as I can tell, against the mville setup, it's loading the header data (phone and agency name), but just not getting filled on the report (by Allen)
### Fixed and Verified
* [x] (Tim) When you drill down to a incident report from a master name search, and then click the audit trail button on the preview window's tool-bar, it immediately shows a "Permission not granted to perform this action" error message.  However, if you go to the incident module, choose to print the report from the incident search screen, and then view the audit trail from that preview window, it works, so there's obviously something different about the way it checks security when it's going through the core service. (by Steve)
#### CFS Report
* [X] (Steve) Always shows SD1 for incident # (by Steve)
* [X] (Steve) Doesn't show Dispatcher or Disposition (by Steve)
* [X] (Steve) Doesn't show RP/DS (it's called Reg & DL in the UI) (by Steve)
* [X] (Steve) Doesn't show OTHER CONTACTS (it's called Other Info in the UI) (by Steve)
* [X] (Steve) RP/DS and OTHER CONTACTS don't continue onto additional pages when they are too long to fit on the first page (by Steve)
* [X] (Steve) RP/DS on the summary page should be re-labeled to REG & DL (by Steve)
* [X] (Steve) OTHER CONTACTS on the summary page should be re-labeled to OTHER INFO (by Steve)
* [X] (Steve) Doesn't show CALL BACK number (by Steve)
* [X] (Steve) Doesn't show GRID (by Steve)
* [X] (Steve) Prints "0" for height and weight when they are left blank in the UI (by Steve)
* [X] (Steve) Doesn't include apartment #, nor line 2, in the address fields (by Steve)
* [X] (Steve) People Supplement page always prints person's primary phone number as the employer phone number (by Steve)
* [X] (Steve) Only the first line of the Vehicle remarks are printed (by Steve)
* [X] (Steve) Call Description field is too close to the edges of the box that it's printed inside of (by Steve)
* [X] (Steve) If Call Description field is too long to fit on one page, it doesn't print the rest on a continuation page (by Steve)
* [X] (Steve) "Apt#:" in addresses should be "APT#" (by Steve)
* [X] (Steve) Incident-only fields should be removed from the Narrative Supplement page, since it's a CFS narrative, not an incident narrative (viz. the Victim, Offense, Incident Date/Time, Reason Cleared, Date Cleared, and approving officer line) (by Steve)
* [X] (Steve) The CDC# field on People and Vehicle supplement pages should be re-labeled to CFS# (but keep it CDC for IL) (by Steve)
* [X] (Steve) The CD Call Type field on People and Vehicle supplement pages should be re-labeled to "Activity" (but keep it CD Call Type for IL) (by Steve)

## PageDoc
### Critical
### Fixed, Review Please
### Fixed and Verified
* [x] (Tim) When hyperlinking to an incident report, from an involvement report, from a master name search, if you close the preview window, while it's still loading the report, without clicking the cancel button first, it displays this exception: `System.NullReferenceException: Object reference not set to an instance of an object.  at IDN.Pages.Ui.PreviewCoreControl.set_Supplier(IPageDocSupplier value)  at IDN.Pages.Ui.PreviewCoreControl.PreviewCoreControl_Disposed(Object sender, EventArgs e)` (by Steve)
* [x] (Tim) Redactions do not show properly in preview window, even though they appear fine when printed or exported to PDF (by Steve)

## RMS Menu
### Critical
* [ ] When working on training laptops, had to move their domain membership, and immediate logged in.  MSMQ registration failed from message switch, and caused a `Unable to bind to null object` error/exception window.  Need to handle that one quietly and just hide the mail/state query options.   (by Allen)
### High Priority
* [ ] Logging in with user who has permissions to certain apps in `ProductSolution1`, but workstation config is pointed to `ProductSolution2` get several `This application is not installed as part of the current product solution (Applicatoin Handle = EVENT, ProductionSolution ID = ProductSolution2Id` errors.  These should be hidden, and just hide the apps. (by Allen)
### Less Than High Priority
* [ ] Logged into menu, while it was refreshing application views, switched to Setup tab.  `Traffic Citations` and `Warrants` then appeared on the Setup tab.  They did, however, get removed when switching back to `Applications` and then to `Setup` again, and also appeared in `Applications` as expected.  (by Allen) 
### Fixed, Review Please
* [x] (Tim) Parking Ticket App throws error `No current record` **DB TABLES NOT SET UP IN QA-DEV ENVIRONMENT- Tim P**(Tim C)
* [ ] Apps not pulled to front **Don't Understand/Can't Recreate (Tim P)** (Tim C)
* [x] (Tim) No About menu item (Tim C)
* [x] Security Maintenance (Rms Security System) requires a second login **Intentional change in policy - Tim P/Allen** (Tim C)
* [x] Records Deletion app requires a second login **possibly intentional? Tim P** (Tim C)
* [x] Move mail button to main applications list
* [x] Change default app-opening to double-click
* [x] Remove redundant menus in menu bar
* [x] The main list of icons should show scroll bars when there are too many items to fit in the view (by Steve)
* [x] The main list of icons should show which item is currently selected, single clicking on an icon should select it, the arrow keys should work to change which item is selected, and pressing the enter key, while one is selected, should have the same effect as double clicking it (by Steve)
* [x] The menu should never show the Supplementals application when the new incident module is installed (by Steve)
* [x] Name label next to text box should be vertically aligned with the text in the text box (by Steve)
* [x] Logging into the menu while the Core service is down throws an exception about unsupported calls.  This should still launch the menu and put placeholders on the agency name, logged in username, and spinners for the apps, and retry requesting the data several times (30 seconds or 1 minute) before notifying the user and closing out.  (by Allen)
### Fixed and Verified
* [x] Connect all Master Searches to search text box
* [x] Logging in as user that does not have permissions to any applications on the `Setup` tab (Security, Rms Setup, Evidence Collection), the user is presented with a spinner when clicking on that tab.  The spinner should go away once it is determined that there are not available applications on that tab.  This may also be the case on the Administration tab as well.  User `cgriffith` on the Martinsville setup does not have permissions to any Setup applications.  (by Allen)
* [x] Location, form size, and last visible tab selection cached by the menu should be user-specific (similar to the IDN.Pages setting cache).  Currently, it is seemingly on a "per machine" basis.  (by Allen)
* [x] (Tim) Running the new IdnRms.exe directly (without the /noupdate flag) is not shutting down the IdnRms.exe process properly when it shells off to RMS.exe for version checking.  This is causing a warning after version checking completes that Rms is already running.  (by Allen)
* [x] (Tim) RMS Setup is missing from the Setup tab.  This should launch `RMSExplorer.exe` (by Allen)
* [x] (Tim) Launching an application from the menu, closing it, and upon launching a second time, the menu crashes and disappears.  (by Allen)
* [x ] Menu size/location is not persisted between logins. [Testing on 8/20, this is still not working.  Always open to default size] (by Allen)
* [x] Background stretches as form is resized.  The "header" (IDNetworks logo and master search functions on top right) should have a fixed width so they do not get chopped off or "squished" (by Allen)
* [x] Header objects (Agency Name and logged in user info) should be a lighter blue to match the color used on legacy menu.  Also should be less bold. (by Allen)
* [x] The Query Builder icons on the Administration tab don't match the one in the master search tool bar (by Steve)
* [x] No scroll bars available if form is resized and apps go off the page (by Allen)
* [x] The background image should not get warped when you resize the form only vertically or only horizontally (by Steve)
* [x] There should be a minumum window size which keeps every feature still accessible (by Steve)
* [x] The size of the master search tool bar should not change or get cropped as you resize the form horizontally (by Steve)
* [x] The text line, just below the menu, which shows the current agency name and user name should me reformatted to look more like the old menu.  The blue color of the text is too true-blue and clashes with the rest of the screen and the text is too big and bold.  (by Steve)
* [x] Set a minimum height for the quick query buttons on the upper right side. If the form is resized, they go smaller than they should.  (by Allen)
* [x] There should be a scroll bar in the main icons area which appears when there are too many icons to fit in the current window size (by Steve)
* [x] The process doesn't [enable "Visual Styles"](https://docs.microsoft.com/en-us/dotnet/framework/wpf/advanced/how-to-enable-visual-styles-in-a-hybrid-application), so WinForm windows displayed within the same process, like the login screen, look pre-windows XP styled (by Steve)
* [x] The master property search icon looks really bad (you can't even tell what it's supposed to be) (by Steve)
* [x] The tabs change the text to bold when you hover over them with the mouse, but only until you click on one.  Once you do, then the hover indicator goes away.  However, I suggest that, rather than changing the text to bold (which doesn't look any different than the active tab, so you can't see the hover on the active tab and it's a little confusing), it should change the background color of the tab or text, similar to the way the hover works on the module icons. (by Steve)

## CallForService
### Less Than High Priority
* [ ] Clicking `Report Viewer` button in CFS brings up a security login screen and an error message, but then continues to the report viewer after cancelling, when logged in as `service` without spoofing officer. (by Tim)