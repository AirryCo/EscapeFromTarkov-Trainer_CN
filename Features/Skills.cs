using EFT.Trainer.Extensions;
using JetBrains.Annotations;
using UnityEngine;

#nullable enable

namespace EFT.Trainer.Features;

[UsedImplicitly]
internal class Skills : TriggerFeature
{
	public override string Name => "满技能";
	public override string Description => "所有技能达到精英等级（51）且所有武器掌握达到3级。";

	public override KeyCode Key { get; set; } = KeyCode.None;

	protected override void UpdateOnceWhenTriggered()
	{
		var player = GameState.Current?.LocalPlayer;
		if (!player.IsValid())
			return;

		if (player.Skills?.Skills == null)
			return;

		foreach (var skill in player.Skills.DisplayList)
			skill.SetLevel(51);

		if (player.Skills.Mastering == null)
			return;

		foreach (var item in player.Skills.Mastering.Values)
			item.Current = item.MasteringGroup.Level2 + item.MasteringGroup.Level3;
	}
}
