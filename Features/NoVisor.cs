using System;
using JetBrains.Annotations;
using UnityEngine;

#nullable enable

namespace EFT.Trainer.Features;

[UsedImplicitly]
internal class NoVisor : ToggleFeature
{
	public override string Name => "无护目镜";
	public override string Description => "没有护目镜，因此即使使用面罩护目镜，您也看不到它。";

	public override bool Enabled { get; set; } = false;

	protected override void Update()
	{
		base.Update();

		var camera = GameState.Current?.Camera;
		if (camera == null)
			return;

		var component = camera.GetComponent<VisorEffect>();
		if (component == null || Mathf.Abs(component.Intensity - Convert.ToInt32(!Enabled)) < Mathf.Epsilon )
			return;

		component.Intensity = Convert.ToInt32(!Enabled);
	}
}
