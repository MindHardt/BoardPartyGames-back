namespace Application.Spyfall.Models;

public record Game
{
    private int[] _spyIndices;
    public Location Location { get; }
    public int RandomSeed { get; }
    public byte SpiesCount { get; }
    public byte PlayersCount { get; }

    private Game(int[] spyIndices, Location location, int randomSeed, byte spiesCount, byte playersCount)
    {
        _spyIndices = spyIndices;
        Location = location;
        RandomSeed = randomSeed;
        SpiesCount = spiesCount;
        PlayersCount = playersCount;
    }

    /// <summary>
    /// Gets a card for a zero-based <paramref name="index"/>.
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public PlayerCard GetCard(byte index)
        => _spyIndices.Contains(index)
            ? PlayerCard.Spy
            : index <= PlayersCount
                ? new PlayerCard(Location.Name, Location.Roles[index])
                : throw new ArgumentOutOfRangeException(nameof(index), "Card number is bigger than players count.");

    public static Game Create(byte playersCount, byte? spiesCount = null, int? randomSeed = null)
    {
        randomSeed ??= Random.Shared.Next(int.MinValue, int.MaxValue);
        spiesCount ??= CalculateSpiesCount(playersCount);

        var random = new Random(randomSeed.Value);
        var spyIndices = CalculateSpiesIndices(random, playersCount, spiesCount.Value);
        var location = PickLocation(random, playersCount);
        return new Game(spyIndices, location, randomSeed.Value, spiesCount.Value, playersCount);
    }

    private static byte CalculateSpiesCount(byte playersCount)
        => (byte)(playersCount > 5 ? 2 : 1);

    private static Location PickLocation(Random random, byte citizenCount)
        => Location.DefaultLocations
            .Where(x => x.Roles.Count >= citizenCount)
            .MaxBy(_ => random.Next())
        ?? throw new InvalidOperationException("An error occured when retrieving location. Maybe you want too many players...");

    private static int[] CalculateSpiesIndices(Random random, byte playersCount, byte spiesCount)
        => playersCount >= spiesCount
            ? random.GetItems(Enumerable.Range(0, playersCount).ToArray(), spiesCount)
            : throw new ArgumentException("Cannot have more spies than total players", nameof(spiesCount));

    /// <summary>
    /// Creates a key that can be used to recreate
    /// this <see cref="Game"/> with <see cref="FromKey"/>.
    /// </summary>
    /// <returns></returns>
    public string GetKey()
    {
        var randomSeed = BitConverter.GetBytes(RandomSeed);

        var bytes = randomSeed.Append(SpiesCount).Append(PlayersCount).ToArray();
        var hex = Convert.ToHexString(bytes);
        return $"{hex[..4]}-{hex[4..8]}-{hex[8..]}";
    }

    /// <summary>
    /// Creates <see cref="Game"/> from its key,
    /// created by <see cref="GetKey"/>.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static Game FromKey(string key)
    {
        var bytes = Convert.FromHexString(key.Replace("-", string.Empty));

        var randomSeed = BitConverter.ToInt32(bytes.AsSpan()[..4]);
        var spiesCount = bytes[4];
        var playersCount = bytes[5];

        return Create(playersCount, spiesCount, randomSeed);
    }
}