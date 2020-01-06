namespace GFX.Tutorial.Inputs
{
    /// <summary>
    /// Key Event Arguments
    /// </summary>
    public interface IKeyEventArgs
    {
        /// <inheritdoc cref="System.Windows.Input.Key" />
        Key Key { get; }

        /// <inheritdoc cref="Modifiers" />
        KeyModifiers Modifiers { get; }
    }
}
