using System.Linq;
using EFT.InventoryLogic;
using EFT.Trainer.Extensions;
using JetBrains.Annotations;

#nullable enable

namespace EFT.Trainer.Features;

[UsedImplicitly]
internal class Durability : ToggleFeature
{
	public override string Name => "耐久度";
	public override string Description => "物品最大耐久度。";

	public override bool Enabled { get; set; } = false;

	protected override void UpdateWhenEnabled()
	{
		var player = GameState.Current?.LocalPlayer;
		if (!player.IsValid())
			return;

		var allPlayerItems = player.Profile
			.Inventory
			.GetPlayerItems()
			.ToArray();

		foreach (var item in allPlayerItems)
		{
			var repairable = item?.GetItemComponent<RepairableComponent>();
			if (repairable == null)
				continue;

			repairable.MaxDurability = repairable.TemplateDurability;
			repairable.Durability = repairable.MaxDurability;
		}
	}
}
