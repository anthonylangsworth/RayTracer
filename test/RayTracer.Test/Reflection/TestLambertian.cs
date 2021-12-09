using NUnit.Framework;
using RayTracer.Primitives;
using RayTracer.Reflection;
using RayTracer.SampleGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Test.Reflection
{
    internal class TestLambertian
    {
        public void Ctor()
        {
            SampleGenerator<Point2D> testSampleGenerator = new UnitSquareMappedSampleGenerator(SampleAlgorithms.MultiJittered, new Random(), 16, 16);
            double testDiffuseCoefficient = 1;
            RGBColor testDiffuseColor = RGBColors.White;
            Lambertian lambertian = new Lambertian(testSampleGenerator, testDiffuseCoefficient, testDiffuseColor);
            Assert.That(lambertian.DiffuseCoefficient, Is.EqualTo(testDiffuseCoefficient));
            Assert.That(lambertian.DiffuseColor, Is.EqualTo(testDiffuseColor));
            Assert.That(lambertian.SampleGenerator, Is.EqualTo(testSampleGenerator));
        }

        [TestCase(1, 1, 0, 0, Helpers.InversePi, 0, 0)]
        public void F(double diffuseCoefficient, double diffuseRed, double diffuseGreen, double DiffuseBlue, double expectedRed, double expectedGreen, double expectedBlue)
        {
            Lambertian lambertia = new Lambertian(
                new UnitSquareMappedSampleGenerator(SampleAlgorithms.Hammersley, new Random(), 16, 16), diffuseCoefficient, new RGBColor(diffuseRed, DiffuseBlue, diffuseGreen));
            Assert.That(
                lambertia.F(
                    shadeRecord: new ShadeRecord(new Vector3D(0, 0, 0), new Point3D(0, 0, 0)), 
                    incoming: new Vector3D(0, 0, 0), 
                    outgoing: new Vector3D(0, 0, 0)),
                Is.EqualTo(new RGBColor(expectedRed, expectedGreen, expectedBlue)));
        }


        [TestCase(1, 1, 0, 0, 1, 0, 0)]
        public void Rho(double diffuseCoefficient, double diffuseRed, double diffuseGreen, double DiffuseBlue, double expectedRed, double expectedGreen, double expectedBlue)
        {
            Lambertian lambertia = new Lambertian(
                new UnitSquareMappedSampleGenerator(SampleAlgorithms.Hammersley, new Random(), 16, 16), diffuseCoefficient, new RGBColor(diffuseRed, DiffuseBlue, diffuseGreen));
            Assert.That(
                lambertia.Rho(
                    shadeRecord: new ShadeRecord(new Vector3D(0, 0, 0), new Point3D(0, 0, 0)),
                    outgoing: new Vector3D(0, 0, 0)),
                Is.EqualTo(new RGBColor(expectedRed, expectedGreen, expectedBlue)));
        }
    }
}
