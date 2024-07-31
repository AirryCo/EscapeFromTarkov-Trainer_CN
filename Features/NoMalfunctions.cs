using EFT.InventoryLogic;
using EFT.Trainer.Extensions;
using JetBrains.Annotations;

#nullable enable

namespace EFT.Trainer.Features;

[UsedImplicitly]
internal class NoMalfunctions : ToggleFeature
{
	public override string Name => "武器无故障";
	public override string Description => "武器无故障：无哑火、弹射或供弹失败。无枪栓卡住或过热。";

	public override bool Enabled { get; set; } = false;

	protected override void UpdateWhenEnabled()
	{
		var player = GameState.Current?.LocalPlayer;
		if (!player.IsValid())
			return;

		if (player.HandsController.Item is not Weapon)
			return;

		if (player.HandsController is not Player.FirearmController controller) 
			return;

		var template = controller.Item?.Template;
		if (template == null)
			return;

		template.AllowFeed = false;
		template.AllowJam = false;
		template.AllowMisfire = false;
		template.AllowOverheat = false;
		template.AllowSlide = false;
	}
}
