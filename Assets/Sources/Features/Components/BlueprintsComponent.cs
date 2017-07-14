using Entitas;
using Entitas.CodeGeneration;
using Entitas.CodeGeneration.Attributes;
using Entitas.Unity.Serialization.Blueprints;


[Game, Unique]
public sealed class BlueprintsComponent : IComponent {

    public Blueprints instance;
}
