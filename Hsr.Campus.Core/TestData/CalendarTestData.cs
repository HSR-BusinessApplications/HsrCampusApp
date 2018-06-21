// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

// The classes with test data must only be compiled in the Test-Build!
#pragma warning disable SA1124 // Do not use regions
#if TEST_DATA
namespace Hsr.Campus.Core.TestData
{
    using System;

    public static class CalendarTestData
    {
#region CalendarFeed1

        /// <summary>
        /// Gets 5 calendar entries
        /// </summary>
        public static string CalendarFeed1 => Stamp(@"[
    {
        ""id"":""jok0v6160k354gsushr817ckqs_20181004T100000Z"",
        ""htmlLink"":""https://www.google.com/calendar/event?eid=am9rMHY2MTYwazM1NGdzdXNocjgxN2NrcXNfMjAxNjEwMDRUMTAwMDAwWiBoc3JzcG9ydGFuZ2Vib3RAbQ"",
        ""summary"":""1-1Mittagsrunning"",
        ""description"":""<<STAMP>> start 2018-10-04T12:00:00+02:00  end 2018-10-04T13:00:00+02:00 Bist du ein erfahrener Läufer und möchtest zusätzlich über Mittag ein intensives Lauftraining absolvieren, bist du beim Mittags-Running am richtigen Ort.\nWeitere Infos auf der HSR-Sport-Webseite.\nwww.hsr.ch/sport"",
        ""location"":""Gebäude 1"",
        ""start"":""2018-10-04T12:00:00+02:00"",
        ""end"":""2018-10-04T13:00:00+02:00""
    },
    {
        ""id"":""qem1usou1b9iha4kh2a5e8agfg_20181004T161500Z"",
        ""htmlLink"":""https://www.google.com/calendar/event?eid=cWVtMXVzb3UxYjlpaGE0a2gyYTVlOGFnZmdfMjAxNjEwMDRUMTYxNTAwWiBoc3JzcG9ydGFuZ2Vib3RAbQ"",
        ""summary"":""1-2Ju-Jitsu (ASVZ)"",
        ""description"":""<<STAMP>>Hast du Lust, diese alte japanische Kampfkunst kennenzulernen?\nDann hast du zwei mal pro Woche die Möglichkeit, beim ASVZ an einem Einsteigertraining teilzunehmen. \nDas Schnuppertraining ist gratis, die Tarife für die Jahresmitgliedschaft und weitere Infos findest du auf der HSR-Sport Webseite.\nwww.hsr.ch/sport"",
        ""location"":""Kantonschule Rämismühle"",
        ""start"":""2018-10-04T18:15:00+02:00"",
        ""end"":""2018-10-04T19:45:00+02:00""
    },
    {
        ""id"":""84nqcnvqbpuq8o60v19cqj9s04_20181004T164500Z"",
        ""htmlLink"":""https://www.google.com/calendar/event?eid=ODRucWNudnFicHVxOG82MHYxOWNxajlzMDRfMjAxNjEwMDRUMTY0NTAwWiBoc3JzcG9ydGFuZ2Vib3RAbQ"",
        ""summary"":""1-3Volleyball"",
        ""description"":""<<STAMP>>weitere Infos: http://www.hsr.ch/Volleyball.7313.0.html\n\nJeden Dienstag treffen wir uns um 18:45 Uhr vor der Lenggis Turnhalle um zusammen Volleyball zu spielen. Ein wirkliches Training erwartet dich bei uns nicht, es geht vor allem um den Spass am Spiel. Willkommen sind alle. Eine Anmeldung ist nicht nötig. Im Winter spielen wir in der Halle. Sobald es das Wetter zulässt, gehen wir auf die Beachfelder im Grünfeld. Natürlich sind bei uns auch Anfänger herzlich willkommen. Nach dem Spielen gehört es dazu, dass man noch etwas Trinken geht!\n\nKosten: gratis"",
        ""location"":null,
        ""start"":""2018-10-04T18:45:00+02:00"",
        ""end"":""2018-10-04T20:15:00+02:00""
    },
    {
        ""id"":""f3brud5v0p23k0v76qbgc1q1m0_20181004T170000Z"",
        ""htmlLink"":""https://www.google.com/calendar/event?eid=ZjNicnVkNXYwcDIzazB2NzZxYmdjMXExbTBfMjAxNjEwMDRUMTcwMDAwWiBoc3JzcG9ydGFuZ2Vib3RAbQ"",
        ""summary"":""1-4Baseball"",
        ""description"":""<<STAMP>>Ob Anfänger, Fortgeschritten oder Profi,  beim Baseballclub Bandits in Rapperswil-Jona ist  Jeder und Jede willkommen! Bist du interessiert Baseball kennen zulernen, hast du Spass am Spiel oder bist du ein ambitionierter Spieler? - Im Fun-Team, 1. Liga-Team, NLA-Team und im Training wird dir das ermöglicht.\n\nWeitere Info zu Sommer- und Wintertrainings, Ausrüstung, Mitgliedschaft, Ansprechpersonen, etc. findest du auf der Webseite des HSR-Sport und unter www.bandits.ch"",
        ""location"":null,
        ""start"":""2018-10-04T19:00:00+02:00"",
        ""end"":""2018-10-04T21:00:00+02:00""
    },
    {
        ""id"":""h9l76pogmpstolg9l2g854d9ak_20181005T100000Z"",
        ""htmlLink"":""https://www.google.com/calendar/event?eid=aDlsNzZwb2dtcHN0b2xnOWwyZzg1NGQ5YWtfMjAxNjEwMDVUMTAwMDAwWiBoc3JzcG9ydGFuZ2Vib3RAbQ"",
        ""summary"":""1-5Sonditionstraining"",
        ""description"":""<<STAMP>>Würdest du dich gerne öfter sportlich betätigen, kannst dich aber nach einem langen Schultag nicht mehr zu einem Training motivieren oder hast ganz einfach Lust, dich tagsüber mehr zu bewegen? Dann ist das HSR-Konditraining über Mittag eine Möglichkeit für dich.\nDas Konditraining deckt Kraft-, Ausdauer- und Koordnationsaspekte zugleich ab. \nFür weitere Infos einfach mal reinschauen!"",
        ""location"":""Aula"",
        ""start"":""2018-10-05T12:00:00+02:00"",
        ""end"":""2018-10-05T13:00:00+02:00""
    }]");

#endregion

#region CalendarFeed2

