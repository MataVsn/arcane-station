using Content.Shared._Art.TTS;
using Content.Shared.Cloning.Events;
using Content.Shared.Humanoid;

namespace Content.Shared._Arcane.Speech;

public sealed class TtsCloningSystem : EntitySystem
{
    [Dependency] private readonly SharedHumanoidAppearanceSystem _humanoidAppearance = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<TTSComponent, CloningEvent>(OnCloned);
    }

    private void OnCloned(Entity<TTSComponent> ent, ref CloningEvent args)
    {
        var cloneTts = EnsureComp<TTSComponent>(args.CloneUid);
        cloneTts.VoicePrototype = ent.Comp.VoicePrototype;
        cloneTts.Effect = ent.Comp.Effect;

        if (ent.Comp.VoicePrototype is { } voice)
            _humanoidAppearance.SetTTSVoice(args.CloneUid, voice, false);

        Dirty(args.CloneUid, cloneTts);
    }
}
