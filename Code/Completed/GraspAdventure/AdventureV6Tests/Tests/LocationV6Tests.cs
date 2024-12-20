﻿using AdventureV6;

namespace AdventureV6Tests.Tests;

public class LocationV6Tests
{
    private readonly Dictionary<string, object?> _locationData = new()
    {
        ["name"] = "Room 1",
        ["description"] = "This is a room",
        ["connections"] = new Dictionary<string, object> {["north"] = "Room 2"}
    };

    [Fact]
    public void FromData_ShouldCreateLocationCorrectly()
    {
        Location unit = Location.FromData(_locationData);

        Assert.Equal("Room 1", unit.Name);
        Assert.Equal("This is a room", unit.Description);
        Assert.Empty(unit.ConnectedLocations);
        Assert.Empty(unit.ConnectedDirections);
    }

    [Fact]
    public void GetConnectedLocation_ShouldThrowForInvalidDirection()
    {
        Location unit = Location.FromData(_locationData);
        Assert.Throws<ArgumentException>(() => unit.GetConnectedLocation("nowhere"));
    }

    [Fact]
    public void ConnectedDirections_ShouldBeCorrect()
    {
        Location room2 = Location.FromData(new Dictionary<string, object?> {["name"] = "Room 2"});
        Location unit = Location.FromData(_locationData);
        unit.SetConnectedLocation("north", room2);

        Assert.Equal<string>(["north"], unit.ConnectedDirections);
        Assert.Equal(room2, unit.GetConnectedLocation("north"));
    }

    [Fact]
    public void ToString_ShouldContainRelevantInfo()
    {
        Location unit = Location.FromData(_locationData);
        var result = unit.ToString();

        Assert.Contains("Room 1", result);
        Assert.Contains("This is a room", result);
    }

    // Not sure whether this is important.
    // [Fact]
    // public void Equals_ShouldBeEqualForSameData()
    // {
    //     var unit1 = Location.FromData(_locationData);
    //     var unit2 = Location.FromData(_locationData);
    //
    //     Assert.NotSame(unit1, unit2);
    //     Assert.Equal(unit1, unit2);
    //     Assert.Equal(unit1.GetHashCode(), unit2.GetHashCode());
    // }
}
