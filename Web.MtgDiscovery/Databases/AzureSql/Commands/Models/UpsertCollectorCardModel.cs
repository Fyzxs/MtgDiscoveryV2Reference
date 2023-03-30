namespace Web.MtgDiscovery.Databases.AzureSql.Commands.Models;

public sealed class UpsertCollectorCardModel
{
    public string CollectorId { get; set; }
    public string SetId { get; set; }
    public string CardId { get; set; }
    public int Unmodified { get; set; }
    public int Signed { get; set; }
    public int Altered { get; set; }
    public int Proof { get; set; }
}
