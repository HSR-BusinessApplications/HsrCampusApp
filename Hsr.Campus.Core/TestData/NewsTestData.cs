// Copyright (c) Hochschule für Technik Rapperswil. All Rights Reserved.
// Licensed under the Apache 2.0 license. See LICENSE.txt in the project root for license information.

// The classes with test data must only be compiled in the Test-Build!
#pragma warning disable SA1124 // Do not use regions
#if TEST_DATA
namespace Hsr.Campus.Core.TestData
{
    using System;

    public static class NewsTestData
    {
#region Icons

        public static byte[] IconHsr25 => new byte[] { 239, 191, 189, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82, 0, 0, 0, 25, 0, 0, 0, 25, 8, 6, 0, 0, 0, 239, 191, 189, 239, 191, 189, 99, 0, 0, 0, 4, 115, 66, 73, 84, 8, 8, 8, 8, 124, 8, 100, 239, 191, 189, 0, 0, 0, 9, 112, 72, 89, 115, 0, 0, 13, 239, 191, 189, 0, 0, 13, 239, 191, 189, 1, 66, 40, 239, 191, 189, 120, 0, 0, 0, 25, 116, 69, 88, 116, 83, 111, 102, 116, 119, 97, 114, 101, 0, 119, 119, 119, 46, 105, 110, 107, 115, 99, 97, 112, 101, 46, 111, 114, 103, 239, 191, 189, 239, 191, 189, 60, 26, 0, 0, 1, 39, 73, 68, 65, 84, 72, 239, 191, 189, 239, 191, 189, 239, 191, 189, 49, 40, 239, 191, 189, 97, 20, 6, 239, 191, 189, 239, 191, 189, 74, 239, 191, 189, 239, 191, 189, 114, 39, 38, 41, 239, 191, 189, 88, 110, 36, 239, 191, 189, 239, 191, 189, 239, 191, 189, 40, 73, 239, 191, 189, 45, 202, 181, 239, 191, 189, 216, 141, 6, 22, 239, 191, 189, 239, 191, 189, 40, 239, 191, 189, 239, 191, 189, 100, 50, 42, 6, 100, 32, 239, 191, 189, 76, 239, 191, 189, 239, 191, 189, 239, 191, 189, 27, 239, 191, 189, 239, 191, 189, 239, 191, 189, 239, 191, 189, 239, 191, 189, 42, 239, 191, 189, 65, 239, 191, 189, 111, 239, 191, 189, 239, 191, 189, 239, 191, 189, 239, 191, 189, 239, 191, 189, 239, 191, 189, 13, 10, 123, 13, 10, 32, 32, 32, 32, 239, 191, 189, 115, 239, 191, 189, 239, 191, 189, 239, 191, 189, 239, 191, 189, 31, 239, 191, 189, 239, 191, 189, 5, 10, 239, 191, 189, 66, 239, 191, 189, 16, 41, 68, 239, 191, 189, 239, 191, 189, 239, 191, 189, 59, 106, 239, 191, 189, 75, 239, 191, 189, 77, 239, 191, 189, 239, 191, 189, 239, 191, 189, 15, 48, 239, 191, 189, 1, 239, 191, 189, 37, 239, 191, 189, 79, 239, 191, 189, 239, 191, 189, 239, 191, 189, 58, 239, 191, 189, 112, 239, 191, 189, 23, 239, 191, 189, 112, 239, 191, 189, 219, 164, 239, 191, 189, 26, 39, 73, 126, 239, 191, 189, 89, 84, 239, 191, 189, 239, 191, 189, 209, 132, 13, 10, 32, 32, 32, 32, 123, 13, 10, 32, 32, 32, 32, 32, 32, 32, 32, 239, 191, 189, 239, 191, 189, 21, 239, 191, 189, 96, 31, 239, 191, 189, 77, 239, 191, 189, 239, 191, 189, 239, 191, 189, 239, 191, 189, 47, 239, 191, 189, 20, 28, 239, 191, 189, 99, 239, 191, 189, 69, 239, 191, 189, 24, 22, 239, 191, 189, 221, 148, 239, 191, 189, 239, 191, 189, 36, 208, 135, 239, 191, 189, 24, 61, 113, 239, 191, 189, 44, 13, 10, 32, 32, 32, 32, 32, 32, 32, 32, 239, 191, 189, 35, 239, 191, 189, 81, 22, 86, 117, 239, 191, 189, 239, 191, 189, 34, 83, 239, 191, 189, 18, 214, 157, 33, 239, 191, 189, 31, 126, 64, 239, 191, 189, 239, 191, 189, 76, 20, 76, 209, 137, 51, 239, 191, 189, 8, 239, 191, 189, 239, 191, 189, 70, 119, 121, 239, 191, 189, 69, 239, 191, 189, 239, 191, 189, 86, 78, 214, 147, 239, 191, 189, 115, 78, 239, 191, 189, 56, 239, 191, 189, 239, 191, 189, 239, 191, 189, 57, 39, 91, 239, 191, 189, 3, 239, 191, 189, 86, 78, 126, 239, 191, 189, 61, 22, 239, 191, 189, 239, 191, 189, 85, 239, 191, 189, 239, 191, 189, 111, 239, 191, 189, 58, 239, 191, 189, 239, 191, 189, 239, 191, 189, 239, 191, 189, 239, 191, 189, 195, 151, 239, 191, 189, 21, 239, 191, 189, 39, 239, 191, 189, 67, 120, 73, 239, 191, 189, 82, 239, 191, 189, 15, 239, 191, 189, 239, 191, 189, 31, 97, 48, 114, 239, 191, 189, 239, 191, 189, 239, 191, 189, 27, 239, 191, 189, 6, 94, 113, 239, 191, 189, 239, 191, 189, 239, 191, 189, 111, 239, 191, 189, 23, 239, 191, 189, 0, 239, 191, 189, 97, 71, 239, 191, 189, 239, 191, 189, 85, 75, 239, 191, 189, 0, 0, 0, 0, 73, 69, 78, 68, 239, 191, 189, 66, 96, 239, 191, 189, 13, 10 };

