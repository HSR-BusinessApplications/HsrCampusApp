// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

// The classes with test data must only be compiled in the Test-Build!
#pragma warning disable SA1124 // Do not use regions
#if TEST_DATA
namespace Hsr.Campus.Core.TestData
{
    using System;

    internal static class MenuTestData
    {
        private static readonly Random Rand = new Random();

#region Feeds

        /// <summary>
        /// Gets a list of all available feeds
        /// </summary>
        public static string Feeds
        {
            get
            {
                return @"[
    {
        ""Id"":""mensa"",
        ""ShortName"":""Mensa"",
        ""Name"":""Mensa Hochschule Rapperswil"",
        ""Days"":
        [
            {
                ""Date"":""" + $"{DateTime.Today.AddDays(-1):s}" + @""",
                ""ContentHash"":""1e4e54db01ef270effde32ae34c1bbdc""
            },
            {
                ""Date"":""" + $"{DateTime.Today:s}" + @""",
                ""ContentHash"":""" + $"{Rand.Next():x8}{Rand.Next():x8}{Rand.Next():x8}{Rand.Next():x8}" + @"""
            },
            {
                ""Date"":""" + $"{DateTime.Today.AddDays(1):s}" + @""",
                ""ContentHash"":""4b79ebbc7f1384769b6e01b7014ffca5""
            },
            {
                ""Date"":""" + $"{DateTime.Today.AddDays(3):s}" + @""",
                ""ContentHash"":""5b79ebbc7f1384769b6e01b7014ffca5""
            }]
    },
    {
        ""Id"":""bistro"",
        ""ShortName"":""Bistro"",
        ""Name"":""Forschungszentrum"",
        ""Days"":
        [
            {
                ""Date"":""" + $"{DateTime.Today.AddDays(-1):s}" + @""",
                ""ContentHash"":""23e181fe73765832b4c61d57cbf01e30""
            },
            {
                ""Date"":""" + $"{DateTime.Today:s}" + @""",
                ""ContentHash"":""9d23312ac90cba3a9b359982786d26df""
            },
            {
                ""Date"":""" + $"{DateTime.Today.AddDays(2):s}" + @""",
                ""ContentHash"":""d722194f53bffa487f6836474cdb9b7d""
            },
            {
                ""Date"":""" + $"{DateTime.Today.AddDays(3):s}" + @""",
                ""ContentHash"":""6b79ebbc7f1384769b6e01b7014ffca5""
            }]
    }]";
            }
        }

#endregion

#region Mensa

        /// <summary>
        /// Gets the Mensa Yesterday
        /// </summary>
        public static string MensaYesterday => Stamp(CreateMensaMenu(DateTime.Today.AddDays(-1)));

        /// <summary>
        /// Gets the Mensa Today
        /// </summary>
        public static string MensaToday => Stamp(CreateMensaMenu(DateTime.Today));

        /// <summary>
        /// Gets the Mensa Tomorrow
        /// </summary>
        public static string MensaTomorrow => Stamp(CreateMensaMenu(DateTime.Today.AddDays(1)));

        /// <summary>
        /// Gets the Mensa in three days
        /// </summary>
        public static string MensaIn3Days => Stamp(CreateMensaMenu(DateTime.Today.AddDays(3)));

#endregion

#region Bistro

        /// <summary>
        /// Gets the Bistro Yesterday
        /// </summary>
        public static string BistroYesterday => Stamp(CreateBistroMenu(DateTime.Today.AddDays(-1)));

        /// <summary>
        /// Gets the Bistro Today
        /// </summary>
        public static string BistroToday => Stamp(CreateBistroMenu(DateTime.Today));

        /// <summary>
        /// Gets the Bistro the Day after Tomorrow
        /// </summary>
        public static string BistroAfterTomorrow => Stamp(CreateBistroMenu(DateTime.Today.AddDays(2)));

        /// <summary>
        /// Gets the Bistro in three days
        /// </summary>
        public static string BistroIn3Days => Stamp(CreateBistroMenu(DateTime.Today.AddDays(3)));

#endregion

#region Functions

