namespace Ether
{
    public interface ICarrier
    {
        string Signature { get; }
        float Value { get; } // Normally range 0f<=>1f
    }
}
