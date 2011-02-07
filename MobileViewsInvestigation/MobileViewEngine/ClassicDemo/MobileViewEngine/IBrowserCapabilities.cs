namespace ClassicDemo
{
  public interface IBrowserCapabilities
  {
    bool IsMobileDevice { get; }
    string Platform { get; }
  }
}