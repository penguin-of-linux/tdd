using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization {
    public static class Geometry {
        public const double offsetAlongRay = 8;
        public static double SpiralEquation(double k, double fi) {
            return k * fi;
        }

        public static Point FromPolarToDecart(double p, double fi) {
            var x = p * Math.Cos(fi);
            var y = p * Math.Sin(fi);
            return new Point((int)Math.Round(x), (int)Math.Round(y));
        }

        public static IEnumerable<Point> GetNewSpiralPoint(bool isBreaking = false) {
            var step = Math.PI / 180;
            var rotationAngle = 0.0;

            while (!isBreaking) {
                rotationAngle += step;
                var radiusVector = SpiralEquation(offsetAlongRay, rotationAngle);
                yield return FromPolarToDecart(radiusVector, rotationAngle);
            }
            yield break;
        }
    }
}
