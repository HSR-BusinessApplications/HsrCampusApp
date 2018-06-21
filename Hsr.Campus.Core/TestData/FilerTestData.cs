// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

// The classes with test data must only be compiled in the Test-Build!
#pragma warning disable SA1124 // Do not use regions
#if TEST_DATA
namespace Hsr.Campus.Core.TestData
{
    using System;
    using System.Text;

    public static class FilerTestData
    {
#region Root

        public static string Root => $@"{{""data"":
    [
        {{
            ""IsDirectory"":true,
            ""IsLink"":false,
            ""Name"":""LV1_Dir1"",
            ""FullPath"":""LV1_Dir1/"",
            ""LastModified"":""2011-08-02T15:26:06.254944+02:00"",
            ""Size"":null,
            ""RawSize"":0,
            ""Url"":""http://skripte.hsr.ch/Bauingenieurwesen/""
        }},
        {{
            ""IsDirectory"":true,
            ""IsLink"":false,
            ""Name"":""Endless"",
            ""FullPath"":""Endless/"",
            ""LastModified"":""2010-12-02T14:45:01.143789+01:00"",
            ""Size"":null,
            ""RawSize"":0,
            ""Url"":""http://skripte.hsr.ch/Elektrotechnik/""
        }},
        {{
            ""IsDirectory"":true,
            ""IsLink"":false,
            ""Name"":""LV1_Dynamic"",
            ""FullPath"":""LV1_Dynamic/"",
            ""LastModified"":""{DateTimeOffset.Now:O}"",
            ""Size"":null,
            ""RawSize"":0,
            ""Url"":""http://skripte.hsr.ch/Elektrotechnik/""
        }},
        {{
            ""IsDirectory"":true,
            ""IsLink"":false,
            ""Name"":""MobileAppTestData"",
            ""FullPath"":""MobileAppTestData/"",
            ""LastModified"":""2010-12-02T18:45:01.123456+01:00"",
            ""Size"":null,
            ""RawSize"":0,
            ""Url"":""http://skripte.hsr.ch/Elektrotechnik/""
        }},
        {{
            ""IsDirectory"":true,
            ""IsLink"":false,
            ""Name"":""BigDir500"",
            ""FullPath"":""BigDir500/"",
            ""LastModified"":""2010-12-02T14:45:01.143789+01:00"",
            ""Size"":null,
            ""RawSize"":0,
            ""Url"":""http://skripte.hsr.ch/Elektrotechnik/""
        }},
        {{
            ""IsDirectory"":false,
            ""IsLink"":false,
            ""Name"":""favicon.ico"",
            ""FullPath"":""favicon.ico"",
            ""LastModified"":""2011-09-27T16:29:39.369991+02:00"",
            ""Size"":""1150 B"",
            ""RawSize"":1150,
            ""Url"":""http://mobile-test.hsr.ch/MobileAppTestData/favicon.ico""
        }}]}}";

#endregion

#region LV1_Dynamic

        public static string Lv1Dynamic => $@"{{""data"":
    [
        {{
            ""IsDirectory"":false,
            ""IsLink"":false,
            ""Name"":""File_{DateTime.Now:mmss}.txt"",
            ""FullPath"":""LV1_Dynamic/File_{DateTime.Now:mmss}.txt"",
            ""LastModified"":""{DateTimeOffset.Now:O}"",
            ""Size"":""1{DateTime.Now:mmss} B"",
            ""RawSize"":1{DateTime.Now:mmss},
            ""Url"":""http://skripte.hsr.ch/Erneuerbare_Energien_Umwelttechnik/""
        }}]}}";

#endregion

#region SingleFile

        public static string SingleFile => @"{{""data"":
    [
        {{
            ""IsDirectory"":false,
            ""IsLink"":false,
            ""Name"":""favicon.ico"",
            ""FullPath"":""favicon.ico"",
            ""LastModified"":""2011-09-27T16:29:39.369991+02:00"",
            ""Size"":""1150 B"",
            ""RawSize"":1150,
            ""Url"":""http://mobile-test.hsr.ch/MobileAppTestData/favicon.ico""
        }}]}}";
#endregion

#region Empty

        public static string Empty => @"{""data"":[]}";

#endregion

