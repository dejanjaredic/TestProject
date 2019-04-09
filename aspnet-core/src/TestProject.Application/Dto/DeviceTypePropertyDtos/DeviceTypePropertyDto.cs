namespace TestProject.Dto.DeviceTypePropertyDtos
{
    //[AutoMap(typeof(DeviceTypeProperty))]
    public class DeviceTypePropertyDto
    {
        public string NameProperty { get; set; }
        public bool Required { get; set; }
        public string Type { get; set; }
    }
}
