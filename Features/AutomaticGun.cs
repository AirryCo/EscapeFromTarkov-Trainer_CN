using EFT.InventoryLogic;
using EFT.Trainer.Configuration;
using EFT.Trainer.Extensions;
using JetBrains.Annotations;

#nullable enable

namespace EFT.Trainer.Features;

[UsedImplicitly]
internal class AutomaticGun : ToggleFeature
{
	public override string Name => "强制射击模式";
	public override string Description => "强制所有枪支（甚至是栓动枪）使用具有可定制射速的自动射击模式。";

	public override bool Enabled { get; set; } = false;

	[ConfigurationProperty]
	public int Rate { get; set; } = 500;

	protected override void UpdateWhenEnabled()
	{
		var player = GameState.Current?.LocalPlayer;
		if (!player.IsValid())
			return;

		if (player.HandsController.Item is not Weapon weapon)
			return;

		var fireModeComponent = weapon.GetItemComponent<FireModeComponent>();
		if (fireModeComponent == null)
			return;

		fireModeComponent.FireMode = Weapon.EFireMode.fullauto;

		if (player.HandsController is not Player.FirearmController controller) 
			return;

		var template = controller.Item?.Template;
		if (template == null)
			return;

		template.BoltAction = false;
		template.bFirerate = Rate;
	}
}
