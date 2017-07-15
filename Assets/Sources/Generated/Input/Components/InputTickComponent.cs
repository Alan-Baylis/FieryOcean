//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputContext {

    public InputEntity tickEntity { get { return GetGroup(InputMatcher.Tick).GetSingleEntity(); } }
    public TickComponent tick { get { return tickEntity.tick; } }
    public bool hasTick { get { return tickEntity != null; } }

    public InputEntity SetTick(ulong newValue) {
        if (hasTick) {
            throw new Entitas.EntitasException("Could not set Tick!\n" + this + " already has an entity with TickComponent!",
                "You should check if the context already has a tickEntity before setting it or use context.ReplaceTick().");
        }
        var entity = CreateEntity();
        entity.AddTick(newValue);
        return entity;
    }

    public void ReplaceTick(ulong newValue) {
        var entity = tickEntity;
        if (entity == null) {
            entity = SetTick(newValue);
        } else {
            entity.ReplaceTick(newValue);
        }
    }

    public void RemoveTick() {
        tickEntity.Destroy();
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputEntity {

    public TickComponent tick { get { return (TickComponent)GetComponent(InputComponentsLookup.Tick); } }
    public bool hasTick { get { return HasComponent(InputComponentsLookup.Tick); } }

    public void AddTick(ulong newValue) {
        var index = InputComponentsLookup.Tick;
        var component = CreateComponent<TickComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceTick(ulong newValue) {
        var index = InputComponentsLookup.Tick;
        var component = CreateComponent<TickComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveTick() {
        RemoveComponent(InputComponentsLookup.Tick);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class InputMatcher {

    static Entitas.IMatcher<InputEntity> _matcherTick;

    public static Entitas.IMatcher<InputEntity> Tick {
        get {
            if (_matcherTick == null) {
                var matcher = (Entitas.Matcher<InputEntity>)Entitas.Matcher<InputEntity>.AllOf(InputComponentsLookup.Tick);
                matcher.componentNames = InputComponentsLookup.componentNames;
                _matcherTick = matcher;
            }

            return _matcherTick;
        }
    }
}