using Code.Entities;
using UnityEngine;

namespace Code.Core
{
    public class CoreManager : MonoBehaviour
    {
        public static CoreManager Instance;
        
        public Level CurrentLevel => _levelsSwitcher.CurrentLevel;
        public Wave CurrentWave => _wavesSwitcher.CurrentWave;
        public Map Map => _map;

        [SerializeField] private SceneRenderer _sceneRenderer;

        private LevelsSwitcher _levelsSwitcher;
        private WavesSwitcher _wavesSwitcher;

        private Map _map;
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
            _map = _mapGenerator.Generate(0);
            _sceneRenderer.Render(_map);

            _levelsSwitcher.Switch();

            CoreEventsProvider.LevelStarted.Invoke();
        }

        public int GetWidth()
        {
            return _map.Width;
        }
        public int GetHeight()
        {
            return _map.Height;
        }
    }
}