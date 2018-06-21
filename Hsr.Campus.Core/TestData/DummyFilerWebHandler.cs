// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

// The classes with test data must only be compiled in the Test-Build!
#if TEST_DATA
namespace Hsr.Campus.Core.TestData
{
    using System.Threading;
    using System.Threading.Tasks;
    using ApplicationServices;
    using Hsr.Campus.Core.OAuth;
    using Infrastructure.WebHandler;
    using ViewModels;

    public class DummyFilerWebHandler : FilerWebHandler
    {
        public DummyFilerWebHandler(IDevice device, IAccountService account, IOAuthUtils oAuth, IHttpClientConfiguration httpClientConfiguration, IServiceApi serviceApi)
            : base(device, account, oAuth, httpClientConfiguration, serviceApi)
        {
        }

        protected override Task<string> GetDirectoryListingJsonAsync(FilerViewModel.FilerArgs args, CancellationToken cancellationToken)
        {
            return Task<string>.Factory.StartNew(() => GetTestData(args), cancellationToken);
        }

        private static string GetTestData(FilerViewModel.FilerArgs args)
        {
            if (args.CurrentDirectory.StartsWith("Endless"))
            {
                return FilerTestData.GetEndless(args.CurrentDirectory);
            }

            switch (args.CurrentDirectory)
            {
                case "":
                    return FilerTestData.Root;
                case "LV1_Dynamic/":
                    return FilerTestData.Lv1Dynamic;
                case "BigDir500/":
                    return FilerTestData.GetBigDir(args.CurrentDirectory, 500);
                case "MobileAppTestData/":
                    return FilerTestData.MobileAppTestData;
                default:
                    return FilerTestData.Empty;
            }
        }
    }
}
#endif
