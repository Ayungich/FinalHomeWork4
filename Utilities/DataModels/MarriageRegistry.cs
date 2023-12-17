using System.Globalization;

namespace Utilities.DataModels
{
    // Основной класс из варианта.
    public class MarriageRegistry
    {
        int _rowNum;
        string? _commonName;
        string? _fullName;
        string? _shortName;
        string? _chiefName;
        string? _chiefPosition;
        string? _chiefPhone;
        string? _contactPhone;
        string? _archivePhone;
        string? _signPGU;
        string? _workingHours;
        string? _clarificationOfWorkingHours;
        string? _webSite;
        double _x_WGS;
        double _y_WGS;
        long _globalID;
        Address? _address; // Комопзиция с классом Address.
        // Открытые свойства для доступа к полям класса.
        public int RowNum { get; set; }
        public string? CommonName { get; set; }
        public string? FullName { get; set; }
        public string? ShortName { get; set; }
        public string? ChiefName { get; set; }
        public string? ChiefPosition { get; set; }
        public string? ChiefPhone { get => _chiefPhone; set => _chiefPhone = value is null ? "NaN" : value; }
        public string? ContactPhone { get => _contactPhone; set => _contactPhone = value is null ? "NaN" : value; }
        public string? ArchivePhone { get => _archivePhone; set => _archivePhone = value is null ? "NaN" : value; }
        public string? SignPGU { get; set; }
        public string? WorkingHours { get; set; }
        public string? ClarificationOfWorkingHours { get; set; }
        public string? WebSite { get; set; }
        public double X_WGS { get; set; }
        public double Y_WGS { get; set; }
        public long GlobalID { get; set; }
        public Address? Address { get; set; }
        // Конструктор класса без параметров.
        public MarriageRegistry() { }
        // Конструктор класса c нужными параметрами.
        public MarriageRegistry(int rowNum,
                                string? commonName,
                                string? fullName,
                                string? shortName,
                                string? chiefName,
                                string? chiefPosition,
                                string? chiefPhone,
                                string? contactPhone,
                                string? archivePhone,
                                string? signPGU,
                                string? workingHours,
                                string? clarificationOfWorkingHours,
                                string? webSite,
                                double x_WGS,
                                double y_WGS,
                                long globalID,
                                string? admAreaCode,
                                string? admArea,
                                string? district,
                                int postalCode,
                                string? address,
                                string? nearestMetroStations)
        {
            RowNum = rowNum;
            CommonName = commonName;
            FullName = fullName;
            ShortName = shortName;
            ChiefName = chiefName;
            ChiefPosition = chiefPosition;
            ChiefPhone = chiefPhone;
            ContactPhone = contactPhone;
            ArchivePhone = archivePhone;
            SignPGU = signPGU;
            WorkingHours = workingHours;
            ClarificationOfWorkingHours = clarificationOfWorkingHours;
            WebSite = webSite;
            X_WGS = x_WGS;
            Y_WGS = y_WGS;
            GlobalID = globalID;
            Address = Address.Create(admAreaCode, admArea, district, postalCode, address, nearestMetroStations);
        }
        // Переопределенный ToString() для удобного вывода записей из .csv файла.
        public override string ToString() => $"\n[RowNum]: {RowNum}\n" + $"[CommonName]: {CommonName}\n" + $"[FullName]: {FullName}\n"
            + $"[ShortName]: {ShortName}\n" + Address!.ToString() + $"[ChiefName]: {ChiefName}\n" + $"[ChiefPosition]: {ChiefPosition}\n" 
            + $"[ChiefPhone]: {ChiefPhone}\n" + $"[ContactPhone]: {ContactPhone}\n" + $"[ArchivePhone]: {ArchivePhone}\n" + $"[SignPGU]: {SignPGU}\n" + 
            $"[WorkingHours]: {WorkingHours}\n" + $"[ClarificationOfWorkingHours]: {ClarificationOfWorkingHours}\n" + $"[WebSite]: {WebSite}\n" +
            $"[X_WGS]: {X_WGS}\n" + $"[Y_WGS]: {Y_WGS}\n" + $"[GlobalID]: {GlobalID}\n";
        // Метод, формирующий строку в необходимом формате для её дальнейшей записи в .csv файл.
        public string CorrectString() => $"{RowNum};" + $"\"{CommonName}\";" + $"\"{FullName}\";"
           + $"\"{ShortName}\";" + Address!.CorrectString() + $"\"{ChiefName}\";" + $"\"{ChiefPosition}\";"
           + $"\"{ChiefPhone}\";" + $"\"{ContactPhone}\";" + $"\"{ArchivePhone}\";" + $"\"{SignPGU}\";" +
           $"\"{WorkingHours}\";" + $"\"{ClarificationOfWorkingHours}\";" + $"\"{WebSite}\";" +
           $"\"{X_WGS.ToString(CultureInfo.InvariantCulture)}\";" + $"\"{Y_WGS.ToString(CultureInfo.InvariantCulture)}\";" + $"\"{GlobalID}\";";
    }
}