        private static string CreateMensaMenu(DateTime date)
        {
            return @"<!DOCTYPE html>
<html>
<head>
    <meta charset=""utf-8"" />
    <title>Men&uuml;s am " + $"{date:dd.MM.yyyy HH:mm:ss}" + @"</title>
    <style type=""text/css"">
        body
{
    font-family: ""Frutiger Serif"",
    sans-serif,
    Arial;
            text-align: center;
}

        hr
{
    width: 70%;
}

        .offer
{
    color: gray;
            font-weight: bold;
}

        .title
{
    font-size: larger;
            font-weight: bold;
}

        .price
{
    color: gray;
            font-size: small;
}

        .menu
{
}

        .description
{
}

        .provenance
{
    color: gray;
            font-size: small;
}
    </style>
</head>
<body>
    <div>
        <div class=""menu"">
            <div class=""offer"">Tagesteller</div>
            <div class=""title""><<STAMP>></div>
            <div class=""description"">mit Rotweinsauce, Teigwaren &amp; Erbsen</div>
            <div class=""price"">INT 8.00, EXT 10.60</div>
            <div class=""provenance"">Herkunft: Herkunft: Schweiz</div>
        </div>
        <hr/>
        <div class=""menu"">
            <div class=""offer"">Vegetarisch</div>
            <div class=""title"">" + $"{date:dd.MM.yyyy HH:mm:ss}" + @"</div>
            <div class=""description"">mit leichter Weisswein- &amp; Senfnote, Greyerzerk&#228;se und einem Tagessalat</div>
            <div class=""price"">INT 8.00, EXT 10.60</div>
            <div class=""provenance"">Herkunft: vegetarisch</div>
        </div>
        <hr/>
        <div class=""menu"">
            <div class=""offer"">Wochen-Hit</div>
            <div class=""title"">Kalter Roastbeefteller</div>
            <div class=""description"">mit Pommes Frites und Tartarsauce<br /><br />Zubereitungszeit ca. 4 min</div>
            <div class=""price"">INT 14.50, EXT 16.50</div>
            <div class=""provenance"">Herkunft: Herkunft: Schweiz</div>
        </div>
        <hr/>
        <div class=""menu"">
            <div class=""offer"">T&#228;glich im Angebot</div>
            <div class=""title"">Free - Choice Buffet</div>
            <div class=""description"">-Pak Choi mit Honig und Sesam<br />ger&#246;stetes Wurzelgem&#252;se mit frischen Kr&#228;utern<br />und weitere gluschtige Beilagen sind am Buffet erh&#228;ltlich</div>
            <div class=""price"">INT 8.00, EXT 10.60</div>
        </div>
    </div>
</body>
</html>";
        }

        private static string CreateBistroMenu(DateTime date)
        {
            return @"<!DOCTYPE html>
<html>
<head>
    <meta charset=""utf-8"" />
    <title>Men&uuml;s am " + $"{date:dd.MM.yyyy HH:mm:ss}" + @"</title>
    <style type=""text/css"">
        body
{
    font-family: ""Frutiger Serif"",
    sans-serif,
    Arial;
            text-align: center;
}

        hr
{
    width: 70%;
}

        .offer
{
    color: gray;
            font-weight: bold;
}

        .title
{
    font-size: larger;
            font-weight: bold;
}

        .price
{
    color: gray;
            font-size: small;
}

        .menu
{
}

        .description
{
}

        .provenance
{
    color: gray;
            font-size: small;
}
    </style>
</head>
<body>
    <div>
        <div class=""menu"">
            <div class=""offer"">Daily Bowl</div>
            <div class=""title""><<STAMP>></div>
            <div class=""description"">im Soja-Ingwersud, dazu Basmati und Chinakohl ged&#252;nstet<br /><br />Regular CHF 8.00/10.60<br />Small CHF 6.00/8.00</div>
            <div class=""price""></div>
            <div class=""provenance"">Herkunft: Herkunft: Schweiz</div>
        </div>
        <hr/>
        <div class=""menu"">
            <div class=""offer"">Veggi Bowl</div>
            <div class=""title"">" + $"{date:dd.MM.yyyy HH:mm:ss}" + @"</div>
            <div class=""description"">mit Basmati und s&#252;sssaurem Gem&#252;se<br /><br />Regular CHF 8.00/10.60<br />Small CHF 6.00/8.00</div>
            <div class=""price""></div>
            <div class=""provenance"">Herkunft: vegetarisch</div>
        </div>
        <hr/>
        <div class=""menu"">
            <div class=""offer"">Chef Salad</div>
            <div class=""title"">Caesar Salad Classic</div>
            <div class=""description"">mit gebratenem Poulet, Speck,<br />knusprigen Cro&#251;tons und Parmesanflakes</div>
            <div class=""price"">INT 8.00, EXT 10.60</div>
        </div>
        <hr/>
        <div class=""menu"">
            <div class=""offer"">Season Salad</div>
            <div class=""title"">Farmer Salad</div>
            <div class=""description"">Bunter Blattsalat<br />mit Ei, Cherrytomaten und ger&#246;steten Kernenmix</div>
            <div class=""price"">INT 6.00, EXT 8.00</div>
        </div>
    </div>
</body>
</html>";
        }

        private static string Stamp(string input)
        {
            return input.Replace("<<STAMP>>", DateTime.Now.ToString("HH:mm:ss.fff"));
        }

#endregion
    }
}
#endif
#pragma warning restore SA1124 // Do not use regions
