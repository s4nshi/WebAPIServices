namespace WebAPI.Services
{
    public interface IAdPlatformService
    {
        Task LoadFromFile(string content);
        IEnumerable<string> SearchByLocation(string location);
    }

    public class AdPlatformService : IAdPlatformService
    {
        private Dictionary<string, List<string>> _platforms = new Dictionary<string, List<string>>();

        public async Task LoadFromFile(string content)
        {
            _platforms.Clear();

            var lines = content.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                var parts = line.Split(':');
                if (parts.Length != 2) continue;

                var platformName = parts[0].Trim();
                var locations = parts[1].Split(',').Select(x => x.Trim());

                foreach (var location in locations)
                {
                    if (!_platforms.ContainsKey(location))
                        _platforms[location] = new List<string>();

                    _platforms[location].Add(platformName);
                }
            }
        }

        public IEnumerable<string> SearchByLocation(string location)
        {
            var result = new HashSet<string>();

            foreach (var key in _platforms.Keys)
            {
                if (location.StartsWith(key))
                    result.UnionWith(_platforms[key]);
            }

            return result;
        }
    }
}
