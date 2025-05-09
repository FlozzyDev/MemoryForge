namespace MemoryForge;

public class DataManager
{
    private List<string> _datasetNames = new List<string>();
    private string _datasetPath = "datasets";

    public DataManager()
    {
        if (!Directory.Exists(_datasetPath))
        {
            Directory.CreateDirectory(_datasetPath);
        }
        CreateDefaultDataset();
    }

    public void LoadDatasets()
    {
        _datasetNames.Clear(); // clear first
        if (!Directory.Exists(_datasetPath))
        {
            Directory.CreateDirectory(_datasetPath);
        }

        // make array to hold all files
        string[] files = Directory.GetFiles(_datasetPath, "*.csv"); // get any CSVs in /datasets

        for (int i = 0; i < files.Length; i++)
        {
            _datasetNames.Add(Path.GetFileNameWithoutExtension(files[i])); // get the display name
        }
    }

    public List<string> GetDatasetNames()
    {
        return _datasetNames;
    }

    public List<DataPair> LoadDataset(string datasetName)
    {
        List<DataPair> dataPairs = new List<DataPair>();
        string filePath = Path.Combine(_datasetPath, $"{datasetName}.csv");
        CreateDefaultDataset();

        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] parts = line.Split(',');

                if (parts.Length >= 3)
                {
                    if (int.TryParse(parts[0], out int id))
                    {
                        dataPairs.Add(new DataPair(id, parts[1], parts[2]));
                    }
                }
            }
        }
        return dataPairs;
    }

    private void CreateDefaultDataset() // STRETCH CHALLENGE - Write to a CSV with defaults
    {
        string defaultCountryCapitolsPath = Path.Combine(_datasetPath, "CountryCapitols.csv"); // make a country/capital dataset on default
        if (!File.Exists(defaultCountryCapitolsPath))
        {
            using (StreamWriter writer = new StreamWriter(defaultCountryCapitolsPath))
            {
                writer.WriteLine("id,Country,Capitol");
                writer.WriteLine("1,Russia,Moscow");
                writer.WriteLine("2,Canada,Ottawa");
                writer.WriteLine("3,United States,Washington");
                writer.WriteLine("4,China,Beijing");
                writer.WriteLine("5,Brazil,Brasilia");
                writer.WriteLine("6,Australia,Canberra");
                writer.WriteLine("7,India,New Delhi");
                writer.WriteLine("8,Argentina,Buenos Aires");
                writer.WriteLine("9,Kazakhstan,Nur-Sultan");
                writer.WriteLine("10,Algeria,Algiers");
                writer.WriteLine("11,Democratic Republic of the Congo,Kinshasa");
                writer.WriteLine("12,Saudi Arabia,Riyadh");
                writer.WriteLine("13,Mexico,Mexico City");
                writer.WriteLine("14,Indonesia,Jakarta");
                writer.WriteLine("15,Sudan,Khartoum");
                writer.WriteLine("16,Libya,Tripoli");
                writer.WriteLine("17,Iran,Tehran");
                writer.WriteLine("18,Mongolia,Ulaanbaatar");
                writer.WriteLine("19,Peru,Lima");
                writer.WriteLine("20,Chad,N'Djamena");
                writer.WriteLine("21,Niger,Niamey");
                writer.WriteLine("22,Angola,Luanda");
                writer.WriteLine("23,Mali,Bamako");
                writer.WriteLine("24,South Africa,Pretoria");
                writer.WriteLine("25,Colombia,Bogota");
                writer.WriteLine("26,Ethiopia,Addis Ababa");
                writer.WriteLine("27,Bolivia,La Paz");
                writer.WriteLine("28,Mauritania,Nouakchott");
                writer.WriteLine("29,Egypt,Cairo");
                writer.WriteLine("30,Tanzania,Dodoma");
                writer.WriteLine("31,Nigeria,Abuja");
                writer.WriteLine("32,Venezuela,Caracas");
                writer.WriteLine("33,Pakistan,Islamabad");
                writer.WriteLine("34,Namibia,Windhoek");
                writer.WriteLine("35,Mozambique,Maputo");
                writer.WriteLine("36,Turkey,Ankara");
                writer.WriteLine("37,Chile,Santiago");
                writer.WriteLine("38,Zambia,Lusaka");
                writer.WriteLine("39,Myanmar,Naypyidaw");
                writer.WriteLine("40,Afghanistan,Kabul");
                writer.WriteLine("41,South Sudan,Juba");
                writer.WriteLine("42,Somalia,Mogadishu");
                writer.WriteLine("43,Central African Republic,Bangui");
                writer.WriteLine("44,Ukraine,Kyiv");
                writer.WriteLine("45,Madagascar,Antananarivo");
                writer.WriteLine("46,Botswana,Gaborone");
                writer.WriteLine("47,Kenya,Nairobi");
                writer.WriteLine("48,France,Paris");
                writer.WriteLine("49,Yemen,Sana'a");
                writer.WriteLine("50,Thailand,Bangkok");
                writer.WriteLine("51,Spain,Madrid");
                writer.WriteLine("52,Turkmenistan,Ashgabat");
                writer.WriteLine("53,Cameroon,Yaoundé");
                writer.WriteLine("54,Papua New Guinea,Port Moresby");
                writer.WriteLine("55,Sweden,Stockholm");
                writer.WriteLine("56,Uzbekistan,Tashkent");
                writer.WriteLine("57,Morocco,Rabat");
                writer.WriteLine("58,Iraq,Baghdad");
                writer.WriteLine("59,Paraguay,Asunción");
                writer.WriteLine("60,Zimbabwe,Harare");
                writer.WriteLine("61,Norway,Oslo");
                writer.WriteLine("62,Japan,Tokyo");
                writer.WriteLine("63,Germany,Berlin");
                writer.WriteLine("64,Finland,Helsinki");
                writer.WriteLine("65,Vietnam,Hanoi");
                writer.WriteLine("66,Malaysia,Kuala Lumpur");
                writer.WriteLine("67,Côte d'Ivoire,Yamoussoukro");
                writer.WriteLine("68,Poland,Warsaw");
                writer.WriteLine("69,Oman,Muscat");
                writer.WriteLine("70,Italy,Rome");
                writer.WriteLine("71,Philippines,Manila");
                writer.WriteLine("72,Ecuador,Quito");
                writer.WriteLine("73,Burkina Faso,Ouagadougou");
                writer.WriteLine("74,New Zealand,Wellington");
                writer.WriteLine("75,Gabon,Libreville");
                writer.WriteLine("76,Guinea,Conakry");
                writer.WriteLine("77,United Kingdom,London");
                writer.WriteLine("78,Uganda,Kampala");
                writer.WriteLine("79,Ghana,Accra");
                writer.WriteLine("80,Romania,Bucharest");
                writer.WriteLine("81,Laos,Vientiane");
                writer.WriteLine("82,Guyana,Georgetown");
                writer.WriteLine("83,Belarus,Minsk");
                writer.WriteLine("84,Kyrgyzstan,Bishkek");
                writer.WriteLine("85,Senegal,Dakar");
                writer.WriteLine("86,Syria,Damascus");
                writer.WriteLine("87,Cambodia,Phnom Penh");
                writer.WriteLine("88,Uruguay,Montevideo");
                writer.WriteLine("89,Tunisia,Tunis");
                writer.WriteLine("90,Suriname,Paramaribo");
                writer.WriteLine("91,Bangladesh,Dhaka");
                writer.WriteLine("92,Nepal,Kathmandu");
                writer.WriteLine("93,Tajikistan,Dushanbe");
                writer.WriteLine("94,Greece,Athens");
                writer.WriteLine("95,Nicaragua,Managua");
                writer.WriteLine("96,North Korea,Pyongyang");
                writer.WriteLine("97,Malawi,Lilongwe");
                writer.WriteLine("98,Eritrea,Asmara");
                writer.WriteLine("99,Benin,Porto-Novo");
                writer.WriteLine("100,Honduras,Tegucigalpa");
            }
        }
    }
}