        /// <summary>
        /// Gets 5 entries, which are further into the future than <see cref="CalendarFeed1"/>
        /// </summary>
        public static string CalendarFeed2 => Stamp(@"[
    {
        ""id"":""q436tdj80u5u7in6nnq640vc5g_20181005T183000Z"",
        ""htmlLink"":""https://www.google.com/calendar/event?eid=cTQzNnRkajgwdTV1N2luNm5ucTY0MHZjNWdfMjAxNjEwMDVUMTgzMDAwWiBoc3JzcG9ydGFuZ2Vib3RAbQ"",
        ""summary"":""2-1Unihockey"",
        ""description"":""<<STAMP>>Du liebst Unihockey und willst dich sportlich betätigen während dem Studi-Alltag? Dann schau doch einmal in ein Unihockey-Training rein. Fairness und Spass haben erste Priorität, dein spielerisches Niveau ist zweitrangig!\nWeitere Infos: www.hsr.ch/sport\n"",
        ""location"":""Turnhalle Südquartier"",
        ""start"":""2018-10-05T20:30:00+02:00"",
        ""end"":""2018-10-05T22:00:00+02:00""
    },
    {
        ""id"":""ul26sabhtfu7a7vb4oktk1mfhk_20181006T100000Z"",
        ""htmlLink"":""https://www.google.com/calendar/event?eid=dWwyNnNhYmh0ZnU3YTd2YjRva3RrMW1maGtfMjAxNjEwMDZUMTAwMDAwWiBoc3JzcG9ydGFuZ2Vib3RAbQ"",
        ""summary"":""2-2Rope-Skipping"",
        ""description"":""<<STAMP>>Kennst du das „Seiligumpe“ vom Pausenplatz oder hast Erinnerungen an den Film „Jump in“?\n\nRope Skipping ist mehr. Die moderne, akrobatische, dynamische und abwechslungsreiche Sportart des Seilspringens. Sie verbindet Kondition mit Koordination und Kraft, fördert den Teamgeist und regt die Kreativität an. In diversen Seilformen werden die verschiedensten Arten von Tricks gesprungen, wobei der Kreativität hier keine Grenzen gesetzt sind.\n\nDu bist neugierig geworden und hast Lust, Rope-Skipping selbst auszuprobieren? Dann schau doch einfach mal vorbei!\n\nWeitere Infos findest du auf der Webseite des HSR-Sport.\nwww.hsr.ch/sport"",
        ""location"":""Aula"",
        ""start"":""2018-10-06T12:00:00+02:00"",
        ""end"":""2018-10-06T13:00:00+02:00""
    },
    {
        ""id"":""jahmp68j38hf2vaejaa7i0r0d4_20181006T150000Z"",
        ""htmlLink"":""https://www.google.com/calendar/event?eid=amFobXA2OGozOGhmMnZhZWphYTdpMHIwZDRfMjAxNjEwMDZUMTUwMDAwWiBoc3JzcG9ydGFuZ2Vib3RAbQ"",
        ""summary"":""2-3HSR Fussball"",
        ""description"":""<<STAMP>>Bist du ein Fussballfan, Ballkünstler oder Hobby-Kicker? Dann ist unsere Gruppe genau das Richtige für dich.\n\nWeitere Infos zur Durchführung im Sommer/Winter und zur Anmeldung findest du auf der Webseite des HSR-Sport.\nwww.hsr.ch/sport"",
        ""location"":""Sommer/Winter unterschiedlich!"",
        ""start"":""2018-10-06T17:00:00+02:00"",
        ""end"":""2018-10-06T19:00:00+02:00""
    },
    {
        ""id"":""qem1usou1b9iha4kh2a5e8agfg_20181006T161500Z"",
        ""htmlLink"":""https://www.google.com/calendar/event?eid=cWVtMXVzb3UxYjlpaGE0a2gyYTVlOGFnZmdfMjAxNjEwMDZUMTYxNTAwWiBoc3JzcG9ydGFuZ2Vib3RAbQ"",
        ""summary"":""2-4Ju-Jitsu (ASVZ)"",
        ""description"":""<<STAMP>>Hast du Lust, diese alte japanische Kampfkunst kennenzulernen?\nDann hast du zwei mal pro Woche die Möglichkeit, beim ASVZ an einem Einsteigertraining teilzunehmen. \nDas Schnuppertraining ist gratis, die Tarife für die Jahresmitgliedschaft und weitere Infos findest du auf der HSR-Sport Webseite.\nwww.hsr.ch/sport"",
        ""location"":""Kantonschule Rämismühle"",
        ""start"":""2018-10-06T18:15:00+02:00"",
        ""end"":""2018-10-06T19:45:00+02:00""
    },
    {
        ""id"":""abfgt36684tcgib1k8ti769omc_20181006T170000Z"",
        ""htmlLink"":""https://www.google.com/calendar/event?eid=YWJmZ3QzNjY4NHRjZ2liMWs4dGk3NjlvbWNfMjAxNjEwMDZUMTcwMDAwWiBoc3JzcG9ydGFuZ2Vib3RAbQ"",
        ""summary"":""2-5 (dup)Ju-Jitsu (PJJCZ)"",
        ""description"":""<<STAMP>>Hast du Lust, diese alte japanische Kampfkunst kennenzulernen?\nDann hast du einmal pro Woche die Möglichkeit, beim Polizei-Ju-Jitsu-Club der Stadt Zürich an einem Einsteigertraining teilzunehmen. \nDas Schnuppertraining ist gratis, die Tarife für die Jahresmitgliedschaft und weitere Infos findest du auf der HSR-Sport Webseite."",
        ""location"":""Sportanlage Sihlhölzli"",
        ""start"":""2018-10-06T19:00:00+02:00"",
        ""end"":""2018-10-06T21:00:00+02:00""
    }]");

