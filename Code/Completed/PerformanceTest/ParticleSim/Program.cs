using System.Diagnostics;

namespace ParticleSim;

internal static class Program
{
    // Reference type (class) version
    private class ParticleClass
    {
        public float X;
        public float VelocityX;

        public override string ToString()
        {
            return $"X: {X}, VelocityX: {VelocityX}";
        }
    }

    // Value type (struct) version
    private struct ParticleStruct
    {
        public float X;
        public float VelocityX;

        public override string ToString()
        {
            return $"X: {X}, VelocityX: {VelocityX}";
        }
    }

    public static void Main()
    {
        const int particleCount = 10_000_000;
        const int iterations = 1000;

        // Test with classes (reference types)
        Console.WriteLine("Testing with classes (reference types)...");
        {
            var particles = new ParticleClass[particleCount];

            // Initialize particles
            for (var i = 0; i < particleCount; i++)
            {
                particles[i] = new ParticleClass
                {
                    X = i,
                    VelocityX = 1,
                };
            }

            var sw = Stopwatch.StartNew();

            // Update simulation
            for (var iter = 0; iter < iterations; iter++)
            {
                for (var i = 0; i < particleCount; i++)
                {
                    particles[i].X += particles[i].VelocityX;
                }
            }

            sw.Stop();
            Console.WriteLine($"Class version took: {sw.ElapsedMilliseconds}ms");
            Console.WriteLine($"Example particle: {particles[0]}");
        }

        GC.Collect(2, GCCollectionMode.Aggressive);
        GC.WaitForPendingFinalizers();

        // Test with structs (value types)
        Console.WriteLine("\nTesting with structs (value types)...");
        {
            var particles = new ParticleStruct[particleCount];

            // Initialize particles
            for (var i = 0; i < particleCount; i++)
            {
                particles[i] = new ParticleStruct
                {
                    X = i,
                    VelocityX = 1,
                };
            }

            var sw = Stopwatch.StartNew();

            // Update simulation
            for (var iter = 0; iter < iterations; iter++)
            {
                for (var i = 0; i < particleCount; i++)
                {
                    particles[i].X += particles[i].VelocityX;
                }
            }

            sw.Stop();
            Console.WriteLine($"Struct version took: {sw.ElapsedMilliseconds}ms");
            Console.WriteLine($"Example particle: {particles[0]}");
        }
    }
}
