using System.Timers;
using Vok.Domain.Interfaces;
using Vok.Domain.Models;

namespace Vok.Infrastructure.Services;

public class SwitchScanningService : ISwitchControlService {
    private readonly IVocabularyService _vocab;
    private System.Timers.Timer _scanTimer;
    private int _currentIndex = 0;
    private string _currentCategory = "needs";
    
    public bool IsScanningEnabled { get; set; }
    public ScanningMode Mode { get; set; } = ScanningMode.Sequential;
    public event Action<AacTile>? OnTileSelected;
    public event Action<int, int?>? OnScanHighlight;

    public SwitchScanningService(IVocabularyService vocab) {
        _vocab = vocab;
        _scanTimer = new System.Timers.Timer(1000);
        _scanTimer.Elapsed += OnTimerElapsed;
    }

    public void StartScanning() {
        IsScanningEnabled = true;
        _currentIndex = 0;
        _scanTimer.Start();
    }

    public void StopScanning() {
        IsScanningEnabled = false;
        _scanTimer.Stop();
    }

    private void OnTimerElapsed(object? sender, System.Timers.ElapsedEventArgs e) {
        var tiles = _vocab.GetTiles(_currentCategory);
        if (tiles.Count == 0) return;

        _currentIndex = (_currentIndex + 1) % tiles.Count;
        OnScanHighlight?.Invoke(_currentIndex, null);
    }

    public void TriggerSelect() {
        var tiles = _vocab.GetTiles(_currentCategory);
        if (tiles.Count > 0 && _currentIndex < tiles.Count) {
            OnTileSelected?.Invoke(tiles[_currentIndex]);
        }
    }
}
