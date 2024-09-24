// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>xUnit Fixtures</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
//
// ## Probleme mit einfachen Tests
//
// - Kompliziertes Setup (und Teardown)
//   - Wiederholung von Code
//   - Schwer zu warten und verstehen
// - Viele ähnliche Tests

// %%
public class Dependency1
{
    public Dependency1(int i, int j)
    {
        this.i = i;
        this.j = j;
    }

    public int Value()
    {
        return i + 2 * j;
    }

    private int i;
    private int j;
}

// %%
public class Dependency2
{
    public Dependency2(Dependency1 d1, int k)
    {
        this.d1 = d1;
        this.k = k + d1.Value();
    }

    public int Value()
    {
        return d1.Value() + 3 * k;
    }

    private Dependency1 d1;
    public int k;
}

// %%
public class MyClass
{
    public MyClass(Dependency2 d2, int m) { this.d2 = d2; this.m = m; }
    public int Value()
    {
        if (m < - 10) { return d2.Value() + 2 * m; }
        else if (m < 0) { return d2.Value() - 3 * m; }
        else if (m < 10) { return d2.Value() + 4 * m; }
        else { return d2.Value() - 5 * m; }
    }
    public void ReleaseResources() { d2 = null;  }

    private Dependency2 d2;
    private int m;
}
// %%
#r "nuget: xunit, *"

// %%
using Xunit;

// %%
public class MyClassTest1
{
    [Fact]
    public void TestValue1()
    {
        Dependency1 d1 = new Dependency1(1, 2);
        Dependency2 d2 = new Dependency2(d1, 3);
        MyClass unit = new MyClass(d2, -11);

        Assert.Equal(7, unit.Value());

        unit.ReleaseResources();
    }

    [Fact]
    public void TestValue2()
    {
        Dependency1 d1 = new Dependency1(1, 2);
        Dependency2 d2 = new Dependency2(d1, 4);
        MyClass unit = new MyClass(d2, -1);

        Assert.Equal(35, unit.Value());

        unit.ReleaseResources();
    }

    [Fact]
    public void TestValue3()
    {
        Dependency1 d1 = new Dependency1(1, 2);
        Dependency2 d2 = new Dependency2(d1, 3);
        MyClass unit = new MyClass(d2, 1);

        Assert.Equal(33, unit.Value());

        unit.ReleaseResources();
    }

    [Fact]
    public void TestValue4()
    {
        Dependency1 d1 = new Dependency1(1, 2);
        Dependency2 d2 = new Dependency2(d1, 3);
        MyClass unit = new MyClass(d2, 11);

        Assert.Equal(-26, unit.Value());

        unit.ReleaseResources();
    }
}

// %%
#load "XunitTestRunner.cs"

// %%
using static XunitTestRunner;

// %%
RunTests(typeof(MyClassTest1));

// %%
public class MyClassTest2
{
    private void SetupDependencies()
    {
        Dependency1 d1 = new Dependency1(1, 2);
        d2 = new Dependency2(d1, 3);
    }

    private Dependency2 d2;

    [Fact]
    public void TestValue1()
    {
        SetupDependencies();
        MyClass unit = new MyClass(d2, -11);

        Assert.Equal(7, unit.Value());

        unit.ReleaseResources();
    }

    [Fact]
    public void TestValue2()
    {
        SetupDependencies();
        MyClass unit = new MyClass(d2, -1);

        Assert.Equal(32, unit.Value());

        unit.ReleaseResources();
    }

    [Fact]
    public void TestValue3()
    {
        SetupDependencies();
        MyClass unit = new MyClass(d2, 1);

        Assert.Equal(33, unit.Value());

        unit.ReleaseResources();
    }

    [Fact]
    public void TestValue4()
    {
        SetupDependencies();
        MyClass unit = new MyClass(d2, 11);

        Assert.Equal(-26, unit.Value());

        unit.ReleaseResources();
    }
}


// %%
RunTests(typeof(MyClassTest2));


// %%
public class MyClassTest3
{
    public MyClassTest3()
    {
        Dependency1 d1 = new Dependency1(1, 2);
        d2 = new Dependency2(d1, 3);
    }

    [Fact]
    public void TestValue1()
    {
        MyClass unit = new MyClass(d2, -11);
        // d2.k = 4;
        Assert.Equal(7, unit.Value());
        unit.ReleaseResources();
    }

    [Fact]
    public void TestValue2()
    {
        MyClass myClass = new MyClass(d2, -1);
        Assert.Equal(32, myClass.Value());
        myClass.ReleaseResources();
    }

    private Dependency2 d2;
}


// %%
RunTests(typeof(MyClassTest3));