        #endregion

        #region CalendarFeed3

        /// <summary>
        /// Gets 5 entries which are further into the future than <see cref="CalendarFeed2"/>
        /// The first entry is the same as the last from <see cref="CalendarFeed2"/>
        /// </summary>
        public static string CalendarFeed3 => Stamp(@"[
    {
        ""id"":""abfgt36684tcgib1k8ti769omc_20181006T170000Z"",
        ""htmlLink"":""https://www.google.com/calendar/event?eid=YWJmZ3QzNjY4NHRjZ2liMWs4dGk3NjlvbWNfMjAxNjEwMDZUMTcwMDAwWiBoc3JzcG9ydGFuZ2Vib3RAbQ"",
        ""summary"":""3-1 (dup) Ju-Jitsu (PJJCZ)"",
        ""description"":""<<STAMP>>Hast du Lust, diese alte japanische Kampfkunst kennenzulernen?\nDann hast du einmal pro Woche die Möglichkeit, beim Polizei-Ju-Jitsu-Club der Stadt Zürich an einem Einsteigertraining teilzunehmen. \nDas Schnuppertraining ist gratis, die Tarife für die Jahresmitgliedschaft und weitere Infos findest du auf der HSR-Sport Webseite."",
        ""location"":""Sportanlage Sihlhölzli"",
        ""start"":""2018-10-06T19:00:00+02:00"",
        ""end"":""2018-10-06T21:00:00+02:00""
    },
    {
        ""id"":""f3brud5v0p23k0v76qbgc1q1m0_20181006T170000Z"",
        ""htmlLink"":""https://www.google.com/calendar/event?eid=ZjNicnVkNXYwcDIzazB2NzZxYmdjMXExbTBfMjAxNjEwMDZUMTcwMDAwWiBoc3JzcG9ydGFuZ2Vib3RAbQ"",
        ""summary"":""3-2  Baseball"",
        ""description"":""<<STAMP>>Ob Anfänger, Fortgeschritten oder Profi,  beim Baseballclub Bandits in Rapperswil-Jona ist  Jeder und Jede willkommen! Bist du interessiert Baseball kennen zulernen, hast du Spass am Spiel oder bist du ein ambitionierter Spieler? - Im Fun-Team, 1. Liga-Team, NLA-Team und im Training wird dir das ermöglicht.\n\nWeitere Info zu Sommer- und Wintertrainings, Ausrüstung, Mitgliedschaft, Ansprechpersonen, etc. findest du auf der Webseite des HSR-Sport und unter www.bandits.ch"",
        ""location"":null,
        ""start"":""2018-10-06T19:00:00+02:00"",
        ""end"":""2018-10-06T21:00:00+02:00""
    },
    {
        ""id"":""81urne3ouiaf2buv0asugpts1o_20181007T101000Z"",
        ""htmlLink"":""https://www.google.com/calendar/event?eid=ODF1cm5lM291aWFmMmJ1djBhc3VncHRzMW9fMjAxNjEwMDdUMTAxMDAwWiBoc3JzcG9ydGFuZ2Vib3RAbQ"",
        ""summary"":""3-3Salsa-Tanzen"",
        ""description"":""<<STAMP>>Bewegst du dich gerne zu Musik? Dann schau doch einfach mal an einer Veranstaltung vorbei!\nWeitere Infos auf der HSR-Sport-Webseite!"",
        ""location"":""Aula"",
        ""start"":""2018-10-07T12:10:00+02:00"",
        ""end"":""2018-10-07T12:55:00+02:00""
    },
    {
        ""id"":""agbe2vgii0q3lgpmdrngs8nehs_20181010T100000Z"",
        ""htmlLink"":""https://www.google.com/calendar/event?eid=YWdiZTJ2Z2lpMHEzbGdwbWRybmdzOG5laHNfMjAxNjEwMTBUMTAwMDAwWiBoc3JzcG9ydGFuZ2Vib3RAbQ"",
        ""summary"":""3-4Fitnesstraining"",
        ""description"":""<<STAMP>>Würdest du dich gerne öfter sportlich betätigen, kannst dich aber nach einem langen Schultag nicht mehr zu einem Training motivieren oder hast ganz einfach Lust, dich tagsüber mehr zu bewegen? Dann ist das HSR-Fitnesstraining über Mittag eine Möglichkeit für dich.\nNach einem Aufwärmen werden in lockerer Athmosphäre verschiedene Körpergewichts-Übungen vorgezeigt oder es wird individuell gedehnt, Seil gesprungen, usw.\nFür weitere Infos solltest du ganz einfach mal reinschauen!"",
        ""location"":""Aula"",
        ""start"":""2018-10-10T12:00:00+02:00"",
        ""end"":""2018-10-10T13:00:00+02:00""
    },
    {
        ""id"":""jok0v6160k354gsushr817ckqs_20181011T100000Z"",
        ""htmlLink"":""https://www.google.com/calendar/event?eid=am9rMHY2MTYwazM1NGdzdXNocjgxN2NrcXNfMjAxNjEwMTFUMTAwMDAwWiBoc3JzcG9ydGFuZ2Vib3RAbQ"",
        ""summary"":""3-5Mittagsrunning"",
        ""description"":""<<STAMP>>Bist du ein erfahrener Läufer und möchtest zusätzlich über Mittag ein intensives Lauftraining absolvieren, bist du beim Mittags-Running am richtigen Ort.\nWeitere Infos auf der HSR-Sport-Webseite.\nwww.hsr.ch/sport"",
        ""location"":""Gebäude 1"",
        ""start"":""2018-10-11T12:00:00+02:00"",
        ""end"":""2018-10-11T13:00:00+02:00""
    }]");

#endregion
        private static string Stamp(string input)
        {
            return input.Replace("<<STAMP>>", DateTime.Now.ToString("hh:mm:s.fff"));
        }
    }
}
#endif
#pragma warning restore SA1124 // Do not use regions
