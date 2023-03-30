namespace Web.MtgDiscovery.Primitives;

public sealed class ToLowerInvariantArtistName : ArtistName
{
    private readonly string _origin;

    public ToLowerInvariantArtistName(string origin) => _origin = origin;

    public override string AsSystemType() => _origin.ToLowerInvariant();
}
