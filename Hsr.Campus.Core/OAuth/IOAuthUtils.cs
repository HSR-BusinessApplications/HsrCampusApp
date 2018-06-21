// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Hsr.Campus.Core.OAuth
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MvvmCross.Core.ViewModels;

    public interface IOAuthUtils
    {
        string State { get; }

        void DisplayCodeFlowUrl(IMvxViewModel viewModel);

        void DisplalyWebGuiUri();

        string CreateCodeFlowUrl();

        Task<TokenResponse> RefreshTokenAsync(string refreshToken);

        Task<TokenResponse> AuthTokenAsync(AuthorizeResponse response);

        /// <summary>
        /// Parses the authorization token
        /// Checks and resets the state
        /// </summary>
        /// <param name="rawString">Should be something like: hsrcampus://?code=a43f9a4d26704a82b6e913f1eb664515&amp;state=628c5b48-693b-4518-9177-746548403a6a</param>
        /// <returns>Parsed answer</returns>
        AuthorizeResponse AuthToken(string rawString);

        AuthorizeResponse AuthToken(IDictionary<string, string> rawMap);

        Task OAuthSetupAsync(AuthorizeResponse authToken);

        void SaveToken(TokenResponse token);

        Task<string> BearerTokenAsync();
    }
}