        public static byte[] IconHsr50 => new byte[] { 239, 191, 189, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82, 0, 0, 0, 50, 0, 0, 0, 50, 8, 6, 0, 0, 0, 30, 63, 239, 191, 189, 239, 191, 189, 0, 0, 0, 4, 115, 66, 73, 84, 8, 8, 8, 8, 124, 8, 100, 239, 191, 189, 0, 0, 0, 9, 112, 72, 89, 115, 0, 0, 27, 239, 191, 189, 0, 0, 27, 239, 191, 189, 1, 94, 26, 239, 191, 189, 28, 0, 0, 0, 25, 116, 69, 88, 116, 83, 111, 102, 116, 119, 97, 114, 101, 0, 119, 119, 119, 46, 105, 110, 107, 115, 99, 97, 112, 101, 46, 111, 114, 103, 239, 191, 189, 239, 191, 189, 60, 26, 0, 0, 2, 106, 73, 68, 65, 84, 104, 239, 191, 189, 239, 191, 189, 239, 191, 189, 57, 104, 21, 65, 24, 7, 239, 191, 189, 239, 191, 189, 51, 7, 239, 191, 189, 239, 191, 189, 36, 68, 239, 191, 189, 49, 10, 30, 40, 68, 82, 239, 191, 189, 66, 60, 239, 191, 189, 239, 191, 189, 239, 191, 189, 239, 191, 189, 239, 191, 189, 82, 68, 84, 210, 137, 239, 191, 189, 71, 33, 90, 104, 239, 191, 189, 239, 191, 189, 239, 191, 189, 239, 191, 189, 239, 191, 189, 22, 239, 191, 189, 239, 191, 189, 88, 239, 191, 189, 75, 239, 191, 189, 81, 40, 239, 191, 189, 239, 191, 189, 66, 239, 191, 189, 68, 239, 191, 189, 64, 69, 239, 191, 189, 239, 191, 189, 239, 191, 189, 239, 191, 189, 201, 186, 239, 191, 189, 239, 191, 189, 71, 239, 191, 189, 53, 239, 191, 189, 31, 6, 239, 191, 189, 239, 191, 189, 239, 191, 189, 239, 191, 189, 239, 191, 189, 239, 191, 189, 239, 191, 189, 124, 239, 191, 189, 108, 5, 85, 51, 0, 239, 191, 189, 239, 191, 189, 13, 10, 123, 13, 10, 32, 32, 32, 32, 1, 239, 191, 189, 239, 191, 189, 239, 191, 189, 72, 239, 191, 189, 80, 18, 41, 26, 74, 34, 69, 67, 73, 239, 191, 189, 104, 40, 239, 191, 189, 20, 13, 37, 239, 191, 189, 239, 191, 189, 239, 191, 189, 36, 82, 52, 239, 191, 189, 68, 239, 191, 189, 239, 191, 189, 239, 191, 189, 72, 239, 191, 189, 80, 18, 41, 26, 74, 34, 69, 67, 73, 239, 191, 189, 104, 40, 239, 191, 189, 20, 13, 239, 191, 189, 239, 191, 189, 239, 191, 189, 47, 239, 191, 189, 77, 239, 191, 189, 57, 56, 239, 191, 189, 17, 239, 191, 189, 17, 23, 19, 239, 191, 189, 19, 239, 191, 189, 239, 191, 189, 24, 239, 191, 189, 5, 239, 191, 189, 239, 191, 189, 33, 94, 98, 1, 122, 239, 191, 189, 17, 239, 191, 189, 51, 239, 191, 189, 239, 191, 189, 239, 191, 189, 82, 239, 191, 189, 126, 54, 239, 191, 189, 99, 19, 239, 191, 189, 239, 191, 189, 38, 239, 191, 189, 70, 239, 191, 189, 107, 239, 191, 189, 239, 191, 189, 72, 239, 191, 189, 106, 239, 191, 189, 73, 239, 191, 189, 211, 151, 239, 191, 189, 83, 239, 191, 189, 21, 239, 191, 189, 205, 153, 115, 113, 78, 239, 191, 189, 239, 191, 189, 69, 38, 104, 239, 191, 189, 65, 124, 79, 199, 166, 119, 100, 239, 191, 189, 239, 191, 189, 8, 239, 191, 189, 239, 191, 189, 35, 239, 191, 189, 239, 191, 189, 22, 124, 17, 118, 239, 191, 189, 239, 191, 189, 239, 191, 189, 49, 239, 191, 189, 239, 191, 189, 35, 44, 13, 10, 32, 32, 32, 32, 239, 191, 189, 112, 90, 126, 11, 59, 214, 129, 51, 239, 191, 189, 102, 239, 191, 189, 200, 177, 239, 191, 189, 103, 107, 66, 40, 239, 191, 189, 239, 191, 189, 205, 137, 239, 191, 189, 119, 239, 191, 189, 63, 239, 191, 189, 127, 194, 158, 239, 191, 189, 214, 155, 239, 191, 189, 109, 239, 191, 189, 101, 239, 191, 189, 239, 191, 189, 239, 191, 189, 68, 13, 10, 32, 32, 32, 32, 91, 13, 10, 32, 32, 32, 32, 32, 32, 32, 32, 239, 191, 189, 239, 191, 189, 239, 191, 189, 239, 191, 189, 120, 11, 112, 42, 239, 191, 189, 78, 59, 52, 239, 191, 189, 200, 188, 72, 239, 191, 189, 41, 124, 209, 137, 239, 191, 189, 121, 36, 239, 191, 189, 78, 27, 199, 186, 35, 95, 113, 36, 67, 239, 191, 189, 239, 191, 189, 239, 191, 189, 73, 119, 239, 191, 189, 114, 34, 15, 99, 13, 14, 96, 16, 239, 191, 189, 234, 136, 175, 239, 191, 189, 127, 71, 239, 191, 189, 27, 239, 191, 189, 112, 50, 239, 191, 189, 87, 112, 60, 43, 32, 239, 191, 189, 212, 131, 239, 191, 189, 239, 191, 189, 239, 191, 189, 239, 191, 189, 12, 239, 191, 189, 54, 33, 9, 239, 191, 189, 203, 137, 25, 43, 107, 85, 239, 191, 189, 239, 191, 189, 125, 61, 39, 54, 239, 191, 189, 72, 43, 6, 50, 90, 95, 239, 191, 189, 68, 239, 191, 189, 239, 191, 189, 239, 191, 189, 13, 25, 239, 191, 189, 90, 239, 191, 189, 15, 13, 18, 239, 191, 189, 16, 239, 191, 189, 239, 191, 189, 40, 239, 191, 189, 239, 191, 189, 214, 145, 239, 191, 189, 13, 11, 239, 191, 189, 62, 239, 191, 189, 239, 191, 189, 113, 239, 191, 189, 239, 191, 189, 239, 191, 189, 8, 239, 191, 189, 239, 191, 189, 54, 210, 159, 239, 191, 189, 78, 34, 239, 191, 189, 118, 52, 239, 191, 189, 13, 10, 32, 32, 32, 32, 93, 63, 14, 239, 191, 189, 56, 239, 191, 189, 14, 69, 239, 191, 189, 46, 25, 5, 113, 239, 191, 189, 239, 191, 189, 90, 239, 191, 189, 35, 239, 191, 189, 79, 3, 113, 219, 133, 239, 191, 189, 10, 111, 239, 191, 189, 13, 10, 32, 32, 32, 32, 123, 13, 10, 32, 32, 32, 32, 32, 32, 32, 32, 50, 10, 239, 191, 189, 34, 50, 239, 191, 189, 11, 239, 191, 189, 28, 239, 191, 189, 94, 239, 191, 189, 116, 239, 191, 189, 239, 191, 189, 111, 96, 239, 191, 189, 239, 191, 189, 239, 191, 189, 36, 106, 56, 239, 191, 189, 239, 191, 189, 212, 140, 239, 191, 189, 117, 35, 239, 191, 189, 239, 191, 189, 194, 138, 239, 191, 189, 47, 46, 239, 191, 189, 109, 239, 191, 189, 239, 191, 189, 17, 239, 191, 189, 119, 239, 191, 189, 239, 191, 189, 66, 29, 239, 191, 189, 239, 191, 189, 54, 239, 191, 189, 77, 239, 191, 189, 209, 170, 21, 239, 191, 189, 63, 120, 239, 191, 189, 7, 239, 191, 189, 239, 191, 189, 239, 191, 189, 122, 239, 191, 189, 34, 57, 239, 191, 189, 239, 191, 189, 75, 239, 191, 189, 35, 239, 191, 189, 218, 149, 116, 65, 108, 199, 181, 72, 239, 191, 189, 194, 146, 239, 191, 189, 103, 97, 239, 191, 189, 207, 160, 239, 191, 189, 42, 239, 191, 189, 10, 207, 137, 111, 239, 191, 189, 22, 239, 191, 189, 239, 191, 189, 54, 239, 191, 189, 62, 89, 106, 239, 191, 189, 239, 191, 189, 94, 239, 191, 189, 97, 63, 239, 191, 189, 239, 191, 189, 239, 191, 189, 60, 63, 54, 84, 36, 239, 191, 189, 239, 191, 189, 127, 199, 140, 239, 191, 189, 67, 44, 13, 10, 32, 32, 32, 32, 32, 32, 32, 32, 239, 191, 189, 20, 13, 37, 239, 191, 189, 239, 191, 189, 239, 191, 189, 47, 101, 239, 191, 189, 10, 121, 239, 191, 189, 5, 239, 191, 189, 239, 191, 189, 0, 0, 0, 0, 73, 69, 78, 68, 239, 191, 189, 66, 96, 239, 191, 189, 13, 10 };

