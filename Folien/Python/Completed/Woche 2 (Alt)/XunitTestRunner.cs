using System;
using System.Reflection;
using System.Linq;
using Xunit;
using Xunit.Sdk;
using System.Collections.Generic;

public class XunitTestRunner
{
    public static void RunTests<TTestClass>() where TTestClass : class
    {
        RunTests(typeof(TTestClass));
    }

    public static void RunTests(Type testClassType)
    {
        int totalTests = 0;
        int passedTests = 0;
        int failedTests = 0;

        // Set up fixture instances
        var fixtureTypeToInstance = new Dictionary<Type, object>();

        var fixtureTypes = testClassType.GetInterfaces()
            .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IClassFixture<>))
            .Select(i => i.GetGenericArguments()[0])
            .Distinct().ToList();

        // Create instances of the fixtures
        foreach (var fixtureType in fixtureTypes)
        {
            var fixtureInstance = Activator.CreateInstance(fixtureType);
            fixtureTypeToInstance.Add(fixtureType, fixtureInstance);
        }

        var methods = testClassType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
            .Where(m => m.GetCustomAttributes(typeof(FactAttribute), true).Any() ||
                        m.GetCustomAttributes(typeof(TheoryAttribute), true).Any())
            .ToList();

        // Get the constructor of the test class
        var constructor = testClassType.GetConstructors().FirstOrDefault();

        // Get constructor parameters
        var constructorParameters = constructor.GetParameters();
        var parameters = constructorParameters.Select(p =>
            fixtureTypeToInstance.ContainsKey(p.ParameterType) ?
                fixtureTypeToInstance[p.ParameterType] : null).ToArray();

        foreach (var method in methods)
        {
            var dataAttributes = method.GetCustomAttributes(typeof(DataAttribute), true).Cast<DataAttribute>().ToList();

            if (dataAttributes.Any())
            {
                // Handle [Theory] methods with data attributes
                foreach (var dataAttribute in dataAttributes)
                {
                    var dataSets = dataAttribute.GetData(method);
                    foreach (var data in dataSets)
                    {
                        totalTests++;
                        var testClassInstance = constructor.Invoke(parameters);
                        try
                        {
                            method.Invoke(testClassInstance, data);
                            Console.WriteLine($"[PASS] {method.Name}({FormatParameters(data)})");
                            passedTests++;
                        }
                        catch (TargetInvocationException ex)
                        {
                            Console.WriteLine($"[FAIL] {method.Name}({FormatParameters(data)}): {ex.InnerException?.Message ?? ex.Message}");
                            failedTests++;
                        }
                        finally
                        {
                            if (testClassInstance is IDisposable disposableInstance)
                            {
                                disposableInstance.Dispose();
                            }
                        }
                    }
                }
            }
            else
            {
                // Handle [Fact] methods without data attributes
                totalTests++;
                var testClassInstance = constructor.Invoke(parameters);
                try
                {
                    method.Invoke(testClassInstance, null);
                    Console.WriteLine($"[PASS] {method.Name}");
                    passedTests++;
                }
                catch (TargetInvocationException ex)
                {
                    Console.WriteLine($"[FAIL] {method.Name}: {ex.InnerException?.Message ?? ex.Message}");
                    failedTests++;
                }
                finally
                {
                    if (testClassInstance is IDisposable disposableInstance)
                    {
                        disposableInstance.Dispose();
                    }
                }
            }
        }

        // Dispose of fixture instances
        foreach (var fixtureInstance in fixtureTypeToInstance.Values)
        {
            if (fixtureInstance is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        Console.WriteLine($"Total tests: {totalTests}, Passed: {passedTests}, Failed: {failedTests}");
    }

    private static string FormatParameters(object[] parameters)
    {
        if (parameters == null)
            return string.Empty;
        return string.Join(", ", parameters.Select(p => p == null ? "null" : p.ToString()));
    }
}
