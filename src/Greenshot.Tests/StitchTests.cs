// Greenshot - a free and open source screenshot tool
// Copyright (C) 2007-2020 Thomas Braun, Jens Klingen, Robin Krom
// 
// For more information see: http://getgreenshot.org/
// The Greenshot project is hosted on GitHub https://github.com/greenshot/greenshot
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 1 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System.Drawing.Imaging;
using System.IO;
using Greenshot.Gfx;
using Greenshot.Gfx.Formats;
using Greenshot.Gfx.Stitching;
using Xunit;

namespace Greenshot.Tests
{
    public class StitchTests
    {
        public StitchTests()
        {
            BitmapHelper.RegisterFormatReader<GenericGdiFormatReader>();
        }

        [Fact]
        public void BitmapStitcher_Default()
        {
            using var bitmapStitcher = new BitmapStitcher();
            bitmapStitcher
                .AddBitmap(BitmapHelper.LoadBitmap(@"TestFiles\scroll0.png"))
                .AddBitmap(BitmapHelper.LoadBitmap(@"TestFiles\scroll35.png"))
                .AddBitmap(BitmapHelper.LoadBitmap(@"TestFiles\scroll70.png"))
                .AddBitmap(BitmapHelper.LoadBitmap(@"TestFiles\scroll105.png"))
                .AddBitmap(BitmapHelper.LoadBitmap(@"TestFiles\scroll124.png"));

            using var completedBitmap = bitmapStitcher.Result();
            using var expectedBitmap = BitmapHelper.LoadBitmap(@"TestFiles\scroll-result.png");

            var expectedBmp = expectedBitmap.NativeBitmap;
            var actualBmp = completedBitmap.NativeBitmap;

            Assert.Equal(expectedBmp.Width, actualBmp.Width);
            Assert.Equal(expectedBmp.Height, actualBmp.Height);

            for (int y = 0; y < expectedBmp.Height; y++)
            {
                for (int x = 0; x < expectedBmp.Width; x++)
                {
                    Assert.Equal(expectedBmp.GetPixel(x, y), actualBmp.GetPixel(x, y));
                }
            }
        }
    }
}