        public static byte[] IconHsr75 => new byte[] { 239, 191, 189, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82, 0, 0, 0, 75, 0, 0, 0, 75, 8, 6, 0, 0, 0, 56, 78, 122, 239, 191, 189, 0, 0, 0, 4, 115, 66, 73, 84, 8, 8, 8, 8, 124, 8, 100, 239, 191, 189, 0, 0, 0, 9, 112, 72, 89, 115, 0, 0, 41, 239, 191, 189, 0, 0, 41, 239, 191, 189, 1, 34, 239, 191, 189, 223, 140, 0, 0, 0, 25, 116, 69, 88, 116, 83, 111, 102, 116, 119, 97, 114, 101, 0, 119, 119, 119, 46, 105, 110, 107, 115, 99, 97, 112, 101, 46, 111, 114, 103, 239, 191, 189, 239, 191, 189, 60, 26, 0, 0, 3, 239, 191, 189, 73, 68, 65, 84, 120, 239, 191, 189, 239, 191, 189, 239, 191, 189, 73, 239, 191, 189, 92, 69, 24, 239, 191, 189, 239, 191, 189, 239, 191, 189, 239, 191, 189, 73, 36, 97, 239, 191, 189, 18, 13, 25, 84, 84, 239, 191, 189, 104, 84, 34, 40, 239, 191, 189, 74, 114, 48, 110, 7, 199, 139, 55, 81, 114, 48, 239, 191, 189, 120, 82, 80, 92, 14, 122, 239, 191, 189, 32, 239, 191, 189, 239, 191, 189, 122, 113, 65, 114, 239, 191, 189, 120, 20, 239, 191, 189, 239, 191, 189, 239, 191, 189, 24, 21, 81, 52, 81, 8, 106, 239, 191, 189, 104, 70, 20, 51, 34, 6, 239, 191, 189, 49, 36, 239, 191, 189, 30, 239, 191, 189, 27, 239, 191, 189, 239, 191, 189, 111, 239, 191, 189, 239, 191, 189, 239, 191, 189, 116, 15, 239, 191, 189, 31, 239, 191, 189, 239, 191, 189, 213, 171, 239, 191, 189, 239, 191, 189, 239, 191, 189, 127, 239, 191, 189, 239, 191, 189, 239, 191, 189, 122, 48, 19, 239, 191, 189, 200, 180, 98, 217, 168, 7, 239, 191, 189, 239, 191, 189, 200, 178, 2, 100, 89, 1, 239, 191, 189, 239, 191, 189, 0, 89, 86, 239, 191, 189, 44, 43, 64, 239, 191, 189, 21, 32, 239, 191, 189, 10, 239, 191, 189, 101, 5, 200, 178, 2, 100, 89, 1, 239, 191, 189, 239, 191, 189, 0, 89, 86, 239, 191, 189, 44, 43, 64, 239, 191, 189, 21, 32, 239, 191, 189, 10, 239, 191, 189, 101, 5, 200, 178, 2, 100, 89, 1, 239, 191, 189, 239, 191, 189, 0, 89, 86, 239, 191, 189, 44, 43, 64, 239, 191, 189, 21, 32, 239, 191, 189, 10, 239, 191, 189, 101, 5, 200, 178, 2, 100, 89, 1, 239, 191, 189, 239, 191, 189, 0, 89, 86, 239, 191, 189, 44, 43, 64, 239, 191, 189, 21, 32, 239, 191, 189, 10, 239, 191, 189, 101, 5, 200, 178, 2, 100, 89, 1, 239, 191, 189, 239, 191, 189, 0, 89, 86, 239, 191, 189, 44, 43, 64, 239, 191, 189, 21, 32, 239, 191, 189, 10, 239, 191, 189, 101, 5, 239, 191, 189, 239, 191, 189, 40, 63, 239, 191, 189, 47, 10, 101, 103, 239, 191, 189, 212, 134, 120, 123, 239, 191, 189, 71, 239, 191, 189, 239, 191, 189, 106, 239, 191, 189, 23, 28, 239, 191, 189, 239, 191, 189, 120, 15, 239, 191, 189, 239, 191, 189, 55, 76, 97, 13, 239, 191, 189, 239, 191, 189, 10, 92, 239, 191, 189, 239, 191, 189, 239, 191, 189, 239, 191, 189, 39, 239, 191, 189, 239, 191, 189, 121, 126, 92, 55, 239, 191, 189, 52, 86, 68, 6, 239, 191, 189, 41, 239, 191, 189, 14, 239, 191, 189, 239, 191, 189, 123, 239, 191, 189, 239, 191, 189, 110, 239, 191, 189, 53, 83, 104, 239, 191, 189, 239, 191, 189, 69, 239, 191, 189, 222, 181, 11, 239, 191, 189, 96, 239, 191, 189, 97, 239, 191, 189, 239, 191, 189, 45, 98, 239, 191, 189, 104, 239, 191, 189, 222, 186, 125, 109, 239, 191, 189, 239, 191, 189, 239, 191, 189, 109, 239, 191, 189, 59, 46, 239, 191, 189, 239, 191, 189, 117, 92, 239, 191, 189, 119, 53, 239, 191, 189, 47, 209, 170, 33, 239, 191, 189, 239, 191, 189, 239, 191, 189, 239, 191, 189, 120, 12, 239, 191, 189, 239, 191, 189, 213, 186, 239, 191, 189, 239, 191, 189, 32, 107, 31, 110, 239, 191, 189, 63, 37, 239, 191, 189, 86, 27, 239, 191, 189, 239, 191, 189, 103, 28, 239, 191, 189, 113, 239, 191, 189, 119, 199, 177, 239, 191, 189, 239, 191, 189, 66, 85, 239, 191, 189, 90, 76, 239, 191, 189, 50, 56, 239, 191, 189, 239, 191, 189, 227, 184, 180, 123, 63, 47, 239, 191, 189, 239, 191, 189, 215, 176, 112, 239, 191, 189, 239, 191, 189, 60, 45, 239, 191, 189, 121, 239, 191, 189, 42, 77, 213, 151, 239, 191, 189, 119, 95, 239, 191, 189, 67, 120, 2, 47, 85, 5, 25, 117, 239, 191, 189, 90, 95, 104, 115, 26, 239, 191, 189, 106, 239, 191, 189, 46, 239, 191, 189, 239, 191, 189, 118, 239, 191, 189, 239, 191, 189, 239, 191, 189, 105, 48, 239, 191, 189, 239, 191, 189, 83, 21, 119, 239, 191, 189, 211, 176, 239, 191, 189, 31, 11, 101, 91, 12, 55, 47, 239, 191, 189, 113, 239, 191, 189, 239, 191, 189, 239, 191, 189, 239, 191, 189, 79, 69, 239, 191, 189, 28, 239, 191, 189, 239, 191, 189, 239, 191, 189, 56, 88, 40, 239, 191, 189, 125, 239, 191, 189, 239, 191, 189, 48, 85, 239, 191, 189, 95, 239, 191, 189, 98, 69, 30, 239, 191, 189, 239, 191, 189, 73, 105, 239, 191, 189, 239, 191, 189, 239, 191, 189, 52, 239, 191, 189, 23, 239, 191, 189, 121, 124, 80, 40, 239, 191, 189, 84, 85, 57, 239, 191, 189, 239, 191, 189, 119, 227, 157, 134, 58, 115, 239, 191, 189, 120, 61, 110, 239, 191, 189, 239, 191, 189, 125, 239, 191, 189, 11, 239, 191, 189, 93, 18, 118, 15, 110, 239, 191, 189, 239, 191, 189, 35, 239, 191, 189, 239, 191, 189, 239, 191, 189, 46, 220, 135, 239, 191, 189, 239, 191, 189, 239, 191, 189, 38, 239, 191, 189, 64, 93, 239, 191, 189, 239, 191, 189, 9, 239, 191, 189, 72, 104, 239, 191, 189, 239, 191, 189, 239, 191, 189, 226, 164, 154, 24, 239, 191, 189, 239, 191, 189, 32, 126, 105, 17, 239, 191, 189, 46, 239, 191, 189, 95, 239, 191, 189, 239, 191, 189, 112, 9, 214, 150, 60, 95, 38, 239, 191, 189, 239, 191, 189, 117, 113, 71, 46, 239, 191, 189, 239, 191, 189, 239, 191, 189, 52, 239, 191, 189, 86, 239, 191, 189, 17, 41, 239, 191, 189, 13, 107, 7, 79, 239, 191, 189, 239, 191, 189, 221, 140, 239, 191, 189, 91, 239, 191, 189, 29, 11, 89, 29, 105, 239, 191, 189, 115, 239, 191, 189, 239, 191, 189, 218, 140, 239, 191, 189, 40, 12, 75, 239, 191, 189, 74, 105, 239, 191, 189, 25, 99, 107, 89, 15, 239, 191, 189, 211, 134, 107, 115, 239, 191, 189, 77, 68, 86, 239, 191, 189, 239, 191, 189, 239, 191, 189, 117, 122, 239, 191, 189, 11, 62, 239, 191, 189, 16, 239, 191, 189, 106, 26, 94, 239, 191, 189, 239, 191, 189, 239, 191, 189, 239, 191, 189, 118, 239, 191, 189, 239, 191, 189, 24, 91, 107, 89, 239, 191, 189, 239, 191, 189, 32, 93, 239, 191, 189, 14, 98, 59, 239, 191, 189, 44, 25, 239, 191, 189, 9, 239, 191, 189, 239, 191, 189, 239, 191, 189, 109, 211, 166, 239, 191, 189, 67, 239, 191, 189, 91, 239, 191, 189, 239, 191, 189, 239, 191, 189, 29, 124, 239, 191, 189, 70, 239, 191, 189, 117, 239, 191, 189, 99, 57, 110, 239, 191, 189, 239, 191, 189, 239, 191, 189, 80, 120, 118, 239, 191, 189, 239, 191, 189, 39, 239, 191, 189, 8, 87, 239, 191, 189, 239, 191, 189, 66, 239, 191, 189, 94, 239, 191, 189, 64, 95, 239, 191, 189, 56, 239, 191, 189, 239, 191, 189, 6, 119, 239, 191, 189, 239, 191, 189, 239, 191, 189, 63, 202, 184, 119, 26, 239, 191, 189, 58, 61, 91, 239, 191, 189, 96, 41, 239, 191, 189, 34, 239, 191, 189, 239, 191, 189, 69, 78, 62, 202, 152, 211, 184, 239, 191, 189, 80, 239, 191, 189, 22, 239, 191, 189, 239, 191, 189, 106, 48, 14, 239, 191, 189, 239, 191, 189, 224, 171, 154, 239, 191, 189, 239, 191, 189, 239, 191, 189, 76, 239, 191, 189, 108, 18, 239, 191, 189, 15, 239, 191, 189, 239, 191, 189, 109, 239, 191, 189, 239, 191, 189, 5, 239, 191, 189, 80, 85, 121, 28, 100, 239, 191, 189, 47, 29, 104, 55, 239, 191, 189, 81, 239, 191, 189, 239, 191, 189, 25, 222, 150, 86, 197, 141, 239, 191, 189, 239, 191, 189, 239, 191, 189, 239, 191, 189, 122, 239, 191, 189, 50, 239, 191, 189, 239, 191, 189, 103, 239, 191, 189, 95, 88, 63, 219, 165, 99, 239, 191, 189, 0, 239, 191, 189, 239, 191, 189, 61, 239, 191, 189, 39, 239, 191, 189, 239, 191, 189, 96, 109, 239, 191, 189, 71, 239, 191, 189, 239, 191, 189, 239, 191, 189, 239, 191, 189, 239, 191, 189, 239, 191, 189, 86, 239, 191, 189, 239, 191, 189, 89, 239, 191, 189, 57, 239, 191, 189, 239, 191, 189, 239, 191, 189, 98, 239, 191, 189, 113, 239, 191, 189, 101, 239, 191, 189, 6, 239, 191, 189, 197, 155, 239, 191, 189, 51, 5, 13, 10, 123, 13, 10, 32, 32, 32, 32, 20, 239, 191, 189, 34, 60, 87, 86, 239, 191, 189, 239, 191, 189, 53, 97, 48, 239, 191, 189, 30, 223, 162, 239, 191, 189, 66, 239, 191, 189, 239, 191, 189, 94, 239, 191, 189, 239, 191, 189, 77, 239, 191, 189, 52, 239, 191, 189, 68, 239, 191, 189, 239, 191, 189, 87, 239, 191, 189, 122, 220, 138, 239, 191, 189, 213, 159, 35, 123, 76, 26, 28, 127, 213, 187, 110, 239, 191, 189, 117, 210, 166, 239, 191, 189, 239, 191, 189, 239, 191, 189, 239, 191, 189, 117, 65, 127, 239, 191, 189, 9, 221, 157, 239, 191, 189, 24, 112, 8, 63, 72, 239, 191, 189, 96, 78, 74, 239, 191, 189, 39, 74, 3, 46, 59, 239, 191, 189, 46, 58, 239, 191, 189, 36, 107, 239, 191, 189, 25, 239, 191, 189, 239, 191, 189, 239, 191, 189, 100, 200, 178, 2, 100, 89, 1, 239, 191, 189, 239, 191, 189, 0, 89, 86, 239, 191, 189, 44, 43, 64, 239, 191, 189, 21, 32, 239, 191, 189, 10, 239, 191, 189, 31, 82, 102, 239, 191, 189, 63, 239, 191, 189, 239, 191, 189, 69, 239, 191, 189, 0, 0, 0, 0, 73, 69, 78, 68, 239, 191, 189, 66, 96, 239, 191, 189, 13, 10 };

#endregion

#region Feeds

