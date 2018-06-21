// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

namespace Foundation
{
    using System.IO;
    using System.Xml.Serialization;

    public static class NsDataExtensions
    {
        public static NSData SerializeToNSData<T>(this T obj)
            where T : new()
        {
            var serializer = new XmlSerializer(typeof(T));

            using (var textWriter = new StringWriter())
            {
                serializer.Serialize(textWriter, obj);

                return NSData.FromString(textWriter.ToString());
            }
        }

        public static T Deserialize<T>(this NSData source)
            where T : new()
        {
            var serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(new StringReader(source.ToString()));
        }
    }
}
