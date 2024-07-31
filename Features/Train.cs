using System;
using EFT.MovingPlatforms;
using JetBrains.Annotations;
using UnityEngine;

#nullable enable

namespace EFT.Trainer.Features;

[UsedImplicitly]
internal class Train : TriggerFeature
{
	public override string Name => "召唤火车";
	public override string Description => "在兼容地图（如保护区或灯塔）上召唤火车。";

	public override KeyCode Key { get; set; } = KeyCode.None;

	protected override void UpdateOnceWhenTriggered()
	{
		var locomotive = FindObjectOfType<Locomotive>();
		if (locomotive == null)
			return;

		locomotive.Init(DateTime.UtcNow);
	}
}