// %% [markdown]
//
// ## Testklassen und Fixtures
//
// - Testklassen können
//   - mehrere Tests zusammenfassen
//   - gemeinsamen Zustand für die Tests bereitstellen
// - `@BeforeEach` für Setup (Initialisierung)
// - `@AfterEach` für Teardown (Freigabe)
// - `@BeforeAll` und `@AfterAll` für Setup und Teardown auf Klassenebene

// %%
using Xunit;
using System;

// %%
public class TestFixture : IDisposable
{
    public TestFixture()
    {
        Console.WriteLine("Creating the class fixture");
    }

    public void Dispose()
    {
        Console.WriteLine("Disposing the class fixture");
    }
}

// %%
public class XUnitFixturesTest : IDisposable, IClassFixture<TestFixture>
{
    private List<string> testList;
    private TestFixture testFixture;
    private int id;

    public XUnitFixturesTest(TestFixture fixture)
    {
        testList = ["test"];
        testFixture = fixture;
        id = this.GetHashCode();
        Console.WriteLine($"Instantiating test: {id,8}. Fixture is {testFixture.GetHashCode(),8}");
    }

    public void Dispose()
    {
        testList = null;
        Console.WriteLine($"Disposing test:     {id,8}. Fixture is {testFixture.GetHashCode(),8}");
    }

    [Fact]
    public void TestListContents()
    {
        Assert.Single(testList);
        Assert.Equal(["test"], testList);
    }

    [Fact]
    public void TestListOperations()
    {
        testList.Add("another");
        Assert.Equal(2, testList.Count);
    }
}

// %%
RunTests(typeof(XUnitFixturesTest));

// %%
public class MyClassTest4 : IDisposable
{
    public MyClassTest4()
    {
        Dependency1 d1 = new Dependency1(1, 2);
        d2 = new Dependency2(d1, 3);
    }

    public void Dispose()
    {
        d2 = null;
    }

    [Fact]
    public void TestValue1()
    {
        MyClass unit = new MyClass(d2, -11);
        Assert.Equal(7, unit.Value());
    }

    [Fact]
    public void TestValue2()
    {
        MyClass unit = new MyClass(d2, -1);
        Assert.Equal(32, unit.Value());
    }

    [Fact]
    public void TestValue3()
    {
        MyClass unit = new MyClass(d2, 1);
        Assert.Equal(33, unit.Value());
    }

    [Fact]
    public void TestValue4()
    {
        MyClass unit = new MyClass(d2, 11);
        Assert.Equal(-26, unit.Value());
    }

    private Dependency2 d2;
}

// %%
RunTests(typeof(MyClassTest4));


// %% [markdown]
//
// ## Workshop: xUnit Fixtures für einen Musik-Streaming-Dienst
//
// In diesem Workshop werden wir Tests für ein einfaches Musik-Streaming-System
// mit xUnit implementieren.
//
// In diesem System haben wir die Klassen `User`, `Song`, `PlaylistEntry` und `Playlist`.
// - Die Klasse `User` repräsentiert einen Benutzer des Streaming-Dienstes.
// - Ein `Song` repräsentiert ein Musikstück, das im Streaming-Dienst verfügbar ist.
// - Ein `PlaylistEntry` repräsentiert einen Eintrag in einer Playlist, also ein
//   Musikstück und die Anzahl der Wiedergaben.
// - Eine `Playlist` repräsentiert eine Sammlung von Musikstücken, also eine Liste
//   von `PlaylistEntry`-Objekten.

// %%
public class User
{
    public string Username { get; }
    public string Email { get; }
    public bool IsPremium { get; }

    public User(string username, string email, bool isPremium)
    {
        Username = username;
        Email = email;
        IsPremium = isPremium;
    }
}

// %%
public class Song
{
    public string Title { get; }
    public string Artist { get; }
    public int DurationInSeconds { get; }

    public Song(string title, string artist, int durationInSeconds)
    {
        Title = title;
        Artist = artist;
        DurationInSeconds = durationInSeconds;
    }
}

// %%
public class PlaylistEntry
{
    public Song Song { get; }
    public int PlayCount { get; private set; }

    public PlaylistEntry(Song song)
    {
        Song = song;
        PlayCount = 0;
    }

    public void IncrementPlayCount()
    {
        PlayCount++;
    }
}

// %%
public class Playlist
{
    private List<PlaylistEntry> entries = new List<PlaylistEntry>();
    private User owner;
    public string Name { get; }

    public Playlist(User owner, string name)
    {
        this.owner = owner;
        Name = name;
    }

    public void AddSong(Song song)
    {
        entries.Add(new PlaylistEntry(song));
    }

    public int GetTotalDuration()
    {
        return entries.Sum(entry => entry.Song.DurationInSeconds);
    }

    public int GetTotalPlayCount()
    {
        return entries.Sum(entry => entry.PlayCount);
    }

