using Lib.MtgDiscovery.AzureSql.Core.Commands.SprocArgs;
using Lib.MtgDiscovery.Primitives.Core.Cards;
using Lib.MtgDiscovery.Primitives.Core.Collectors;
using Lib.MtgDiscovery.Primitives.Core.Sets;
using Web.MtgDiscovery.Databases.AzureSql.Commands.Models;

namespace Web.MtgDiscovery.Databases.AzureSql.Commands.SprocArgs;

public sealed class UpsertCollectorCardSprocArgs : IAzSqlSprocArgs<UpsertCollectorCardModel>
{
    private const char AlteredChar = 'a';
    private const char SignedChar = 's';
    private const char ProofChar = 'p';

    private const int InputIsAltered = 1;
    private const int InputIsdSigned = 2;
    private const int InputIsProof = 4;
    private const int InputIsUnmodified = 8;

    private readonly CollectorId _collectorId;
    private readonly SetId _setId;
    private readonly CardId _cardId;
    private readonly string _userInput;

    public UpsertCollectorCardSprocArgs(CollectorId collectorId, SetId setId, CardId cardId, string input)
    {
        _collectorId = collectorId;
        _setId = setId;
        _cardId = cardId;
        _userInput = input;
    }
    public UpsertCollectorCardModel AsSqlParams()
    {
        return new UpsertCollectorCardModel
        {
            CollectorId = _collectorId,
            SetId = _setId,
            CardId = _cardId,
            Unmodified = QuantityFor(InputIsUnmodified),
            Altered = QuantityFor(InputIsAltered),
            Proof = QuantityFor(InputIsProof),
            Signed = QuantityFor(InputIsdSigned)
        };
    }

    private int UpdateCategory()
    {
        if (_userInput.StartsWith(AlteredChar)) return InputIsAltered;
        if (_userInput.StartsWith(SignedChar)) return InputIsdSigned;
        if (_userInput.StartsWith(ProofChar)) return InputIsProof;

        return InputIsUnmodified;
    }

    private int Quantity()
    {
        string value = _userInput;
        if (UpdateCategory() != InputIsUnmodified) value = value[1..];

        return int.Parse(value);
    }

    private int QuantityFor(int quantityType) => UpdateCategory() != quantityType ? 0 : Quantity();


}
