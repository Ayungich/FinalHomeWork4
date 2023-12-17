using System.Globalization;
using System.Text;
using Utilities.Handlers;

namespace Utilities.DataModels.MainFunctionality
{
    public  class WorkWithFiles
    {
        public static List<MarriageRegistry> LoadData(string? filePath)
        {
            if (string.IsNullOrEmpty(filePath) || filePath is null)
                throw new ArgumentNullException("File path is null or empty.");

            if (!Checkers.IsValidAbsolutePath(filePath, needFileExisting: true))
                throw new ArgumentException("File path is incorrect.");

            if (!Checkers.FileExtensionCheck(Path.GetFileName(filePath), fileExtension: ".csv"))
                throw new ArgumentException("File extension is incorrect.");

            var dataFromFile = new List<MarriageRegistry>();

            using (var reader = new StreamReader(filePath, Encoding.UTF8))
            {
                string fileLine;
                reader.ReadLine(); // Пропускаем строку заголовков

                while ((fileLine = reader.ReadLine()!) != null)
                {
                    // Удаление кавычек из значений
                    var values = fileLine.Split(new string[] { "\";\"", "\";", ";\"" }, StringSplitOptions.None).Select(v => v.Trim('\"').Trim(';')).ToArray();

                    int rowNum = int.TryParse(values[0], out _) ? int.Parse(values[0]) :
                        throw new ArgumentException($"Invalid data in file.");

                    string? commonName = values[1];
                    string? fullName = values[2];
                    string? shortName = values[3];
                    string? admAreaCode = values[4];

                    string? admArea = values[5];
                    string? district = values[6];
                    int postalCode = int.TryParse(values[7], out _) ? int.Parse(values[7]) :
                        throw new ArgumentException($"Invalid data in file.");

                    string? address = values[8];
                    string? nearestMetroStations = values[9];
                    string? chiefName = values[10];
                    string? chiefPosition = values[11];
                    string? chiefPhone = values[12];
                    string? contactPhone = values[13];
                    string? archivePhone = values[14];

                    string? signPGU = string.Compare(values[15], "да", StringComparison.OrdinalIgnoreCase) == 0 ||
                                      string.Compare(values[15], "нет", StringComparison.OrdinalIgnoreCase) == 0 ?
                                      values[15] : throw new ArgumentException("Incorrect data in file.");

                    string? workingHours = values[16];
                    string? clarificationOfWorkingHours = values[17];
                    string? webSite = values[18];

                    double x_WGS = double.TryParse(values[19], NumberStyles.Any, CultureInfo.InvariantCulture, out _) ?
                                   double.Parse(values[19], NumberStyles.Any, CultureInfo.InvariantCulture) :
                                   throw new ArgumentException("Incorrect data in file.");

                    double y_WGS = double.TryParse(values[20], NumberStyles.Any, CultureInfo.InvariantCulture, out _) ?
                                   double.Parse(values[20], NumberStyles.Any, CultureInfo.InvariantCulture) :
                                   throw new ArgumentException("Incorrect data in file.");

                    long globalID = long.TryParse(values[7], out _) ? long.Parse(values[7]) :
                        throw new ArgumentException($"Invalid data in file.");

                    var marriageRegistry = new MarriageRegistry(rowNum, commonName, fullName, shortName, chiefName,
                        chiefPosition, chiefPhone, contactPhone, archivePhone, signPGU, workingHours, clarificationOfWorkingHours,
                        webSite, x_WGS, y_WGS, globalID, admAreaCode, admArea, district, postalCode, address, nearestMetroStations);

                    dataFromFile.Add(marriageRegistry);
                }
            }

            return dataFromFile;
        }

        public static void WriteDataToFile(in List<MarriageRegistry> listWithData, string? filePath, bool newFileFlag)
        {
            if (listWithData is null || listWithData.Count == 0)
                throw new ArgumentNullException(nameof(listWithData));

            if (string.IsNullOrEmpty(filePath) || filePath is null)
                throw new ArgumentNullException("File path is null or empty.");

            if (!Checkers.IsValidAbsolutePath(filePath, needFileExisting: newFileFlag))
                throw new ArgumentException("File path is incorrect.");

            if (!Checkers.FileExtensionCheck(Path.GetFileName(filePath), fileExtension: ".csv"))
                throw new ArgumentException("File extension is incorrect.");


            using (var reader = new StreamWriter(filePath, newFileFlag, Encoding.UTF8))
            {
                if (!newFileFlag)
                {
                    string fileLine = "ROWNUM;" + "\"CommonName\";" + "\"FullName\";"
                                    + "\"ShortName\";" + "\"AdmAreaCode\";" +
                                      "\"AdmArea\";" + "\"District\";" + "\"PostalCode\";" + "\"Address\";" +
                                      "\"NearestMetroStations\";" + "\"ChiefName\";" + "\"ChiefPosition\";" +
                                      "\"ChiefPhone\";" + "\"ContactPhone\";" + "\"ArchivePhone\";" + "\"SignPGU\";" +
                                      "\"WorkingHours\";" + "\"ClarificationOfWorkingHours\";" + "\"WebSite\";" +
                                      "\"X_WGS\";" + "\"Y_WGS\";" + "\"GLOBALID\";";
                    reader.WriteLine(fileLine);
                }
                foreach (var data in listWithData) reader.WriteLine(data.CorrectString());
                reader.Close();
            }
        }
    }
}
