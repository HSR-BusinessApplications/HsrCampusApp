// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Newtonsoft.Json
{
    using System.IO;

    public static class JsonExtensions
    {
        public static T CreateFromJsonStream<T>(this Stream stream)
        {
            T data;
            using (var streamReader = new StreamReader(stream))
            {
                var decode = streamReader.ReadToEnd();
                data = JsonConvert.DeserializeObject<T>(decode);
            }

            return data;
        }

        public static T CreateFromJsonString<T>(this string json) => JsonConvert.DeserializeObject<T>(json);
    }
}
