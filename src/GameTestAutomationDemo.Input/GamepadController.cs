using Nefarius.ViGEm.Client;
using Nefarius.ViGEm.Client.Targets;
using Nefarius.ViGEm.Client.Targets.Xbox360;

public class GamepadController : IDisposable
{
    private readonly ViGEmClient _client;
    private readonly IXbox360Controller _controller;

    public GamepadController()
    {
        _client = new ViGEmClient();
        _controller = _client.CreateXbox360Controller();
        _controller.Connect();
    }

    // =========================
    // BOTONES
    // =========================

    public void PressButton(Xbox360Button button, int durationMs = 100)
    {
        _controller.SetButtonState(button, true);
        Thread.Sleep(durationMs);
        _controller.SetButtonState(button, false);
    }

    public void ButtonDown(Xbox360Button button)
    {
        _controller.SetButtonState(button, true);
    }

    public void ButtonUp(Xbox360Button button)
    {
        _controller.SetButtonState(button, false);
    }

    // =========================
    // ANALOG STICK
    // =========================

    public void MoveLeftStick(float x, float y, int durationMs = 100)
    {
        short sx = (short)(x * short.MaxValue);
        short sy = (short)(y * short.MaxValue);

        _controller.SetAxisValue(Xbox360Axis.LeftThumbX, sx);
        _controller.SetAxisValue(Xbox360Axis.LeftThumbY, sy);

        Thread.Sleep(durationMs);

        // Reset
        _controller.SetAxisValue(Xbox360Axis.LeftThumbX, 0);
        _controller.SetAxisValue(Xbox360Axis.LeftThumbY, 0);
    }

    public void SetLeftStick(float x, float y)
    {
        short sx = (short)(x * short.MaxValue);
        short sy = (short)(y * short.MaxValue);

        _controller.SetAxisValue(Xbox360Axis.LeftThumbX, sx);
        _controller.SetAxisValue(Xbox360Axis.LeftThumbY, sy);
    }

    public void ResetStick()
    {
        _controller.SetAxisValue(Xbox360Axis.LeftThumbX, 0);
        _controller.SetAxisValue(Xbox360Axis.LeftThumbY, 0);
    }

    // =========================
    // TRIGGERS
    // =========================

    public void SetTrigger(Xbox360Slider trigger, byte value)
    {
        _controller.SetSliderValue(trigger, value);
    }

    // =========================
    // CLEANUP
    // =========================

    public void Dispose()
    {
        _controller.Disconnect();
        _client.Dispose();
    }
}
