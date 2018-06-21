// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

// The classes with test data must only be compiled in the Test-Build!
#if TEST_DATA
namespace Hsr.Campus.Core.TestData
{
    using System;
    using System.Collections.Generic;
    using Model;

    internal static class MapTestData
    {
        public static MapOverview MapData => new MapOverview()
        {
            Id = new Guid(1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0),
            Hash = default(Guid).ToString(),
            Name = "HSR",
            Buildings = new List<MapBuilding>()
            {
                new MapBuilding()
                {
                    Id = new Guid("38f773b6-7266-4a76-b9a3-c73a331e8f66"),
                                    Hash = Guid.NewGuid().ToString(),
                                    Coordinates = "78,400,600,1400",
                                    Name = "Hauptgebäude 1",
                                    Floors = new List<MapHashable>()
                                    {
                                        new MapHashable()
                                        {
                                            Id = new Guid(2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0),
                                                            Hash = default(Guid).ToString(),
                                                            Name = "UG"
                                        },
                                        new MapHashable()
                                        {
                                            Id = new Guid(3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0),
                                                            Hash = default(Guid).ToString(),
                                                            Name = "EG"
                                        },
                                        new MapHashable()
                                        {
                                            Id = new Guid(4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0),
                                                            Hash = "iIyp0CD7ueQotLws/VjiltWtErY=",
                                                            Name = "01"
                                        },
                                        new MapHashable()
                                        {
                                            Id = new Guid(5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0),
                                                            Hash = default(Guid).ToString(),
                                                            Name = "02"
                                        },
                                    }
                },
                new MapBuilding()
                {
                    Id = new Guid("05f9576c-3ab7-461e-99d9-dd5caa1cec5b"),
                                    Hash = Guid.NewGuid().ToString(),
                                    Coordinates = "841,587,1128, 1100",
                                    Name = "Laborgebäude 2",
                                    Floors = new List<MapHashable>()
                                    {
                                        new MapHashable()
                                        {
                                            Id = new Guid(6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0),
                                                            Hash = default(Guid).ToString(),
                                                            Name = "EG"
                                        },
                                        new MapHashable()
                                        {
                                            Id = new Guid(7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0),
                                                            Hash = default(Guid).ToString(),
                                                            Name = "01"
                                        },
                                        new MapHashable()
                                        {
                                            Id = new Guid(8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0),
                                                            Hash = default(Guid).ToString(),
                                                            Name = "02"
                                        },
                                    }
                },
                new MapBuilding()
                {
                    Id = new Guid("55972700-2f0e-48fb-8395-c7cf9ce5a78a"),
                                    Hash = Guid.NewGuid().ToString(),
                                    Coordinates = "640,50,1150,300",
                                    Name = "Hörsaalgebäude 3",
                                    Floors = new List<MapHashable>()
                                    {
                                        new MapHashable()
                                        {
                                            Id = new Guid(9, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0),
                                                            Hash = default(Guid).ToString(),
                                                            Name = "EG"
                                        },
                                        new MapHashable()
                                        {
                                            Id = new Guid(10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0),
                                                            Hash = default(Guid).ToString(),
                                                            Name = "01"
                                        }
                                    }
                }
            }
        };
    }
}
#endif