        /// <summary>
        /// Gets a list of all available feeds
        /// </summary>
        public static string Feeds => @"[
    {
        ""Key"":""hsrTest"",
        ""Name"":""HSR"",
        ""HsrSport"":""false""
    },
    {
        ""Key"":""vshsrTest"",
        ""Name"":""VSHSR"",
        ""HsrSport"":""false""
    },
    {
        ""Key"":""hsrsportTest"",
        ""Name"":""HSR Sport"",
        ""HsrSport"":""true""
    }]";
        #endregion

        #region Feed1344Part1

        /// <summary>
        /// Gets VSHSR Feed part 1 (4 posts) ordered by age descending (new to old)
        /// </summary>
        public static string Feed1344Part1 => Stamp(@"[
  {
    ""NewsId"": ""134468179973906_302726776764354"",
    ""Message"": ""TEST 1344 Part 1!! <<STAMP>> VSHSR hat eine Veranstaltung hinzugefügt.\nSollte Datum 2016-09-12T11:22:34+02:00 haben"",
    ""Title"": ""Partner- und Untervereinsvorstellung"",
    ""Url"": ""https://www.facebook.com/events/302726776764354/"",
    ""Date"": ""2016-09-12T11:22:34+02:00"",
    ""PictureBitmap"": null
  },
  {
    ""NewsId"": ""134468179973906_326268244388350"",
    ""Message"": ""Post2 <<STAMP>> VSHSR hat eine Veranstaltung hinzugefügt."",
    ""Title"": ""Generalversammlung"",
    ""Url"": ""https://www.facebook.com/events/326268244388350/"",
    ""Date"": ""2016-09-12T11:18:02+02:00"",
    ""PictureBitmap"": null
  },
  {
    ""NewsId"": ""134468179973906_122704591519981"",
    ""Message"": ""Post3 <<STAMP>> VSHSR hat eine Veranstaltung hinzugefügt."",
    ""Title"": ""Welcome Bar"",
    ""Url"": ""https://www.facebook.com/events/122704591519981/"",
    ""Date"": ""2016-09-12T11:15:17+02:00"",
    ""PictureBitmap"": null
  },
  {
    ""NewsId"": ""134468179973906_132704591519981"",
    ""Message"": ""Post4 <<STAMP>> VSHSR hat eine Veranstaltung hinzugefügt."",
    ""Title"": ""Welcome Bar"",
    ""Url"": ""https://www.facebook.com/events/122704591519981/"",
    ""Date"": ""2016-09-12T11:15:16+02:00"",
    ""PictureBitmap"": null
  },
  {
    ""NewsId"": ""134468179973906_142704591519981"",
    ""Message"": ""Post5 <<STAMP>> VSHSR hat eine Veranstaltung hinzugefügt."",
    ""Title"": ""Welcome Bar"",
    ""Url"": ""https://www.facebook.com/events/122704591519981/"",
    ""Date"": ""2016-09-12T11:15:15+02:00"",
    ""PictureBitmap"": null
  },
  {
    ""NewsId"": ""134468179973906_1104376426316405"",
    ""Message"": ""TEST 1344 Part 1 Doppel  <<STAMP>> -- VSHSR hat sein/ihr Titelbild aktualisiert."",
    ""Title"": ""TEST 1344 Part 1 Doppel-- VSHSRs Titelbild"",
    ""Url"": ""https://www.facebook.com/vshsr/photos/a.251884944898895.61957.134468179973906/1104376332983081/?type=3"",
    ""Date"": ""2016-09-12T11:10:50+02:00"",
    ""PictureBitmap"": ""iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKAQMAAAC3/F3+AAAAA1BMVEUA/wA0XsCoAAAACXBIWXMAAAsTAAALEwEAmpwYAAAAC0lEQVQI12NgwAcAAB4AAW6FRzIAAAAASUVORK5CYIINCg==""
  }]");
        #endregion

