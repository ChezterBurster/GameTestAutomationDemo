using OpenCvSharp;
using System.Runtime.InteropServices;

public class VisionEngine
{
    public static Point? Find(string elementName, double threshold = 0.8)
    {
        var templatePath = TemplateRepository.Get(elementName);

        using var screen = Capture();
        using var template = Cv2.ImRead(templatePath, ImreadModes.Color);

        if (template.Empty())
            throw new Exception($"Template not found: {templatePath}");

        using var result = new Mat();

        Cv2.MatchTemplate(screen, template, result, TemplateMatchModes.CCoeffNormed);

        Cv2.MinMaxLoc(result, out _, out double maxVal, out _, out Point maxLoc);

        if (maxVal >= threshold)
        {
            return new Point(
                maxLoc.X + template.Width / 2,
                maxLoc.Y + template.Height / 2
            );
        }

        return null;
    }

    // =============================
    // SCREEN CAPTURE
    // =============================

    public static Mat Capture()
    {
        int width = GetSystemMetrics(0);
        int height = GetSystemMetrics(1);

        using var bmp = new System.Drawing.Bitmap(
            width,
            height,
            System.Drawing.Imaging.PixelFormat.Format24bppRgb
        );

        using (var g = System.Drawing.Graphics.FromImage(bmp))
        {
            g.CopyFromScreen(0, 0, 0, 0, new System.Drawing.Size(width, height));
        }

        var mat = new Mat(height, width, MatType.CV_8UC3);

        unsafe
        {
            var data = bmp.LockBits(
                new System.Drawing.Rectangle(0, 0, width, height),
                System.Drawing.Imaging.ImageLockMode.ReadOnly,
                bmp.PixelFormat
            );

            try
            {
                Buffer.MemoryCopy(
                    (void*)data.Scan0,
                    (void*)mat.Data,
                    height * mat.Step(),
                    height * data.Stride
                );
            }
            finally
            {
                bmp.UnlockBits(data);
            }
        }

        return mat;
    }

    [DllImport("user32.dll")]
    private static extern int GetSystemMetrics(int nIndex);
}
