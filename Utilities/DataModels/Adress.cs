namespace Utilities.DataModels
{
    public class Address
    {
        string? _admAreaCode;
        string? _admArea;
        string? _district;
        int _postalCode;
        string? _address;
        string? _nearestMetroStations;

        public string? AdmArea { get; set; }
        public string? AdmAreaCode { get; set; }

        private Address() { }
        private Address(string? admAreaCode,
                        string? admArea,
                        string? district,
                        int postalCode,
                        string? address,
                        string? nearestMetroStations)
        {
            AdmAreaCode = admAreaCode;
            AdmArea = admArea;
            _district = district;
            _postalCode = postalCode;
            _address = address;
            _nearestMetroStations = nearestMetroStations;
        }
        // Метод для создания объекта классa Address.
        public static Address Create(string? admAreaCode,
                                   string? admArea,
                                   string? district,
                                   int postalCode,
                                   string? address,
                                   string? nearestMetroStations)
        {
            return new Address(admAreaCode, admArea, district, postalCode, address, nearestMetroStations);
        }
        // Переопределенный ToString() для удобного вывода записей из .csv файла.
        public override string ToString() => $"[AdmAreaCode]: {AdmAreaCode!.TrimStart('0')} \n" +
            $"[AdmArea]: {AdmArea}\n" + $"[District]: {_district}\n" + $"[PostalCode]: {_postalCode}\n" + $"{_address}\n" +
            $"[NearestMetroStation]: {_nearestMetroStations}\n";
        // Метод, формирующий строку в необходимом формате для её дальнейшей записи в .csv файл.
        public string CorrectString() => $"\"{AdmAreaCode}\";" +
            $"\"{AdmArea}\";" + $"\"{_district}\";" + $"\"{_postalCode}\";" + $"\"{_address}\";" +
            $"\"{_nearestMetroStations}\";";
    }
}
