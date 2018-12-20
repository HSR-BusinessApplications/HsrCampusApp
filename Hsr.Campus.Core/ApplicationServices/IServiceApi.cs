// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.ApplicationServices
{
    public interface IServiceApi
    {
        string News { get; }

        string Map { get; }

        string Menu { get; }

        string OAuthClient { get; }

        string OAuthSecret { get; }

        string AccountApiUri { get; }

        string TimeperiodUri { get; }

        string TimetableUri { get; }

        string ExamUri { get; }

        string FilerApiUriBase { get; }

        string BuildingsUri { get; }

        string ImageUriBase { get; }

        string MenuFeedsUri { get; }

        string MenuUri { get; }

        string NewsFeedsUri { get; }

        string SportsUri { get; }

        string IconUri { get; }

        string NewsUri { get; }

        string PictureUri { get; }

        string WebGuiUri { get; }

        string AuthUri { get; }

        string TokenUri { get; }
    }
}
