using Code.Core.RewardsAndBalance;
using Code.Entities;
using UnityEngine;

namespace Code.Core
{
    public class CoreManager : MonoBehaviour
    {
        public static CoreManager Instance;

        public Level CurrentLevel => _levelsSwitcher.CurrentLevel;
        public Wave CurrentWave => _wavesSwitcher.CurrentWave;
        public int CurrentWaveNumber => _wavesSwitcher.CurrentWaveNumber;

        public Map Map { get; private set; }
        public BalanceMediator BalanceMediator { get; private set; }

        [SerializeField] private SceneRenderer _sceneRenderer;

        private LevelsSwitcher _levelsSwitcher;
        private WavesSwitcher _wavesSwitcher;

        private MapGenerator _mapGenerator;

        public void Victory()
        {

        }

        private void Awake()
        {
            Instance = this;
            _mapGenerator = new();

            _levelsSwitcher = GetComponent<LevelsSwitcher>();
            _wavesSwitcher = GetComponent<WavesSwitcher>();
        }

        private void Start()
        {
            InitLevel();
        }

        private void InitLevel()
        {
            Map = _mapGenerator.Generate(0);
            _sceneRenderer.Render(Map);

            _levelsSwitcher.Switch();

            BalanceMediator = new(CurrentLevel.startBalance);

            CoreEventsProvider.LevelStarted.Invoke();
        }

        public int GetWidth()
        {
            return Map.Width;
        }
        public int GetHeight()
        {
            return Map.Height;
        }
    }
}