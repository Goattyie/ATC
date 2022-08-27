using ATC.Wpf.Services.DataGenerators;
using System;
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
        private readonly BenefitGenerator _benefitGenerator;
        private readonly AbonentGenerator _abonentGenerator;
        private readonly AtcGenerator _atcGenerator;
        private readonly CallGenerator _callGenerator;

        public static event Action OnGenerate;

        public DataInitializer(CountryGenerator countryGenerator, 
            CityGenerator cityGenerator,
            AreaGenerator areaGenerator,
            TariffGenerator tariffGenerator,
            BenefitTypeGenerator benefitTypeGenerator,
            SocialStatusGenerator socialStatusGenerator,
            BenefitGenerator benefitGenerator,
            AbonentGenerator abonentGenerator,
            AtcGenerator atcGenerator, 
            CallGenerator callGenerator)
        {
            _countryGenerator = countryGenerator;
            _cityGenerator = cityGenerator;
            _areaGenerator = areaGenerator;
            _tariffGenerator = tariffGenerator;
            _benefitTypeGenerator = benefitTypeGenerator;
            _socialStatusGenerator = socialStatusGenerator;
            _benefitGenerator = benefitGenerator;
            _abonentGenerator = abonentGenerator;
            _atcGenerator = atcGenerator;
            _callGenerator = callGenerator;
        }

        public async Task Generate()
        {
            await _countryGenerator.Generate();
            await _cityGenerator.Generate();
            await _areaGenerator.Generate();
            await _tariffGenerator.Generate();
            await _benefitTypeGenerator.Generate();
            await _socialStatusGenerator.Generate();
            await _benefitGenerator.Generate();
            await _abonentGenerator.Generate();
            await _atcGenerator.Generate();
            await _callGenerator.Generate();

            OnGenerate?.Invoke();
        }
    }
}