    public bool CanAddMoreSongs()
    {
        if (owner.IsPremium)
        {
            return true;
        }
        return entries.Count < 100;  // Non-premium users limited to 100 songs
    }

    public List<PlaylistEntry> GetEntries()
    {
        return new List<PlaylistEntry>(entries);
    }
}

// %% [markdown]
//
// Implementieren Sie Tests für dieses System mit xUnit. Verwenden Sie dabei Fixtures,
// um die Tests zu strukturieren und Code-Duplikation zu vermeiden.
//
// Beachten Sie bei der Implementierung der Tests die folgenden Aspekte:
// - Grundlegende Funktionen wie das Hinzufügen von Songs zu einer Playlist
// - Berechnung der Gesamtdauer einer Playlist
// - Unterschiedliche Benutzerrechte (Premium vs. Nicht-Premium)
// - Begrenzung der Playlist-Größe für Nicht-Premium-Benutzer
// - Zählung der Wiedergaben von Songs
//
// Bewerten Sie Ihre Tests anhand der Kriterien für gute Unit-Tests, die wir
// in früheren Lektionen besprochen haben.

// %%
public class PlaylistEntryTest
{
    private Song song;
    private PlaylistEntry entry;

    public PlaylistEntryTest()
    {
        song = new Song("Test Song", "Test Artist", 180);
        entry = new PlaylistEntry(song);
    }

    [Fact]
    public void TestInitialPlayCount()
    {
        Assert.Equal(0, entry.PlayCount);
    }

    [Fact]
    public void TestIncrementPlayCount()
    {
        entry.IncrementPlayCount();
        entry.IncrementPlayCount();
        Assert.Equal(2, entry.PlayCount);
    }
}

// %%
RunTests(typeof(PlaylistEntryTest));

// %%
public class PlaylistBasicsTest
{
    private User user;
    private Song song1;
    private Song song2;
    private Playlist playlist;

    public PlaylistBasicsTest()
    {
        user = new User("Alice", "alice@example.com", false);
        song1 = new Song("Song 1", "Artist 1", 180);
        song2 = new Song("Song 2", "Artist 2", 240);
        playlist = new Playlist(user, "My Playlist");
    }

    [Fact]
    public void TestPlaylistInitiallyEmpty()
    {
        Assert.Empty(playlist.GetEntries());
    }

    [Fact]
    public void TestAddSong()
    {
        playlist.AddSong(song1);
        playlist.AddSong(song2);
        Assert.Equal(2, playlist.GetEntries().Count);
    }

    [Fact]
    public void TestTotalDurationForEmptyPlaylist()
    {
        Assert.Equal(0, playlist.GetTotalDuration());
    }

    [Fact]
    public void TestTotalDurationForNonEmptyPlaylist()
    {
        playlist.AddSong(song1);
        playlist.AddSong(song2);
        Assert.Equal(420, playlist.GetTotalDuration());
    }
}

// %%
RunTests(typeof(PlaylistBasicsTest));


// %%
public class PlaylistLimitsTest
{
    private User premiumUser;
    private User nonPremiumUser;
    private Song song;
    private Playlist premiumPlaylist;
    private Playlist nonPremiumPlaylist;

    public PlaylistLimitsTest()
    {
        premiumUser = new User("premium", "premium@example.com", true);
        nonPremiumUser = new User("regular", "regular@example.com", false);
        song = new Song("Test Song", "Test Artist", 180);
        premiumPlaylist = new Playlist(premiumUser, "Premium Playlist");
        nonPremiumPlaylist = new Playlist(nonPremiumUser, "Regular Playlist");
    }

    [Fact]
    public void TestCanAddMoreSongsForPremiumUser()
    {
        for (int i = 0; i < 150; i++)
        {
            premiumPlaylist.AddSong(song);
        }
        Assert.True(premiumPlaylist.CanAddMoreSongs());
    }

    [Fact]
    public void TestCanAddMoreSongsForNonPremiumUser()
    {
        for (int i = 0; i < 99; i++)
        {
            nonPremiumPlaylist.AddSong(song);
        }
        Assert.True(nonPremiumPlaylist.CanAddMoreSongs());
        nonPremiumPlaylist.AddSong(song);
        Assert.False(nonPremiumPlaylist.CanAddMoreSongs());
    }

    [Fact]
    public void TestTotalPlayCount()
    {
        premiumPlaylist.AddSong(song);
        premiumPlaylist.GetEntries()[0].IncrementPlayCount();
        premiumPlaylist.GetEntries()[0].IncrementPlayCount();
        Assert.Equal(2, premiumPlaylist.GetTotalPlayCount());
    }
}

// %%
RunTests(typeof(PlaylistLimitsTest));

// %%
