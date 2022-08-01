using ATC.Wpf.Services.DataGenerators;
using System.Threading.Tasks;

namespace ATC.Wpf.Services
{
    internal class DataInitializer
    {
        private readonly CountryGenerator _countryGenerator;
        private readonly CityGenerator _cityGenerator;
        private readonly AreaGenerator _areaGenerator;
        private readonly TariffGenerator _tariffGenerator;
        private readonly BenefitTypeGenerator _benefitTypeGenerator;
        private readonly SocialStatusGenerator _socialStatusGenerator;

        public DataInitializer(CountryGenerator countryGenerator, 
            CityGenerator cityGenerator,
            AreaGenerator areaGenerator,
            TariffGenerator tariffGenerator,
            BenefitTypeGenerator benefitTypeGenerator,
            SocialStatusGenerator socialStatusGenerator)
        {
            _countryGenerator = countryGenerator;
            _cityGenerator = cityGenerator;
            _areaGenerator = areaGenerator;
            _tariffGenerator = tariffGenerator;
            _benefitTypeGenerator = benefitTypeGenerator;
            _socialStatusGenerator = socialStatusGenerator;
        }

        public async Task Generate()
        {
            await _countryGenerator.Generate();
            await _cityGenerator.Generate();
            await _areaGenerator.Generate();
            await _tariffGenerator.Generate();
            await _benefitTypeGenerator.Generate();
            await _socialStatusGenerator.Generate();
        }
    }
}