#region MobileAppTestData

        public static string MobileAppTestData => @"{
    ""data"":
    [
        {
            ""IsDirectory"":false,
            ""IsLink"":false,
            ""Name"":""100MB.vhd"",
            ""FullPath"":""MobileAppTestData/100MB.vhd"",
            ""LastModified"":""2016-10-06T12:41:43.0775172+02:00"",
            ""Size"":""100.00 MB"",
            ""RawSize"":104858112,
            ""Url"":""http://mobile-test.hsr.ch/MobileAppTestData/100MB.vhd""
        },
        {
            ""IsDirectory"":false,
            ""IsLink"":false,
            ""Name"":""empty.txt"",
            ""FullPath"":""MobileAppTestData/empty.txt"",
            ""LastModified"":""2016-10-06T12:25:08.3799546+02:00"",
            ""Size"":""0 B"",
            ""RawSize"":0,
            ""Url"":""http://mobile-test.hsr.ch/MobileAppTestData/empty.txt""
        },
        {
            ""IsDirectory"":false,
            ""IsLink"":false,
            ""Name"":""Excel.xlsx"",
            ""FullPath"":""MobileAppTestData/Excel.xlsx"",
            ""LastModified"":""2016-10-06T12:31:03.9092129+02:00"",
            ""Size"":""8.31 KB"",
            ""RawSize"":8509,
            ""Url"":""http://mobile-test.hsr.ch/MobileAppTestData/Excel.xlsx""
        },
        {
            ""IsDirectory"":false,
            ""IsLink"":false,
            ""Name"":""favicon.ico"",
            ""FullPath"":""MobileAppTestData/favicon.ico"",
            ""LastModified"":""2016-10-05T09:15:30.894392+02:00"",
            ""Size"":""1.12 KB"",
            ""RawSize"":1150,
            ""Url"":""http://mobile-test.hsr.ch/MobileAppTestData/favicon.ico""
        },
        {
            ""IsDirectory"":false,
            ""IsLink"":false,
            ""Name"":""Image.bmp"",
            ""FullPath"":""MobileAppTestData/Image.bmp"",
            ""LastModified"":""2016-10-06T12:36:41.792887+02:00"",
            ""Size"":""506.12 KB"",
            ""RawSize"":518270,
            ""Url"":""http://mobile-test.hsr.ch/MobileAppTestData/Image.bmp""
        },
        {
            ""IsDirectory"":false,
            ""IsLink"":false,
            ""Name"":""Image.jpeg"",
            ""FullPath"":""MobileAppTestData/Image.jpeg"",
            ""LastModified"":""2016-10-06T12:36:02.5999094+02:00"",
            ""Size"":""91.30 KB"",
            ""RawSize"":93489,
            ""Url"":""http://mobile-test.hsr.ch/MobileAppTestData/Image.jpeg""
        },
        {
            ""IsDirectory"":false,
            ""IsLink"":false,
            ""Name"":""Image.png"",
            ""FullPath"":""MobileAppTestData/Image.png"",
            ""LastModified"":""2016-10-06T12:35:30.6505075+02:00"",
            ""Size"":""19.94 KB"",
            ""RawSize"":20417,
            ""Url"":""http://mobile-test.hsr.ch/MobileAppTestData/Image.png""
        },
        {
            ""IsDirectory"":false,
            ""IsLink"":false,
            ""Name"":""Powerpoint.pptx"",
            ""FullPath"":""MobileAppTestData/Powerpoint.pptx"",
            ""LastModified"":""2016-10-06T12:34:11.1222266+02:00"",
            ""Size"":""53.25 KB"",
            ""RawSize"":54525,
            ""Url"":""http://mobile-test.hsr.ch/MobileAppTestData/Powerpoint.pptx""
        },
        {
            ""IsDirectory"":false,
            ""IsLink"":false,
            ""Name"":""someText.txt"",
            ""FullPath"":""MobileAppTestData/someText.txt"",
            ""LastModified"":""2016-10-06T12:25:54.942565+02:00"",
            ""Size"":""53 B"",
            ""RawSize"":53,
            ""Url"":""http://mobile-test.hsr.ch/MobileAppTestData/someText.txt""
        },
        {
            ""IsDirectory"":false,
            ""IsLink"":false,
            ""Name"":""Test.pdf"",
            ""FullPath"":""MobileAppTestData/Test.pdf"",
            ""LastModified"":""2016-10-06T12:38:02.7443991+02:00"",
            ""Size"":""170.75 KB"",
            ""RawSize"":174845,
            ""Url"":""http://mobile-test.hsr.ch/MobileAppTestData/Test.pdf""
        },
        {
            ""IsDirectory"":false,
            ""IsLink"":false,
            ""Name"":""Word.docx"",
            ""FullPath"":""MobileAppTestData/Word.docx"",
            ""LastModified"":""2016-10-06T12:37:41.2724035+02:00"",
            ""Size"":""32.59 KB"",
            ""RawSize"":33373,
            ""Url"":""http://mobile-test.hsr.ch/MobileAppTestData/Word.docx""
        }
    ]
}
";
#endregion

#region Endless

        public static string GetEndless(string path) => $@"{{""data"":
    [
        {{
            ""IsDirectory"":true,
            ""IsLink"":false,
            ""Name"":""Endless"",
            ""FullPath"":""{path}Endless/"",
            ""LastModified"":""2011-08-02T15:26:06.254944+02:00"",
            ""Size"":null,
            ""RawSize"":0,
            ""Url"":""http://skripte.hsr.ch/Bauingenieurwesen/""
        }},
        {{
            ""IsDirectory"":false,
            ""IsLink"":false,
            ""Name"":""favicon.ico"",
            ""FullPath"":""{path}favicon.ico"",
            ""LastModified"":""2011-09-27T16:29:39.369991+02:00"",
            ""Size"":""1150 B"",
            ""RawSize"":1150,
            ""Url"":""http://mobile-test.hsr.ch/MobileAppTestData/favicon.ico""
        }}]}}";

#endregion

#region GetBigDir

        public static string GetBigDir(string path, int count)
        {
            var content = new StringBuilder();
            content.Append(@"{""data"":[");
            for (var i = 1; i < count; i++)
            {
                content.Append(GetFileEntry(path, i));
                content.Append(",");
            }

            content.Append(GetFileEntry(path, count));
            content.Append("]}");
            return content.ToString();
        }

        private static string GetFileEntry(string path, int index)
        {
            return $@"{{
            ""IsDirectory"":false,
            ""IsLink"":false,
            ""Name"":""File{index}.txt"",
            ""FullPath"":""{path}File{index}.txt"",
            ""LastModified"":""2011-09-27T16:29:39.369991+02:00"",
            ""Size"":""1150 B"",
            ""RawSize"":1150,
            ""Url"":""http://mobile-test.hsr.ch/MobileAppTestData/favicon.ico""
        }}";
        }

#endregion

    }
}
#endif
#pragma warning restore SA1124 // Do not use regions
