# Testdata Description

## When to use the Testdata
You need to use the Testdata if you don't have the API Keys for the following services:
* Calendar
* News
* Map
* Menu
* OAuth

If you need the real data you can request the API Keys from the HSR IT-BA team.

## General Information
To use the Testdata you need to take the following steps:
1. In Visual Studio switch the Build to "Test"
2. Uninstall the App from the device (emulator or real device)
3. Deploy and run the App on your device
4. Run the authentication to receive a dummy token

After you've received a dummy token, all functionality should be available and you should be able to use the app as usual.

## Specific Information and Limitations
Generally all values delivered by the services listed above (Calendar, News, Map, Menu, OAuth) will be fake values unless you implemented the correct API keys and are using the Debug or the Release build.

### News Button
* The "HSR" tab will always be empty.
* The "VSHSR" tab will have fake data. The dates of the fake data can be old.
* Clicking the read more button can lead to wrong homepages. It is not assured that the links are correct urls regarding the posts.
* There are multiple News- entries which have a timestamp. This timestamp should be renewed every time you refresh.
* All images will be a big green box.

### HSR Sport Button
* The "Agenda" tab is the only tab shown and will have fake data.
* The "read more" button will not work
* There are multiple news- entries which have a timestamp. This timestamp should be renewed every time you refresh.

### Campus Map Button
* There are three Buildings available in the test data
* All floors have color codes: UG - grey, EG - white, OG1 - blue, OG2 - orange
* The first building has four floors (UG, EG, OG1, OG2)
* The second building has three floors (EG, OG1, OG2)
* The third building has two floors (EG, OG1)

### Menu Button
* The "Mensa" tab will have fake menues from Yesterday, Today, Tomorrow and in three days (example: Today is Wednesday, following days will be shown: Tue, Wed, Thu, Sat)
* The "Bistro" tab will have fake menues from Yesterday, Today, in two days and in three days (example: Today is Wednesday, following days will be shown: Tue, Wed, Fri, Sat)
* Switching between days should affect the other tab's day selection. This behaviour can be weird with the test data. Example: Switching from Yesterday to Today (and vice versa) works perfectly fine but switching from Today to Tomorrow in the Mensa will lead to the switch from Today to "in two days" in the Bistro tab (due to the way the data is set up).
* There are multiple menue- entries which have a timestamp. This timestamp should be renewed every time you refresh.

### Lecture Notes Button
* The Lectore Notes will show a couple of folders with some fake data.
* LV1_Dir1 is empty
* BigDir500 LV1_Dynamic and MobileAppTestData will have some files in it.
* Endless can be opened endlessly and will always contain an Endless folder and a file (favicon.ico)
* All files can be used as if the data was real.

### Timetable (Calendar) Button
* To get the Timetable you need to authenticate first. There should be an "Account Setup" on the homescreen if you're not authenticated yet. Once you click on "HSR OAuth" you will receive a popup message asking you if you want to accept the dummy token. If you click on OK you will be authenticated using the dumy token and will be able to use the calendar.
* There will always be at least two fake timetables shown: [FS/HS] YYYY - Unterricht (dummy) (the current semester) and [FS/HS] YYYY - timestamp (dummy) (the current semester's exams). If you are near the end of the semester a third timetable will be shown for the semester coming [FS/HS] YYYY - Unterricht (dummy) (the next semester). FS stands for spring semester, HS for autumn semester.
* The current semester will always have the following tabs: MISC, MO, TU, We, TH, SA, SU
* The current semester exams will always have the following tabs: WEEK 1, WEEK 2, WEEK 3, WEEK 4, WEEK 5 where WEEK 5 will always be empty (no exams)
* The next semester will always have the following tabs: MO, TU, WE, TH, FR
* The timestamp of the exams timetable will be renewed every time you refresh the timetable view.

### Badge Button
* The Badge will always show a fake value.
* Refreshing the view will increase the amount shown.

### Account Setup Button
* There should be an "Account Setup" on the homescreen if you're not authenticated yet. Once you click on "HSR OAuth" you will receive a popup message asking you if you want to accept the dummy token. If you click on OK you will be authenticated using the dumy token and will be able to use the app as an authenticated user.
* You can't open the OAuth Permissions url with a dummy token
