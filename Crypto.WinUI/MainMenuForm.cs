using Crypto.Core.Services;
using ScottPlot;
using System.Globalization;

namespace Crypto.WinUI;

public partial class MainMenuForm : Form
{
    ScottPlot.Plottables.Scatter MyScatter;
    ScottPlot.Plottables.Crosshair MyCrosshair;
    
    private readonly ICryptoService _cryptoService;
        
    public MainMenuForm(ICryptoService cryptoService)
    {
        _cryptoService = cryptoService;
        
        InitializeComponent();

        formsPlot1.MouseMove += (s, e) =>
        {
            if (MyScatter == null || MyCrosshair == null)
                return;

            // determine where the mouse is and get the nearest point
            Pixel mousePixel = new(e.Location.X, e.Location.Y);
            Coordinates mouseLocation = formsPlot1.Plot.GetCoordinates(mousePixel);
            DataPoint nearest = MyScatter.Data.GetNearest(mouseLocation, formsPlot1.Plot.LastRender);

            // place the crosshair over the highlighted point
            if (nearest.IsReal)
            {
                MyCrosshair.IsVisible = true;
                MyCrosshair.Position = nearest.Coordinates;
                formsPlot1.Refresh();
                Text = $"Index={nearest.Index}, X={DateTime.FromOADate(nearest.X)}, Y={nearest.Y:0.######}";
            }

            // hide the crosshair when no point is selected
            if (!nearest.IsReal && MyCrosshair.IsVisible)
            {
                MyCrosshair.IsVisible = false;
                formsPlot1.Refresh();
                Text = $"No point selected";
            }
        };

        lblTrend.Text = string.Empty;

        cbInstruments.DropDownStyle = ComboBoxStyle.DropDownList;
        
        cbPoints.DropDownStyle = ComboBoxStyle.DropDownList;
        cbPoints.SelectedItem = "100";

        CheckUi();
    }

    private async void btnLoad_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(cbInstruments.Text))
            return;

        btnLoad.Enabled  = cbInstruments.Enabled = cbPoints.Enabled = false;

        var evaluation = await _cryptoService.GeValuations(cbInstruments.Text, int.Parse(cbPoints.Text));

        var allDates = evaluation.Select(c => DateTimeOffset.FromUnixTimeMilliseconds(c.Timestamp).DateTime.ToLocalTime());

        double[] dataX = allDates.Select(c => c.ToOADate()).ToArray();
        double[] dataY = evaluation.Select(c => double.Parse(c.Value, CultureInfo.InvariantCulture)).ToArray();

        var evol = 100 - (dataY.Last() * 100 / dataY.First());
        var symbol = (evol > 0) ? "+" : " - ";
        var diff = allDates.Max() - allDates.Min();

        lblTrend.Text = $"{symbol} {evol:0.####} % last {diff.TotalMinutes:0} minutes";

        lblTrend.ForeColor = evol switch
        {
            > 0 => System.Drawing.Color.Green,
            0 => System.Drawing.Color.Blue,
            < 0 => System.Drawing.Color.Red
        };

        formsPlot1.Plot.Clear();
        formsPlot1.Plot.Axes.DateTimeTicksBottom();
        MyScatter = formsPlot1.Plot.Add.Scatter(dataX, dataY);
        MyScatter.Color = evol switch
        {
            > 0 => ScottPlot.Colors.Green,
            0 => ScottPlot.Colors.Blue,
            < 0 => ScottPlot.Colors.Red
        };
        MyCrosshair = formsPlot1.Plot.Add.Crosshair(dataX.First(), dataY.First());
        formsPlot1.Plot.Axes.AutoScale();

        formsPlot1.Refresh();
        
        btnLoad.Enabled = cbInstruments.Enabled = cbPoints.Enabled = true;
    }

    private void cbInstruments_SelectedIndexChanged(object sender, EventArgs e)
    {
        CheckUi();
    }

    private void cbPoints_SelectedIndexChanged(object sender, EventArgs e)
    {
        CheckUi();
    }

    private void CheckUi()
    {
        if (string.IsNullOrEmpty(cbInstruments.Text) || string.IsNullOrEmpty(cbPoints.Text))
            btnLoad.Enabled = false;
        else
            btnLoad.Enabled = true;
    }
}