        #region Feed1344Part2

        /// <summary>
        /// Gets VSHSR Feed part 2 (5 posts) ordered by age descending (new to old)
        /// The first post has the same id as Post 4 from part one
        /// </summary>
        public static string Feed1344Part2 => Stamp(@"[{
    ""NewsId"": ""134468179973906_1104376426316405"",
    ""Message"": ""TEST 1344 Part 2 Doppel --  <<STAMP>> VSHSR hat sein/ihr Titelbild aktualisiert."",
    ""Title"": ""TEST 1344 Part 2 Doppel -- VSHSRs Titelbild"",
    ""Url"": ""https://www.facebook.com/vshsr/photos/a.251884944898895.61957.134468179973906/1104376332983081/?type=3"",
    ""Date"": ""2016-09-12T11:10:50+02:00"",
    ""PictureBitmap"": ""iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKAQMAAAC3/F3+AAAAA1BMVEUA/wA0XsCoAAAACXBIWXMAAAsTAAALEwEAmpwYAAAAC0lEQVQI12NgwAcAAB4AAW6FRzIAAAAASUVORK5CYIINCg==""
  },
  {
    ""NewsId"": ""134468179973906_1082020565218658"",
    ""Message"": ""Part2 Post2  <<STAMP>> Project Helin - Drones as a Service\n\nFür ihre Bachelorarbeit des Studiengangs Informatik haben Martin Stypinski, Kirusanth Poopalasingham und Marcel Amsler diese einzigartige Drone entwickelt. \n\nSeht sie euch an, hier im Video."",
    ""Title"": ""Open Source Autonomous Drone Delivery"",
    ""Url"": ""https://youtu.be/fH7SLorDS0A"",
    ""Date"": ""2016-08-17T09:11:39+02:00"",
    ""PictureBitmap"": null
  },
  {
    ""NewsId"": ""134468179973906_1028660960554619"",
    ""Message"": ""Part2 Post3  <<STAMP>> VSHSR hat Fachschaft Erneuerbare Energien und Umwelttechnik HSRs Veranstaltung geteilt."",
    ""Title"": ""HSR BeachParty"",
    ""Url"": ""https://www.facebook.com/events/1729113473970115/"",
    ""Date"": ""2016-05-26T20:56:04+02:00"",
    ""PictureBitmap"": ""iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKAQMAAAC3/F3+AAAAA1BMVEUA/wA0XsCoAAAACXBIWXMAAAsTAAALEwEAmpwYAAAAC0lEQVQI12NgwAcAAB4AAW6FRzIAAAAASUVORK5CYIINCg==""
  },
  {
    ""NewsId"": ""134468179973906_1006326779436034"",
    ""Message"": ""Part2 Post4  <<STAMP>> Votet bis zum 12. Juni für eine Erweiterung der Sportinfrastruktur an der HSR: https://www.sgkb.ch/150jahre/freizeitzone-rapperswil\n\n2018 feiert die St.Galler Kantonalbank ihr 150-Jahre Jubliäum. Unter dem Motto «Gemeinsam weiter wachsen» will die SGKB Projekte umsetzen, welche die Lebensqualität in unserer Region erhöhen. Der HSR Sport hat das Projekt „Aufwertung einer Freizeit- und Bewegungszone in Rapperswil-Jona“ eingereicht.\n\nDie Freizeit- und Bewegungszone zwischen der HSR und dem See soll durch fünf neue Elemente noch interessanter und vielfältiger werden.\n\n• Kraftgerüst mit Übungsanleitungen\n• 1 Korb Basketball Spielfeld\n• Frischwasserdusche für die Seeschwimmer\n• Fixpunkte für Slackline\n• Start und Ziel Gelände für vermessene und markierte Laufrunden"",
    ""Title"": ""www.sgkb.ch"",
    ""Url"": ""https://www.sgkb.ch/150jahre/freizeitzone-rapperswil"",
    ""Date"": ""2016-05-17T14:43:25+02:00"",
    ""PictureBitmap"": null
  },
  {
    ""NewsId"": ""134468179973906_997438260306150"",
    ""Message"": ""Part2 Post 5  <<STAMP>> Hoi mitenand. Mir tönd im uftrag vode universität st.galle e umfrog für e instutition in rapperswil-jona durefüehre. Mir wäret sehr froh, wenn er eu churz 3min ziit nehmet und die umfrog usfüllet.\nBeste Dank\n\n"",
    ""Title"": ""Umfrage \""Kirche im Prisma\"""",
    ""Url"": ""http://goo.gl/forms/ZFK5qrhrpu"",
    ""Date"": ""2016-05-06T17:15:07+02:00"",
    ""PictureBitmap"": null
  }]");
        #endregion

        #region Feed3058Part1

        /// <summary>
        /// Gets HSR Sport Feed part 1 (4 posts) ordered by age ascending (old to new)
        /// HSR Sport Feed 1. Teil (4 Posts). Reihenfolge alt zu neu.
        /// </summary>
        public static string Feed3058Part1 => Stamp(@"[
  {
    ""NewsId"": ""305881572763606_789292237755868"",
    ""Message"": ""TEST 3058 Part 1 Post 4 (LAST) <<STAMP>> Statt em übliche Konditraining ade HSR,  Zumba usprobiert  :-)\nDanke isch super gsi!"",
    ""Title"": null,
    ""Url"": ""https://www.facebook.com/305881572763606/posts/789292237755868"",
    ""Date"": ""2014-05-26T16:20:10+02:00"",
    ""PictureBitmap"": null
  },
  {
    ""NewsId"": ""305881572763606_790185007666591"",
    ""Message"": ""TEST 3058 Part 1 Post 3  <<STAMP>> Liebe Unihockeyaner\nDas Training findet heute und über die Ferien nicht statt.\nDer nächste Termin ist in der Beratungswoche.\nIch wünsche euch schöne Ferien!"",
    ""Title"": null,
    ""Url"": ""https://www.facebook.com/305881572763606/posts/790185007666591"",
    ""Date"": ""2014-05-28T09:06:33+02:00"",
    ""PictureBitmap"": null
  },
  {
    ""NewsId"": ""305881572763606_791755337509558"",
    ""Message"": ""TEST 3058 Part 1 Post 2  <<STAMP>> Die beiden HSR Teams sind für den morgigen Ironman 70.3 Rapperswil bereit! Natürlich freuen sie sich auf Fans  ;-)\nStartnr. 2659 & 2660\nStartzeit: 09:40"",
    ""Title"": null,
    ""Url"": ""https://www.facebook.com/photo.php?fbid=10202106259364651&set=gm.791755337509558&type=3"",
    ""Date"": ""2014-05-31T15:52:36+02:00"",
    ""PictureBitmap"": null
  },
  {
    ""NewsId"": ""305881572763606_792743730744052"",
    ""Message"": ""TEST 3058 Part 1 Post 1  <<STAMP>>  Resultate der HSR HIM Teams 2014 in der Kategorie Team Männer."",
    ""Title"": null,
    ""Url"": ""https://www.facebook.com/photo.php?fbid=10201043496369788&set=gm.792743730744052&type=3"",
    ""Date"": ""2014-06-02T14:05:12+02:00"",
    ""PictureBitmap"": null
  }
]");
        #endregion

        #region Feed3058Part2

        /// <summary>
        /// Gets HSR Sport feed part 2 (7 posts) ordered by age ascending (old to new)
        /// </summary>
        public static string Feed3058Part2 => Stamp(@"[
  {
    ""NewsId"": ""305881572763606_756808851004207"",
    ""Message"": ""TEST 3058 Part 2 Post 7 (last) <<STAMP>> -- BMC IRONMAN 70.3 Rapperswil-Jona sucht noch Volunteers!\nFalls du am 1. Juni nicht am IRONMAN mitmachst und dafür eine gute Ausrede suchst, dann melde dich doch als Volunteer. Wir suchen aktuell noch in allen Ressorts Helfer. Es sind wohl auch noch einige OK-Stellen offen.\n\nIch im Speziellen suche noch Streckenposten für der Laufstrecke. Deine Aufgabe wäre  in der Altstadt von Rapperswil und am See entlang dafür zu sorgen, dass sich Fussgänger und Athleten nicht in die Quere kommen. Für Lunch/Verpflegung ist gesorgt und eine kleine Entschädigung liegt auch drin.\nFalls es dich also interessieren würde, mal bei einem Grossanlass etwas hinter die Kulissen zu blicken, dann melde dich doch als Volunteer.\n\nAnmeldung:\nKein bestimmtes Ressort: https://volunteer.swissolympic.ch/cms/event_helper_person_show.aspx?HeraEventId=253\nRun Course: https://volunteer.swissolympic.ch/cms/event_helper_person_show.aspx?HeraEventId=253&Fixed_RessortId=5325&SetLang=de-CH\n\nBei Fragen: PM oder Mail an mich\nPS.: Für Zürich suche ich auch noch Helfer. Der IRONMAN Switzerland wird am 26./27. Juli durchgeführt und ist nochmals etwas grösser.\n\nVielleicht bis zum Event\ngreetz, Lukas"",
    ""Title"": ""TEST 3058 Part 2 Post 7-- Swiss Olympic Volunteer"",
    ""Url"": ""https://volunteer.swissolympic.ch/cms/event_helper_person_show.aspx?HeraEventId=253"",
    ""Date"": ""2014-03-24T21:56:19+01:00"",
    ""PictureBitmap"": null
  },
  {
    ""NewsId"": ""305881572763606_776586115693147"",
    ""Message"": ""TEST 3058 Part 2 Post 6  <<STAMP>> Ab Samstag 3. Mai beginnt wieder der offizielle Rennvelo-Treff zu folgenden Zeiten:\n\nTreffpunkt jeweils Montag und Mittwoch um 18 Uhr und am Samstag um 10 Uhr beim Restaurant Zimmermann Jona. Der Treffpunkt am Samstag gilt nicht bei offiziellen Touren. Diese künde ich frühzeitig an.\n\nDie Region um Rapperswil hat nicht nur einen schönen See zu bieten. Es gibt auch wunderschöne Velostrecken. Du fährst mit ortskundigen Clubmitglieder und lernst so die guten Strecken kennen. Auf einer gemütlichen Ausfahrt nach Feierabend, kannst du deinen Kopf lüften und den Studienalltag hinter dir lassen.\n\nIch freue mich auf zahlreiche Teilnehmer ^^."",
    ""Title"": null,
    ""Url"": ""https://www.facebook.com/photo.php?fbid=10152450098469026&set=gm.776586115693147&type=3"",
    ""Date"": ""2014-05-01T16:56:27+02:00"",
    ""PictureBitmap"": null
  },
  {
    ""NewsId"": ""305881572763606_779641838720908"",
    ""Message"": ""TEST 3058 Part 2 Post 5  <<STAMP>> Am Samstag 10. Mai findet die erste Rennvelotour dieser Saisson statt. Sie führt über den Schauenberg und die Hulftegg (96km). Wir treffen uns um 10:00 beim Restaurant Zimmermann in Jona. \n\nEs gibt drei Gruppen. Jeder kann mitkommen.\n\nWenn die Tour wegen schlechtem Wetter abgesagt wird, informiere ich spätestens eine Stunde vor Abfahrt über diesen Facebook Eintrag.\n\nHier sind die GPS Daten der Strecke.\nhttp://www.gpsies.com/mapOnly.do?fileId=pvomaghtimrzsigx&isFullScreenLeave=true\n\nIch freue mich über euer zahlreiches Erscheinen."",
    ""Title"": ""TEST 3058 Part 2 Post 5 Rennrad Strecke Jona | Schauenberg-Hulftegg | GPSies"",
    ""Url"": ""http://www.gpsies.com/mapOnly.do?fileId=pvomaghtimrzsigx&isFullScreenLeave=true"",
    ""Date"": ""2014-05-07T23:36:28+02:00"",
    ""PictureBitmap"": null
  },
  {
    ""NewsId"": ""305881572763606_779812278703864"",
    ""Message"": ""TEST 3058 Part 2 Post 4  <<STAMP>> Dringend!!!\nWir suchen einen Golie für unser Unihockeyteam an der FHO-Meisterschaft.\nBei Interesse bitte meldet euch bei mir."",
    ""Title"": null,
    ""Url"": ""https://www.facebook.com/305881572763606/posts/779812278703864"",
    ""Date"": ""2014-05-08T09:42:07+02:00"",
    ""PictureBitmap"": null
  },
  {
    ""NewsId"": ""305881572763606_783973051621120"",
    ""Message"": ""TEST 3058 Part 2 Post 3  <<STAMP>> Am Samstag 17. Mai findet die Veloclub Jona Rennvelotour \""Zugerseeschlaufe\"" statt (120km).\n\nTreffpunkt:   10.00 Uhr Restaurant Zimmermann Jona\n\nZwischenhalt: Restaurant Gartenlaube in Arth (48km)\n\nRoutenplan:   http://www.bikemap.net/de/route/2601568-zugerseeschlaufe/#gsc.tab=0\n\nIch freue mich auf zahlreiche Teilnehmer."",
    ""Title"": ""Route: Zugerseeschlaufe"",
    ""Url"": ""http://www.bikemap.net/de/route/2601568-zugerseeschlaufe/#gsc.tab=0"",
    ""Date"": ""2014-05-16T00:42:13+02:00"",
    ""PictureBitmap"": null
  },
  {
    ""NewsId"": ""305881572763606_785569038128188"",
    ""Message"": ""TEST 3058 Part 2 Post 2  <<STAMP>> Für den Segel Schnupperkurs diesen Donnerstag um 17.00 Uhr ist wieder ein Platz frei geworden. Wer Lust hat, soll sich bei mir melden."",
    ""Title"": null,
    ""Url"": ""https://www.facebook.com/305881572763606/posts/785569038128188"",
    ""Date"": ""2014-05-19T09:39:50+02:00"",
    ""PictureBitmap"": null
  },
  {
    ""NewsId"": ""305881572763606_786887611329664"",
    ""Message"": ""TEST 3058 Part 2 Post 1  <<STAMP>> Diesen Samstag, den 24. Mai, findet die naechste Rennvelotour statt. Zur Vorspeise fahren wir ueber die Sattelegg, zum Hauptgang nehmen wird die Ibergeregg und wer danach immer noch weiss, worum es geht kann zum Dessert noch den ueber den Raten fahren (darf auch weggelassen werden).\n\nAuf der Ibergergeregg gibt es Kaffee und Nussgipfel.\n\nTreffpunkt:   9.30 Uhr Restaurant Zimmermann Jona\nRoutenplan:   http://www.bikemap.net/de/route/2612048-sattelegg-ibergeregg-raten/#gsc.tab=0\n\nBei Absage wegen unsicherer Wetterlage  informiere bis spätestens 1 Stunde vor Abfahrt in diesem Eintrag.\n\nWaere toll wenn wieder moeglichst viele dabei sind."",
    ""Title"": null,
    ""Url"": ""https://www.facebook.com/photo.php?fbid=10152496618654026&set=gm.786887611329664&type=3"",
    ""Date"": ""2014-05-21T23:03:52+02:00"",
    ""PictureBitmap"": ""iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKAQMAAAC3/F3+AAAAA1BMVEUA/wA0XsCoAAAACXBIWXMAAAsTAAALEwEAmpwYAAAAC0lEQVQI12NgwAcAAB4AAW6FRzIAAAAASUVORK5CYIINCg==""
  }
]");
#endregion

        private static string Stamp(string input)
        {
            return input.Replace("<<STAMP>>", DateTime.Now.ToString("hh:mm:s.fff"));
        }
    }
}
#endif
#pragma warning restore SA1124 // Do not use regions
