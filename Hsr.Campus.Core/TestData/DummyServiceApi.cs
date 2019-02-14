// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

// Classes with test data may only be compiled in the Test-Build
#if TEST_DATA
namespace Hsr.Campus.Core.TestData
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Hsr.Campus.Core.ApplicationServices;

    public class DummyServiceApi : IServiceApi
    {
        public string News => "dummyNewsKey";

        public string Map => "dummyMapKey";

        public string Menu => "dummyMenuKey";

        public string OAuthClient => "dummyOAuthClient";

        public string OAuthSecret => "dummyOAuthSecret";

        public string AccountApiUri => "https://example.com";

        public string TimeperiodUri => "https://example.com";

        public string TimetableUri => "https://example.com";

        public string ExamUri => "https://example.com";

        public string FilerApiUriBase => "https://example.com";

        public string BuildingsUri => "https://example.com";

        public string ImageUriBase => "https://example.com";

        public string MenuFeedsUri => "https://example.com";

        public string MenuUri => "https://example.com";

        public string NewsFeedsUri => "https://example.com";

        public string SportsUri => "https://example.com";

        public string IconUri => "https://example.com";

        public string NewsUri => "https://example.com";

        public string PictureUri => "https://example.com";

        public string WebGuiUri => "https://example.com";

        public string AuthUri => "https://example.com";

        public string TokenUri => "https://example.com";
    }
}
#endif
