using Entitas;
using Entitas.CodeGeneration.Attributes;

[Input, Unique]
public sealed class TickComponent : IComponent {

    public ulong value;
}
