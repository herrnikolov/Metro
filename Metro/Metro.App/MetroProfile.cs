using AutoMapper;
using Metro.DataProcessor.Dto.ExportDto;
using Metro.DataProcessor.Dto.ExportDtoXml;
using Metro.DataProcessor.Dto.ImportDto;
using Metro.Models;

namespace Metro.App
{
    public class MetroProfile :Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
        public MetroProfile ()
        {
            CreateMap<StationDtoImp, Station>();
            CreateMap<StationDtoExpJson, Station>();
            CreateMap<StationDtoExpXml, Station>();
        }
    }
}
